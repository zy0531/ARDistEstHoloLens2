using Microsoft.MixedReality.Toolkit.UI;
using TMPro;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Examples.Demos;

public class ShowMySliderValue : MonoBehaviour
{
    [SerializeField]
    private TextMeshPro textMesh = null;

    [SerializeField]
    float sliderOffset = 0.5f;

    public void OnSliderUpdated(SliderEventData eventData)
    {
        if (textMesh == null)
        {
            textMesh = GetComponent<TextMeshPro>();
        }

        if (textMesh != null)
        {
            textMesh.text = $"{eventData.NewValue- sliderOffset:F2}";
        }
    }
}
