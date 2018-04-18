using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {
    Vector3 dist;
    float rotSpeed = 3;
    Vector3 startPos;
    float posX;
    float posZ;
    float posY;

    float speed = 1.5f;

    bool test = false;

    void Update() {
    }
    void OnMouseDown() {
        startPos = transform.position;
        dist = Camera.main.WorldToScreenPoint(transform.position);
        posX = Input.mousePosition.x - dist.x;
        posY = Input.mousePosition.y - dist.y;
        posZ = Input.mousePosition.z - dist.z;
    }

    void OnMouseDrag() {
        float Distance = Vector3.Distance(Camera.main.transform.position, this.transform.position);
        speed = Distance;
        //  print("Distance = " + Distance);
        //  print("speed = " + speed);

        float disX = Input.mousePosition.x - posX;
        float disY = Input.mousePosition.y - posY;
        float disZ = Input.mousePosition.z - posZ;
        Vector3 lastPos = Camera.main.ScreenToWorldPoint(new Vector3(disX, disY, disZ));

#if UNITY_EDITOR
        transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
#endif
        //If you have 1 finger on the screen, send a raycast and use it to move the object.
            if (Input.touchCount == 1) {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit)) {
                    transform.position = new Vector3(lastPos.x, startPos.y, lastPos.z);
                }
            }
        //If you have 2 fingers on the screen, you can rotate
        if (Input.touchCount == 2) {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;

            transform.RotateAround(Vector3.up, -rotX);
        }
    }
}