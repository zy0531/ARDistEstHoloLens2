using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate: MonoBehaviour
{
    public float degree = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Rotate the object clockwise
    public void RotateObjectClockwise()
    {
        transform.Rotate(0.0f, degree, 0.0f, Space.World);
    }

    // Rotate the object counterclockwise
    public void RotateObjectCounterclockwise()
    {
        transform.Rotate(0.0f, -1.0f*degree, 0.0f, Space.World);
    }
}
