using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using TMPro;

public class TriggerTrials : MonoBehaviour
{
    [SerializeField]
    GenerateTrialOrder generateTrialOrder;

    [SerializeField]
    ObjectPlacement objectPlacement;

    [SerializeField]
    GameObject mainCamera;

    [SerializeField]
    TMP_Text trialText;

    [SerializeField]
    [Tooltip("Set a gameObject to be instantiate for distance estimation")]
    GameObject m_gameObjectPrefab;

    [SerializeField]
    AudioSource audioNextTrial;

    [SerializeField]
    AudioSource audioReadyToWalk;

    public bool isSetUp { get; set; } // "True" when type in participant ID and send data

    InputDevice device;
    bool isControllerFound;

    bool buttonDown_XRInput;
    bool menuValue;

    List<int> trialData;
    int trialNum;

    bool isObjectPresent;
    GameObject gameObjectSoccer;

    // Start is called before the first frame update
    void Start()
    {
        trialData = generateTrialOrder.distanceTrial;
        trialNum = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetForNextTrial()
    {  
        if (isSetUp)
        {
            // if the gameobject is absent -> instantiate a new gameObject for next trial
            if (trialNum < trialData.Count && !isObjectPresent)
            {
                audioNextTrial.Play();
                trialText.text = "Trial#: " + trialNum.ToString() + "\n" + "Say \"Ready For Walking\" if you believe you have identified the virtual object's location!";
                trialText.text.Replace("\\n", "\n");
                gameObjectSoccer = GenerateGameObject_withDistance(objectPlacement.hitPoint, m_gameObjectPrefab, trialData[trialNum], objectPlacement.hitNormal, objectPlacement.forwardDirection);
                isObjectPresent = true;
            }
            else
            {
                trialText.text = "You have finished all trials! Congrats!";

            }
        }
        
    }

    public void SetForBlindWalking()
    {
        if (isSetUp)
        {
            // if the gameobject is present -> destroy the current gameObject
            if (isObjectPresent)
            {
                audioReadyToWalk.Play();
                trialText.text = "Now, with your eyes closed, walk towards the virtual object you observed! ";
                trialText.text.Replace("\\n", "\n");
                DestroyGameObject(gameObjectSoccer);
                isObjectPresent = false;
                trialNum++;
            }
        }
    }


    GameObject GenerateGameObject_withDistance(Vector3 hitpoint, GameObject objectToGenerate, float dis, Vector3 planeNormal, Vector3 forwardOffset)
    {
        float yOffset = 0.11f; // radius of a size 5 soccer ball
        // Vector3 forwardOffset = Vector3.ProjectOnPlane(mainCamera.transform.forward, planeNormal);
        Vector3 pos = new Vector3(mainCamera.transform.position.x, hitpoint.y + yOffset, mainCamera.transform.position.z) + dis * forwardOffset;
        GameObject gameobject = Instantiate(objectToGenerate, pos, Quaternion.identity);
        Debug.Log("Generate!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!Soccer");
        return gameobject;
    }

    void DestroyGameObject(GameObject objectToDestroy)
    {
        Destroy(objectToDestroy);
    }


    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
