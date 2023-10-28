using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonDisAdjust : MonoBehaviour
{
    public Transform ObjectTransform { get; set; }
    public float Original_x { get; set; }
    public float Original_y { get; set; }
    public float Original_z { get; set; }

    public float CoarseAdjValue = 0.05f; //m
    public float FineAdjValue = 0.01f; //m
    public bool IsFineAdj { get; set; }


    // X adjustment
    public void OnButtonIncrementX()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x + FineAdjValue, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x + CoarseAdjValue, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z);
        }
    }

    public void OnButtonDecrementX()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x - FineAdjValue, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x - CoarseAdjValue, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z);
        }
    }

    // Y adjustment
    public void OnButtonIncrementY()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y + FineAdjValue, ObjectTransform.localPosition.z);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y + CoarseAdjValue, ObjectTransform.localPosition.z);
        }
    }

    public void OnButtonDecrementY()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y - FineAdjValue, ObjectTransform.localPosition.z);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y - CoarseAdjValue, ObjectTransform.localPosition.z);
        }
    }

    // Z adjustment
    public void OnButtonIncrementZ()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z + FineAdjValue);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z + CoarseAdjValue);
        }
    }

    public void OnButtonDecrementZ()
    {
        if (ObjectTransform != null)
        {
            if (IsFineAdj)
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z - FineAdjValue);
            else
                ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z - CoarseAdjValue);
        }
    }

}
