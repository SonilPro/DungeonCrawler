using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpawnType
{
    Circle,
    Rectangle
}

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private int enemyCount;
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private SpawnType spawnType = SpawnType.Circle;
    [SerializeField] private float spawnRadius;
    [SerializeField] private Vector2 spawnRange;

    private bool allDead = false;
    private bool spawnerEnabled = false;

    private void Update()
    {
        if (transform.childCount == 0 && !allDead && spawnerEnabled)
        {
            allDead = true;
            spawnerEnabled = false;
            EventHandler.StartSpawnerClearEvent(transform.parent.GetInstanceID());
        }
    }
    
    public void Spawn()
    {
        spawnerEnabled = true;
        while (enemyCount-- > 0)
        {
            Instantiate(enemies[Random.Range(0, enemies.Length)], (spawnType == SpawnType.Circle) ? RandomCircle() : RandomRectangle(), transform.rotation, transform);
        }
    }

    private Vector3 RandomCircle()
    {
        Vector3 center = transform.position;
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + spawnRadius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + spawnRadius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }

    private Vector3 RandomRectangle()
    {
        Vector3 center = transform.position;
        Vector3 pos;
        float halfWidth = spawnRange.x / 2;
        float halfHeight = spawnRange.y / 2;
        pos.x = Random.Range(center.x - halfWidth, center.x + halfWidth);
        pos.y = Random.Range(center.y - halfHeight, center.y + halfHeight);
        pos.z = center.z;
        return pos;
    }

    private void OnDrawGizmosSelected()
    {
        if (spawnType == SpawnType.Circle)
        {
            Gizmos.DrawWireSphere(transform.position, spawnRadius);
        }
        else
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(spawnRange.x, spawnRange.y, 0));
        }
    }
}