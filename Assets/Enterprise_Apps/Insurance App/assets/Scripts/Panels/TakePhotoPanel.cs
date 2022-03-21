using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TakePhotoPanel : MonoBehaviour, IPanel
{
    public RawImage photoTaken;
    public InputField photoNotes;
    public Text caseNumberTitle;
    public OverviewPanel OverviewPanel;

    private string _imagePath;

    public void OnEnable()
    {
        caseNumberTitle.text = "CASE NUMBER " + UIManager.Instance.activeCase.caseID;
    }

    public void TakePictureButton()
    {
		TakePicture(512);
    }

    public void ProcessInfo()
    {
        if (string.IsNullOrEmpty(photoNotes.text) == false)
        {
            UIManager.Instance.activeCase.photoNotes = photoNotes.text;
        }

        byte[] imgData = null;

        if (string.IsNullOrEmpty(_imagePath) == false)
        {
            Texture2D img = NativeCamera.LoadImageAtPath(_imagePath, 512, false);
            imgData = img.EncodeToPNG();
        }


        UIManager.Instance.activeCase.photoTaken = imgData;
        UIManager.Instance.activeCase.photoNotes = photoNotes.text;

        OverviewPanel.gameObject.SetActive(true);
    }

	private void TakePicture(int maxSize)
	{
		NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
		{
			Debug.Log("Image path: " + path);
			if (path != null)
			{
				// Create a Texture2D from the captured image
				Texture2D texture = NativeCamera.LoadImageAtPath(path, maxSize, false);
				if (texture == null)
				{
					Debug.Log("Couldn't load texture from " + path);
					return;
				}

                photoTaken.texture = texture;
                photoTaken.gameObject.SetActive(true);
                _imagePath = path;

			}
		}, maxSize);

		Debug.Log("Permission result: " + permission);
	}
}
