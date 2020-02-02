using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed;
    Rigidbody2D rb;
    public Text collectedText;
    public static int collectedAmount = 0;
    public bool moving;

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



        //MOVING RIGHT
        if (horizontal > 0.2)
        {
            RightMovement();
        }

        //MOVING LEFT
        else if (horizontal < -0.2)
        {
            LeftMovement();
        }
        
        //MOVING UP || DOWN
        else if (vertical > 0.2 || vertical < -0.2 )
        {
            UpOrDownMovement(vertical);
        }

        //NEITHER MOVING RIGHT NOR LEFT
        else
        {
            StandingStill();
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

    void LeftMovement()
    {
        anim.SetBool("IsUpRight", false);
        anim.SetBool("IsDownLeft", false);
        anim.SetBool("IsRunningRight", false);
        anim.SetBool("IsIdleRight", false);
        anim.SetBool("IsIdleLeft", false);
        anim.SetBool("IsRunningLeft", true);
        lastPosition = transform.position;
    } 

    void RightMovement()
    {
        anim.SetBool("IsUpRight", false);
        anim.SetBool("IsDownLeft", false);
        anim.SetBool("IsRunningLeft", false);
        anim.SetBool("IsIdleRight", false);
        anim.SetBool("IsIdleLeft", false);
        anim.SetBool("IsRunningRight", true);
        lastPosition = transform.position;
    }

    void StandingStill()
    {
        if (lastPosition.x >= transform.position.x)
        {
            anim.SetBool("IsUpRight", false);
            anim.SetBool("IsDownLeft", false);
            anim.SetBool("IsRunningLeft", false);
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsIdleLeft", true);

        }

        else if (lastPosition.x < transform.position.x)
        {
            anim.SetBool("IsUpRight", false);
            anim.SetBool("IsDownLeft", false);
            anim.SetBool("IsRunningRight", false);
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsIdleRight", true);

        }
        
    }

    void UpOrDownMovement(float vertical)
    {
        //MOVING UP
        if(vertical > 0.2)
        {
            anim.SetBool("IsIdleRight", false);
            anim.SetBool("IsUpRight", true);
            anim.SetBool("IsDownLeft", true);
        }

        //MOVING DOWN
        else
        {
            anim.SetBool("IsIdleLeft", false);
            anim.SetBool("IsDownLeft", true);
            anim.SetBool("IsUpRight", true);

        }
    }


    


}
