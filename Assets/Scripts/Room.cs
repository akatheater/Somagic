using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//挂在每个打了cube tag 的cube上
public class Room : MonoBehaviour
{
    public enum roomType {  };
    public int roomMood;
    public Material[] roomMaterial=new Material[5];

    public enum npcType { common,special };
    public GameObject npc;
    public int npcCount;

    // Start is called before the first frame update
    void Start()
    {
        //房屋材质
        for(int i=0;i<=4;i++)
        {
            roomMaterial[i] = Resources.Load("Materials/mood_"+i) as Material;
        }       
    }

    // Update is called once per frame
    void Update()
    {
        CheckMood();
    }

    //根据情绪改变房间颜色
    void CheckMood()
    {
        this.GetComponent<MeshRenderer>().material = roomMaterial[roomMood];

        Transform[] tr=this.GetComponentsInChildren<Transform>();
        foreach(var rc in tr)
        {
            Debug.Log("???");
            if (rc.CompareTag("roomcube")==true)
            {
                Debug.Log("!!!");
                rc.gameObject.GetComponent<MeshRenderer>().material = roomMaterial[roomMood];
            }
        }
    }

    public void CreateCommonNPC()
    {
            npc = Instantiate<GameObject>((GameObject)Resources.Load("Prefabs/NPC/commonNpc"));
            npc.name = "common" + CreateNpcAsset.commonNpcNum;
            npc.transform.parent = this.transform;
            npc.transform.localPosition = new Vector3(0f, 0f, -0.5f);
            npc.transform.localEulerAngles = new Vector3(0f, 90f, 0f);
            npc.transform.localScale = new Vector3(0.1f, 0.025f, 0.025f);
            npc.tag = "npc";
            npc.GetComponent<NPCController>().InitiateCommonNpc();
        }
    }
