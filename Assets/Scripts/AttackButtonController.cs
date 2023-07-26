using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class AttackButtonController : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject battleManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Clicked()
    {
        battleManager.GetComponent<BattleManager>().SelectAttack();
    }
}
