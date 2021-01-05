using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;
using UnityEditor;

public class SaveController : MonoBehaviour
{
    public int saveAmount;
    public int saveCurrent;

    SaveButton savebtn;

    public GameObject cb;


    private void Start()
    {
        if (!Directory.Exists(Application.persistentDataPath + "/SaveData/Day0"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/Day0");
        }
    }

    //摁下ok按钮
    public void AutoSave()
    {
        //生成存档按钮
        GameObject.Find("saveslot").GetComponent<SaveSlot>().CreateNewSave();

        //////////////【以下均为随机选择房间+随机生成npc】
        ////Debug.Log("after save before create:count="+ Timecontroller.commonNpcCount);
        ////开始选房间
        //GameObject.Find("Gametime").GetComponent<Timecontroller>().RandomChooseRoom();
        //int tmpCube = GameObject.Find("Gametime").GetComponent<Timecontroller>().getTmpInt();
        ////Debug.Log("choose cube " + tmpCube);
        //GameObject tmpGo = GameObject.Find("Gametime").GetComponent<Timecontroller>().getTmpGo();
        //Debug.Log("choose cube " + tmpCube + "   choose room " + tmpGo.name);
        ////存档前就生成！
        //GameObject[] obs = GameObject.FindGameObjectsWithTag("cube");
        //foreach (var cube in obs)
        //{
        //    cube.GetComponent<raythree>().RandomCreateNpc("Cube" + tmpCube.ToString(), tmpGo.name);
        //}
        //////////////【以上均为随机选择房间+随机生成npc】

        //////////////【以下均为固定流程生成npc】
        GameObject.Find("Gametime").GetComponent<Timecontroller>().GoByChapter();
        //////////////【以上均为固定流程生成npc】


        //存档路径
        int today = Timecontroller.today;
        string pathDirectory = Application.persistentDataPath + "/SaveData/Day" + today.ToString();
        string pathCube = pathDirectory + "/CubeData.xml";
        string pathNpc = pathDirectory + "/NpcData.xml";
        string pathInformation = pathDirectory + "/InformationData.txt";
        string pathAnalysis = pathDirectory + "/AnalysisData.txt";
        if (!Directory.Exists(pathDirectory))
        {
            Directory.CreateDirectory(pathDirectory);
            
        }

        //真正的存档过程
        GameObject[] go = GameObject.FindGameObjectsWithTag("npc");
        foreach (var npc in go)
        {
            npc.GetComponent<NPCController>().DecreaseLife();          
        }

        cb.GetComponent<Cube>().SimpleSaveCube(pathCube);
        cb.GetComponent<Cube>().SimpleSaveNpcs(pathNpc);
        this.GetComponent<SaveLogs>().SaveInformation(pathInformation);
        this.GetComponent<SaveLogs>().SaveAnalysis(pathAnalysis);
        //Debug.Log("autosave: today="+Timecontroller.today);
        GameObject[] go2 = GameObject.FindGameObjectsWithTag("npc");
        foreach (var npc in go2)
        {
           
            Debug.Log("已存，Day" + today.ToString()+"   npc=" + npc.name + "  life=" + npc.GetComponent<NPCController>().GetNpcLife());
        }

        //时间流逝
        Timecontroller.today++;
        if (today == 4)
            Timecontroller.chapter = 2;
        
        //UI恢复
        Cube.step = 0;
        GameObject.Find("Canvas").GetComponent<GameUIController>().ChangeStepUI();
        Cube.islocked = false;
        GameObject.Find("Gametime").GetComponent<Timecontroller>().tmpString = "none";
        
    }

    public void ResetToday()
    {
        int resetday = Timecontroller.today-1;
        string pathDirectory = Application.persistentDataPath + "/SaveData/Day" + resetday.ToString();
        string pathCube = pathDirectory + "/CubeData.xml";
        string pathNpc = pathDirectory + "/NpcData.xml";
        string pathInformation = pathDirectory + "/InformationData.txt";
        string pathAnalysis = pathDirectory + "/AnalysisData.txt";
        //Debug.Log("load:" + pathDirectory);
        cb.GetComponent<Cube>().SimpleLoadCube(pathCube);
        cb.GetComponent<Cube>().SimpleLoadNpcs(pathNpc);
        this.GetComponent<SaveLogs>().LoadInformation(pathInformation);
        this.GetComponent<SaveLogs>().LoadAnalysis(pathAnalysis);

        GameObject[] go = GameObject.FindGameObjectsWithTag("npc");
        foreach (var npc in go)
        {
            Debug.Log("load by /SaveData/Day" + resetday.ToString()+"  npclife:" + npc.GetComponent<NPCController>().GetNpcLife());
        }


        Cube.step = 0;
        //GameObject.Find("Gametime").GetComponent<Timecontroller>().ChangeStepUI();
        GameObject.Find("Canvas").GetComponent<GameUIController>().ChangeStepUI();
        Cube.islocked = false;
    }

    public void LoadBySaveButton(int buttonNum)
    {
        Timecontroller.today = buttonNum;
        string pathDirectory = Application.persistentDataPath + "/SaveData/Day" + (buttonNum-1).ToString();
        string pathCube = pathDirectory + "/CubeData.xml";
        string pathNpc = pathDirectory + "/NpcData.xml";
        string pathInformation = pathDirectory + "/InformationData.txt";
        string pathAnalysis = pathDirectory + "/AnalysisData.txt";
        //Debug.Log("load:" + pathDirectory);

        cb.GetComponent<Cube>().SimpleLoadCube(pathCube);
        cb.GetComponent<Cube>().SimpleLoadNpcs(pathNpc);
        this.GetComponent<SaveLogs>().LoadInformation(pathInformation);
        this.GetComponent<SaveLogs>().LoadAnalysis(pathAnalysis);

        GameObject[] go = GameObject.FindGameObjectsWithTag("npc");
        foreach (var npc in go)
        {
            //Debug.Log(npc.transform.parent.transform.parent.name+"  "+npc.transform.parent.name);
            //Debug.Log("btn"+buttonNum+"  load by day"+ (buttonNum - 1).ToString()+"  today:"+Timecontroller.today+"  "+npc.name+" life:"+npc.GetComponent<NPCController>().GetNpcLife());
        }

        Cube.step = 0;
        //GameObject.Find("Gametime").GetComponent<Timecontroller>().ChangeStepUI();
        GameObject.Find("Canvas").GetComponent<GameUIController>().ChangeStepUI();
        Cube.islocked = false;
    }

    public void DestroySaveFile(int buttonNum)
    {
        string pathDirectory = Application.persistentDataPath + "/SaveData/Day" + buttonNum.ToString();
        string pathCube = pathDirectory + "/CubeData.xml";
        string pathNpc = pathDirectory + "/NpcData.xml";
        string pathInformation = pathDirectory + "/InformationData.txt";
        string pathAnalysis = pathDirectory + "/AnalysisData.txt";

        File.Delete(pathCube);
        File.Delete(pathNpc);
        File.Delete(pathInformation);
        File.Delete(pathAnalysis);
        Directory.Delete(pathDirectory);


    }
}
