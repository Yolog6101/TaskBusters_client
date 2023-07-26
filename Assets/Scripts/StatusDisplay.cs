using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class StatusDisplay : MonoBehaviour
{
     //各キャラ・ステータスアセットへのアクセス
    [SerializeField] private CharacterStatus characterStatus_hero;//GameObjectとして呼び出す(GwtComponentするため)
    [SerializeField] private TextMeshProUGUI heroHPText;
    [SerializeField] private TextMeshProUGUI heroMPText;
    [SerializeField] private TextMeshProUGUI heroPowerText;
    [SerializeField] private TextMeshProUGUI heroMagicText;
    [SerializeField] private TextMeshProUGUI heroVText;
    [SerializeField] private TextMeshProUGUI heroDefenseText;
    [SerializeField] private TextMeshProUGUI heroResistText;

    [SerializeField] private CharacterStatus characterStatus_warrior;
    [SerializeField] private TextMeshProUGUI warriorHPText;
    [SerializeField] private TextMeshProUGUI warriorMPText;
    [SerializeField] private TextMeshProUGUI warriorPowerText;
    [SerializeField] private TextMeshProUGUI warriorMagicText;
    [SerializeField] private TextMeshProUGUI warriorVText;
    [SerializeField] private TextMeshProUGUI warriorDefenseText;
    [SerializeField] private TextMeshProUGUI warriorResistText;

    [SerializeField] private CharacterStatus characterStatus_mage;
    [SerializeField] private TextMeshProUGUI mageHPText;
    [SerializeField] private TextMeshProUGUI mageMPText;
    [SerializeField] private TextMeshProUGUI magePowerText;
    [SerializeField] private TextMeshProUGUI mageMagicText;
    [SerializeField] private TextMeshProUGUI mageVText;
    [SerializeField] private TextMeshProUGUI mageDefenseText;
    [SerializeField] private TextMeshProUGUI mageResistText;

    [SerializeField] private CharacterStatus characterStatus_priest;
    [SerializeField] private TextMeshProUGUI priestHPText;
    [SerializeField] private TextMeshProUGUI priestMPText;
    [SerializeField] private TextMeshProUGUI priestPowerText;
    [SerializeField] private TextMeshProUGUI priestMagicText;
    [SerializeField] private TextMeshProUGUI priestVText;
    [SerializeField] private TextMeshProUGUI priestDefenseText;
    [SerializeField] private TextMeshProUGUI priestResistText;

    // Start is called before the first frame update
    void Start()
    {
        //プロフィール表示(username,lv,explvはクライアント保持情報かつステータス画面では変動しないのでそのまま表示)
        TextMeshProUGUI unameobj=GameObject.Find("Canvas").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("Username").gameObject.transform.Find("UsernameValue").GetComponent<TextMeshProUGUI>();
        unameobj.text=PlayerBasedata.GetUsername();
        TextMeshProUGUI ulvobj=GameObject.Find("Canvas").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("Level").gameObject.transform.Find("LevelValue").GetComponent<TextMeshProUGUI>();
        ulvobj.text=PlayerBasedata.GetUserLevel().ToString();
        Slider uexplvobj=GameObject.Find("Canvas").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("NextLevel").gameObject.transform.Find("NextLevelSlider").GetComponent<Slider>();
        TextMeshProUGUI uexplvobj2=GameObject.Find("Canvas").gameObject.transform.Find("UI").gameObject.transform.Find("Scroll View").gameObject.transform.Find("Viewport").gameObject.transform.Find("Content").gameObject.transform.Find("NextLevel").gameObject.transform.Find("NextLevelValue").GetComponent<TextMeshProUGUI>();

        //PlayerBasedata.GetUserexp()//現在の経験値(レベル上昇で0にリセット)
        float expper=(float)PlayerBasedata.GetUserexp()/((float)(PlayerBasedata.GetUserLevel()+1)*100);
        uexplvobj.value=expper*100;//[0,100]表示のため
        uexplvobj2.text=((PlayerBasedata.GetUserLevel()+1)*100-PlayerBasedata.GetUserexp()).ToString();


        //キャラクターレベル設定・ステータス調整
        //(UnityEditor系はAssetに入れて使えない&Scriptableobjectはシーンを切り替えると初期化される(.assetを変更していない)ため毎度設定する)
        characterStatus_hero.SetLevel(PlayerBasedata.GetUserLevel());
        characterStatus_warrior.SetLevel(PlayerBasedata.GetUserLevel());
        characterStatus_mage.SetLevel(PlayerBasedata.GetUserLevel());
        characterStatus_priest.SetLevel(PlayerBasedata.GetUserLevel());


        //ステータス表示
        //character statusデータアセットにあるのでそれをアタッチする
        heroHPText.text=characterStatus_hero.GetHP().ToString();
        heroMPText.text=characterStatus_hero.GetMP().ToString();
        heroPowerText.text=characterStatus_hero.GetSTR().ToString();
        heroMagicText.text=characterStatus_hero.GetMGC().ToString();       
        heroVText.text=characterStatus_hero.GetSPD().ToString();
        heroDefenseText.text=characterStatus_hero.GetDEF().ToString();
        heroResistText.text=characterStatus_hero.GetRES().ToString();

        warriorHPText.text=characterStatus_warrior.GetHP().ToString();
        warriorMPText.text=characterStatus_warrior.GetMP().ToString();
        warriorPowerText.text=characterStatus_warrior.GetSTR().ToString();
        warriorMagicText.text=characterStatus_warrior.GetMGC().ToString();       
        warriorVText.text=characterStatus_warrior.GetSPD().ToString();
        warriorDefenseText.text=characterStatus_warrior.GetDEF().ToString();
        warriorResistText.text=characterStatus_warrior.GetRES().ToString();

        mageHPText.text=characterStatus_mage.GetHP().ToString();
        mageMPText.text=characterStatus_mage.GetMP().ToString();
        magePowerText.text=characterStatus_mage.GetSTR().ToString();
        mageMagicText.text=characterStatus_mage.GetMGC().ToString();       
        mageVText.text=characterStatus_mage.GetSPD().ToString();
        mageDefenseText.text=characterStatus_mage.GetDEF().ToString();
        mageResistText.text=characterStatus_mage.GetRES().ToString();

        priestHPText.text=characterStatus_priest.GetHP().ToString();
        priestMPText.text=characterStatus_priest.GetMP().ToString();
        priestPowerText.text=characterStatus_priest.GetSTR().ToString();
        priestMagicText.text=characterStatus_priest.GetMGC().ToString();       
        priestVText.text=characterStatus_priest.GetSPD().ToString();
        priestDefenseText.text=characterStatus_priest.GetDEF().ToString();
        priestResistText.text=characterStatus_priest.GetRES().ToString();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
