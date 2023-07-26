using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
[CreateAssetMenu(fileName = "Skill", menuName = "CreateSkill")]
public class Skill : ScriptableObject
{
    //技名
    [SerializeField] private string skillName;
    //消費MP
    [SerializeField] private int mp;
    //技詳細
    [SerializeField] private string info;
    //技範囲　0:単体　1:全体　2:敵味方全体
    [SerializeField] private int range;
    //技タイプ　0:物理攻撃　1:魔法攻撃　2:回復　3:バフ　4:デバフ　5:属性ガード　6:肩代わり
    [SerializeField] private int skillType;
    //属性　攻撃・属性ガード:(0:無　1:炎　2:氷　3:雷　4:守備ダウン)　回復:(0:回復　1:蘇生　2:MP回復　3:弱体解除)　バフ・デバフ:(0:攻撃　1:魔力　2:速さ　3:守備　4:魔防　5:力&魔力　6:守備&魔防)
    [SerializeField] private int element;
    //倍率　攻撃回復:乗算　バフデバフ:段階　MP回復:回復値
    [SerializeField] private float power;

    public string GetSkillName()
    {
        return skillName;
    }

    public int GetMP()
    {
        return mp;
    }

    public string GetInfo()
    {
        return info;
    }

    public int GetRange()
    {
        return range;
    }

    public int GetSkillType()
    {
        return skillType;
    }

    public int GetElement()
    {
        return element;
    }

    public float GetPower()
    {
        return power;
    }
}
