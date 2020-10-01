using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnArea : MonoBehaviour
{

    public GameObject monster;

    [Range(0f, 5f)]
    public float radius=1f;

    public GameObject Spawn() {
        if(monster == null) {
            Debug.LogError("No monster set in " + gameObject.name, gameObject);
            return null;
        }
        Vector3 position = transform.position;

        Vector3 rnd = Random.insideUnitSphere * radius;
        rnd.y = 0;
        position += rnd;

        GameObject newMonster = Instantiate(monster, position, Quaternion.identity);

        return newMonster;
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
