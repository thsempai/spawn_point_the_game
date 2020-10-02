using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public List<GameObject> pool = new List<GameObject>();

    public void Add(GameObject item) {
        pool.Add(item);
        item.SetActive(false);
    }

    public GameObject GiveOne(Vector3 position, Quaternion rotation) {
        if(pool.Count > 0) {
            GameObject item = pool[0];
            pool.RemoveAt(0);
            item.SetActive(true);
            item.transform.position = position;
            item.transform.rotation = rotation;
            return item;
        }
        return null;
    }

    public GameObject GiveOne() {
        return GiveOne(Vector3.zero, Quaternion.identity);
    }
}
