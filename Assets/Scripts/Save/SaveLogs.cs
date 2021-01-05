using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;

public class SaveLogs : MonoBehaviour
{
    public NpcLogs myNpcInformation;
    public NpcLogs myNpcAnalysis;

    private void Start()
    {
        myNpcInformation = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcInformation.asset", typeof(NpcLogs));
        myNpcAnalysis = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcAnalysis.asset", typeof(NpcLogs));

        if (!Directory.Exists(Application.persistentDataPath + "/SaveData/Day0"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/Day0");           
        }
        SaveInformation(Application.persistentDataPath + "/SaveData/Day0"+"/InformationData.txt");
        SaveAnalysis(Application.persistentDataPath + "/SaveData/Day0"+"/AnalysisData.txt");
    }

    public void SaveInformation(string savepath)
    {     
        BinaryFormatter npcInformationFormatter = new BinaryFormatter();
       // FileStream npcInformationfile = File.Create(Application.persistentDataPath + "/SaveData/myNpcInformation.txt");
        FileStream npcInformationfile = File.Create(savepath);
        var npcInformationjson = JsonUtility.ToJson(myNpcInformation);
        npcInformationFormatter.Serialize(npcInformationfile, npcInformationjson);
        npcInformationfile.Close();      
    }

    public void SaveAnalysis(string savepath)
    {
        BinaryFormatter npcAnalysisformatter = new BinaryFormatter();
        //FileStream npcAnalysisfile = File.Create(Application.persistentDataPath + "/SaveData/myNpcAnalysis.txt");
        FileStream npcAnalysisfile = File.Create(savepath);
        var npcAnalysisjson = JsonUtility.ToJson(myNpcAnalysis);
        npcAnalysisformatter.Serialize(npcAnalysisfile, npcAnalysisjson);
        npcAnalysisfile.Close();
    }

    public void LoadInformation(string savepath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        if(File.Exists(savepath))
        {
            FileStream npcInformationfile = File.Open(savepath, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(npcInformationfile), myNpcInformation);
            npcInformationfile.Close();
        }     
    }

    public void LoadAnalysis(string savepath)
    {
        BinaryFormatter bf2 = new BinaryFormatter();
        if (File.Exists(savepath))
        {
            FileStream npcAnalysisfile = File.Open(savepath, FileMode.Open);
            JsonUtility.FromJsonOverwrite((string)bf2.Deserialize(npcAnalysisfile), myNpcAnalysis);
            npcAnalysisfile.Close();
        }
    }

  
}
