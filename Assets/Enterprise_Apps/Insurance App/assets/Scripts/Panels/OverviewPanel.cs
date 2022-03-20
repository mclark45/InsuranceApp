using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class OverviewPanel : MonoBehaviour, IPanel
{
    public Text caseNumberTitle;
    public Text nameTitle;
    public Text dateTitle;
    public Text locationNotes;
    public RawImage photoTaken;
    public Text photoNotes;

    public void OnEnable()
    {
        caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
        nameTitle.text = UIManager.Instance.activeCase.name;
        dateTitle.text = DateTime.Today.ToString();
        locationNotes.text = "LOCATION NOTES: \n" + UIManager.Instance.activeCase.locationNotes;
        photoTaken.texture = UIManager.Instance.activeCase.photoTaken;
        photoNotes.text = UIManager.Instance.activeCase.photoNotes;

    }

    public void ProcessInfo()
    {

    }
}
