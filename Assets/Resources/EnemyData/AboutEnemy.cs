//敵データベース関連
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using TMPro;
public class AboutEnemy : MonoBehaviour
{
    private static string assetdirpath_ch=Application.persistentDataPath+"\\Enemydata";
    
    public int id;
    public string enemyname;
    public string enemytype;
    public int hp;
    public int attack;
    public int guard;
    public int point;

    public int important;//重要度(1-3)
    public int deadlineday;//期限までの日程

    public static int CreateAsset(string enemyname,string enemytype,int hp,int attack,int guard,int important,int deadlineday){
        
        Directory.CreateDirectory(assetdirpath_ch);
        
        //IDは存在しないものを選ぶ
        int id=0;
        while(System.IO.File.Exists(Path.Combine(assetdirpath_ch+"\\Enemy"+id.ToString()+".json"))){
            id++;
        }
        string newfilepath=Path.Combine(assetdirpath_ch+"\\Enemy"+id.ToString()+".json");

        //ScriptableObjectは読み取りできるけど編集が無理→JSONを使う
        var makedata=new Enemytemp.Enemy{
            id=id,
            enemyname=enemyname,
            enemytype=enemytype,
            hp=hp,
            attack=attack,
            guard=guard,
            point=SetPoint(important,deadlineday),
        };
        var makejson=JsonUtility.ToJson(makedata);
        File.WriteAllText(newfilepath,makejson);
    
        return id;
    }

    public static void ChangeAsset(int id,string enemyname,string enemytype,int hp,int attack,int guard,int important,int deadlineday){
        Directory.CreateDirectory(assetdirpath_ch);
        
        //上書き
        string enemydata=File.ReadAllText(assetdirpath_ch+"\\Enemy"+id.ToString()+".json");
        Enemytemp.Enemy data=JsonUtility.FromJson<Enemytemp.Enemy>(enemydata);
        var makedata=new Enemytemp.Enemy{
            id=id,
            enemyname=enemyname,
            enemytype=enemytype,
            hp=hp,
            attack=attack,
            guard=guard,
            point=SetPoint(important,deadlineday),
        };
        var makejson=JsonUtility.ToJson(makedata); 
        File.WriteAllText(assetdirpath_ch+"\\Enemy"+id.ToString()+".json",makejson);

    }

    public static void DeleteAsset(int id){
        File.Delete(assetdirpath_ch+"\\Enemy"+id.ToString()+".json");
    }

    public static int SetPoint(int important,int deadlineday){
        //敵のpoint(熟練度)を決める 重要度×登録日or変更日から期限までの日数×10
        return important*deadlineday*10;
    }


}
