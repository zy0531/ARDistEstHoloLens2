using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using System.IO;
using System.Linq;

public class SendDataToGoogleForm : MonoBehaviour
{
    public string googleForm_URL { get; set; }
    public string participantID { get; set; }
    public string device { get; set; }

    [SerializeField]
    GenerateTrialObjButton generateTrialObjButton;

    List<int> trialData;
    List<int> trial;

    void Awake()
    {
        participantID = DateTime.Now.ToString();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        trialData = generateTrialObjButton.getTrialData();
        googleForm_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd22R6JWvfmS_9sNm8SYj-M2ZNdCLdtmBTNTAYtfJih11v6iA/formResponse";
        device = "HoloLens2";
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SendData2GoogleForm()
    {
        trial = new List<int>();
        for (int i = 0; i < trialData.Count; i++)
        {
            trial.Add(i);
        }
        
        string trial_combinedString = string.Join(",", trial.Select(x => x.ToString()));
        string trialData_combinedString = string.Join(",", trialData.Select(x => x.ToString()));

        StartCoroutine(Post(participantID, device, trial_combinedString, trialData_combinedString));
    }


    //post data to Google Form
    public IEnumerator Post(string participantID, string device, string trial, string trueDistance)
    {
        WWWForm form = new WWWForm();
        form.AddField("entry.2119550152", participantID);
        form.AddField("entry.849947158", device);
        form.AddField("entry.120963427", trial);
        form.AddField("entry.1912325077", trueDistance);

        UnityWebRequest www = UnityWebRequest.Post(googleForm_URL, form);
        yield return www.SendWebRequest();

        //if (www.isNetworkError)
        if (www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }
}
