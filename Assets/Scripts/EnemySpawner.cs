using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;

    private bool allDead = false;

    private int enemyCount;

    void Start()
    {
        enemyCount = Random.Range(4, 6);
        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        while (enemyCount-- > 0)
        {
            int index = Random.Range(0, enemies.Length);
            float x = Random.Range(-8f, 8f);
            if (x < 2 && x > -2) x = 4;
            float y = Random.Range(-8f, 8f);
            if (y < 2 && y > -2) y = 4;
            Vector3 position = transform.position;
            position.x += x;
            position.y += y;
            Instantiate(enemies[index], position, transform.rotation, transform);
        }
    }

    void Update()
    {
        if (transform.childCount == 0 && !allDead)
        {
            allDead = true;
            Debug.Log(transform.parent.GetChild(1));
        }
    }
}