using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    private int enemyCount;
    public List<GameObject> enemySpawner1;
    public List<GameObject> enemySpawner2;
    public List<GameObject> enemySpawner3;
    public GameObject enemyPrefab;
    
    void Start()
    {
        enemyCount = Random.Range(1, 4);
        Debug.Log($"Encuetro con {enemyCount} enemigos");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
