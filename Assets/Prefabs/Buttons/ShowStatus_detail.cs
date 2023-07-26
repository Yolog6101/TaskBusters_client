using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShowStatus_detail : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //ボタン内のオブジェクト取得
        TextMeshProUGUI unameobj=GameObject.Find("ShowStatusButton").gameObject.transform.Find("UsernameText").GetComponent<TextMeshProUGUI>();
        unameobj.text=PlayerBasedata.GetUsername();
        TextMeshProUGUI ulvobj=GameObject.Find("ShowStatusButton").gameObject.transform.Find("Level").gameObject.transform.Find("LevelValue").GetComponent<TextMeshProUGUI>();
        ulvobj.text=PlayerBasedata.GetUserLevel().ToString();
        Slider uexpobj=GameObject.Find("ShowStatusButton").gameObject.transform.Find("Level").gameObject.transform.Find("LevelSlider").GetComponent<Slider>();
        float expper=(float)PlayerBasedata.GetUserexp()/((float)(PlayerBasedata.GetUserLevel()+1)*100);
        uexpobj.value=expper;//[0,1]表示のため

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
