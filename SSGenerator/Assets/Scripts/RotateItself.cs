using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateItself : MonoBehaviour
{
    private Vector3 point;

    // Start is called before the first frame update
    void Start()
    {
        point = new Vector3(-290f, -150f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 30 * Time.deltaTime);
        transform.RotateAround(point, Vector3.forward, 30 * Time.deltaTime);
    }
}
