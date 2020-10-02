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

    public Material dyingMaterial;
    private Material originalMaterial;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        originalMaterial = GetComponent<Renderer>().material;
    }

    public void Revive() {
        GetComponent<Renderer>().material = originalMaterial;
    }

    IEnumerator _Kill() {
        yield return new WaitForSeconds(1);
        manager.WhenMonsterKill(this);
        if (manager.pool) {
            manager.pool.Add(gameObject);
        }
        else {
            Destroy(gameObject);
        }
        yield return null;
    }

    public void Kill() {
        GetComponent<Renderer>().material = dyingMaterial;
        agent.isStopped = true;
        StartCoroutine(_Kill());
    }

    private void Update() {
        if (target) {
            agent.SetDestination(target.position);
        }
    }

}
