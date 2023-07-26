using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Share_friendtask : MonoBehaviour
{
    public GameObject clientsystem;
    public int friendnum;
    public Data_friendscore[] receive;
    public bool checker=false;

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject content;
   
    // Start is called before the first frame update
    void Start()
    {    
        clientsystem.GetComponent<ClientAPI>().mode=9;
        var senddata=new Data_friend{
            ID=PlayerBasedata.GetUserid(),
        };
        clientsystem.GetComponent<ClientAPI>().data3=senddata;
        clientsystem.GetComponent<ClientAPI>().StartAPI();   
    }

    void Update(){
        if(checker){
            //Debug.Log(receive);
            generate(friendnum);   
        }
        checker=false;
    }

    //makedata
    public void generate(int num){
        int i;
        Vector2 prefablocate=prefab.GetComponent<RectTransform>().sizeDelta;
        for(i=0;i<num;i++){
            //生成
            GameObject p=Instantiate(prefab,Vector3.zero,Quaternion.identity);
            p.name="Friend"+i.ToString();
            //content以下にセット
            p.transform.SetParent(content.transform,false);
            Vector2 anchor=new Vector2(0,-prefablocate.y/2*(2*i+1));
            p.GetComponent<RectTransform>().anchoredPosition=anchor;

            //適用
            p.gameObject.transform.Find("Name").GetComponent<TextMeshProUGUI>().text=receive[i].Name;
            p.gameObject.transform.Find("Level").gameObject.transform.Find("Lv").GetComponent<TextMeshProUGUI>().text=receive[i].Lv.ToString();
            p.gameObject.transform.Find("Exp").gameObject.transform.Find("exp").GetComponent<TextMeshProUGUI>().text=receive[i].Exp.ToString();
            p.gameObject.transform.Find("Task").gameObject.transform.Find("Tasktext").GetComponent<TextMeshProUGUI>().text=receive[i].Complete+"/"+receive[i].Total;
            


        }
        // Vector2 contentlocate=content.GetComponent<RectTransform>().sizeDelta;
        // contentlocate.y=prefablocate.y*i;
        // content.GetComponent<RectTransform>().sizeDelta=contentlocate;
    }

}
