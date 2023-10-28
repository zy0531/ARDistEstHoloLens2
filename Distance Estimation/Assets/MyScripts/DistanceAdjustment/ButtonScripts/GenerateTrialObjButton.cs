using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTrialObjButton : MonoBehaviour
{
    [SerializeField]
    ObjectPlacement objectPlacement;

    [SerializeField]
    [Tooltip("Set a gameObject to be instantiate for distance estimation")]
    GameObject m_gameObjectPrefab;

    [SerializeField]
    GameObject mainCamera;

    // pass the original transform of the obj in current trial to the ButtonDisAdjust script
    [SerializeField]
    ButtonDisAdjust buttonDisAdjust;


    [SerializeField]
    GameObject InstructionCanvasFinish;


    // temporal trialData
    List<int> trialData;
    int trialNum;

    // Start is called before the first frame update
    void Start()
    {
        trialData = new List<int> { 3, 4, 5, 6 };
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
            // 1. Save Data for the previous finished trial
            // TO DO...



            // 2. For next Trial
            var ObjTransform = GenerateGameObject_withDistance(objectPlacement.hitPoint, m_gameObjectPrefab, trialData[trialNum++], objectPlacement.hitNormal, objectPlacement.forwardDirection);           

            // assign original transform to the object of the current trial
            if (ObjTransform != null)
            {
                buttonDisAdjust.Original_x = ObjTransform.localPosition.x;
                buttonDisAdjust.Original_y = ObjTransform.localPosition.y;
                buttonDisAdjust.Original_z = ObjTransform.localPosition.z;

                buttonDisAdjust.ObjectTransform = ObjTransform;
            }

        }
        else
        {
            // Task completed!
            // show instructions on Canvas
            InstructionCanvasFinish.SetActive(true);
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
