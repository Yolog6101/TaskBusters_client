using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class BossButtonController : MonoBehaviour
{
    private Button button;
    [SerializeField] private TextMeshProUGUI enemyText;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private SelectedEnemy selectedEnemy;
    [SerializeField] private EnemyStatus enemy;
    
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
        enemyText.text = buttonText.text;
        selectedEnemy.SetEnemyStatus(enemy);
    }
}
