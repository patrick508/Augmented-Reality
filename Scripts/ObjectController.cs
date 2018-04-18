using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectController : MonoBehaviour {
    private TangoPointCloud m_pointCloud;

    public static GameObject ActiveObject;    //ActiveObject is the object beeing placed down on click/touch. This may be changed through other scripts.
    public static GameObject SelectedObject;  
    public GameObject Marker;
    bool MarkerCanChange = false;             

    float rotSpeed = 3; //Speed used to rotate the selected object.

    public static int PlacePoints = 0; //the amount of objects the user is able to place. This may be changed through other scripts.

    public GameObject BelliceButtons;
    public GameObject CaruzzoButtons;
    public GameObject PalloneButtons;

    void Start() {
        m_pointCloud = FindObjectOfType<TangoPointCloud>();
    }

    void FixedUpdate() {
        MarkerManager();
    }
    void Update() {
        print("ActiveObject = " + ActiveObject);
        if (Input.touchCount == 1)
        {
            HandleTouch();
        }
#if UNITY_EDITOR
        //If i click & have a point to place, cast a ray and only place ActiveObject when i'm not clicking on UI or another Object
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
                if (EventSystem.current.currentSelectedGameObject == null) {
                    if (Physics.Raycast(ray, out hit)) {
                        //If i hit an object with any other tag than EnviroRoom(the plane) or the marker, change selectedobject to it and put markercanchange on true
                        if (hit.transform.tag != "EnviroRoom" && hit.transform.tag != "Marker" && hit.transform.tag != "Shadow") {
                            SelectedObject = hit.collider.gameObject;
                            ActiveObject = SelectedObject;
                            MarkerCanChange = true;
                        }
                        if (hit.transform.tag == "EnviroRoom")
                        if (PlacePoints == 1) {
                            print("Ik ga hem plaatsen!");
                            Instantiate(ActiveObject, hit.point, Quaternion.LookRotation(hit.normal));
                            PlacePoints--;
                    }
                }
            }
        }
         if (Input.GetButton("Fire2")) {
                 float rotSpeed = 10;
                 float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
                 SelectedObject.transform.RotateAround (Vector3.up * rotSpeed, -rotX);
         }
#endif
        //If Active MaterialButtons dont match the ones selected object needs, change it.
            if (UIManager.MaterialButtons.gameObject.tag != SelectedObject.gameObject.tag) {
                if (SelectedObject.gameObject.tag == "Bellice") {
                    UIManager.MaterialButtons = BelliceButtons;
                }
                if (SelectedObject.gameObject.tag == "Caruzzo") {
                    UIManager.MaterialButtons = CaruzzoButtons;
                }
                if (SelectedObject.gameObject.tag == "Pallone") {
                    UIManager.MaterialButtons = PalloneButtons;
            }
        }
    }

    private void HandleTouch()
    {
        Touch t = Input.GetTouch(0);
        //Cast a raycast from touch position to check if you hit any gameobject and change selectedobject if i click on anything but the marker.
        Ray ray = Camera.main.ScreenPointToRay(t.position);
        RaycastHit hit;
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag != "Marker" && hit.transform.tag != "Shadow")
                {
                    SelectedObject = hit.collider.gameObject;
                    MarkerCanChange = true;
                }
                //Only place a new gameobject if there is no other object at location. && place ActiveObject function when single touch ended.
            }
            if (Physics.Raycast(ray, out hit, 100) == false)
            {
                if (t.phase == TouchPhase.Ended)
                {
                    if (PlacePoints == 1)
                    {
                        PlaceObject(t.position);
                        PlacePoints--;
                    }
                }
            }
        }
    }

    //Enable/disable marker and change it's position to selectedobject
    void MarkerManager() {
            if (SelectedObject != null) {
                Marker.SetActive(true);
        } else if (SelectedObject == null) {
            Marker.SetActive(false);
        } if (MarkerCanChange == true) {
            Vector3 SelectedObjPos = new Vector3(SelectedObject.transform.position.x, SelectedObject.transform.position.y, SelectedObject.transform.position.z);
            Marker.transform.position = SelectedObjPos;
            MarkerCanChange = false;
        }
    }

    #region Instantiate Function 
    void PlaceObject(Vector2 touchPosition) {
        // Find the plane.
        Camera cam = Camera.main;
        Vector3 planeCenter;
        Plane plane;

        if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane)) {
            Debug.Log("cannot find plane.");
            return;
        }
        else {
            print("i found the plane");
        }

        // Place ActiveObject on the surface, and make it face the camera.
        if (Vector3.Angle(plane.normal, Vector3.up) < 30.0f) {
            Vector3 up = plane.normal;
            Vector3 right = Vector3.Cross(plane.normal, cam.transform.forward).normalized;
            Vector3 forward = Vector3.Cross(right, plane.normal).normalized;
            Instantiate(ActiveObject, planeCenter, Quaternion.LookRotation(forward, up));
        }
        else {
            Debug.Log("surface is too steep for object to stand on.");
        }
    }

    #endregion
    public void DeleteSelected() {
        Destroy(SelectedObject);
    }
}