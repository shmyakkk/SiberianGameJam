using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Массив трансформеров для спауна")]
    [SerializeField] private List<GameObject> SpawnCoords;

    [Header("Префаб монстра для спауна")]
    [SerializeField]GameObject Enemy;


    public void Spawn(){
        Instantiate(Enemy,SpawnCoords[Random.Range(0,SpawnCoords.Count)].transform.position, Quaternion.identity);
    }
}
