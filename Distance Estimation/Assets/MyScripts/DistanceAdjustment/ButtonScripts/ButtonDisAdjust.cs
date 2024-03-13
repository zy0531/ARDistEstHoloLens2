using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisAdjust : MonoBehaviour
{
    public Transform ObjectTransform { get; set; }
    //public float Original_x { get; set; }
    //public float Original_y { get; set; }
    //public float Original_z { get; set; }

    public float CoarseAdjValue = 0.05f; //m
    public float FineAdjValue = 0.01f; //m
    public bool IsFineAdj { get; set; }


    // X adjustment
    public void OnButtonIncrementX()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x + FineAdjValue, ObjectTransform.position.y, ObjectTransform.position.z);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x + CoarseAdjValue, ObjectTransform.position.y, ObjectTransform.position.z);
        }
    }

    public void OnButtonDecrementX()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x - FineAdjValue, ObjectTransform.position.y, ObjectTransform.position.z);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x - CoarseAdjValue, ObjectTransform.position.y, ObjectTransform.position.z);
        }
    }

    // Y adjustment
    public void OnButtonIncrementY()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y + FineAdjValue, ObjectTransform.position.z);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y + CoarseAdjValue, ObjectTransform.position.z);
        }
    }

    public void OnButtonDecrementY()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y - FineAdjValue, ObjectTransform.position.z);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y - CoarseAdjValue, ObjectTransform.position.z);
        }
    }

    // Z adjustment
    public void OnButtonIncrementZ()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y, ObjectTransform.position.z + FineAdjValue);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y, ObjectTransform.position.z + CoarseAdjValue);
        }
    }

    public void OnButtonDecrementZ()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y, ObjectTransform.position.z - FineAdjValue);
            else
                ObjectTransform.position = new Vector3(ObjectTransform.position.x, ObjectTransform.position.y, ObjectTransform.position.z - CoarseAdjValue);
        }
    }

}
