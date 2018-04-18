using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class TestScript : MonoBehaviour {
  /*  void Update() {
        if (Input.GetMouseButton(0)) {

            //Base variables for this Raycast.
            Vector3 hitPoint = Vector3.zero;
            Ray rotationRay = Camera.main.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hitInfo;

            //Debug raycast for in the inspector. :3
            Debug.DrawRay(rotationRay.origin, rotationRay.direction, Color.red);

            //Cast a ray to find our attempted selection. Ignores the ignored layer which is defined in the TangoMarkerHandler class.
            if (Physics.Raycast(rotationRay, out hitInfo, Mathf.Infinity)) {

                //Sets the position of the hitPoint (which will be our position) to the point that the ray hits.
                hitPoint = hitInfo.point;

            }

            //Makes the selected model aim towards the hitPoint and then sets the rotations that aren't vertical to 0 so it only rotates on the horizontal axis.
            Vector3 temp = new Vector3(hitPoint.x, ObjectController.SelectedObject.transform.position.y, hitPoint.z);
            ObjectController.SelectedObject.transform.position = temp;
            // GetComponent<TangoMarkerHandler>().Select(GetComponent<TangoMarkerHandler>().currentlySelectedModel);

        }
    } */
}
