//タスク変更
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.IO;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;

public class ChangeTask_toServer : MonoBehaviour
{

    //一時保管したものを取り出すのみ
    private string oldtask;//
    private int enemyID;
    private string regdate;//登録日(DateTime->stringにしたもの)

    //InputFieldなどで取り出すのも含む
    private string newtask;
    private string deadline;//期限
    private int per;//進行度(%
    private int important;//重要度
    private string memo;//memo

    private string taskcheck="none";//taskcheck(完了日/none)

    public bool renew_userdata=false;//ユーザデータ更新の有無(進行度100%時のみ行う)
        
    public GameObject clientsystem; 
    public GameObject checker;//LVやEXP変更時の処理を行うオブジェクト

    public GameObject clearpop;//倒した時に表示されるポップ
    public TextMeshProUGUI defeatenemy;//倒した敵名を表示する

    //各キャラアセットへのアクセス
    [SerializeField] private CharacterStatus characterStatus_yusya;
    [SerializeField] private CharacterStatus characterStatus_warrior;
    [SerializeField] private CharacterStatus characterStatus_madoushi;
    [SerializeField] private CharacterStatus characterStatus_priest;
    
    void Start(){

        //まずは表示
        TMP_InputField taskobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("NameInputField").gameObject.GetComponent<TMP_InputField>();//内容
        TextMeshProUGUI deadlineobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineButton").gameObject.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();//deadline
        Slider importantobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Importance").gameObject.transform.Find("ImportanceSlider").GetComponent<Slider>();//important
        TMP_InputField memoobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Memo").gameObject.transform.Find("MemoInputField").gameObject.GetComponent<TMP_InputField>();//memo
        TMP_InputField perobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Progress").gameObject.transform.Find("ProgressInputField").gameObject.GetComponent<TMP_InputField>();//進行度
        TextMeshProUGUI regdateobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Registered").gameObject.transform.Find("RegisteredDate").GetComponent<TextMeshProUGUI>();//登録日

        Button deadlineobj2=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineButton").gameObject.GetComponent<Button>();//deadline


        //旧タスクデータを取得(表示からタスクを選ぶ関数でPlayerpref使って一時的に保管するようにする)
        oldtask=PlayerPrefs.GetString("task");
        enemyID=Int32.Parse(PlayerPrefs.GetString("eid"));
        regdate=PlayerPrefs.GetString("regdate");

        deadline=PlayerPrefs.GetString("deadline");
        per=Int32.Parse(PlayerPrefs.GetString("per"));
        important=Int32.Parse(PlayerPrefs.GetString("important"));
        memo=PlayerPrefs.GetString("memo");

        //表示(deadlineのみ別スクリプトで表示済み)
        taskobj.text=oldtask;
        regdateobj.text=regdate;
        importantobj.value=(float)important;

        //敵名
        string enemydata=File.ReadAllText(Application.persistentDataPath+"\\Enemydata\\Enemy"+enemyID.ToString()+".json");
        Enemytemp.Enemy data=JsonUtility.FromJson<Enemytemp.Enemy>(enemydata);
        //表示する
        TextMeshProUGUI enemyobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Enemy").gameObject.transform.Find("EnemyDisplay").GetComponent<TextMeshProUGUI>();//敵名
        enemyobj.text=data.enemyname;

        DateTime now=DateTime.Now;
        DateTime deadline_time=DateTime.Parse(deadline);
        //進捗度100%or期限切れの時
        if(per==100 || (deadline_time-now).Days<0){
            //メモ以外操作できなくする
            perobj.interactable=false;
            importantobj.interactable=false;
            taskobj.interactable=false;
            deadlineobj2.interactable=false;
        }
        perobj.text=per.ToString();
        memoobj.text=memo;

    }
    
