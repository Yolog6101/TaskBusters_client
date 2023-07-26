using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

using UnityEngine.EventSystems;

public class Taskgetfromserver : MonoBehaviour
{
    public GameObject clientsystem;
    public int tasknum;
    public Data_task[] receive;
    public bool checker=false;
    private bool checker2=false;
    public List<int> delete;
    public List<int> deleteenemy;

    public GameObject thisobj;

    // Start is called before the first frame update
    void Start()
    {
        //シーン出しと同時に実行
        Taketask();
    
    }

    // Update is called once per frame
    void Update()
    {
        if(checker){
            Display();
            checker2=true;
        }

        if(checker2){
            //期限切れタスクに対する敵データを削除
            if(delete.Count!=0){
                for(int i=0;i<delete.Count;i++){
                    Delete(delete[i],deleteenemy[i]);
                }
            }
            checker2=false;
            delete=null;
        }

    }

    public void Taketask(){
        //タスクを取得する準備
        clientsystem.GetComponent<ClientAPI>().mode=5;
        var senddata=new Data_user{
            ID=PlayerBasedata.GetUserid(),
        };
        clientsystem.GetComponent<ClientAPI>().data1=senddata;
        clientsystem.GetComponent<ClientAPI>().StartAPI();   
    }

    public void Display(){
        //プレハブ作成、データ適用
        clientsystem.GetComponent<TasksContentController>().generate(tasknum);
        for(int i=0;i<tasknum;i++){
            TextMeshProUGUI task=GameObject.Find("Canvas (1)").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("TasksContent").gameObject.transform.Find("Task"+i.ToString()).gameObject.transform.Find("TaskNameText").GetComponent<TextMeshProUGUI>();
            task.text=receive[i].Task;

            Slider per=GameObject.Find("Canvas (1)").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("TasksContent").gameObject.transform.Find("Task"+i.ToString()).gameObject.transform.Find("Progress").gameObject.transform.Find("ProgressSlider").GetComponent<Slider>();
            per.value=(float)receive[i].Per/100;//実際

            Slider deadline=GameObject.Find("Canvas (1)").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("TasksContent").gameObject.transform.Find("Task"+i.ToString()).gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineSlider").GetComponent<Slider>();
            //1-{期限-今日(日)/期限-登録(日)}
            DateTime now=DateTime.Now;//実際
            DateTime deadline_d=DateTime.Parse(receive[i].Deadline);//実際
            DateTime regdate_d=DateTime.Parse(receive[i].Regdate);//実際
            if((deadline_d-regdate_d).Days>=0){
                deadline.value=1.0f-((float)(deadline_d-now).Days/(float)(deadline_d-regdate_d).Days);//実際
            }
            else{
                deadline.value=1.0f;
            }
            //各プレハブの持つhavetaskobj引数にこのシーンの「GameObject」(サーバからもらったタスクデータを持っている)をアタッチ
            GameObject.Find("Canvas (1)").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("TasksContent").gameObject.transform.Find("Task"+i.ToString()).GetComponent<TaskChoiceButton>().havetaskobj=thisobj;
            //各プレハブの持つthisobjnum(Task〇の〇の部分=i)を設定
            GameObject.Find("Canvas (1)").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("TasksContent").gameObject.transform.Find("Task"+i.ToString()).GetComponent<TaskChoiceButton>().thisobjnum=i;
        }
        checker=false;//反映完了

    }

    public void TempSaveData(int t_id){
        //タスク詳細で表示するデータの一時保存(t_idは何番目のタスクか)    
        PlayerPrefs.SetInt("tasknum",receive[t_id].Tasknum);   
        PlayerPrefs.SetString("task",receive[t_id].Task);
        PlayerPrefs.SetString("regdate",receive[t_id].Regdate);
        PlayerPrefs.SetString("deadline",receive[t_id].Deadline);
        PlayerPrefs.SetString("per",receive[t_id].Per.ToString());
        PlayerPrefs.SetString("important",receive[t_id].Important.ToString());
        PlayerPrefs.SetString("eid",receive[t_id].EnemyID.ToString());
        PlayerPrefs.SetString("memo",receive[t_id].Memo);
        //Debug.Log("OK");
    }

    public void Delete(int tasknum,int enemyID){
        clientsystem.GetComponent<ClientAPI>().mode=7;
        var senddata=new Data_task{
            ID=PlayerBasedata.GetUserid(),
            Tasknum=tasknum,
        };
        clientsystem.GetComponent<ClientAPI>().data2=senddata;
        clientsystem.GetComponent<ClientAPI>().StartAPI();

        //Enemydataも削除
        AboutEnemy.DeleteAsset(enemyID);

    }

}
