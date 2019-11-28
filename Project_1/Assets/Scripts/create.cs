using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create : MonoBehaviour {
    public GameObject cloud;
    public GameObject plane;
    int i;
    public int s, e; 

    private void Start()
    {
        i = Random.Range(50, 100);
    }

    void Update () {
        if (plane.transform.position.y >= 5)
        {
            i--;
            if (i==0)
            {
                System.Random rnd = new System.Random();
                float distanceFromCamera = 10.0f;
                float value = rnd.Next(0, Screen.height);
                Instantiate(cloud);
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, value, distanceFromCamera));

                cloud.transform.position = pos;

                i= Random.Range(s, e);
            }
    }
}
}
