using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpaceBody : MonoBehaviour
{
    //Parameters
    public string bodyName = "No Name";
    public int bodyType = 0;  //Custom, Sun, Planet, Asteroid Belt, Moon
    public float bodySpeed = 45f; //Rotation speed

    public void SetParameters(string bName, int bType, float bSpeed)
    {
        bodyName = bName;
        bodyType = bType;
        bodySpeed = bSpeed;
    }

    public abstract void SetVisuals();

}
