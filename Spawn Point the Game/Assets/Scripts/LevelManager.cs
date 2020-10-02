using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [HideInInspector]
    public int monstersNumber = 0;

    public GameManager gameManager;

    public List<SpawnArea> spawnAreas;
    public GameObject playerPrefab;
    public Transform playerSpawn;

    [HideInInspector]
    public GameObject player;

    public List<MonsterManager> monsters = new List<MonsterManager>();
    public List<MonsterManager> attackingMonsters = new List<MonsterManager>();

    public int monstersOnPlayer = 1;
    public int nextSpawnArea = 0;

    [HideInInspector]
    public Pool pool;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
        monstersNumber = gameManager.difficulty;

        pool = FindObjectOfType<Pool>();

        if(playerPrefab == null) {
            Debug.LogError("No player object set in this level.", gameObject);
            return;
        }

        if (playerSpawn == null) {
            Debug.LogError("No player spawn set in this level.", gameObject);
            return;
        }

        player = Instantiate(playerPrefab, playerSpawn.position, playerSpawn.rotation);

        spawnAreas = new List<SpawnArea>(FindObjectsOfType<SpawnArea>());

        for(int index=0; index<spawnAreas.Count; index++) {
            spawnAreas[index].manager = this;

            int number = monstersNumber / spawnAreas.Count;
            int rest = monstersNumber % spawnAreas.Count;
            if(rest > 0) {
                if (index < rest) {
                    number += 1;
                }
            }
            spawnAreas[index].Spawn(number);
        }

        Ticketing();
    }

    public void AddMonster(MonsterManager monster) {
        monsters.Add(monster);
        monster.manager = this;
    }

    public void WhenMonsterKill(MonsterManager monster) {
        monsters.Remove(monster);
        attackingMonsters.Remove(monster);

        spawnAreas[nextSpawnArea].Spawn();
        nextSpawnArea++;
        if(nextSpawnArea >= spawnAreas.Count) {
            nextSpawnArea = 0;
        }

        Ticketing();
    }

    private void Ticketing() {
        if (attackingMonsters.Count >= monstersOnPlayer) return;
        if (monsters.Count - attackingMonsters.Count <= 0) return;

        MonsterManager monster = null;
        float distance=0f;
        for(int index=0; index<monsters.Count; index++) {

            if (attackingMonsters.Contains(monsters[index]))
                continue;

            if(monster == null) {
                monster = monsters[index];
                distance = Vector3.Distance(player.transform.position, monster.transform.position);
            }
            else {
                float newDistance = Vector3.Distance(player.transform.position, monsters[index].transform.position);
                if(newDistance < distance) {
                    distance = newDistance;
                    monster = monsters[index];
                }
            }

        }
        attackingMonsters.Add(monster);
        monster.target = player.transform;

        Ticketing();
    }
}
