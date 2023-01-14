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
    private PlayerController playerController;

    public bool isAtached = false;
    [Space(15)]
    [SerializeField] private GameObject bulletPrefab = default;

    private void Start()
    {
        playerController = GameController.Instance.player.GetComponent<PlayerController>();
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
        this.GetComponent<Animator>().SetTrigger("Trigger");

        switch (weaponType)
        {
            case (WeaponType.Melee):
                Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.transform.position, attackRange, enemyLayers);
                foreach (Collider2D enemy in hitEnemies)
                {
                    enemy.GetComponent<EnemyController>().Death();
                    break;
                }
                break;
            case (WeaponType.Ranged):
                GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                bullet.GetComponent<BulletController>().isPlayerBullet = true;
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (attackPoint == null) return;
        Gizmos.DrawWireSphere(attackPoint.transform.position, attackRange);
    }
}