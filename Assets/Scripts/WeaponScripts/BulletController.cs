using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    [SerializeField] private float lifeTime;
    public bool isEnemyBullet = false;
    public bool isPlayerBullet = false;

    private Vector2 lastPos;
    private Vector2 curPos;
    private Vector2 playerPos;
    private Vector2 enemyPos;

    private GameObject player;

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(0.5f, 0.5f);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (isEnemyBullet)
        {

            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, playerPos, 5f * Time.deltaTime);

            if (curPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = curPos;
        }
        if (isPlayerBullet)
        {
            curPos = transform.position;
            transform.position = Vector2.MoveTowards(transform.position, enemyPos, 5f * Time.deltaTime);
            if (curPos == lastPos)
            {
                Destroy(gameObject);
            }
            lastPos = curPos;
        }

    }
    //SET POSITION OF PLAYER
    public void SetPlayer(Transform player)
    {
        playerPos = player.position;
    }
    //SET POSITION OF ENEMY
    public void SetEnemy(float x, float y)
    {
        enemyPos.x = x;
        enemyPos.y = y;
    }
    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider target)
    {

        if (target.tag == "Enemy" && isPlayerBullet)
        {
            target.gameObject.GetComponent<EnemyController>().Death();
            Destroy(gameObject);
        }

        if (target.tag == "Player" && isEnemyBullet)
        {
            player.GetComponent<PlayerController>().DamagePlayer(1);
            Destroy(gameObject);
        }

    }

}