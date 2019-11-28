using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class move : MonoBehaviour {

    public GameObject cloud;

    private Vector2 movement;

    void Update()
    {
        movement = new Vector2(
          cloud.transform.position.x - 0.1f,
          cloud.transform.position.y);

        cloud.transform.position = movement;
    }

    void OnBecameInvisible()
    {
        Destroy(cloud);
    }
}

