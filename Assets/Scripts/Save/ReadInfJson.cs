using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;

public class ReadInfJson : MonoBehaviour
{
    public InfList infList;
    public NPC inf;

    public void ReadJsonData()
    {
        UnityEngine.TextAsset t = Resources.Load("Json/comInf") as TextAsset;
        string tmp = t.text;
        JsonData js = JsonMapper.ToObject(tmp);

        for (int i = 0; i < js.Count; i++)
        {
            //NPC tmpInf = new NPC();
            NPC tmpInf=ScriptableObject.CreateInstance<NPC>();
            tmpInf.firstName = (string)js[i]["firstName"];
            tmpInf.lastName = (string)js[i]["lastName"];
            tmpInf.job = js[i]["job"].ToString();
            tmpInf.reason = (string)js[i]["reason"];
            tmpInf.iniMood = (int)js[i]["iniMood"];
            tmpInf.nature = (int)js[i]["nature"];
            tmpInf.dialog1 = (string)js[i]["dialog1"];
            tmpInf.dialog2 = (string)js[i]["dialog2"];
            tmpInf.dialog3 = (string)js[i]["dialog3"];
            tmpInf.dialog4 = (string)js[i]["dialog4"];
            tmpInf.answer1_1 = (string)js[i]["answer1_1"];
            tmpInf.answer1_2 = (string)js[i]["answer1_2"];
            infList.npcList.Add(tmpInf);
        }
    }

    public void ShowJsonData(InfList inflist)
    {
        Debug.Log(inflist.npcList.Count);
        for (int i = 0; i < inflist.npcList.Count; i++)
        {
            Debug.Log("firstName:" + inflist.npcList[i].firstName + "  lastName:" + inflist.npcList[i].lastName + "  iniMood:" + inflist.npcList[i].iniMood);
        }
    }

    public string GetFirstName()
    {
        //现在是纯随机 之后可以做得更精细一点
        int ra = Random.Range(0, infList.npcList.Count);
        return infList.npcList[ra].firstName;
    }

    public string GetLastName()
    {
        //现在是纯随机 之后可以做得更精细一点
        int ra = Random.Range(0, infList.npcList.Count);
        return infList.npcList[ra].lastName;
    }

    public string GetJob()
    {
        //现在是纯随机 之后可以做得更精细一点
        int ra = Random.Range(0, infList.npcList.Count);
        return infList.npcList[ra].job;
    }
}
