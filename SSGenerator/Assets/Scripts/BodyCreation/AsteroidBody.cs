using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBody : SpaceBody, IClickable
{
    //Parameters
    public int aDensity; //Sparse, Low, Average, High, Extreme
    public float aLocation;

    public void Click()
    {
        throw new System.NotImplementedException();
    }

    public override void SetVisuals()
    {
        throw new System.NotImplementedException();
    }
}
