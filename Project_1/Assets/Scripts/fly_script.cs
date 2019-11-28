using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class fly_script : MonoBehaviour {

    public GameObject plane;
    double m = 500;
    double N;
    double size = 4.4, sizeX = 1.2, sizeY = 0.17;
    double g = 9.81;
    double angle, weight;
    double x, y, vx=0, vy=0, ax=0, ay=0;
    double thrust, thrustX, thrustY;
    double lift, liftX, liftY;
    double drag, dragX, dragY;
    int airspeed, time;
    bool isCollided, isfly,flag=true;
    double s, sx;

    double[][] liftCoef = {
            new double[] {0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9, 1, 1.1, 1.2, 1.3, 1.4, 1.59, 1.59, 1.49, 1.29, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new double[] { 0.45, 0.55, 0.65, 0.75, 0.85, 0.95, 1.05, 1.15, 1.25, 1.35, 1.45, 1.55, 1.65, 1.75, 1.85, 1.95, 2.05, 2.15, 2.25, 2.35, 2.45, 2.55, 2.65, 2.75, 2.85, 2.95, 3.05, 3.11, 3.15, 3.15, 3.11, 3.05, 2.95, 2.81, 2.65, 2.45, 2.21, 0, 0, 0, 0}
    };

    double[][] dragCoef = {
            new double[]{0.11, 0.13, 0.15, 0.18, 0.2, 0.22, 0.25, 0.27, 0.29, 0.31, 0.34, 0.36, 0.38, 0.4, 0.42, 0.44, 0.46, 0.48, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
            new double[]{ 0.17, 0.22, 0.28, 0.33, 0.38, 0.43, 0.48, 0.53, 0.58, 0.63, 0.68, 0.73, 0.77, 0.82, 0.87, 0.91, 0.97, 1, 1.04, 1.09, 1.13, 1.17, 1.21, 1.25, 1.29, 1.33, 1.37, 1.41, 1.45, 1.48, 1.52, 1.56, 1.59, 1.63, 1.66, 1.69, 1.73, 0, 0, 0, 0}
    };

    public Slider slider_flaps;
    public Slider slider_angle;
    public Slider slider_thrust;

    public Text t_flaps, t_angle, t_thrust, t_airspeed, t_high, t_time, t_X;

    // Use this for initialization
    void Start () {
        thrustX = -1;
        x = plane.transform.position.x;
        y = plane.transform.position.y;

        weight = -m * g;
        N = -weight;
        s = y;
        sx = x;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isCollided = true;
        }
    }

    // Update is called once per frame
    void Update () {


        thrust = 1000 *slider_thrust.value;
        angle = Math.Atan2(vy, vx);

        if (thrustX != -1)
            thrust = Math.Sqrt(thrustX * thrustX + thrustY * thrustY);
        thrustX = thrust * Math.Cos(angle + slider_angle.value * Math.PI / 180);
        thrustY = thrust * Math.Sin(angle + slider_angle.value * Math.PI / 180);

        lift = 0.5 * Math.Exp(-y / 10000) * (vx * vx + vy * vy) * size * sizeX * liftCoef[(int)slider_flaps.value][(int)slider_angle.value];
        liftX = -lift * Math.Sin(angle);
        liftY = lift * Math.Cos(angle);

        drag = 0.5 * Math.Exp(-y / 10000) * (vx * vx + vy * vy) * size * sizeY * dragCoef[(int)slider_flaps.value][(int)slider_angle.value];
        dragX = -drag * Math.Cos(angle);
        dragY = -drag * Math.Sin(angle);

        ax = (thrustX + liftX + dragX) / m;

        if (isCollided==true)
        {
            if (liftY < N)
                ay = 0;
            else
            {
                ay = (thrustY + liftY + dragY + weight) / m;
                isCollided = false;
                isfly = true;
            }
        }

        else
        {
            ay = (thrustY + liftY + dragY + weight) / m;
        }
        

        vx = vx + Time.deltaTime * ax;
        vy = vy + Time.deltaTime * ay;

        x = x + vx * Time.deltaTime;
        y = y + vy * Time.deltaTime;

        Transform_position();
        airspeed = (int)Math.Sqrt(vx * vx + vy * vy);

        if (isfly==true && (y<=s || airspeed>150 ))
        {
            Destroy(plane);
            flag= false;
        }

        time++;
        change_text(flag);
    }

    private void Transform_position()
    {
        double airAngle = 180 * angle / Math.PI + slider_angle.value;

        Vector3 vector;
        vector.x = (float)x;
        vector.y = (float)y;
        vector.z = transform.position.z;

        Vector3 a;
        a.x = plane.transform.eulerAngles.x;
        a.y = plane.transform.eulerAngles.y;
        a.z = (float)airAngle;


        plane.transform.position = vector;
        plane.transform.eulerAngles = a;
    }

    private void change_text(bool flag)
    {
        if(flag==false)
        {
            t_airspeed.text = "CRASH!!!";
            t_airspeed.color = Color.red;
            t_high.text = "Y: 0";
        }

        else
        {
            if (slider_flaps.value == 1)
            {
                t_flaps.text = "Flaps: ON";
            }

            else
            {
                t_flaps.text = "Flaps: OFF";
            }

            int t = 1000 * (int)slider_thrust.value;
            t_thrust.text = "Thrust: " + t;

            t_angle.text = "Attack angle: " + slider_angle.value;

            t_high.text = "Y: " + Math.Round(y - s, 2);
            t_X.text = "X: " + Math.Round((x - sx) / 200, 2);
            t_airspeed.text = "Air speed: " + airspeed;

            t_time.text = "Time: " + (int)time / 100;
        }
    }
}
