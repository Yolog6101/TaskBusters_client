using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossesButtonController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToBosses()
    {
        //IDがある場合のみ移動
        if(PlayerBasedata.GetUserid()!=-1){
            SceneManager.LoadScene("BossesScene");
            //Debug.Log("Go to Bosses Scene");
        }
    }
}
