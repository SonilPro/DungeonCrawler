using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float lifeTime;
    [SerializeField] private float speed = 10f;
    public bool isEnemyBullet = false;
    public bool isPlayerBullet = false;

    private Vector2 lastPos;
    private Vector2 curPos;
    private GameObject player;
    private Vector3 mousePosition;

    void Awake()
    {
        player = GameController.Instance.player;
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        StartCoroutine(DeathDelay());

        Vector3 direction;
        if (isEnemyBullet)
        {
            direction = player.transform.position - transform.position;
        }
        else
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            direction = mousePosition - transform.position;
        }

        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
        this.transform.Rotate(0.0f, 0.0f, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
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
            StartCoroutine(arrowHitCorutine(gameObject));
        }

        if (target.tag == "Player" && isEnemyBullet)
        {
            player.GetComponent<PlayerController>().DamagePlayer(1);
            StartCoroutine(arrowHitCorutine(gameObject));
        }
    }

    IEnumerator arrowHitCorutine(GameObject gameObject)
    {
        AudioManager.Instance.PlaySFX("ArrowHitEnemy");
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }

}