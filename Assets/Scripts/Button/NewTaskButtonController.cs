using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewTaskButtonController : MonoBehaviour
{
    //Button button;
    
    // Start is called before the first frame update
    void Start()
    {
        //button = GetCompornent<Button>();
        //button.onClick.AddListener(ToNewTask);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToNewTask()
    {
        SceneManager.LoadScene("NewTaskScene");
    }
}
