using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using System.Threading;

public class TaskChoiceButton : MonoBehaviour
{
    public GameObject havetaskobj;
    public int thisobjnum=0;

    public void Onclick(){
        havetaskobj.GetComponent<Taskgetfromserver>().TempSaveData(thisobjnum);
        SceneManager.LoadScene("TaskInfoScene");
    }
}
