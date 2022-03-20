using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
