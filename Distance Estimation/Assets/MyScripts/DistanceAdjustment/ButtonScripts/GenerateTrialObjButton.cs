using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using Microsoft.MixedReality.Toolkit.Utilities.Solvers;

public class GenerateTrialObjButton : MonoBehaviour
{
    //[SerializeField]
    //ObjectPlacement objectPlacement;

    //[SerializeField]
    //[Tooltip("Set a gameObject to be instantiate for distance estimation")]
    //GameObject m_gameObjectPrefab;

    //[SerializeField]
    //GameObject mainCamera;

    [SerializeField]
    GameObject alignmentCube;

    [SerializeField]
    GameObject standingRepresentation;


    // pass the original transform of the obj in current trial to the ButtonDisAdjust script
    [SerializeField]
    ButtonDisAdjust buttonDisAdjust;

    // set instruction canvas
    [SerializeField]
    GameObject InstructionCanvasPractice;
    [SerializeField]
    GameObject InstructionCanvasNextTrial;
    [SerializeField]
    GameObject InstructionCanvasVisionChange;
    [SerializeField]
    GameObject InstructionCanvasFinish;

    public AudioClip audioClip; // Assign the audio clip in the Unity Editor
    private AudioSource audioSource;


    // assign gameobjects for experiment at 3,4,5,6m
    /// <summary>
    /// only assign 4 objects with specific distance
    /// </summary>
    public List<GameObject> gameObjectList_Experiment = new List<GameObject>();

    // assign gameobjects for training at 3.5m and 4.5m
    /// <summary>
    /// only assign 2 objects with specific distance
    /// </summary>
    public List<GameObject> gameObjectList_Training = new List<GameObject>();

    private GameObject prefab3;
    private GameObject prefab4;
    private GameObject prefab5;
    private GameObject prefab6;

    private GameObject prefab3_5;
    private GameObject prefab4_5;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private GameObject currentObject;
    private float startingdistance = 3f;

    private Vector3 adjustedPosition;
    private float diff_x, diff_y, diff_z;

    // trialData
    List<int> trialData = new List<int> { 3, 3, 3, 4, 4, 4, 5, 5, 5, 6, 6, 6 };
    int trialNum = 0;
    int visionCondition = 0;
    bool formalTrial;
    bool practiceTrial1, practiceTrial2;

    // dataManager
    DataManager dataManager = new DataManager();
    string filename;

    void Awake()
    {
        // shuffle trials
        trialData = GlobalMethods.shuffle(trialData);
        for (int i = 0; i < trialData.Count; i++)
            Debug.Log(trialData[i]);

        // create a file
        filename = $"Data_{DateTime.Now:yyyyMMdd_HHmmss}.csv";
        dataManager.SaveToCSV(filename, "Time, VisionCondition, Trial, Distance, InitialRotation, InitialPosition, AdjustedPosition, diff_x, diff_y, diff_z");
    }

