using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{

    public Blue respawn;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Respawn()
    {
        respawn.Respawn();
    }
}
