using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    
    public float spawnRate = 1.0f;
    public float timeBetweenWaves = 3.0f;

    public int fishCount;

    public GameObject fish;
    public Transform spawnpoint;

    bool waveIsDone = true;

    void Update()
    {
        if (waveIsDone == true)
        {
            StartCoroutine(waveSpawner());
        }
    }

    IEnumerator waveSpawner()
    {
        waveIsDone = false;

        float randomY = Random.RandomRange(2, -5);

        spawnpoint.position = new Vector3( spawnpoint.position.x, randomY, 0);

        for(int i = 0; i < fishCount; i++)
        {
            GameObject fishClone = Instantiate(fish, spawnpoint.position, spawnpoint.rotation);

            yield return new WaitForSeconds(spawnRate);
        }

        spawnRate -= 0.01f;

        waveIsDone = true;
    }
}
