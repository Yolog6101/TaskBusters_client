using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;//Encoding用
using System;

public class ClientAPI:MonoBehaviour
{
    //シーン内オブジェクトごとに設定できる
    public int mode;
    //送信データ受け取り口
    public Data_user data1;
    public Data_task data2;
    public Data_friend data3;
    //受信データ送信先(オブジェクト)
    public GameObject receiving;
    

    
    public void StartAPI(){
        StartCoroutine(Gets(mode,data1,data2,data3));
    }
    IEnumerator Gets(int mode,Data_user user=null,Data_task task=null,Data_friend friend=null){
        //ベースとなるURL
        string baseurl="http://35.79.81.242:80";//サーバープログラムがデプロイされている仮想サーバー

        //mode...処理切り替え
        string url=baseurl;
        UnityWebRequest req=new UnityWebRequest(url);
        string senddata;
        byte[] senddata_byte;

        switch(mode){
            case -1:
                //何もしない処理
                break;
            case 0:
                //ユーザ登録(ただし、ID登録済みの場合は登録しないようにする)
                url=baseurl+"/reguser";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(user);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                        string rawdata=req.downloadHandler.text;//jsonデータが返ってくる(serverbase.go参照)
                        req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                        Data_user receive=JsonUtility.FromJson<Data_user>(rawdata);
                        PlayerBasedata.SetUserid(receive.ID);
                        PlayerBasedata.SetUserLevel(receive.Lv);
                        PlayerBasedata.SetUserexp(receive.Exp);

                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;            
            case 1:
                //ユーザデータを取得・PlayerBasedataを更新
                url=baseurl+"/getuser";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(user);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                        string rawdata=req.downloadHandler.text;//jsonデータが返ってくる(serverbase.go参照)
                        req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                        //json->class
                        Data_user receive=JsonUtility.FromJson<Data_user>(rawdata);
                        PlayerBasedata.SetUsername(receive.Name);
                        PlayerBasedata.SetUserLevel(receive.Lv);
                        PlayerBasedata.SetUserexp(receive.Exp);
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;
            case 2:
                //ユーザデータの更新
                url=baseurl+"/chuser";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(user);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;
            case 3:
                //友人検索(ユーザデータ取得)
                url=baseurl+"/getuser";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(user);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                        string rawdata=req.downloadHandler.text;//jsonデータが返ってくる(serverbase.go参照)
                        req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                        Data_user receive=JsonUtility.FromJson<Data_user>(rawdata);
                        receiving.GetComponent<FriendSearch>().userdata=receive;//FriendSearch.cs用
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;
            case 4:
                url=baseurl+"/regfri";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(friend);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                break;
            case 5:
                //タスク取得
                url=baseurl+"/taketask";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(user);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                    req.Dispose();
                }
                else{
                    if(req.responseCode==200){//成功
                        string rawdata=req.downloadHandler.text;//jsonデータが返ってくる(serverbase.go参照)
                        req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                        var datalist=new List<Data_task>();
                        DateTime now=DateTime.Now;
                        Data_task tmp;
                        var deletelist=new List<int>();
                        var deleteenemylist=new List<int>();
                        for(int i=0;i<Int32.Parse(rawdata.Split('{')[0]);i++){ 
                            tmp=JsonUtility.FromJson<Data_task>("{"+rawdata.Split('{')[i+1]);
                            if((DateTime.Parse(tmp.Deadline)-now).Days<=-10){
                                //期限から10日経過していれば削除
                                deletelist.Add(i);
                                deleteenemylist.Add(tmp.EnemyID);
                            }
                            else{
                                datalist.Add(tmp);
                                datalist[datalist.Count-1].Tasknum=i;//tasknumはデータベースに準拠するため
                            }
                        }

                        Data_task[] data=new Data_task[datalist.Count];//定義しておく

                        for(int i=0;i<datalist.Count;i++){
                            data[i]=datalist[i];
                        }

                        //データ数とタスクデータをreceivingに
                        receiving.GetComponent<Taskgetfromserver>().tasknum=datalist.Count;
                        receiving.GetComponent<Taskgetfromserver>().delete=deletelist;
                        receiving.GetComponent<Taskgetfromserver>().deleteenemy=deleteenemylist;
                        receiving.GetComponent<Taskgetfromserver>().receive=data;
                        receiving.GetComponent<Taskgetfromserver>().checker=true;//受信完了
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                        req.Dispose();
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;
            case 6:
                //タスク登録
                url=baseurl+"/posttask";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(task);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
            
                break;
            case 7:
                //タスク削除
                url=baseurl+"/deltask";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(task);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)

                break;
            case 8:
                //タスク変更
                url=baseurl+"/chtask";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(task);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                break;
            case 9:
                url=baseurl+"/chfri";
                req=new UnityWebRequest(url);
                senddata=JsonUtility.ToJson(friend);
                senddata_byte=Encoding.UTF8.GetBytes(senddata);
                req.uploadHandler=new UploadHandlerRaw(senddata_byte);
                req.downloadHandler=new DownloadHandlerBuffer();
                req.SetRequestHeader("Content-Type", "application/json");
                req.method = UnityWebRequest.kHttpVerbPOST;
                yield return req.SendWebRequest();
                if(req.result==UnityWebRequest.Result.ConnectionError){
                    Debug.Log(req.error);
                }
                else{
                    if(req.responseCode==200){//成功
                        string rawdata=req.downloadHandler.text;//jsonデータが返ってくる(serverbase.go参照)
                        req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                        receiving.GetComponent<Share_friendtask>().friendnum=Int32.Parse(rawdata.Split('{')[0]);
                        Data_friendscore[] data=new Data_friendscore[Int32.Parse(rawdata.Split('{')[0])];
                        for(int i=0;i<Int32.Parse(rawdata.Split('{')[0]);i++){
                            data[i]=JsonUtility.FromJson<Data_friendscore>("{"+rawdata.Split('{')[i+1]);
                        }
                        receiving.GetComponent<Share_friendtask>().receive=data;
                        receiving.GetComponent<Share_friendtask>().checker=true;
                    }
                    else{//エラー
                        Debug.Log(req.responseCode);
                    }
                }
                req.Dispose();//使い終わったらDisposeさせる(メモリリーク回避)
                break;
            default:
                break;
        }

    }
}

[System.Serializable]
public class Data_user{
    public int ID;
    public string Name;
    public int Lv;
    public int Exp;
}

[System.Serializable]
public class Data_task{
    public int ID;
    public int Tasknum;
    public string Task;
    public string Taskcheck;
    public string Regdate;
    public string Deadline;
    public int Per;
    public int Important;
    public int EnemyID;
    public string Memo;
}

[System.Serializable]
public class Data_friend{
    public int ID;
    public int FID;
}

[System.Serializable]
public class Data_friendscore{
    public int ID;
    public string Name;
    public int Lv;
    public int Exp;
    public int Complete;
    public int Total;
}