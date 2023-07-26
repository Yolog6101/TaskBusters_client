using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeIcon : MonoBehaviour
{
    public List<Sprite> iconlist;//アイコン候補一覧(Spriteしてます)
    private int iconlistnum;//アイコン候補数
    private static int num=0;//現在選択中のアイコン番号(<iconlistnum)

    public GameObject icon;//アイコンオブジェクト
    public GameObject icon_fornormal;//登録後のポップのアイコンオブジェクト
  

    public void Display(){
        num=PlayerPrefs.GetInt("iconID",-1);
        if(num==-1){
            num=0;
        }
        iconlistnum=iconlist.Count;
        icon.GetComponent<Image>().sprite=iconlist[num];
        icon_fornormal.GetComponent<Image>().sprite=iconlist[num];
        
    }

    public void OnClick(){
        num++;
        if(num==iconlistnum){
            num=0;
        }
        icon.GetComponent<Image>().sprite=iconlist[num];

    }

    public void Seticon(){
        icon_fornormal.GetComponent<Image>().sprite=icon.GetComponent<Image>().sprite;
        PlayerPrefs.SetInt("iconID",num);
    }
}
