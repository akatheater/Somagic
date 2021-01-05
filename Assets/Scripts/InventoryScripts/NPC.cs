using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new NPC",menuName ="new NPC")]
public class NPC : ScriptableObject
{
    public string NpcID; //gameobject的名字
    public string NpcName;

    //会动态改变
    public Material NpcMaterial;
    public int NpcLife;
    public int curMood;
    public int shadowNum;

    //从文件中读取
    public string firstName;
    public string lastName;
    public string job;
    public string reason;
    public int iniMood;
    public int nature;
    public string dialog1;
    public string dialog2;
    public string dialog3;
    public string dialog4;
    public string answer1_1;
    public string answer1_2;


    [TextArea]
    public string NpcInfomation;
}
