using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveButton : MonoBehaviour
{
    public int buttonNum;

    public void ClickSaveBtn()
    {
        GameObject.Find("Gamesave").GetComponent<SaveController>().LoadBySaveButton(buttonNum);

        //!!!!!!!要改！
        //GameObject.Find("anaboss").SetActive(false);
        string pathDirectory = Application.persistentDataPath + "/SaveData/Day";      
        for (int i=buttonNum;i<=20;i++)
        {
            if (Directory.Exists(pathDirectory+i.ToString()))
            {
                //Debug.Log("destroybutton:"+ i);
                GameObject.Find("Gamesave").GetComponent<SaveController>().DestroySaveFile(i);
                GameObject.Find("saveslot").GetComponent<SaveSlot>().DestroySaveBtn(i);
            }
        }
    }
}
