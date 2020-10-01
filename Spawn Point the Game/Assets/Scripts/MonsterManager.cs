using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class MonsterManager : MonoBehaviour
{

    public LevelManager manager;

    private Transform _target;

    public Transform target = null;

    private NavMeshAgent agent;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
    }

    public void Kill() {
        manager.WhenMonsterKill(this);
        Destroy(gameObject);
    }

    private void Update() {
        if (target) {
            agent.SetDestination(target.position);
        }
    }

}
