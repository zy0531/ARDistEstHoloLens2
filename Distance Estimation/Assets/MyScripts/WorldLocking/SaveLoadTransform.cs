using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SaveLoadTransform : MonoBehaviour
{
    private string transformKey = "CubeTransform";


    // Load the player transform when starting the game
    void Start()
    {
        LoadPlayerTransform();
    }

    // Load the player transform
    private void LoadPlayerTransform()
    {
        if (PlayerPrefs.HasKey(transformKey + "_PosX"))
        {
            float posX = PlayerPrefs.GetFloat(transformKey + "_PosX");
            float posY = PlayerPrefs.GetFloat(transformKey + "_PosY");
            float posZ = PlayerPrefs.GetFloat(transformKey + "_PosZ");

            float rotX = PlayerPrefs.GetFloat(transformKey + "_RotX");
            float rotY = PlayerPrefs.GetFloat(transformKey + "_RotY");
            float rotZ = PlayerPrefs.GetFloat(transformKey + "_RotZ");
            float rotW = PlayerPrefs.GetFloat(transformKey + "_RotW");

            transform.position = new Vector3(posX, posY, posZ);
            transform.rotation = new Quaternion(rotX, rotY, rotZ, rotW);
        }
    }


    // Interface: Save the cube transform
    public void saveCubeTransform()
    {
        SavePlayerTransform();
    }


    // Save the player transform when leaving the game
    void OnApplicationQuit()
    {
        SavePlayerTransform();
    }

    // Save the player transform when the script is disabled
    void OnDisable()
    {
        SavePlayerTransform();
    }

    // Save the player transform
    private void SavePlayerTransform()
    {
        PlayerPrefs.SetFloat(transformKey + "_PosX", transform.position.x);
        PlayerPrefs.SetFloat(transformKey + "_PosY", transform.position.y);
        PlayerPrefs.SetFloat(transformKey + "_PosZ", transform.position.z);

        PlayerPrefs.SetFloat(transformKey + "_RotX", transform.rotation.x);
        PlayerPrefs.SetFloat(transformKey + "_RotY", transform.rotation.y);
        PlayerPrefs.SetFloat(transformKey + "_RotZ", transform.rotation.z);
        PlayerPrefs.SetFloat(transformKey + "_RotW", transform.rotation.w);

        PlayerPrefs.Save();
    }
}
