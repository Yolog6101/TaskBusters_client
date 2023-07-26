using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClosePopupButtonController : MonoBehaviour
{
    public GameObject popup_window;
    //複数ある場合(ポップ内ポップなど)
    public GameObject popup_window2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseThePopupWindow(){
        popup_window.SetActive(false);
        if(popup_window2!=null){
        popup_window2.SetActive(false);
        }
    }
}
