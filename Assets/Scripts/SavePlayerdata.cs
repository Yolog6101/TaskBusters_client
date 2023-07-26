using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SavePlayerdata : MonoBehaviour
{
    public GameObject clientsystem;
    public TMP_InputField name;
    public GameObject login;
    
    //更新時用(setting)
    public GameObject change_icon_button;
    public GameObject change_name_field;
    public GameObject save_button;


    // Start is called before the first frame update
    public void Saveusernamedata(){
        //ユーザ名をクライアント自身に登録
        //まだIDを持っていない場合はサーバに送信してIDをもらう
        //そうでない場合は更新データをサーバに送信する

        PlayerBasedata.SetUsername(name.text);
        var senddata=new Data_user{
                Name=name.text,
        };
        if(PlayerBasedata.GetUserid()==-1){
            //新規登録
            clientsystem.GetComponent<ClientAPI>().mode=0;//ClientAPIにセット
            login.GetComponent<SignUpController>().isLoginProperty=true;
            //登録済みにする　参考 https://note.com/08_14/n/n0fe88efe0159
            
        }
        else{
            //以下、更新処理
            senddata.ID=PlayerBasedata.GetUserid();
            senddata.Lv=PlayerBasedata.GetUserLevel();
            senddata.Exp=PlayerBasedata.GetUserexp();
            clientsystem.GetComponent<ClientAPI>().mode=2;//ClientAPIにセット
            
            change_icon_button.SetActive(false);
            change_name_field.SetActive(false);
            save_button.SetActive(false);

        }
        clientsystem.GetComponent<ClientAPI>().data1=senddata;//ClientAPIにセット
    }
    
}
