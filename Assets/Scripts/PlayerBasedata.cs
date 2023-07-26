//ユーザが最低限持つデータの受け取り・取り出し
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//https://qiita.com/riekure/items/3fd4526b13d8e89a7fc6

public class PlayerBasedata : MonoBehaviour
{
    private static bool startup=false;

    // //初期化(起動時のみ)
    // void Start(){
    //     Debug.Log(startup);
    //     if(!startup){
    //         Initplayerdata();
    //         startup=true;
    //     }
    // }

    public static void SetUsername(string uname){
        PlayerPrefs.SetString("UserName", uname);
    }
    public static string GetUsername(){
        return PlayerPrefs.GetString("UserName");
    }
    public static void SetUserid(int uid){
        PlayerPrefs.SetInt("UserID",uid);
    }
    public static int GetUserid(){
        //未登録=-1
        return PlayerPrefs.GetInt("UserID",-1);
    }
    public static void SetUserLevel(int lv){
        PlayerPrefs.SetInt("Level",lv);
    }
    public static int GetUserLevel(){
        return PlayerPrefs.GetInt("Level");
    }
    public static void SetUserexp(int exp){
        PlayerPrefs.SetInt("Exp",exp);
    }
    public static int GetUserexp(){
        return PlayerPrefs.GetInt("Exp");
    }
    public static void SetIP(string ip){
        PlayerPrefs.SetString("IP",ip);
    }
    public static string GetIP(){
        return PlayerPrefs.GetString("IP");
    }

    // public static void Initplayerdata(){
    //     PlayerPrefs.DeleteAll();
        
    // }

}
