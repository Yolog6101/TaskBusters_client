using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

using System.Threading.Tasks;
using UnityEngine.UI;

public class FriendSearch : MonoBehaviour
{
    public GameObject clientsystem;
    public TMP_InputField enterid;
    public TextMeshProUGUI name;
    public TextMeshProUGUI id;

    public GameObject pop;
    public Button button;

    public Data_user userdata;


    public void Search(){
        //友人検索準備
        clientsystem.GetComponent<ClientAPI>().mode=3;
        var senddata=new Data_user{
            ID=int.Parse(enterid.text),
        };
        clientsystem.GetComponent<ClientAPI>().data1=senddata;
    }

    public async void Display(){
        //一時的にボタンを無効化する
        button.interactable = false;
        await Task.Delay(1000);
        button.interactable = true;
        //友人表示(直後は反映されない)

        if(userdata.Name!=""){
            name.text=userdata.Name;
            id.text=userdata.ID.ToString();
            pop.SetActive(true);
        }   
    }

    public void Addfriend(){
        //登録準備
        clientsystem.GetComponent<ClientAPI>().mode=4;
        var senddata=new Data_friend{
            ID=PlayerBasedata.GetUserid(),
            FID=int.Parse(id.text),
        };
        clientsystem.GetComponent<ClientAPI>().data3=senddata;
        userdata=null;//初期化
    }

    public void init(){
        userdata.Name="";
        userdata.ID=0;
        userdata.Lv=0;
        userdata.Exp=0;
    }

}
