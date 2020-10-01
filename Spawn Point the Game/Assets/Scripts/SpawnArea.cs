using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{

    public GameObject monster;
    public LevelManager manager;

    [Range(0f, 5f)]
    public float radius=1f;

    public void Awake() {
        if(monster.GetComponent<MonsterManager>() == null) {
            Debug.LogError("GameObject choose as monster don't have MonsterManager", gameObject);
        }
    }

    public void Spawn() {
        if(monster == null) {
            Debug.LogError("No monster set in " + gameObject.name, gameObject);
        }
        Vector3 position = transform.position;

        Vector3 rnd = Random.insideUnitSphere * radius;
        rnd.y = 0;
        position += rnd;

        GameObject newMonster = Instantiate(monster, position, Quaternion.identity);

        MonsterManager monsterManager = newMonster.GetComponent<MonsterManager>();
        manager.AddMonster(monsterManager);

    }

    public void Spawn(int number) {
        for(int n=0; n < number; n++) {
            Spawn();
        }
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = new Color(0f, 0f, 1f, 0.25f);
        Gizmos.DrawWireSphere(transform.position, radius);
    }


}
