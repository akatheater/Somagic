using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//用来测试的脚本

public class raythree : MonoBehaviour
{
    public float rayheight = 2;
    public bool ishitdown = false;
    public bool ishitup = false;
    public bool ishitleft = false;
    public bool ishitright = false;

    public bool roomHasNpc = false;

    public int moodlevel;
    public Material[] roomMaterial = new Material[5];

    public enum npcType { common, special };
    public GameObject npc;
    public GameObject shadow;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
            
        //Debug.Log(this.transform.parent.name);
        //Debug.Log(this.transform.parent.name+" & "+GetComponent<raythree>().transform.parent.name);
        //测试 load材质
        for (int i = 0; i <= 4; i++)
        {
            roomMaterial[i] = Resources.Load("Materials/mood_" + i) as Material;
        }

        //测试 随机改变房屋mood
        this.moodlevel = Random.Range(0, 4);


        //测试 射线检测
        test();

        //CreateCommonNPC();
    }

    // Update is called once per frame
    void Update()
    {
        checkMood();
    }

    //病毒测试
    //private void OnMouseDown()
    //{
    //    anim.SetBool("virus", true);
    //}

    //测试 根据心情改变房屋材质
    void checkMood()
    {
        //this.GetComponent<MeshRenderer>().material = roomMaterial[moodlevel];

        Transform[] tr = this.GetComponentsInChildren<Transform>();
        foreach (var rc in tr)
        {
            if (rc.CompareTag("roomcube") == true)
            {
                rc.gameObject.GetComponent<MeshRenderer>().material = roomMaterial[moodlevel];
            }
        }
    }

    void test()
    {
        RaycastHit hit1, hit2, hit3, hit4;
        Ray raydown = new Ray(transform.position, -transform.up);
        if (Physics.Raycast(raydown, out hit2, rayheight))
        {
            ishitdown = true;
            if (hit2.collider.tag == "cube" && ishitdown == true)
            {
                //Debug.Log(transform.parent.name+" "+name+"  hitdown");
            }
        }

        Ray rayup = new Ray(transform.position, transform.up);
        if (Physics.Raycast(rayup, out hit1, rayheight))
        {
            ishitup = true;
            if (hit1.collider.tag == "cube" && ishitup == true)
            {
                //Debug.Log(transform.parent.name + " " + name+"  hitup");
            }
        }

        Ray rayleft = new Ray(transform.position, -transform.right);
        if (Physics.Raycast(rayleft, out hit3, rayheight))
        {
            ishitleft = true;
            if (hit3.collider.tag == "cube" && ishitleft == true)
            {
                //Debug.Log(transform.parent.name + " " + name + "  hitleft");
            }
        }

        Ray rayright = new Ray(transform.position, transform.right);
        if (Physics.Raycast(rayright, out hit4, rayheight))
        {
            ishitright = true;
            if (hit4.collider.tag == "cube" && ishitright == true)
            {
                //Debug.Log(transform.parent.name + " " + name + "  hitright");
            }
        }

    }

    public void CreateFirstNPC()
    {
        //在第0天放一个默认npc
        if (this.transform.parent.name == "Cube5" && this.name == "Cubeleft" && roomHasNpc == false)
        {

            //Debug.Log(Timecontroller.today);
            npc = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/NPC/commonNpc"));
            npc.name = "common0";
            npc.transform.parent = this.transform;
            npc.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
            npc.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            npc.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
            npc.tag = "npc";
            npc.GetComponent<NPCController>().InitiateFirstNpc();
            roomHasNpc = true;

            CreateShadow(npc);
        }
    }

    public void RandomCreateNpc(string cubeName, string roomName)
    {
        if (this.transform.parent.name == cubeName && this.name == roomName && Timecontroller.commonNpcCount+Timecontroller.specialNpcCount<4)
        {
            npc = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/NPC/commonNpc"));
            npc.name = "common" + Timecontroller.commonNpcGo;
            npc.transform.parent = this.transform;
            npc.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
            npc.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
            npc.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
            npc.tag = "npc";
            npc.GetComponent<NPCController>().InitiateCommonNpc();
            roomHasNpc = true;

            //生成npc的同时，生成用于投射的影子
            CreateShadow(npc);
        }     
    }

    public void CreateShadow(GameObject npc)
    {
        shadow = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/NPC/NpcShadow"));
        shadow.name = "shadow for " + npc.name;
        shadow.transform.localPosition = new Vector3(0f, 0f, 0f);
        shadow.transform.localEulerAngles = new Vector3(0f, 0f, 0f);
        shadow.transform.localScale = new Vector3(1f, 1f, 1f);

        //同时最多存在4个npc，将npc与4个showroom一一对应
        if (Cube.shadowHasNpc[0]==false)
        {
            Cube.shadowHasNpc[0] = true;
            npc.GetComponent<NPCController>().SetNpcShadow(0);
            Transform[] tr = shadow.GetComponentsInChildren<Transform>();
            foreach (var s in tr)
            {
                s.gameObject.layer = 11;
            }
        }
        else if(Cube.shadowHasNpc[1]==false)
        {
            Cube.shadowHasNpc[1] = true;
            npc.GetComponent<NPCController>().SetNpcShadow(1);
            Transform[] tr = shadow.GetComponentsInChildren<Transform>();
            foreach (var s in tr)
            {
                s.gameObject.layer = 8;
            }
        }
        else if (Cube.shadowHasNpc[2] == false)
        {
            Cube.shadowHasNpc[2] = true;
            npc.GetComponent<NPCController>().SetNpcShadow(2);
            Transform[] tr = shadow.GetComponentsInChildren<Transform>();
            foreach (var s in tr)
            {
                s.gameObject.layer = 9;
            }
        }
        else if (Cube.shadowHasNpc[3] == false)
        {
            Cube.shadowHasNpc[3] = true;
            npc.GetComponent<NPCController>().SetNpcShadow(3);
            Transform[] tr = shadow.GetComponentsInChildren<Transform>();
            foreach (var s in tr)
            {
                s.gameObject.layer = 10;
            }
        }

    }
}