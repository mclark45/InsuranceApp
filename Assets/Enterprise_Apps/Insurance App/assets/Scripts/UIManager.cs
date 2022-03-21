using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("The UI Manager is Null");

            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }

    public Case activeCase;
    public ClientInfoPanel clientInfoPanel;
    public GameObject borderPanel;

    public void CreateNewCase()
    {
        activeCase = new Case();

        activeCase.caseID = (Random.Range(0, 1000).ToString());

        clientInfoPanel.gameObject.SetActive(true);
        borderPanel.SetActive(true);
    }

    public void SubmitButton()
    {
        //create a new case to save
        Case awsCase = new Case
        {
            //populate the case data
            caseID = activeCase.caseID,
            name = activeCase.name,
            date = activeCase.date,
            locationNotes = activeCase.locationNotes,
            photoTaken = activeCase.photoTaken,
            photoNotes = activeCase.photoNotes
        };

        //open a data stream to turn that object (case) into a file
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = Application.persistentDataPath + "/case#" + awsCase.caseID + ".dat";
        FileStream file = File.Create(filePath);
        bf.Serialize(file, awsCase);
        file.Close();

        Debug.Log("Application Data Path: " + Application.persistentDataPath);

        //begin AWS process
        AWSManager.Instance.UploadToS3(filePath, awsCase.caseID);

    }
}
