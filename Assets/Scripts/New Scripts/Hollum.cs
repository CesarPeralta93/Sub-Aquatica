using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hollum : MonoBehaviour
{
    public Transform player;
    public Transform spawnPoint;
    public Vector3 spawnPointPosition;
    public float speed;

    void Start()
    {
        spawnPointPosition = transform.position;
        spawnPoint = transform;
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
        float distanceToSpawnpoint = Vector3.Distance(spawnPoint.transform.position, transform.position);
        if(distanceToPlayer <= 10)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, spawnPointPosition, speed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Estalactitas")
        {
            Destroy(this.gameObject);
        }
    }
}
