using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateNpcAsset : MonoBehaviour
{
    static public int commonNpcNum;
    public int specialNpcNum;

    //////////////【随机生成NPC的asset】
    static public void CreateCommonNpcAsset()
    {
        
        if (!System.IO.File.Exists("Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset"))
        {
            string path = "Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset";
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            RandomCommonInformation(scriptableObj);
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
            Timecontroller.commonNpcGo++;
        }
        else
        {
            Timecontroller.commonNpcGo++;
            string path = "Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset";
            //UnityEditor.AssetDatabase.DeleteAsset(path);
            // Debug.Log("delete!");
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            RandomCommonInformation(scriptableObj);
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
            
        }
        
    }

    //为每个commonnpc的asset赋值
    static public void RandomCommonInformation(NPC npc)
    {
        npc.NpcID = "common" + Timecontroller.commonNpcGo;
        npc.NpcLife = 5;
        npc.shadowNum = -1;

        //下面开始读json文件
        GameObject.Find("Gamesave").GetComponent<ReadInfJson>().ReadJsonData();
        npc.firstName = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetFirstName();
        npc.lastName= GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetLastName();
        npc.job= GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetJob();

    }

    void CreateSpecialNpcAsset()
    {

    }
    //////////////【随机生成NPC和NPC的asset】

    //////////////【固定流程生成NPC的asset】
    static public void CreateFixedNpcAsset()
    {
        if (!System.IO.File.Exists("Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset"))
        {
            string path = "Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset";
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            FixedInformation(scriptableObj);
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
            Timecontroller.commonNpcGo++;
        }
        else
        {
            Timecontroller.commonNpcGo++;
            string path = "Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset";
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            FixedInformation(scriptableObj);
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
        }
    }

    //为每个commonnpc的asset赋值
    static public void FixedInformation(NPC npc)
    {
        npc.NpcID = "common" + Timecontroller.commonNpcGo;     
        npc.shadowNum = -1;   
        if (Timecontroller.chapter==1)
        {
            npc.NpcLife = 2;
        }
        else if(Timecontroller.chapter==2)
        {
            npc.NpcLife = 6 - Timecontroller.today;
        }

        //下面开始读json文件
        GameObject.Find("Gamesave").GetComponent<ReadInfJson>().ReadJsonData();
        npc.firstName = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetFirstName();
        npc.lastName = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetLastName();
        npc.job = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetJob();

    }

    static public void CreateFirstNpcAsset()
    {
        if (!System.IO.File.Exists("Assets/Resources/NPC/common0.asset"))
        {
            string path = "Assets/Resources/NPC/common0.asset";
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            FirstInformation(scriptableObj);  
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
            Timecontroller.commonNpcGo++;
        }
        else
        {
            //有问题
            Timecontroller.commonNpcGo++;
            string path = "Assets/Resources/NPC/common" + Timecontroller.commonNpcGo + ".asset";
            NPC scriptableObj = ScriptableObject.CreateInstance<NPC>();
            //从此commonnpc拥有了独特的asset
            FirstInformation(scriptableObj);
            UnityEditor.AssetDatabase.CreateAsset(scriptableObj, path);
            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
            Timecontroller.commonNpcCount++;
        }
    }

    //为每个commonnpc的asset赋值
    static public void FirstInformation(NPC npc)
    {
        npc.NpcID = "common" + Timecontroller.commonNpcGo;
        npc.shadowNum = -1;
        npc.NpcLife = 1;
        
        //下面开始读json文件
        GameObject.Find("Gamesave").GetComponent<ReadInfJson>().ReadJsonData();
        npc.firstName = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetFirstName();
        npc.lastName = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetLastName();
        npc.job = GameObject.Find("Gamesave").GetComponent<ReadInfJson>().GetJob();

    }
    //////////////【固定流程生成NPC和NPC的asset】

}
