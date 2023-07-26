using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePlayerInfoButtonController : MonoBehaviour
{
    public GameObject change_icon_button;
    public GameObject change_name_field;
    public GameObject save_button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartEditPlayersInfo(){
        change_icon_button.SetActive(true);
        change_name_field.SetActive(true);
        save_button.SetActive(true);
    }
}
