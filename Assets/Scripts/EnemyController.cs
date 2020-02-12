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
};

public enum EnemyType
{
    Melee,
    Ranged,

};

public class EnemyController : MonoBehaviour
{
    public int enemyNumber;

    private PlayerController playerController;

    GameObject player;

    private Animator anim;

    public EnemyState currState = EnemyState.Wander;
    public EnemyType enemyType;

    public float range;

    public float speed;

    public float attackRange;

    public float coolDown;

    private bool chooseDir = false;

    private bool coolDownAttack = false;

    public int randomDir;

    private Vector2 lastPosition;

    public GameObject bulletPrefab;

    public bool smtg = true;

    
    void Start()
    { 
        
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        playerController.Setup(this.gameObject);

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

        if (IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Follow;
        }
        else if (!IsPlayerInRange(range) && currState != EnemyState.Die)
        {
            currState = EnemyState.Wander;
        }

        if (Vector3.Distance(transform.position, player.transform.position) <= attackRange)
        {
            currState = EnemyState.Attack;
        }

    }

    private bool IsPlayerInRange(float range)
    {
        return Vector3.Distance(transform.position, player.transform.position) <= range;
    }

    private IEnumerator ChooseDirection()
    {
        chooseDir = true;
        yield return new WaitForSeconds(UnityEngine.Random.Range(1f, 3f));
        randomDir = UnityEngine.Random.Range(1, 8);
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

        if (IsPlayerInRange(range))
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
        lastPosition = transform.position;



        if (lastPosition.x == transform.position.x && player.transform.position.x > transform.position.x)
        {
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", true);
        }
        else if (lastPosition.x == transform.position.x && player.transform.position.x < transform.position.x)
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
                    GameController.DamagePlayer(1);
                    StartCoroutine(CoolDown());
                    break;

                case (EnemyType.Ranged):
                    
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<BulletController>().GetPlayer(player.transform);
                    bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
                    bullet.GetComponent<BulletController>().isEnemyBullet = true;
                    StartCoroutine(CoolDown());
                    break;
            }
        }


    }

    private IEnumerator CoolDown()
    {
        coolDownAttack = true;
        yield return new WaitForSeconds(coolDown);
        coolDownAttack = false;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}