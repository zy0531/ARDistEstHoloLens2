using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class DataManager : MonoBehaviour
{


    public void SaveToCSV(string fileName, string csvData)
    {
        string filePath = Path.Combine(Application.persistentDataPath, fileName);

        try
        {
            // Check if the file already exists
            if (File.Exists(filePath))
            {
                // Append new data to the existing file
                using (StreamWriter writer = File.AppendText(filePath))
                {
                    writer.WriteLine(csvData);
                }
            }
            else
            {
                // Create a new file and write the data
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(csvData);
                }
            }

            Debug.Log($"CSV file saved to: {filePath}");
        }
        catch (Exception e)
        {
            Debug.LogError($"Error saving CSV file: {e.Message}");
        }
    }
}
