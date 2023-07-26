using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class DeadlineButtonController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dateText;

    // Start is called before the first frame update
    void Start()
    {
        DateTime date=DateTime.Now;
       
        if(PlayerPrefs.GetString("deadline")!=""){
            //元々の期限(タスク変更)
            date=DateTime.Parse(PlayerPrefs.GetString("deadline"));
        }
     
        dateText.text = date.Year + "/" + date.Month + "/" + date.Day;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
