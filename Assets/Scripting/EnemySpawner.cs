using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] Enemies;
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    public GameObject[] SpawnEnemies()
    {
        GameObject[] SpawnedEnemies = new GameObject[SpawnPoints.Length];
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            System.Random rnd = new System.Random();
            GameObject Enemy = Enemies[rnd.Next(0, Enemies.Length-1)];
            SpawnedEnemies[i] = Instantiate(Enemy, SpawnPoints[i].position, SpawnPoints[i].rotation);
        }
        return Enemies;
    }
}
