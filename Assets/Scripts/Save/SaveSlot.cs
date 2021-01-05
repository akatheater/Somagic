using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SaveSlot : MonoBehaviour
{
    //static SaveSlot instance;

    public SaveButton saveButtonPrefab;
    public GameObject saveGrid;


    public void CreateNewSave()
    {
        saveGrid = GameObject.Find("savegrid");
        SaveButton newButton = Instantiate(saveButtonPrefab, saveGrid.transform.position,Quaternion.identity);
        newButton.gameObject.transform.SetParent(saveGrid.transform);
        newButton.name = "savebutton" + (Timecontroller.today);
        newButton.buttonNum = Timecontroller.today;
        
    }

    public void DestroySaveBtn(int buttonNum)
    {
        saveGrid = GameObject.Find("savegrid");
        Transform[] tr = saveGrid.GetComponentsInChildren<Transform>();
        foreach(var button in tr)
        {
            if(button.name=="savebutton"+buttonNum)
            {
                Destroy(button.gameObject);
            }
        }
    }
}
