using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AddTask_toServer : MonoBehaviour
{
    private string task;
    private DateTime regdate_d;//登録日(DateTime)
    private string regdate;//登録日
    private DateTime deadline_d;//期限(DateTime)
    private string deadline;//期限
    private int deadlineday;//期限までの日程
    private int important;//重要度
    private int enemyID;//敵ID
    private string memo;//memo

    public GameObject clientsystem;

    private int uid;
    
    
    void Start(){
        TextMeshProUGUI regobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Registered").gameObject.transform.Find("RegisteredDate").GetComponent<TextMeshProUGUI>();//登録日=今日
        //表示のみ
        regobj.text=(DateTime.Now.Year).ToString()+"/"+(DateTime.Now.Month).ToString()+"/"+(DateTime.Now.Day).ToString();
    }

    public void OnClick(){
        TMP_InputField taskobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("NameInputField").GetComponent<TMP_InputField>();//内容
        task=taskobj.text;
        regdate_d=DateTime.Now;//regdate(今日の日付を習得)
        regdate=regdate_d.ToString("yyyyMMdd");
        TextMeshProUGUI deadlineobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineButton").gameObject.transform.Find("datatext").GetComponent<TextMeshProUGUI>();//deadline
        deadline=deadlineobj.text;
        DateTime tmp=DateTime.Parse(deadline);
        deadline=tmp.ToString("yyyyMMdd");
        deadline_d=DateTime.Parse(tmp.ToString("yyyy/MM/dd"));//時刻なくても勝手に0:00:00を入れてくれるのでOKのはず
        deadlineday=(deadline_d-regdate_d).Days;
        
        Slider importantobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Importance").gameObject.transform.Find("ImportanceSlider").GetComponent<Slider>();//important
        //TMP_InputField importantobj=GameObject.Find("Canvas").gameObject.transform.Find("Importance").GetComponent<TMP_InputField>();//important
        important=(int)importantobj.value;
        TMP_InputField memoobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Memo").gameObject.transform.Find("MemoInputField").GetComponent<TMP_InputField>();//memo
        memo=memoobj.text;
        
        //まず敵データベースに敵を生成し追加(返り値は敵ID)
        string enemyname="";//敵名を重要度で区分
        if(important==1){
            enemyname="Slime";
        }
        else if(important==2){
            enemyname="Big Slime";
        }
        else{
            enemyname="Mega Slime";
        }
        enemyID=AboutEnemy.CreateAsset(enemyname,"normal",1,1,1,important,deadlineday);

        //タスク送信準備
        clientsystem.GetComponent<ClientAPI>().mode=6;
        var senddata=new Data_task{
            ID=PlayerBasedata.GetUserid(),
            Tasknum=0,
            Task=task,
            Taskcheck="none",
            Regdate=regdate,
            Deadline=deadline,
            Per=0,
            Important=important,
            EnemyID=enemyID,
            Memo=memo,
        };
        clientsystem.GetComponent<ClientAPI>().data2=senddata;



        //最後にタスク一覧に戻る
        SceneManager.LoadScene("TasksScene");

    }
}
