using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using Microsoft.MixedReality.Toolkit.Examples.Demos;

public class SliderDisAdjust : MonoBehaviour
{
    [SerializeField]
    float sliderOffset = 0.5f;

    public Transform ObjectTransform { get; set; } 
    public float Original_x { get; set; }
    public float Original_y { get; set; }
    public float Original_z { get; set; }

    void Start()
    {
        /*Original_x = ObjectTransform.localPosition.x;
        Original_y = ObjectTransform.localPosition.y;
        Original_z = ObjectTransform.localPosition.z;*/
    }


    public void OnSliderUpdatedX(SliderEventData eventData)
    {
        if (ObjectTransform != null)
        {
            // Move the target object using Slider's eventData.NewValue
            ObjectTransform.localPosition = new Vector3(Original_x + eventData.NewValue - sliderOffset, ObjectTransform.localPosition.y, ObjectTransform.localPosition.z);
        }
    }
    public void OnSliderUpdatedY(SliderEventData eventData)
    {
        if (ObjectTransform != null)
        {
            // Move the target object using Slider's eventData.NewValue
            ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, Original_y + eventData.NewValue - sliderOffset, ObjectTransform.localPosition.z);
        }
    }
    public void OnSliderUpdatedZ(SliderEventData eventData)
    {
        if (ObjectTransform != null)
        {
            // Move the target object using Slider's eventData.NewValue
            ObjectTransform.localPosition = new Vector3(ObjectTransform.localPosition.x, ObjectTransform.localPosition.y, Original_z + eventData.NewValue - sliderOffset);
        }
    }
}
