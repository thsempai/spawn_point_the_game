using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance=null;
    private static string[] scenes = { "scene1", "scene2", "scene3" };

    public int difficulty = 1;

    public void GotoNextLevel() {
        difficulty++;
        string current = SceneManager.GetActiveScene().name;
        int index = Random.Range(0, scenes.Length);
        while (scenes[index] == current)
            index = Random.Range(0, scenes.Length);
        SceneManager.LoadScene(scenes[index]);
    }

    // Start is called before the first frame update
    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else {
            Destroy(gameObject);
        }
    }
}
