using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

using UnityEditor;


public class LevelExpUP : MonoBehaviour
{
    public bool checker=false;//変更の有無
    public GameObject clientsystem; 

    [SerializeField] private CharacterStatus hero;
    [SerializeField] private CharacterStatus warrior;
    [SerializeField] private CharacterStatus mage;
    [SerializeField] private CharacterStatus priest;


    // Start is called before the first frame update
    void Start()
    {
        checker=false;
    }


    public async void UP(){
        if(checker){  
            //サーバに送る準備
            clientsystem.GetComponent<ClientAPI>().mode=2;
            var senddata=new Data_user{
                ID=PlayerBasedata.GetUserid(),
                Name=PlayerBasedata.GetUsername(),
                Lv=PlayerBasedata.GetUserLevel(),
                Exp=PlayerBasedata.GetUserexp(),
            };
            clientsystem.GetComponent<ClientAPI>().data1=senddata;
            clientsystem.GetComponent<ClientAPI>().StartAPI();
            await Task.Delay(100);

            SceneManager.LoadScene("TasksScene");

        }
        else{
            clientsystem.GetComponent<ClientAPI>().mode=-1;
            await Task.Delay(100);
            SceneManager.LoadScene("TasksScene");

        }
        
        
    }
}
