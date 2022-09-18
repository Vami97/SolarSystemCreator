using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopSpinning : MonoBehaviour
{
    public RotateItself r;

    void OnMouseOver()
    {
        r.rotating = false;
    }

    void OnMouseExit()
    {
        r.rotating = true;
    }
}
