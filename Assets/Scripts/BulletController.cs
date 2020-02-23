using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float lifeTime;

    public bool isEnemyBullet = false;

    public bool isPlayerBullet = false;

    public Vector2 lastPos;
    public Vector2 curPos;
    private Vector2 playerPos;
    private Vector2 enemyPos;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DeathDelay());
        if (!isEnemyBullet)
        {
            transform.localScale = new Vector2(GameController.BulletSize, GameController.BulletSize);
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
            
            transform.position = Vector2.MoveTowards(transform.position, enemyPos, 5f * Time.deltaTime);
        }
    }
    //GET POSITION OF PLAYER
    public void GetPlayer(Transform player)
    {
        playerPos = player.position;
        
    }

    //GET POSITION OF ENEMY
    public void GetEnemy(float x, float y)
    {
        enemyPos.x = x;
        enemyPos.y = y;
        
    }

    IEnumerator DeathDelay()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target)
    {

        if (target.tag == "Enemy" && isPlayerBullet)
        {
            target.gameObject.GetComponent<EnemyController>().Death();

        }

        if (target.tag == "Player" && isEnemyBullet)
        {
            GameController.DamagePlayer(1);
            Destroy(gameObject);
        }



    }

}
