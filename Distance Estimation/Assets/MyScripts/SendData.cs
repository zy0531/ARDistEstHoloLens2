using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;

public class SendData : MonoBehaviour
{
    public string googleForm_URL { get; set; }
    public string participantID { get; set; }
    public string device { get; set; }

    [SerializeField]
    GenerateTrialOrder generateTrialOrder;

    [SerializeField]
    TMP_Text inputID;

    List<int> trialData;

    // Start is called before the first frame update
    void Start()
    {
        trialData = generateTrialOrder.distanceTrial;
        googleForm_URL = "https://docs.google.com/forms/u/0/d/e/1FAIpQLSd22R6JWvfmS_9sNm8SYj-M2ZNdCLdtmBTNTAYtfJih11v6iA/formResponse";
        device = "HoloLens2";

        // send 2(practice trials) + 11(formal trials) responses to the google form for 1 participant
        //for (int i=0; i<trialData.Count; i++)
        //{
        //    StartCoroutine(Post(participantID, device, (i-1).ToString(), trialData[i].ToString()));
        //}

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetParticipantID(TMP_Text inputID)
    {
        participantID = inputID.text;
    }

    public void SendDataToGoogleForm()
    {
        // send 2(practice trials) + 11(formal trials) responses to the google form for 1 participant
        for (int i = 0; i < trialData.Count; i++)
        {
            StartCoroutine(Post(participantID, device, i.ToString(), trialData[i].ToString()));
        }
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
        if(www.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Form upload complete!");
        }
    }

}
