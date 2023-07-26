using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class TargetButtonController : MonoBehaviour
{
    private Button button;
    private GameObject battleManager;

    // Start is called before the first frame update
    void Start()
    {
        battleManager = GameObject.Find("BattleManager");
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        battleManager.GetComponent<BattleManager>().SelectTarget(transform.Find("TargetNameText").gameObject.GetComponent<TextMeshProUGUI>().text);
    }
}
