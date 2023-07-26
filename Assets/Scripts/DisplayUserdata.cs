using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//プレイヤーデータを更新取得して任意場所に表示
public class DisplayUserdata : MonoBehaviour
{
    public GameObject clientsystem;
    public TextMeshProUGUI name;
    public TextMeshProUGUI id;


    public void Renew(){
        //更新準備(PlayerpBasedata)
        clientsystem.GetComponent<ClientAPI>().mode=1;
        var senddata=new Data_user{
            ID=PlayerBasedata.GetUserid(),
        };
        clientsystem.GetComponent<ClientAPI>().data1=senddata;
    }

    public void Display(){
        if(name!=null){
        name.text=PlayerBasedata.GetUsername();
        }
        if(id!=null){
        id.text=PlayerBasedata.GetUserid().ToString();
        }
    }
    
}
