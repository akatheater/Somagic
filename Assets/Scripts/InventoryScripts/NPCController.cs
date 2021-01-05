using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NPCController : MonoBehaviour
{
    public int npcMood;
    public enum moveDirection { stay,up,down,left,right };
    public moveDirection movedirection;
    public float rayheight = 2;

    public NPC npc;
    public NpcLogs npcinformation;
    public NpcLogs npcanalysis;
    public bool isCreate=false;

    public int clickNpc=0;

    public Animator anim;


    void Update()
    {   
        NpcMove(movedirection);
    }

    public void ChangeBehaviour()
    {
        anim.SetBool("eat", true);
    }

    //生成普通npc时 对npc进行初始化 
    //生成npc的方法写在room里，因为要获取出生位置，除了生成其他的npc有关方法都写在npccontroller里
    public void InitiateCommonNpc()
    {
        //生成普通npc的asset asset是随机的
        //CreateNpcAsset.CreateCommonNpcAsset();
        
        //生成npc的asset
        CreateNpcAsset.CreateFixedNpcAsset();
        //为npc绑定asset
        npc = (NPC)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPC/common" + (Timecontroller.commonNpcGo - 1) + ".asset", typeof(NPC));
        npcinformation = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcInformation.asset", typeof(NpcLogs));
        npcanalysis = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcAnalysis.asset", typeof(NpcLogs));
        AddNpcToInformation(npc);
    }

    public void InitiateFirstNpc()
    {
        //生成npc的asset
        CreateNpcAsset.CreateFirstNpcAsset();
        //为npc绑定asset
        npc = (NPC)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPC/common" + (Timecontroller.commonNpcGo - 1) + ".asset", typeof(NPC));
        npcinformation = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcInformation.asset", typeof(NpcLogs));
        npcanalysis = (NpcLogs)AssetDatabase.LoadAssetAtPath("Assets/Resources/NPCLogs/NpcAnalysis.asset", typeof(NpcLogs));
        AddNpcToInformation(npc);
    }

    public int GetNpcShadow()
    {
        return npc.shadowNum;
    }

    public void SetNpcShadow(int npcShadow)
    {
        npc.shadowNum=npcShadow;
    }

    public void DecreaseLife()
    {
        npc.NpcLife--;
        if (npc.NpcLife <= 0)
            CommonNpcLeave();
    }

    public int GetNpcLife()
    {
        return npc.NpcLife;
    }

    public void SetNpcLife(int npclife)
    {
        npc.NpcLife = npclife;
    }

    //生命为0的情况下npc离开
    public void CommonNpcLeave()
    {
        //获得自身
        GameObject go = transform.gameObject;
        //获得影子
        GameObject shadow = GameObject.Find("shadow for " + this.name);
        //恢复影子房间状态
        Cube.shadowHasNpc[GetNpcShadow()]= false;
        //先销毁影子
        Destroy(shadow);
        //再销毁自身
        Destroy(go);
        Timecontroller.commonNpcCount--;
    }

    //动画切换怎么写
    //private void OnMouseDown()
    //{
    //    //Debug.Log("!!!");
    //    GameObject.Find("Canvas").GetComponent<GameUIController>().ShowInfo(npc.firstName,npc.lastName,npc.job,npc.reason,this.name);
    //    //之后一定要改！！！
    //   anim.SetBool("eat", false);
    //    //临时的 之后要改！！！
    //    GameObject.Find("Canvas").GetComponent<GameUIController>().ShowDialog(clickNpc);
    //    clickNpc++;
    //}

    public void AddNpcToInformation(NPC npc)
    {
      if(!npcinformation.npcList.Contains(npc))
        {
            npcinformation.npcList.Add(npc);
            
        }
    }

    public void AddNpcToAnalysis(NPC npc)
    {
        if (!npcanalysis.npcList.Contains(npc))
        {
            npcanalysis.npcList.Add(npc);

        }
    }




    private void NpcMove(moveDirection direction)
    {
        RaycastHit hit1, hit2, hit3, hit4;
        switch (direction)
        {
            case moveDirection.up:
                Ray rayup = new Ray(transform.parent.transform.position, transform.parent.transform.transform.up);
                if (Physics.Raycast(rayup, out hit1, rayheight))
                {                 
                    if (hit1.collider.tag == "cube" )
                    {
                        Debug.Log(this.transform.parent.name+" "+this.name+"hit up");
                        this.transform.parent = hit1.transform;
                        this.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
                        this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);

                    }
                }
                break;
            case moveDirection.down:
                Ray raydown = new Ray(transform.parent.transform.position, -transform.parent.transform.up);
                if (Physics.Raycast(raydown, out hit2, rayheight))
                {
                    if (hit2.collider.tag == "cube")
                    {
                        Debug.Log(this.transform.parent.name + " " + this.name + "hit down");
                        this.transform.parent = hit2.transform;
                        this.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
                        this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
                    }
                }
                break;
            case moveDirection.left:
                Ray rayleft = new Ray(transform.parent.transform.position, -transform.parent.transform.right);
                if (Physics.Raycast(rayleft, out hit3, rayheight))
                {
                    if (hit3.collider.tag == "cube")
                    {
                        Debug.Log(this.transform.parent.name + " " + this.name + "hit left");
                        this.transform.parent = hit3.transform;
                        this.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
                        this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
                    }
                }
                break;
            case moveDirection.right:
                Ray rayright = new Ray(transform.parent.transform.position, transform.parent.transform.right);
                if (Physics.Raycast(rayright, out hit4, rayheight))
                {
                    if (hit4.collider.tag == "cube")
                    {
                        Debug.Log(this.transform.parent.name + " " + this.name + "hit right");
                        this.transform.parent = hit4.transform;
                        this.transform.localPosition = new Vector3(0f, -0.4f, -0.25f);
                        this.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
                        this.transform.localScale = new Vector3(0.15f, 0.15f, 0.6f);
                    }
                }
                break;
            default:
                break;
        }
    }
}
