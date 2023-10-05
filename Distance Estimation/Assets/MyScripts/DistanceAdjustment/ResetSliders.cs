using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

public class ResetSliders : MonoBehaviour
{
    [SerializeField]
    PinchSlider pinSlider_X;
    [SerializeField]
    PinchSlider pinSlider_Y;
    [SerializeField]
    PinchSlider pinSlider_Z;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetSliderValue()
    {
        pinSlider_X.SliderValue = 0.5f;
        pinSlider_Y.SliderValue = 0.5f;
        pinSlider_Z.SliderValue = 0.5f;
    }
}
