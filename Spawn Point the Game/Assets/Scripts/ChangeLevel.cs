using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChangeLevel : MonoBehaviour
{
    GameManager gameManager;

    private void Awake() {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            gameManager.GotoNextLevel();
        }
    }
}
