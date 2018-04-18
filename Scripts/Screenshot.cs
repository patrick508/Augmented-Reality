using UnityEngine;
using System.Collections;

public class Screenshot : MonoBehaviour {
    // Use this for initialization
    CaptureAndSave snapShot;
    GameObject LastSelected;

    void Start () {
        snapShot = GameObject.FindObjectOfType<CaptureAndSave>();
    }

    public void MakeScreenshot() {

        StartCoroutine(CaptureScreen());
    }

    public IEnumerator CaptureScreen() {
        // disable SelectedObject and the canvas.
        yield return null;
        LastSelected = ObjectController.SelectedObject;
        ObjectController.SelectedObject = null;
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;

        yield return new WaitForSeconds(0.1f);
        snapShot.CaptureAndSaveToAlbum(ImageType.JPG);
        //ScreenEffect.SetActive(false);
        print("Shot taken!");
        yield return new WaitForSeconds(0.1f);
        ObjectController.SelectedObject = LastSelected;
        // Show UI after we're done
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }
}
