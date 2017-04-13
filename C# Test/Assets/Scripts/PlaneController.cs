using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour {


    private Transform player;
    private float level = -8f;
    private RaycastHit hit;


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Vector3 fwd = transform.TransformDirection(Vector3.down);

    }

    // Update is called once per frame
    void Update () {



        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {
            print("Found an object - distance: " + hit.distance);

           }
        else
        {
            level = -10000;
        }
        transform.position = new Vector3(player.transform.position.x, level, player.transform.position.z);

    }
}
