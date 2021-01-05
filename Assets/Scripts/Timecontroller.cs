using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//可以绑在按钮上

public class Timecontroller : MonoBehaviour
{
    static public int today=1;
    static public int chapter = 1;
    static public int commonNpcCount;
    static public int commonNpcGo;
    static public int specialNpcCount;
    static public int specialNpcGo;

    public bool whetherCanCreate = false;
    public GameObject chosenRoom;
    public int tmpInt;
    public string tmpString="none";
    public GameObject tmpGo;

    //////////////【随机】选择cube
    public void RandomChooseCube()
    {
        //cube1到27 除了14中间随机一个出来
        int cubeNum = Random.Range(1, 27);
        while (cubeNum == 14)
        {
            cubeNum = Random.Range(1, 27);
        }
        //Debug.Log("cubeNum="+cubeNum);
        tmpInt = cubeNum;
    }

    //////////////【随机】选择房间
    public void RandomChooseRoom()
    {       
        while(tmpString=="none")
        {
            RandomChooseCube();
            //对每个cube的子物体检测 找到标签是cube的房间
            Transform[] tr = GameObject.Find("Cube" + tmpInt).GetComponentsInChildren<Transform>();
            foreach (var cube in tr)
            {
                if (cube.CompareTag("cube") && tmpString == "none")
                {
                    tmpString = cube.name;
                    //Debug.Log(tmpString + "  in cube" + tmpInt);
                    tmpGo = cube.gameObject;
                    //之后改成room
                    if (tmpGo.GetComponent<raythree>().roomHasNpc==true)
                        tmpString = "none";
                   
                }
            }
        }

    }

    public int getTmpInt()
    {
        return tmpInt;
    }

    public string getTmpString()
    {
        return tmpString;
    }

    public GameObject getTmpGo()
    {
        return tmpGo;
    }

    //////////////【以下均为固定流程】
    public void GoByChapter()
    {
        if (chapter == 1)
            ChapterOne();
        else if (chapter == 2)
            ChapterTwo();
    }

    public void ChapterOne()
    {
        if (today == 1)
            Day2();       
        else if (today == 2)
            Day3();

    }

    public void Day2()
    {
        Debug.Log("day2 begin");
        GameObject[] obs = GameObject.FindGameObjectsWithTag("cube");
        foreach (var cube in obs)
        {
            cube.GetComponent<raythree>().RandomCreateNpc("Cube5", "Cubeleft");
            cube.GetComponent<raythree>().RandomCreateNpc("Cube13" , "Cubefront");
        }
    }
    public void Day3()
    {
        Debug.Log("day3 begin");
        GameObject[] obs = GameObject.FindGameObjectsWithTag("cube");
        foreach (var cube in obs)
        {
            cube.GetComponent<raythree>().RandomCreateNpc("Cube5", "Cubeleft");
            cube.GetComponent<raythree>().RandomCreateNpc("Cube13", "Cubefront");
        }
    }

    public void ChapterTwo()
    {
        if (today == 4)
            Day4();
        else if (today == 5)
            Day5();
        else if (today == 6)
            Day6();
        else if (today == 7)
            Day7();
        else if (today == 8)
            Day8();
    }
    public void Day4()
    {

    }
    public void Day5()
    {

    }
    public void Day6()
    {

    }
    public void Day7()
    {

    }
    public void Day8()
    {

    }
}
