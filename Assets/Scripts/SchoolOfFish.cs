using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class SchoolOfFish : MonoBehaviour
{

    private SpriteRenderer[] fishies;

    public int sortinLayer;

    void Start()
    {
        fishies = GetComponentsInChildren<SpriteRenderer>();
        sortinLayer = Random.Range(4, 8);
        for (int i = 0; i < fishies.Length; i++)
        {
            fishies[i].sortingOrder = sortinLayer;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
