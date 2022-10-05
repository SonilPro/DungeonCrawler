using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    Wander,

    Follow,

    Die,

    Attack,
}

public enum EnemyType
{
    Melee,
    Ranged,
}

public class EnemyController : MonoBehaviour
{
    private GameObject player;

    private Animator anim;

    [SerializeField] private EnemyState currState = EnemyState.Wander;
    [SerializeField] private EnemyType enemyType;

    //Stats
    [SerializeField] private float visionRange;
    [SerializeField] private float speed;
    [SerializeField] private float attackRange;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private GameObject bulletPrefab;

    //Utilities
    private bool chooseDir = false;
    private bool coolDownAttack = false;
    private int randomDir;
    private Vector2 lastPosition;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        player.GetComponent<PlayerController>().AddEnemy(this.gameObject);

    }

    // Update is called once per frame

    void Update()
    {

        switch (currState)
        {
            case (EnemyState.Wander):
                Wander();
                break;
            case (EnemyState.Follow):
                Follow();
                break;
            case (EnemyState.Die):
                break;
            case (EnemyState.Attack):
                Attack();
                break;
        }

        if (IsPlayerInRange(visionRange) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
            if (!WallBetween())
            {
                currState = EnemyState.Follow;
            }
            else
            {
                currState = EnemyState.Wander;
            }

        }
        else if (!IsPlayerInRange(visionRange) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currState = EnemyState.Attack;
        }

    }

    private bool IsPlayerInRange(float VisionRange)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= VisionRange;
    }

    private bool WallBetween()
    {

        Vector3 direction = (player.transform.position - transform.position).normalized;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);

        if (hit == false)
        {
            return false;
        }
        else
        {
            return (hit.collider.tag == "Wall");
        }

    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        randomDir = UnityEngine.Random.Range(1, 8);
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        chooseDir = false;
    }

    void Wander()
    {
        if (!chooseDir)
        {
            StartCoroutine(ChooseDirection());
        }

        switch (randomDir)
        {
            case 1:
                anim.SetBool("IsRunningLeft", false);
                anim.SetBool("IsRunningRight", true);
                transform.position += transform.right * speed * Time.deltaTime;
                break;
            case 2:
                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsRunningLeft", true);
                transform.position += -transform.right * speed * Time.deltaTime;
                break;
            case 3:
                transform.position += transform.up * speed * Time.deltaTime;
                break;
            case 4:
                transform.position += -transform.up * speed * Time.deltaTime;
                break;
            case 5:
                anim.SetBool("IsRunningLeft", false);
                anim.SetBool("IsRunningRight", true);
                transform.position += transform.right * speed * Time.deltaTime / 2;
                transform.position += transform.up * speed * Time.deltaTime;
                break;
            case 6:
                anim.SetBool("IsRunningLeft", false);
                anim.SetBool("IsRunningRight", true);
                transform.position += transform.right * speed * Time.deltaTime / 2;
                transform.position += -transform.up * speed * Time.deltaTime;
                break;
            case 7:
                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsRunningLeft", true);
                transform.position += -transform.right * speed * Time.deltaTime / 2;
                transform.position += transform.up * speed * Time.deltaTime;
                break;
            case 8:
                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsRunningLeft", true);
                transform.position += -transform.right * speed * Time.deltaTime / 2;
                transform.position += -transform.up * speed * Time.deltaTime;
                break;
            default:
                Debug.Log("Error: number out of bounds");
                break;
        }

        if (IsPlayerInRange(visionRange))
        {
            currState = EnemyState.Follow;
        }
    }

    void Follow()
    {
        lastPosition = transform.position;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        if (lastPosition.x >= transform.position.x)
        {
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsRunningLeft", true);
        }
        if (lastPosition.x <= transform.position.x)
        {

            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsRunningRight", true);

        }

    }
    void Attack()
    {

        if (player.transform.position.x > transform.position.x)
        {
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", true);
        }
        else
        {
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleLeft", true);
        }

        if (!coolDownAttack)
        {

            switch (enemyType)
            {
                case (EnemyType.Melee):
                    player.GetComponent<PlayerController>().DamagePlayer(1);
                    StartCoroutine(CoolDown());
                    break;

                case (EnemyType.Ranged):

                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().SetPlayer(player.transform);
                    //bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }

    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(attackCoolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        Destroy(gameObject);
        player.GetComponent<PlayerController>().DeleteEnemy(gameObject);

    }
}