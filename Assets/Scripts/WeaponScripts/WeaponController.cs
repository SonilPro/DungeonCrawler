using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
    Melee,

    Ranged,
}

public class WeaponController : MonoBehaviour
{

    public WeaponType weaponType;

    [SerializeField] private Transform attackPoint = default;
    [SerializeField] private float attackRange = 0.5f;
    [SerializeField] private float attackSpeed = 0.5f;

    [Space(15)]
    [SerializeField] private LayerMask enemyLayers = default;
    private float nextAttackTime = 0f;
    public float SpriteOffset = 0.0f;

    [SerializeField] private GameObject player = default;
    private PlayerController playerController;

    public bool isAtached = false;
    [Space(15)]
    [SerializeField] private GameObject bulletPrefab = default;

    private void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    private void Update()
    {
        if (Time.time >= nextAttackTime && isAtached == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Attack();
                nextAttackTime = Time.time + attackSpeed;
            }
        }

    }

    void Attack()
    {
        //Play attack animation
        this.GetComponent<Animator>().SetTrigger("Trigger");

        switch (weaponType)
        {
            case (WeaponType.Melee):
                //Detect enemies in range of attack
                //2D
                //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);
                //3D
                Collider[] hitEnemies = Physics.OverlapSphere(attackPoint.transform.position, attackRange, enemyLayers);
                //Damage them
                //2D
                // foreach (Collider2D enemy in hitEnemies)
                // {
                //     Debug.Log(enemy.tag);
                //     playerController.DeleteEnemy(enemy.gameObject);
                //     Destroy(enemy.gameObject);
                //     break;
                // }
                //3D
                foreach (Collider enemy in hitEnemies)
                {
                    Debug.Log(enemy.tag);
                    playerController.DeleteEnemy(enemy.gameObject);
                    Destroy(enemy.gameObject);
                    break;
                }
                break;
            case (WeaponType.Ranged):
                bool found = playerController.FindClosestEnemy();
                Debug.Log(found);
                if (found == true)
                {
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().SetEnemy(playerController.closestEnemy.transform.position.x, playerController.closestEnemy.transform.position.y);
                    bullet.GetComponent<BulletController>().isPlayerBullet = true;
                }
                break;
        }

    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }

}