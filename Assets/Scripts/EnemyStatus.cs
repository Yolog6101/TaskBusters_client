using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "EnemyStatus", menuName = "CreateEnemyStatus")]
public class EnemyStatus : ScriptableObject
{
    [SerializeField] private string enemyName;
    [SerializeField] private int hp;
    [SerializeField] private int mp;
    [SerializeField] private int str;
    [SerializeField] private int mgc;
    [SerializeField] private int spd;
    [SerializeField] private int def;
    [SerializeField] private int res;
    [SerializeField] private float fire;
    [SerializeField] private float ice;
    [SerializeField] private float thunder;
    [SerializeField] private List<Skill> skillList;
    [SerializeField] private Sprite sprite;

    public string GetEnemyName()
    {
        return enemyName;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMP()
    {
        return mp;
    }

    public int GetSTR()
    {
        return str;
    }

    public int GetMGC()
    {
        return mgc;
    }

    public int GetSPD()
    {
        return spd;
    }

    public int GetDEF()
    {
        return def;
    }

    public int GetRES()
    {
        return res;
    }

    public float GetFire()
    {
        return fire;
    }

    public float GetIce()
    {
        return ice;
    }

    public float GetThunder()
    {
        return thunder;
    }

    public List<Skill> GetSkillList()
    {
        return skillList;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
