using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWaterPlane : MonoBehaviour {

    // Use this for initialization
    Collider collider;
    Transform player;
    void Start () {
        // in your Start method
        collider = GetComponent<Collider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update () {

        DeleteFar();
    }

    void DeleteFar()
    {
        if (Vector3.Distance(transform.position, player.position) > 200)
        {
            Destroy(transform.gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "WaterPlane")
        {
            Physics.IgnoreCollision(col.collider, collider);
        }

        if (col.gameObject.tag == "Terrain")
        {
            Physics.IgnoreCollision(col.collider, collider);
        }


    }
}
