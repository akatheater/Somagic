using UnityEngine;
using System.Collections.Generic;
using SaveSystem;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEditor;
using System.IO;

public class Cube : MonoBehaviour
{
    public MyClass savecubes = new MyClass();
    public MyClass savelogs = new MyClass();

    public NpcLogs myNpcInformation;
    public NpcLogs myNpcAnalysis;

    public static int step=0;
    public static bool islocked=false;

    static public bool[] shadowHasNpc= new bool[4];

    struct RotateInfo
    {
        public string axis;
        public float dir;
        public float a;
    };

    const float TRANS = 2;
    public Camera camera;
    Vector3 offset;
    public GameObject[] cubePrefab=new GameObject[27];
    public GameObject cubeParent;
    GameObject[] cubes = new GameObject[27];
    Transform[] matx = new Transform[27];
    Vector3 vecs;
    Vector3 vece;
    GameObject pickedNodes;
    GameObject pickedNodee;
    Vector3 vecms;
    Vector3 vecme;
    bool done = false;
    RotateInfo rotateInfo;
    bool isRotate = false;
    float rotateDegree = 0;

    void Start()
    {
        //初始化shadowNum
        for (int i = 0; i <= 3; i++)
        {
            shadowHasNpc[i] = false;
        }

        for (int i = 0; i < 27; i++)
        {
            cubes[i] = Instantiate(cubePrefab[i], new Vector3(0, 0, 0), Quaternion.identity);
            cubes[i].transform.parent = cubeParent.transform;
        }

        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                for (int k = -1; k <= 1; k++)
                {
                    int number = 9 * (i + 1) + 3 * (j + 1) + (k + 2);
                    GameObject cube = cubes[number-1];
                    cube.name = "Cube" + number;
                    cube.transform.Translate(new Vector3(i * TRANS, j * TRANS, k * TRANS));
                    matx[number - 1] = cube.transform;
                }
            }
        }

        offset = camera.transform.position - cubeParent.transform.position;

        //创建第一个（默认）npc
        GameObject[] obs=GameObject.FindGameObjectsWithTag("cube");
        foreach(var cube in obs)
        {
            cube.GetComponent<raythree>().CreateFirstNPC();
        }

        //创建默认存档save0
        if (!Directory.Exists(Application.persistentDataPath+"/SaveData/Day0/"))
        {
            Directory.CreateDirectory(Application.persistentDataPath + "/SaveData/Day0/");

        }
        SimpleSaveCube(Application.persistentDataPath + "/SaveData/Day0/CubeData.xml");
        SimpleSaveNpcs(Application.persistentDataPath + "/SaveData/Day0/NpcData.xml");
    }

    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            done = false;
        }

        if (isRotate)
        {
            if ((rotateDegree+3.0f) > 90)
            {
                rotate(rotateInfo, 90 - rotateDegree);
                rotateDegree = 0;
                isRotate = false;
                return;
            }

            rotateDegree += 3.0f;
            rotate(rotateInfo, 3.0f);
            //Debug.Log("isRotate!!!");
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            pickedNodes = null;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                pickedNodes = hit.collider.gameObject.transform.parent.gameObject;
                vecs = hit.point;
            }

            if (pickedNodes != null)
            {
                for (int i = 0; i < 27; i++)
                {
                    if (pickedNodes.name == cubes[i].name)
                    {
                        vecms = matx[i].transform.position;
                        Debug.Log(pickedNodes.name+" "+ hit.collider.gameObject.name);

                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (!done)
            {
                pickedNodee = null;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 100))
                {
                    pickedNodee = hit.collider.gameObject.transform.parent.gameObject;
                    vece = hit.point;
                }

                if (pickedNodes != null)
                {
                    if (pickedNodee != null)
                    {
                        for (int i = 0; i < 27; i++)
                        {
                            if (pickedNodee.name == cubes[i].name)
                            {
                                vecme = matx[i].transform.position;
                            }
                        }
                    }

                    rotateInfo = rotationinfo();

                    /*steeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeep*/
                    if (rotateInfo.axis != ""&& islocked==false)
                    {
                        //Debug.Log(step);
                        done = true;
                        isRotate = true;
                        step++;
                        
                        if (step >= 5)
                            islocked = true;

                        //GameObject.Find("Gametime").GetComponent<Timecontroller>().ChangeStepUI();
                        GameObject.Find("Canvas").GetComponent<GameUIController>().ChangeStepUI();
                    }
                }
            }
        }

        Rotate();
        Scale();
    }

    RotateInfo rotationinfo()
    {
       // Debug.Log("this is rotationinfo!");
        RotateInfo rotateInfo = new RotateInfo();
        string axis = "";
        float dir = 0.0f;
        float a = 0.0f;

        if ((Mathf.Abs(vecme.x - vecms.x) >= 0.01) || (Mathf.Abs(vecme.y - vecms.y) >= 0.01) || (Mathf.Abs(vecme.z - vecms.z) >= 0.01))
        {
            if ((Mathf.Abs(vecme.x - vecms.x) < 0.01) && (Mathf.Abs(vecme.y - vecms.y) < 0.01))
            {
                if ((Mathf.Abs(Mathf.Abs(vece.x) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.x) - 3.0f) < 0.01))
                {
                    axis = "y";
                    a = vecme.y / 100;
                    float temp = (vece.z - vecs.z) * vece.x;
                    dir = (-1) * temp / Mathf.Abs(temp);
                }
                else if ((Mathf.Abs(Mathf.Abs(vece.y) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.y) - 3.0f) < 0.01))
                {
                    axis = "x";
                    a = vecme.x / 100;
                    float temp = (vece.z - vecs.z) * vece.y;
                    dir = temp / Mathf.Abs(temp);
                }
            }
            else if ((Mathf.Abs(vecme.x - vecms.x) < 0.01) && (Mathf.Abs(vecme.z - vecms.z) < 0.01))
            {
                if ((Mathf.Abs(Mathf.Abs(vece.x) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.x) - 3.0f) < 0.01))
                {
                    axis = "z";
                    a = vecme.z / 100;
                    float temp = (vece.y - vecs.y) * vece.x;
                    dir = temp / Mathf.Abs(temp);
                }
                else if ((Mathf.Abs(Mathf.Abs(vece.z) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.z) - 3.0f) < 0.01))
                {
                    axis = "x";
                    a = vecme.x / 100;
                    float temp = (vece.y - vecs.y) * vece.z;
                    dir = (-1) * temp / Mathf.Abs(temp);
                }
            }
            else if ((Mathf.Abs(vecme.y - vecms.y) < 0.01) && (Mathf.Abs(vecme.z - vecms.z) < 0.01))
            {
                if ((Mathf.Abs(Mathf.Abs(vece.y) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.y) - 3.0f) < 0.01))
                {
                    axis = "z";
                    a = vecme.z / 100;
                    float temp = (vece.x - vecs.x) * vece.y;
                    dir = (-1) * temp / Mathf.Abs(temp);
                }
                else if ((Mathf.Abs(Mathf.Abs(vece.z) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vecs.z) - 3.0f) < 0.01))
                {
                    axis = "y";
                    a = vecme.y / 100;
                    float temp = (vece.x - vecs.x) * vece.z;
                    dir = temp / Mathf.Abs(temp);
                }
            }
        }
        else if ((Mathf.Abs(vecme.x - vecms.x) < 0.01) && (Mathf.Abs(vecme.y - vecms.y) < 0.01) && (Mathf.Abs(vecme.z - vecms.z) < 0.01))
        {
            if (((Mathf.Abs(Mathf.Abs(vecs.x) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.y) - 3.0f) < 0.01)) ||
                ((Mathf.Abs(Mathf.Abs(vecs.y) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.x) - 3.0f) < 0.01)))
            {
                if ((Mathf.Abs(vece.y - vecs.y) > 0.1) && (Mathf.Abs(vece.x - vecs.x) > 0.1))
                {
                    axis = "z";
                    a = vecme.z / 100;
                    float temp = vecs.x * vece.y * (Mathf.Abs(vece.y) - Mathf.Abs(vecs.y));
                    dir = temp / Mathf.Abs(temp);
                }
            }
            else if (((Mathf.Abs(Mathf.Abs(vecs.x) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.z) - 3.0f) < 0.01)) ||
                ((Mathf.Abs(Mathf.Abs(vecs.z) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.x) - 3.0f) < 0.01)))
            {
                if ((Mathf.Abs(vece.z - vecs.z) > 0.1) && (Mathf.Abs(vece.x - vecs.x) > 0.1))
                {
                    axis = "y";
                    a = vecme.y / 100;
                    float temp = vecs.z * vece.x * (Mathf.Abs(vece.x) - Mathf.Abs(vecs.x));
                    dir = temp / Mathf.Abs(temp);
                }
            }
            else if (((Mathf.Abs(Mathf.Abs(vecs.z) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.y) - 3.0f) < 0.01)) ||
                ((Mathf.Abs(Mathf.Abs(vecs.y) - 3.0f) < 0.01) && (Mathf.Abs(Mathf.Abs(vece.z) - 3.0f) < 0.01)))
            {
                if ((Mathf.Abs(vece.z - vecs.z) > 0.1) && (Mathf.Abs(vece.y - vecs.y) > 0.1))
                {
                    axis = "x";
                    a = vecme.x / 100;
                    float temp = vecs.y * vece.z * (Mathf.Abs(vece.z) - Mathf.Abs(vecs.z));
                    dir = temp / Mathf.Abs(temp);
                }
            }
        }

        if (pickedNodee == null)
        {
            if (Mathf.Abs(Mathf.Abs(vecs.x) - 3.0f) < 0.01)
            {
                axis = (Mathf.Abs(vece.y) >= Mathf.Abs(vece.z)) ? "z" : "y";
                string tempos = Mathf.Abs(vece.y) >= Mathf.Abs(vece.z) ? "y" : "z";

                if (axis == "z")
                {
                    a = vecms.z / 100;
                }
                else
                {
                    a = vecms.y / 100;
                }

                float temp;

                if (axis == "z")
                {
                    if (tempos == "y")
                    {
                        temp = vecs.x * vece.y;
                    }
                    else
                    {
                        temp = vecs.x * vece.z;
                    }
                }
                else
                {
                    if (tempos == "y")
                    {
                        temp = (-1) * vecs.x * vece.y;
                    }
                    else
                    {
                        temp = (-1) * vecs.x * vece.z;
                    }
                }

                dir = temp / Mathf.Abs(temp);
            }
            else if (Mathf.Abs(Mathf.Abs(vecs.y) - 3.0f) < 0.01)
            {
                axis = (Mathf.Abs(vece.x) >= Mathf.Abs(vece.z)) ? "z" : "x";
                string tempos = Mathf.Abs(vece.x) >= Mathf.Abs(vece.z) ? "x" : "z";

                if (axis == "z")
                {
                    a = vecms.z / 100;
                }
                else
                {
                    a = vecms.x / 100;
                }

                float temp;

                if (axis == "x")
                {
                    if (tempos == "x")
                    {
                        temp = vecs.y * vece.x;
                    }
                    else
                    {
                        temp = vecs.y * vece.z;
                    }
                }
                else
                {
                    if (tempos == "x")
                    {
                        temp = (-1) * vecs.y * vece.x;
                    }
                    else
                    {
                        temp = (-1) * vecs.y * vece.z;
                    }
                }

                dir = temp / Mathf.Abs(temp);
            }
            else if (Mathf.Abs(Mathf.Abs(vecs.z) - 3.0f) < 0.01)
            {
                axis = (Mathf.Abs(vece.y) >= Mathf.Abs(vece.x)) ? "x" : "y";
                string tempos = Mathf.Abs(vece.y) >= Mathf.Abs(vece.x) ? "y" : "x";

                if (axis == "x")
                {
                    a = vecms.x / 100;
                }
                else
                {
                    a = vecms.y / 100;
                }

                float temp;

                if (axis == "y")
                {
                    if (tempos == "x")
                    {
                        temp = vecs.z * vece.x;
                    }
                    else
                    {
                        temp = vecs.z * vece.y;
                    }
                }
                else
                {
                    if (tempos == "x")
                    {
                        temp = (-1) * vecs.z * vece.x;
                    }
                    else
                    {
                        temp = (-1) * vecs.z * vece.y;
                    }
                }

                dir = temp / Mathf.Abs(temp);
            }
        }

        rotateInfo.axis = axis;
        rotateInfo.dir = dir;
        rotateInfo.a = a;

        return rotateInfo;
    }

    void rotate(RotateInfo rotateInfo, float degree)
    {
        //Debug.Log("this is rotate!");
        if (rotateInfo.axis != "")
        {
            if (rotateInfo.axis == "x")
            {
                for (int i = 0; i < 27; i++)
                {
                    if (Mathf.Abs(matx[i].transform.localPosition.x - 100 * rotateInfo.a) < 1)
                    {
                        matx[i].RotateAround(cubeParent.transform.localPosition, Vector3.right * rotateInfo.dir, degree);
                    }
                }
            }
            else if (rotateInfo.axis == "y")
            {
                for (int i = 0; i < 27; i++)
                {
                    if (Mathf.Abs(matx[i].transform.localPosition.y - 100 * rotateInfo.a) < 1)
                    {
                        matx[i].RotateAround(cubeParent.transform.localPosition, Vector3.up * rotateInfo.dir, degree);
                    }
                }
            }
            else if (rotateInfo.axis == "z")
            {
                for (int i = 0; i < 27; i++)
                {
                    if (Mathf.Abs(matx[i].transform.localPosition.z - 100 * rotateInfo.a) < 1)
                    {
                        matx[i].RotateAround(cubeParent.transform.localPosition, Vector3.forward * rotateInfo.dir, degree);
                    }
                }
            }
        }
        
    }

    private void Scale()
    {
        float dis = offset.magnitude;
        dis += Input.GetAxis("Mouse ScrollWheel") * (-5);

        if (dis < 10 || dis > 40)
        {
            return;
        }

        offset = offset.normalized * dis;
        camera.transform.position = cubeParent.transform.position + offset;
    }

    private void Rotate()
    {
        if (Input.GetMouseButton(1))
        {
            camera.transform.RotateAround(cubeParent.transform.position, Vector3.up, Input.GetAxis("Mouse X") * 10);
            camera.transform.RotateAround(cubeParent.transform.position, Vector3.left, Input.GetAxis("Mouse Y") * 10);
            offset = camera.transform.position - cubeParent.transform.position;
        }
    }

    MyClass Savecubes()
    {

        savecubes.mcPosition.Clear();
        savecubes.mcRotation.Clear();
        

        for (int i = 0; i < 27; i++)
        {
            savecubes.mcPosition.Add(cubes[i].transform.position);
            savecubes.mcRotation.Add(cubes[i].transform.rotation);
           

        }
        savecubes.day = Timecontroller.today;
        return savecubes;
    }

    public void SimpleSaveCube(string savepath)
    {
        MyClass mc = Savecubes();

        //Debug.Log("Your files are located here: " + Application.persistentDataPath);
        FileSave fileSave = new FileSave(FileFormat.Xml);
        //fileSave.WriteToFile(Application.persistentDataPath + "/tryCubeSave.xml", mc);
        fileSave.WriteToFile(savepath, mc);
        //Debug.Log("Saved data: " + mc.mcPosition[26]);

    }

    public void SimpleLoadCube(string savepath)
    {
        FileSave fileSave = new FileSave(FileFormat.Xml);
        //MyClass myClass = fileSave.ReadFromFile<MyClass>(Application.persistentDataPath + "/tryCubeSave.xml");
        MyClass myClass = fileSave.ReadFromFile<MyClass>(savepath);
        //Debug.Log("Loaded data: " + myClass.mcPosition[26]);
        Setcube(myClass);
    }

    private void Setcube(MyClass sb)
    {

        for (int i = 0; i < 27; i++)
        {
            cubes[i].transform.position = sb.mcPosition[i];
            cubes[i].transform.rotation = sb.mcRotation[i];

        }

        //Debug.Log("sb.npcPosition.Count:" + sb.npcPosition.Count);


    }

    MyClass Savenpcs()
    {
        
        savelogs.grandName.Clear();
        savelogs.fatherName.Clear();
        savelogs.npcPosition.Clear();
        savelogs.npcRotation.Clear();
        savelogs.npcName.Clear();
        savelogs.npcLife.Clear();

        savelogs.commonNpcGo = Timecontroller.commonNpcGo;
        savelogs.specialNpcGo = Timecontroller.specialNpcGo;

        for (int i = 0; i < 27; i++)
        {
            
            Transform[] father = cubes[i].GetComponentsInChildren<Transform>();
            foreach (Transform son in father)
            {
                
                if (son.CompareTag("npc"))
                {
                    //Debug.Log("!!!");
                    savelogs.grandName.Add(son.parent.transform.parent.name);
                    savelogs.fatherName.Add(son.parent.name);
                    savelogs.npcPosition.Add(son.position);
                    savelogs.npcRotation.Add(son.rotation);
                    savelogs.npcName.Add(son.name);
                    //npc life!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    savelogs.npcLife.Add(son.GetComponent<NPCController>().GetNpcLife());
                    //Debug.Log("cube-savenpcs-getnpclife="+son.GetComponent<NPCController>().GetNpcLife());
                    //Debug.Log("name:" +son.parent.transform.parent.name);
                }
            }

        }

        return savelogs;
    }

    public void SimpleSaveNpcs(string savepath)
    {
        MyClass mc = Savenpcs();
        FileSave fileSave = new FileSave(FileFormat.Xml);
        //fileSave.WriteToFile(Application.persistentDataPath + "/tryLogsSave.xml", mc);
        fileSave.WriteToFile(savepath, mc);
        //Debug.Log("today:"+Timecontroller.today+"   Saved npcPosition.Count: " + mc.npcPosition.Count);
       
        

    }

    public void SimpleLoadNpcs(string savepath)
    {
        FileSave fileSave = new FileSave(FileFormat.Xml);
        //MyClass myClass = fileSave.ReadFromFile<MyClass>(Application.persistentDataPath + "/tryLogsSave.xml");
        MyClass myClass = fileSave.ReadFromFile<MyClass>(savepath);
        //Debug.Log("Loaded data: " + myClass.npcPosition.Count);
        SetNpcs(myClass);
    }

    private void SetNpcs(MyClass sl)
    {
       
        Transform[] father=GetComponentsInChildren<Transform>();
        foreach (var son in father)
        {
            if(son.CompareTag("npc"))
            {
                Destroy(son.gameObject);
            }
           
        }

        for(int i=0;i<sl.npcName.Count;i++)
        {
            GameObject npc= Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/NPC/commonNpc"));
            npc.name = sl.npcName[i];
            npc.tag = "npc";
            
            npc.GetComponent<NPCController>().npc= (NPC)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPC/" + npc.name + ".asset", typeof(NPC));
            npc.GetComponent<NPCController>().npcinformation = myNpcInformation;
            npc.GetComponent<NPCController>().npcanalysis = myNpcAnalysis;
            //存life-1
            npc.GetComponent<NPCController>().SetNpcLife(sl.npcLife[i]);
            //Debug.Log("simpleload setnpclifes"+sl.npcLife[i]);
            //Debug.Log("simpleload getnpclifes" + npc.GetComponent<NPCController>().GetNpcLife());
            npc.transform.position = sl.npcPosition[i];
            npc.transform.rotation = sl.npcRotation[i];
            

            for(int j=0;j<27;j++)
            {
                if (cubes[j].name == sl.grandName[i])
                {
                    npc.transform.parent = cubes[j].transform.Find(sl.fatherName[i]);
                    npc.transform.parent.GetComponent<raythree>().roomHasNpc = true;
                }
                
            }


        }

       // Timecontroller.commonNpcGo = sl.commonNpcGo;
       // Timecontroller.specialNpcGo = sl.specialNpcGo;

        Timecontroller.commonNpcCount = sl.npcName.Count;
        Timecontroller.specialNpcCount = 0;
    }
       
   


}