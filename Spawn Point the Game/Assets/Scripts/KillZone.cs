using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        MonsterManager manager = other.GetComponent<MonsterManager>();
        if (manager) {
            manager.Kill();
        }
    }
}
