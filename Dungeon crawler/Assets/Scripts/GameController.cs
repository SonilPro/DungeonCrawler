using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private static EnemyController enemyCntrl;

    private static GameObject player1;

    private static float health = 1000;

    private static int maxHealth = 1000;

    private static float moveSpeed = 5f;

    private static float fireRate = 0.5f;

    private static float bulletSize = 0.5f;


    public static float Health { get => health; set => health = value; }

    public static int MaxHealth { get => maxHealth; set => maxHealth = value; }

    public static float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    public static float FireRate { get => fireRate; set => fireRate = value; }

    public static float BulletSize { get => bulletSize; set => bulletSize = value; }


    public Text healthText;

    void Awake()
    {
        enemyCntrl = GameObject.Find("Enemy").GetComponent<EnemyController>();
        player1 = GameObject.FindGameObjectWithTag("Player");

        if (instance == null)
        {
            instance = this;
        }


    }


    void Update()
    {
        healthText.text = "Health: " + health;
    }

    public static void DamagePlayer(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            player1.transform.position = new Vector3(0, 1000, 0);
            Debug.Log("dsdas");

        }



    }

    public static void HealPlayer(float healAmount)
    {
        Health = Mathf.Min(maxHealth, health + healAmount);
    }

    public static void MoveSpeedChange(float speed)
    {
        moveSpeed += speed;
    }

    public static void FireRateChange(float rate)
    {
        fireRate += rate;
    }

    public static void BulletSizeChange(float size)
    {
        bulletSize += size;
    }


}
