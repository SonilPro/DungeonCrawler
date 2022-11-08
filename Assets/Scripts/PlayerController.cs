using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //PLAYER VARIABLES
    private Animator anim;

    //MOVEMENT VARIABLES
    private Vector2 movement;
    [SerializeField] private float speed;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;

    //ENEMIES
    [NonSerialized]
    public GameObject closestEnemy;
    private List<GameObject> enemies = new List<GameObject>();
    private List<Vector2> enemyPositions = new List<Vector2>();

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        transform.Translate(movement * speed * Time.deltaTime, 0);
    }

    void Update()
    {
        Movement();
        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

    }

    public void DamagePlayer(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            KillPlayer();
        }
    }

    public void HealPlayer(float healAmount)
    {
        health = Mathf.Min(maxHealth, health + healAmount);
    }

    public void KillPlayer()
    {
        transform.position = new Vector3(0, 1000, 0);
        Debug.Log("dead");
    }

    public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void DeleteEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    void Movement()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        anim.SetFloat("Horizontal", movement.x);
        anim.SetFloat("Speed", Mathf.Min(movement.sqrMagnitude, 1));
    }

    void GetEnemyPosition()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (enemyPositions.Count == i)
            {

                enemyPositions.Add(enemies[i].transform.position);
            }
            else
            {
                enemyPositions[i] = enemies[i].transform.position;
            }
        }
    }

    public bool FindClosestEnemy()
    {
        GetEnemyPosition();
        bool found = false;
        if (enemyPositions.Count > 0)
        {
            found = true;
            if (enemies.Count == 0) return false;

            closestEnemy = enemies[0];
            float dist = Vector2.Distance(transform.position, enemies[0].transform.position);
            for (int i = 1; i < enemies.Count; i++)
            {
                float tempDist = Vector2.Distance(transform.position, enemies[i].transform.position);
                if (tempDist < dist)
                {
                    closestEnemy = enemies[i];
                    dist = tempDist;
                }
            }
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
        }
        return found;
    }

}