using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonController : MonoBehaviour
{
    public GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test()
    {
        Vector2 sd = content.GetComponent<RectTransform>().sizeDelta;
        sd.y = 800;
        content.GetComponent<RectTransform>().sizeDelta = sd;
    }
}
