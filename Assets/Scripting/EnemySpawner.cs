using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject Enemy;
    public Transform[] SpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies();
    }

    public GameObject[] SpawnEnemies()
    {
        GameObject[] Enemies = new GameObject[SpawnPoints.Length];
        for (int i = 0; i < SpawnPoints.Length; i++)
        {
            Enemies[i] = Instantiate(Enemy, SpawnPoints[i].position, SpawnPoints[i].rotation);
        }
        return Enemies;
    }
}
