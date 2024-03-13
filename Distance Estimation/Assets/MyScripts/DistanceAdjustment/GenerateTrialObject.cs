using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrialObject : MonoBehaviour
{
    [SerializeField]
    ObjectPlacement objectPlacement;

    [SerializeField]
    [Tooltip("Set a gameObject to be instantiate for distance estimation")]
    GameObject m_gameObjectPrefab;

    [SerializeField]
    GameObject mainCamera;

    // pass the original transform of the obj in current trial to the Slider Adjustment script
    [SerializeField]
    SliderDisAdjust sliderDisAdjust;

    Transform adjustableGameObjectTransform;

    // temporal trialData
    List<int> trialData;
    int trialNum;

    // Start is called before the first frame update
    void Start()
    {
        trialData = new List<int> { 3,4,5,6 };
        trialNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetForNextTrial()
    {
        if (trialNum < trialData.Count)
        {
            var ObjTransform = GenerateGameObject_withDistance(objectPlacement.hitPoint, m_gameObjectPrefab, trialData[trialNum++], objectPlacement.hitNormal, objectPlacement.forwardDirection);
            adjustableGameObjectTransform = ObjTransform;

            // assign original transform to the object of the current trial
            if (ObjTransform != null)
            {
                sliderDisAdjust.Original_x = adjustableGameObjectTransform.localPosition.x;
                sliderDisAdjust.Original_y = adjustableGameObjectTransform.localPosition.y;
                sliderDisAdjust.Original_z = adjustableGameObjectTransform.localPosition.z;

                sliderDisAdjust.ObjectTransform = adjustableGameObjectTransform;
            }
            
        }
        else
        {

        }
        
    }

    public Transform GenerateGameObject_withDistance(Vector3 hitpoint, GameObject objectToGenerate, float dis, Vector3 planeNormal, Vector3 forwardOffset)
    {
        float yOffset = 0.11f; // radius of a size 5 soccer ball
        Vector3 pos = new Vector3(mainCamera.transform.position.x, hitpoint.y + yOffset, mainCamera.transform.position.z) + dis * forwardOffset;
        // GameObject gameobject = Instantiate(objectToGenerate, pos, Quaternion.identity);
        objectToGenerate.SetActive(true);
        objectToGenerate.transform.position = pos;
        Debug.Log("Generate!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Soccer");
        return objectToGenerate.transform;
    }
}
