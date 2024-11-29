using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{

    public float speed;
    public GameObject target;
    public Transform targetTr;
    public float targetTrY;


    void Start()
    {
        target = GameObject.FindGameObjectWithTag("FishTarget");
        targetTr = target.GetComponent<Transform>();
        targetTrY = Random.Range(3, -5);
        targetTr.position = new Vector3(targetTr.position.x, targetTrY, 0);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetTr.position, speed);
    }
}
