using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ObjectToSpawn;
    public float distanceToSpawn;
    public float forwardDistanceToSpawn;
    System.Random rnd = new System.Random();
    // Start is called before the first frame update
    void Start()
    {
        distanceToSpawn = 2f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player" && Input.GetKeyDown(KeyCode.F))
        {
            Vector3 spawnLocation = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + distanceToSpawn, gameObject.transform.position.z-3);
            Instantiate(ObjectToSpawn, spawnLocation, Quaternion.identity);
        }
    }
}
