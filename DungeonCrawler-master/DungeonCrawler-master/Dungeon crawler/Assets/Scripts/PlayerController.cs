using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public Text collectedText;
    public static int collectedAmount = 0;

    public GameObject bulletPrefab;
    public float bulletSpeed;
    private float lastFire;
    public float fireDelay;

    private Vector2 lastPosition;

    private Animator anim;

    public Text healthText;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {

        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {
            Shoot(shootHor, shootVert);
            lastFire = Time.time;
        }

        rb.velocity = new Vector3(horizontal * speed, vertical * speed, 0);

        if (horizontal > 0.2)
        {
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsRunningRight", true);
            lastPosition = transform.position;

        }
        else if (horizontal < -0.2)
        {
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsRunningLeft", true);
            lastPosition = transform.position;
        }
        else
        {

            if (lastPosition.x >= transform.position.x)
            {
                anim.SetBool("IsRunningLeft", false);
                anim.SetBool("IsIdleRight", false);
                anim.SetBool("IsIdleLeft", true);
            }

            else if (lastPosition.x < transform.position.x)
            {

                anim.SetBool("IsRunningRight", false);
                anim.SetBool("IsIdleLeft", false);
                anim.SetBool("IsIdleRight", true);
            }

        }



        collectedText.text = "Items collected " + collectedAmount;

    }

    void Shoot(float x, float y)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
        bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
         );
    }

    public void KillPlayer()
    {

    }


}
