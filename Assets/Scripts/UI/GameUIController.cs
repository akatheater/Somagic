using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUIController : MonoBehaviour
{
    public TMP_Text step;
    public TMP_Text coInf_basic;
    public TMP_Text coInf_exp;
    public TMP_Text coInf_current;

    public TMP_Text dialog;
    public GameObject[] dia;

    public Animator anim;

    public void ShowUI()
    {
        Debug.Log("show!");
        anim.SetBool("eat", true);
    }

    public void ChangeStepUI()
    {
        step.text = Cube.step + "/5";
    }

    public void ShowInfo(string firstName,string lastName,string job,string reason,string testNpc)
    {
        //coInf_basic.text = firstName+"·" + lastName + "\n"+"job:"+ job+"\n" +"reason: "+ reason;
        //exp和move有关 先写move
       
        //current和判定心情有关 先写判定
       

        if(testNpc== "common0")
        {
            //coInf_basic.text = "Darell·Winthrop \n job: architect \n reason: Reported";
            //coInf_exp.text = "DAY1 Spent all day in the room and seemed confused.\nDAY2 Went around, accidentally infected by the virus.\nDAY3 The symptoms get worse, making strange noises from time to time.\nDAY4 After taking the medicine, the symptoms have alleviated.\nDAY5 Ready to be discharged.";
            //coInf_current.text = "Now he feels very happy.";

            coInf_basic.text = "Gardner·Joseph \n job: accountant \n reason: Reported";
            coInf_exp.text = "DAY1 Stay in the room to write and draw.\nDAY2 Stay in the room and think. \nDAY3 ... \nDAY4 ... \nDAY5 Ready to be discharged.";
            coInf_current.text = "Now he looks terrible.";
        }
        else
        {
           
        }
    }

    public void ShowDialog(int clickNum)
    {
        if(clickNum<6)
        {
            dia[clickNum].SetActive(true);
        }
    }



    public void ClearInfo()
    {
        coInf_basic.text = " ";
        coInf_exp.text = " ";
        coInf_current.text = " ";
    }

    public void ClearDialog()
    {

    }
}
