using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class scroll : MonoBehaviour {
    private Vector3 backPos;
    public GameObject plane;
    public float width;
    public float height;
    private float X;
    private float Y;


    private void Update()
    {
        //if(plane.transform.position.y>10)
        //{
        //    X = plane.transform.position.x;
        //    Y = backPos.y + height * 2;
        //    //move to new position when invisible
        //    gameObject.transform.position = new Vector3(X, Y, 0f);
        //}
    }

    void OnBecameInvisible()
    {
        //calculate current position
        backPos = gameObject.transform.position;
        //calculate new position
        X = backPos.x + width * 2;
        Y = backPos.y + height * 2;
        //move to new position when invisible
        gameObject.transform.position = new Vector3(X, Y, 0f);
    }
}
