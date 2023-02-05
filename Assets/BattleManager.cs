using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class BattleManager : MonoBehaviour
{
    private int enemyCount;
    public GameObject enemySpawner1;
    public List<GameObject> enemySpawner2;
    public List<GameObject> enemySpawner3;
    public List<GameObject> enemiesPrefabs;
    public GameObject target;
    public int targetId;
    public List<GameObject> currentEnemies = new List<GameObject>();
    public Words words;
    public int difficulty = 1;

    private void Awake()
    {
        WordsService.GetData();
        PlayerStats.health = 100;
        PlayerStats.attackDamage = 10;
    }

    void Start()
    {
        enemyCount = Random.Range(1, 4);
        Debug.Log($"Encuetro con {enemyCount} enemigos");

        SpawnEnemies();

        target = currentEnemies[targetId];
        ExecuteFocus();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            FocusEnemy();
        }
    }

    public void SpawnEnemies()
    {
        switch (enemyCount)
        {
            case 1:
                var enemy = Random.Range(0, enemiesPrefabs.Count);
                var enemyInstance = Instantiate(enemiesPrefabs[enemy], enemySpawner1.transform.position, enemySpawner1.transform.rotation);
                currentEnemies.Add(enemyInstance);
                break;
            case 2:
                for (int i = 0; i < enemyCount; i++)
                {
                    var enemy2 = Random.Range(0, enemiesPrefabs.Count);
                    var enemyInstance2 = Instantiate(enemiesPrefabs[enemy2], enemySpawner2[i].transform.position, enemySpawner2[i].transform.rotation);
                    currentEnemies.Add(enemyInstance2);
                }
                break;
            case 3:
                for (int i = 0; i < enemyCount; i++)
                {
                    var enemy3 = Random.Range(0, enemiesPrefabs.Count);
                    var enemyInstance3 = Instantiate(enemiesPrefabs[enemy3], enemySpawner3[i].transform.position, enemySpawner3[i].transform.rotation);
                    currentEnemies.Add(enemyInstance3);
                }
                break;
        }
    }

    public void FocusEnemy()
    {
        targetId++;
        if (targetId > currentEnemies.Count - 1)
        {
            targetId = 0;
        }
            
        target = currentEnemies[targetId];
        foreach (var enemy in currentEnemies)
        {
            var enemyComp = enemy.GetComponent<Enemy>();
            enemyComp.cursor.enabled = false;
            enemyComp.isFocused = false;
        }

        ExecuteFocus();
    }

    public void RemoveEnemy(GameObject go)
    {
        FocusEnemy();
        currentEnemies.Remove(go);
    }

    public void ExecuteFocus()
    {
        var focusedEnemy = target.GetComponent<Enemy>();
        focusedEnemy.cursor.enabled = true;
        focusedEnemy.isFocused = true;
    }
}
