using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "SelectedEnemy", menuName = "CreateSelectedEnemy")]
public class SelectedEnemy : ScriptableObject
{
    [SerializeField] private EnemyStatus enemyStatus;

    public void SetEnemyStatus(EnemyStatus enemyStatus)
    {
        this.enemyStatus = enemyStatus;
    }

    public EnemyStatus GetEnemyStatus()
    {
        return enemyStatus;
    }
}
