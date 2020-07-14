using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public float spawnProbability = 0.97f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Random.value > spawnProbability)
        {
            // neues Objekt Spawnen:
            GameObject clone = Instantiate(
                prefabs[Random.Range(0, prefabs.Length - 1)],
                new Vector3(Random.Range(-7.5f, 7.5f), 6, 0),
                prefabs[0].transform.rotation);

            float scale = Random.Range(0.5f, 1.5f);
            clone.transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
