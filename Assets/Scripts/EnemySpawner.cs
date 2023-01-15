using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnRange;

    private bool allDead = false;
    private bool spawnerEnabled = false;

    private int enemyCount;

    void Start()
    {
        enemyCount = Random.Range(4, 6);
    }

    void Update()
    {
        if (transform.childCount == 0 && !allDead && spawnerEnabled)
        {
            allDead = true;
            spawnerEnabled = false;
            EventHandler.StartDoorEvent();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !allDead)
        {
            Spawn();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, spawnRange);
    }

    void Spawn()
    {
        spawnerEnabled = true;
        EventHandler.StartDoorEvent();
        while (enemyCount-- > 0)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], RandomCircle(), transform.rotation, transform);
        }
    }

    private Vector3 RandomCircle()
    {
        Vector3 center = transform.position;
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + spawnRange * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + spawnRange * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
}