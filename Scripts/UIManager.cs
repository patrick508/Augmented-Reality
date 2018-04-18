using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public GameObject FurnitureButtons; // all the furnitureButtons
    public static GameObject MaterialButtons; // all the materialButtons

    bool ChangetoItemList = false; // Reads if you clicked the item button
    bool ChangetoMaterialList = false; // Reads if you clicked the material button

    public int MenuLevels = 1; // Int used for defining in which case the switch statement is.

    void Start() {
    }
    // Update is called once per frame
    void Update() {
        MenuSwitch();
    }

    //Switch statement for managing the furniture menu
    void MenuSwitch() {
        switch (MenuLevels) {
            //This is the Idle state, the switch statements always starts out in Case 1
            case 1:
                if (MaterialButtons != null) {
                    MaterialButtons.SetActive(false);
                }
                FurnitureButtons.SetActive(false);
                break;
            //In this case all of the different furniture pop-up and you can choose which one you want to place
            case 2:
                FurnitureButtons.SetActive(true);
                ChangetoMaterialList = false;
                if (MaterialButtons != null) {
                    MaterialButtons.SetActive(false);
                }
                if (ChangetoItemList == false) {
                    MenuLevels = 1;
                }
                break;
            //In this case all of the different materials pop-up and you can choose which one you want to place
            case 3:
                FurnitureButtons.SetActive(false);
                MaterialButtons.SetActive(true);
                ChangetoItemList = false;
                if (ChangetoMaterialList == false) {
                    MenuLevels = 1;
                }
                break;
            //Case if nothing is picked
            default:
                break;
        }
    }

    //Change ChangeItemList bool to true or false, depending on what it is right now.
    public void ClickFurnitureButton() {
        ChangetoItemList = !ChangetoItemList;
        MenuLevels = 2;
    }

    //Change ChangeMaterialList bool to true or false, depending on what it is right now.
    public void ClickMaterialButton() {
            ChangetoMaterialList = !ChangetoMaterialList;       
            MenuLevels = 3;
    }

    // If you click on a item, set bool to false so menu dissapears again (so user doesnt have to click menu item again)
    public void ClickItem() {
        ChangetoItemList = false;
    }
    // If you click on a material, set bool to false so menu dissapears again (so user doesnt have to click menu item again)
    public void ClickMaterial() {
        ChangetoMaterialList = false;
    }
}