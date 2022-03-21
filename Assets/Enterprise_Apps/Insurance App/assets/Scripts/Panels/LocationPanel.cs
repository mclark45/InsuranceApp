using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LocationPanel : MonoBehaviour, IPanel
{
    public RawImage mapImg;
    public InputField mapNotes;
    public Text caseNumberTitle;
    public TakePhotoPanel TakePhotoPanel;

    public string apiKey;
    public float xCord, yCord;
    public int zoom;
    public int imgSize;
    public string url = "https://maps.googleapis.com/maps/api/staticmap?";

    public void OnEnable()
    {
        caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
    }

    public IEnumerator Start()
    {
        //Fetch GEO Data
        if (Input.location.isEnabledByUser == true)
        {
            Input.location.Start();

            int maxWait = 20;
            while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
            {
                yield return new WaitForSeconds(1.0f);
                maxWait--;
            }

            if (maxWait < 1)
            {
                Debug.Log("Timed Out");
                yield break;
            }

            if (Input.location.status == LocationServiceStatus.Failed)
            {
                Debug.Log("Unable to determine device location...");
            }
            else
            {
                xCord = Input.location.lastData.latitude;
                yCord = Input.location.lastData.longitude;
            }

            Input.location.Stop();
        }
        else
        {
            Debug.Log("Location Services are not Enabled");
        }

        url = url + "center=" + xCord + "," + yCord + "&zoom=" + zoom + "&size=" + imgSize + "x" + imgSize + "&key=" + apiKey;

        StartCoroutine(GetLocationRoutine(url));
    }


    IEnumerator GetLocationRoutine(string url)
    {
        //construct appropriate url

        var www = UnityWebRequestTexture.GetTexture(url);
        yield return www.SendWebRequest();

        mapImg.texture = DownloadHandlerTexture.GetContent(www);


        /**using (WWW map = new WWW(url))
        {
            yield return map;

            if (map.error != null)
            {
                Debug.LogError("Map Error: " + map.error);
            }

            mapImg.texture = map.texture;
        }**/
    }

    public void ProcessInfo()
    {
        if (string.IsNullOrEmpty(mapNotes.text) == false)
        {
            UIManager.Instance.activeCase.locationNotes = mapNotes.text;
        }

        TakePhotoPanel.gameObject.SetActive(true);
    }
}
