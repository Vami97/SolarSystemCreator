using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour
{
    public Vector3 point;
    public bool rotating = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(rotating)
        {
            transform.Rotate(Vector3.forward, 30 * Time.deltaTime);
            transform.RotateAround(point, Vector3.forward, 30 * Time.deltaTime);
        }      
    }


}
