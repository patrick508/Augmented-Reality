using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class ObjectToPlace : MonoBehaviour {
    public GameObject MyFurnitureObject;
    public GameObject MyMaterialList;
    public string MyFurnitureLocal; //Used later to check from another script which tag belongs to this object.
    public static string MyFurnitureGlobal; //String used to check which objects belongs to this button

    GameObject NewObjectPlaced; // New instantiated object

    //Replace the ActiveObject of PlaceObjectController to myfurnitureobject
    public void SwitchToMyObject() {
        if (ObjectController.PlacePoints < 1) {
            ObjectController.PlacePoints++;
        }
         ObjectController.ActiveObject = MyFurnitureObject;
      //   MyFurnitureGlobal = MyFurnitureLocal;
     //   FurnitureManager.RunBundle = true;
      //  FurnitureManager.GetFurnitureManager().CheckAssetBundle();
    }

    public void SwitchToMyMaterial() {
            ObjectController.ActiveObject = MyFurnitureObject;
        //if i have 0 points and active object has the same tag as selected object. Replace it with the active object with the same rotation and position. (for material effect)
           if (ObjectController.PlacePoints <= 0) {
            if (ObjectController.ActiveObject.gameObject.tag == ObjectController.SelectedObject.gameObject.tag) {
                Vector3 SavedSelectedPos = ObjectController.SelectedObject.transform.position;
                Quaternion SavedSelectRot = ObjectController.SelectedObject.transform.rotation;
                Destroy(ObjectController.SelectedObject);
                NewObjectPlaced = (GameObject)Instantiate(ObjectController.ActiveObject, SavedSelectedPos, SavedSelectRot);
                ObjectController.SelectedObject = NewObjectPlaced;
            }
        } 
    }

    //Replace MaterialList of UIManager to mymateriallist
    public void SwitchMaterialList() {
        UIManager.MaterialButtons = MyMaterialList;
    }
}