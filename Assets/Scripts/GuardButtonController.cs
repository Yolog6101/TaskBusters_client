using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class GuardButtonController : MonoBehaviour
{
    private Button button;
    [SerializeField] private GameObject battleManager;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(Clicked);
    }

    public void Clicked()
    {
        battleManager.GetComponent<BattleManager>().SelectGuard();
    }
}
