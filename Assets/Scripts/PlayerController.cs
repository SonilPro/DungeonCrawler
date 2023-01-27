using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    //PLAYER VARIABLES
    private Animator anim;
    private Rigidbody2D rb;
    //MOVEMENT VARIABLES
    private Vector2 movement;
    [SerializeField] private float speed;
    [SerializeField] public float health;
    [SerializeField] public float maxHealth;

    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed;
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
        GameController.Instance.ShowDeathScreen();
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
}