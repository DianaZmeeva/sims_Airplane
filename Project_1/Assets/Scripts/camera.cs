﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour {

    public GameObject player;  
    private Vector3 offset;


    // Use this for initialization
    void Start () {
        offset = transform.position - player.transform.position;
    }
	
	// Update is called once per frame
	void LateUpdate(){
        //if (player.transform.position.y >-1)
        //{
        transform.position = player.transform.position + offset;
        //}
    }
}
