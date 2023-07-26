//タスク削除
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class DeleteTask_toServer : MonoBehaviour
{
    public GameObject clientsystem;
    //※表示は変更の方のスクリプトでやっているのでここでは書かない


    public void OnClick(){     
        //タスク削除準備
        clientsystem.GetComponent<ClientAPI>().mode=7;
        var senddata=new Data_task{
            ID=PlayerBasedata.GetUserid(),
            Tasknum=PlayerPrefs.GetInt("tasknum",-1),
        };

        //予期せぬエラーが起きた場合以外はデータをセット
        if(senddata.Tasknum!=-1){
            clientsystem.GetComponent<ClientAPI>().data2=senddata;
        }
        else{
             clientsystem.GetComponent<ClientAPI>().data2=null;
        }

        //Enemydataも削除
        AboutEnemy.DeleteAsset(Int32.Parse(PlayerPrefs.GetString("eid")));

        //最後にタスク一覧に戻る
        SceneManager.LoadScene("TasksScene");
    }
}
