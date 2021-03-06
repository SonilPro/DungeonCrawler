﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public List<GameObject> enemies;
    public List<Vector2> getEnemyPosition;
    
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

    Vector2 closest;
    float distance = 0;
    float closestDistance = 0;



    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    public void AddEnemy(GameObject enemy)
    {
        
        enemies.Add(enemy);
       
    }

    public void DeleteEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }

    void Update()
    {
        

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        fireDelay = GameController.FireRate;
        speed = GameController.MoveSpeed;

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");

        if (Input.GetKey(KeyCode.K) && Time.time > lastFire + fireDelay)
        {
            Shoot();
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


        GetEnemyPosition();
        //collectedText.text = "Items collected " + collectedAmount;


    }

    
        void GetEnemyPosition()
        {
        
        for (int i = 0; i < enemies.Count; i++)
            {
            
                if(getEnemyPosition.Count == i)
                {

                getEnemyPosition.Add(enemies[i].transform.position);

                }
                else
                {
                getEnemyPosition[i] = enemies[i].transform.position;
                
                }
            
            }
        }

        void Shoot()
        {
            FindClosestEnemy();
            /*EnemyPos
            Debug.Log(enemyPositon);
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<BulletController>().GetEnemy(enemyPositon.x, enemyPositon.y);
            bullet.AddComponent<Rigidbody2D>().gravityScale = 0;
            bullet.GetComponent<BulletController>().isPlayerBullet = true;*/
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

    void FindClosestEnemy()
    {


        if (getEnemyPosition.Count > 0) {
            GameObject closestEnemy = enemies[0];
            float dist = Vector2.Distance(transform.position, enemies[0].transform.position);
            for (int i = 0; i < enemies.Count; i++) {
                float tempDist = Vector2.Distance(transform.position, enemies[i].transform.position);
                if (tempDist < dist) {
                    closestEnemy = enemies[i];
                }
            }
            Debug.Log(closestEnemy.transform.position);
            Debug.DrawLine(transform.position, closestEnemy.transform.position);
        }

        /*for (int i = 0; i < getEnemyPosition.Count; i++)
        {
            distance = Vector2.Distance(getEnemyPosition[i], this.transform.position);
            if(i == 0)
            {
                closestDistance = distance;
            }
            else
            {
                closestDistance = Mathf.Min(closestDistance, distance);
                
                
                
                
            }
            if (i == getEnemyPosition.Count - 1 && Vector2.Distance(getEnemyPosition[i], this.transform.position) == closestDistance)
            {
                closest = getEnemyPosition[i];
                
            }

        }



            /*if (closestDistance > Vector2.Distance(getEnemyPosition[i], this.transform.position))
            {
                closestDistance = Vector2.Distance(getEnemyPosition[i], this.transform.position);
                closest = getEnemyPosition[i];
                Debug.Log(closestDistance);
                
                Debug.Log(closest);
            }
            else
            {
                closestDistance = Vector2.Distance(getEnemyPosition[i], this.transform.position);
                Debug.Log(closestDistance);
            }

            
            */


    }
        










    

    
}