    // Start is called before the first frame update
    public void OnClick(){
        TMP_InputField taskobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("NameInputField").gameObject.GetComponent<TMP_InputField>();//内容
        TextMeshProUGUI deadlineobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineButton").gameObject.transform.Find("Text (TMP)").gameObject.GetComponent<TextMeshProUGUI>();//deadline
        Slider importantobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Importance").gameObject.transform.Find("ImportanceSlider").GetComponent<Slider>();//important
        TMP_InputField memoobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Memo").gameObject.transform.Find("MemoInputField").gameObject.GetComponent<TMP_InputField>();//memo
        TMP_InputField perobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Progress").gameObject.transform.Find("ProgressInputField").gameObject.GetComponent<TMP_InputField>();//進行度
        TextMeshProUGUI regdateobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Registered").gameObject.transform.Find("RegisteredDate").GetComponent<TextMeshProUGUI>();//登録日
        
        Button deadlineobj2=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Deadline").gameObject.transform.Find("DeadlineButton").gameObject.GetComponent<Button>();//deadline

        //読み取り
        newtask=taskobj.text;
        deadline=deadlineobj.text;
        DateTime tmp=DateTime.Parse(deadline);
        deadline=tmp.ToString("yyyyMMdd");
        regdate=regdateobj.text;
        DateTime tmp2=DateTime.Parse(regdate);
        regdate=tmp2.ToString("yyyyMMdd");
        int deadlineday=(tmp-tmp2).Days;
        important=(int)importantobj.value;
        per=Int32.Parse(perobj.text);  
        memo=memoobj.text;

        

        //敵名を変更(重要度に左右)・敵データベースを書き換え
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
        AboutEnemy.ChangeAsset(enemyID,enemyname,"normal",100,1,1,important,deadlineday);
        //敵名の書き換え
        TextMeshProUGUI enemyobj=GameObject.Find("Canvas").gameObject.transform.Find("UI Task").gameObject.transform.Find("Enemy").gameObject.transform.Find("EnemyDisplay").GetComponent<TextMeshProUGUI>();//敵名
        enemyobj.text=enemyname;


        //進行度100%か否か  
        if(per==100){//クリアした場合
            renew_userdata=true;
            taskcheck=DateTime.Now.ToString("yyyyMMdd");
            //メモ以外操作できなくする
            perobj.interactable=false;
            importantobj.interactable=false;
            taskobj.interactable=false;
            deadlineobj2.interactable=false;

        } 

        //タスク変更送信準備
        clientsystem.GetComponent<ClientAPI>().mode=8;
        var senddata=new Data_task{
            ID=PlayerBasedata.GetUserid(),
            Tasknum=PlayerPrefs.GetInt("tasknum",-1),
            Task=newtask,
            Taskcheck=taskcheck,
            Regdate=regdate,
            Deadline=deadline,
            Per=per,
            Important=important,
            EnemyID=enemyID,
            Memo=memo,
        };
        //予期せぬエラーが起きた場合以外はデータをセット
        if(senddata.Tasknum!=-1){
            clientsystem.GetComponent<ClientAPI>().data2=senddata;
        }
        else{
             clientsystem.GetComponent<ClientAPI>().data2=null;
        }

    }

    //レベルに対応してキャラステータスを変える関数
    private void UpStatus_lv(CharacterStatus chara,int lv){
        chara.SetLevel(lv);

    }

    //タスクの進行度100%時のみ 経験値・レベル上昇処理+ポップ
    public async void Clear(){
        if(renew_userdata==true){
            string enemydata=File.ReadAllText(Application.persistentDataPath+"\\Enemydata\\Enemy"+enemyID.ToString()+".json");
            Enemytemp.Enemy data=JsonUtility.FromJson<Enemytemp.Enemy>(enemydata);
            int exp=PlayerBasedata.GetUserexp()+data.point;
            int lv=PlayerBasedata.GetUserLevel();
            while((PlayerBasedata.GetUserLevel()+1)*100<=exp){
                exp=exp-(PlayerBasedata.GetUserLevel()+1)*100;
                lv+=1;
            }
            PlayerBasedata.SetUserexp(exp);
            PlayerBasedata.SetUserLevel(lv);
            
            //レベル上がるとステータスも変わるので処理をする
            UpStatus_lv(characterStatus_yusya,PlayerBasedata.GetUserLevel());
            UpStatus_lv(characterStatus_warrior,PlayerBasedata.GetUserLevel());
            UpStatus_lv(characterStatus_madoushi,PlayerBasedata.GetUserLevel());
            UpStatus_lv(characterStatus_priest,PlayerBasedata.GetUserLevel()); 

            checker.GetComponent<LevelExpUP>().checker=true;//閉じる時にレベル上げ処理をする

            defeatenemy.text=data.enemyname;
            clearpop.SetActive(true);
            await Task.Delay(5000);
            clearpop.SetActive(false);
        }
    

    }


}