    // Start is called before the first frame update
    void Start()
    {
        // read in gameobjects in gameObjectList_Experiment
        prefab3 = gameObjectList_Experiment[0];
        prefab4 = gameObjectList_Experiment[1];
        prefab5 = gameObjectList_Experiment[2];
        prefab6 = gameObjectList_Experiment[3];

        // read in gameobjects in gameObjectList_Training
        prefab3_5 = gameObjectList_Training[0];
        prefab4_5 = gameObjectList_Training[1];

        // set up the audio
        // Add an AudioSource component to the GameObject
        audioSource = gameObject.AddComponent<AudioSource>();
        // Set the AudioClip to the AudioSource
        audioSource.clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetForNextTrial()
    {
        // For practice trials
        if (!formalTrial)
        {
            DeactivateAllPrefabs();
           
            // if already finish all practice trials
            if(practiceTrial1 & practiceTrial2)
            {
                prefab4_5.SetActive(false);
                formalTrial = true;

                // Play the audio
                audioSource.Play();

                InstructionCanvasPractice.SetActive(false);
                InstructionCanvasNextTrial.SetActive(true);
            }
            // have not finish all practice trials
            else
            {
                SetForNextTrial_Practice();
            }
        }
        // For formal trials
        else
        {
            if(visionCondition == 0)
            {
                SetForNextTrial_Formal();
            }
            else if(visionCondition == 1)
            {
                InstructionCanvasVisionChange.SetActive(false);
                InstructionCanvasNextTrial.SetActive(true);
                SetForNextTrial_Formal();
            }
            
        }
    }

    public void SetForNextTrial_Practice()
    {
        if (!practiceTrial1 & !practiceTrial2)
        {
            // activate the cube at 3.5m
            prefab3_5.SetActive(true);
            // adjust with buttons
            var ObjTransform = prefab3_5.transform;
            if (ObjTransform != null)
            {
                buttonDisAdjust.ObjectTransform = ObjTransform;
            }

            practiceTrial1 = true;
        }
        else if(!practiceTrial2)
        {
            // deactivate the cube at 3.5m
            prefab3_5.SetActive(false);

            // activate the cube at 4.5m
            prefab4_5.SetActive(true);
            // adjust with buttons
            var ObjTransform = prefab4_5.transform;
            if (ObjTransform != null)
            {
                buttonDisAdjust.ObjectTransform = ObjTransform;
            }

            practiceTrial2 = true;
        }
    }

    public void SetForNextTrial_Formal()
    {
        Debug.Log(trialNum);
        if (trialNum < trialData.Count)
        {
            // 1.1 Save Data for the previous finished trial (trialNum-1)
            RecordTrialData(currentObject);
            // 1.2 restore the original set up
            RestoreInitialPosition(currentObject);


            // 2. For next Trial           
            // 2.1 record the initial position in the system (experimenter setting)
            currentObject = InstantiatePrefabByDistance(trialData[trialNum++]);
            initialPosition = currentObject.transform.position;
            initialRotation = currentObject.transform.rotation;

            // 2.2 adjust with buttons
            var ObjTransform = currentObject.transform;
            // assign original transform to the object of the current trial
            if (ObjTransform != null)
            {
                //buttonDisAdjust.Original_x = ObjTransform.localPosition.x;
                //buttonDisAdjust.Original_y = ObjTransform.localPosition.y;
                //buttonDisAdjust.Original_z = ObjTransform.localPosition.z;

                buttonDisAdjust.ObjectTransform = ObjTransform;
            }

        }
        else
        {
            // for the last trial
            RecordTrialData(currentObject);
            RestoreInitialPosition(currentObject);

            // change visioin condition!
            // show instructions on Canvas
            if (visionCondition == 0)
            {
                InstructionCanvasNextTrial.SetActive(false);
                InstructionCanvasVisionChange.SetActive(true);

                // reset for next vision condition
                visionCondition = 1;
                trialData = GlobalMethods.shuffle(trialData);
                for (int i = 0; i < trialData.Count; i++)
                    Debug.Log(trialData[i]);
                trialNum = 0;
                currentObject = null;
                DeactivateAllPrefabs();
            }
            // Task completed!
            // show instructions on Canvas
            else if (visionCondition == 1)
            {
                InstructionCanvasNextTrial.SetActive(false);
                InstructionCanvasFinish.SetActive(true);
            }


            // Play the audio
            audioSource.Play();
        }

    }


    private void RecordTrialData(GameObject currentObject)
    {
        // Save Data for the previous finished trial (trialNum-1)
        if (currentObject != null)
        {
            adjustedPosition = currentObject.transform.position;
            diff_x = adjustedPosition.x - initialPosition.x;
            diff_y = adjustedPosition.y - initialPosition.y;
            diff_z = adjustedPosition.z - initialPosition.z;
            dataManager.SaveToCSV(filename, DateTime.Now.ToString() + ','
                                            + visionCondition + ','
                                            + (trialNum - 1) + ','
                                            + trialData[trialNum - 1] + ','
                                            + initialRotation.ToString("f3") + ','
                                            + initialPosition.ToString("f3") + ','
                                            + adjustedPosition.ToString("f3") + ','
                                            + diff_x + ','
                                            + diff_y + ','
                                            + diff_z);
        }
    }
    private void RestoreInitialPosition(GameObject currentObject)
    {
        // Restore the original set up
        // Change the transform back to the original transform
        if (currentObject != null)
        {
            currentObject.transform.position = initialPosition;
            currentObject.transform.rotation = initialRotation;
        }
    }

    private GameObject InstantiatePrefabByDistance(float dis)
    {
        DeactivateAllPrefabs();

        GameObject selectedPrefab = GetPrefabByDistance(dis);

        if (selectedPrefab != null)
        {
            // Activate the selected prefab
            selectedPrefab.SetActive(true);
            return selectedPrefab;
        }
        else
        {
            Debug.LogError("Prefab not found for distance: " + dis);
            return null;
        }
    }

    private void DeactivateAllPrefabs()
    {
        prefab3.SetActive(false);
        prefab4.SetActive(false);
        prefab5.SetActive(false);
        prefab6.SetActive(false);
    }

    private GameObject GetPrefabByDistance(float dis)
    {
        switch (dis)
        {
            case 3:
                return prefab3;
            case 4:
                return prefab4;
            case 5:
                return prefab5;
            case 6:
                return prefab6;
            default:
                return null;
        }
    }


    public void ShowStandingPosition()
    {
        // 1. Disable the TapToPlace script on alignmentCube
        TapToPlace tapToPlaceScript = alignmentCube.GetComponent<TapToPlace>();
        if (tapToPlaceScript != null)
        {
            tapToPlaceScript.enabled = false;
        }

        // 2. Disable the renderer on alignmentCube
        Renderer alignmentCubeRenderer = alignmentCube.GetComponent<Renderer>();
        if (alignmentCubeRenderer != null)
        {
            alignmentCubeRenderer.enabled = false;
        }

        // 3. Show standingRepresentation and set its position and orientation the same as alignmentCube
        if (standingRepresentation != null)
        {
            standingRepresentation.SetActive(true);

            // Set position and orientation
            standingRepresentation.transform.position = alignmentCube.transform.position;
            standingRepresentation.transform.rotation = alignmentCube.transform.rotation;
        }
    }




    //public Transform GenerateGameObject_withDistance(Vector3 hitpoint, Quaternion hitRotation, GameObject objectToGenerate, float dis, Vector3 planeNormal, Vector3 forwardOffset)
    //{
    //    Vector3 pos = new Vector3(hitpoint.x, hitpoint.y, hitpoint.z) + dis * forwardOffset;
    //    // Vector3 pos = new Vector3(mainCamera.transform.position.x, hitpoint.y, mainCamera.transform.position.z) + dis * forwardOffset;
    //    // Vector3 pos = new Vector3(mainCamera.transform.position.x, hitpoint.y + objectToGenerate.transform.localScale.y, mainCamera.transform.position.z) + dis * forwardOffset;
    //    objectToGenerate.SetActive(true);
    //    objectToGenerate.transform.position = pos;
    //    objectToGenerate.transform.rotation = hitRotation; //Quaternion.identity;
    //    Debug.Log("Generate!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Soccer");
    //    return objectToGenerate.transform;
    //}


    public List<int> getTrialData() 
    { return trialData; }
}
