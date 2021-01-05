using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MyClass
{
    public int myInt;
    public List<int> myList = new List<int>();

    public List<Vector3> mcPosition = new List<Vector3>();
    public List<Quaternion> mcRotation = new List<Quaternion>();

    //public List<Vector3> cubePosition = new List<Vector3>();
    //public List<Quaternion> cubeRotation = new List<Quaternion>();
    //public List<Transform> cubeTransform = new List<Transform>();
    public List<string> grandName = new List<string>();
    public List<string> fatherName = new List<string>();

    public List<Vector3> npcPosition = new List<Vector3>();
    public List<Quaternion> npcRotation = new List<Quaternion>();
    public List<string> npcName = new List<string>();

    public List<int> npcLife = new List<int>();

    public int day;

    public int specialNpcGo;
    public int commonNpcGo;

    //public NpcLogs myNpcInformation;
    //public NpcLogs myNpcAnalysis;

    #region CONSTRUCTOR
    public MyClass()
    {

    }

    public MyClass(int myInt, List<int> myList)
    {
        this.myInt = myInt;
        this.myList = myList;
    }

    public MyClass(List<Vector3> mcPosition, List<Quaternion> mcRotation,int day)
    {
        this.mcPosition = mcPosition;
        this.mcRotation = mcRotation;
       
        this.day = day;
    }

    public MyClass(List<string> grandName, List<string> fatherName,List<Vector3> npcPosition, List<Quaternion> npcRotation, List<string> npcName, List<int> npcLife, int specialNpcGo, int commonNpcGo)
    {
        //this.cubePosition = cubePosition;
        //this.cubeRotation = cubeRotation;
        //this.cubeTransform = cubeTransform;
        this.grandName = grandName;
        this.fatherName = fatherName;
        this.npcPosition = npcPosition;
        this.npcRotation = npcRotation;
        this.npcName = npcName;

        this.npcLife = npcLife;

        this.specialNpcGo = specialNpcGo;
        this.commonNpcGo = commonNpcGo;

    }



    #endregion

    #region TO STRING
    public override string ToString()
    {
        string output = "";

        output += "myInt = " + myInt + "\n";

        output += "myList = ";
        foreach (int i in myList)
            output += i + ", ";

        return output;
    }
    #endregion
}