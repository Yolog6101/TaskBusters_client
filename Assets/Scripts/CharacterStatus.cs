using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

[Serializable]
[CreateAssetMenu(fileName = "CharacterStatus", menuName = "CreateCharacterStatus")]
public class CharacterStatus : ScriptableObject
{
    [SerializeField] private string characterName;
    [SerializeField] private int level;
    [SerializeField] private int hp;
    [SerializeField] private int mp;
    [SerializeField] private int str;
    [SerializeField] private int mgc;
    [SerializeField] private int spd;
    [SerializeField] private int def;
    [SerializeField] private int res;
    [SerializeField] private List<Skill> skillList;
    [SerializeField] private List<Skill> allSkillList;

    public void SetCharacterName(string characterName)
    {
        this.characterName = characterName;
    }

    public string GetCharacterName()
    {
        return characterName;
    }

    public void SetLevel(int level)
    {
        if(this.level != level)
        {
            this.level = level;
            if(characterName == "勇者")
            {
                SetHeroStatus();
            }
            else if(characterName == "戦士")
            {
                SetWarriorStatus();
            }
            else if(characterName == "魔導士")
            {
                SetMageStatus();
            }
            else if(characterName == "神官")
            {
                SetPriestStatus();
            }
        }
    }

    private void SetHeroStatus()
    {
        hp = 15 + (level - 1) * 5 / 3;
        mp = 10 + (level - 1) * 4 / 5;
        str = 6 + (level - 1);
        mgc = 6 + (level - 1);
        spd = 6 + (level - 1);
        def = 6 + (level - 1);
        res = 6 + (level - 1);
        skillList = new List<Skill>();
        if(level >= 5)
        {
            skillList.Add(allSkillList[0]);
        }
        if(level >= 10)
        {
            skillList.Add(allSkillList[1]);
        }
        if(level >= 15)
        {
            skillList.Add(allSkillList[2]);
        }
        if(level >= 20)
        {
            skillList.Add(allSkillList[3]);
        }
        if(level >= 25)
        {
            skillList.Add(allSkillList[4]);
        }
        if(level >= 30)
        {
            skillList.Add(allSkillList[5]);
        }
        if(level >= 35)
        {
            skillList.Add(allSkillList[6]);
        }
        if(level >= 40)
        {
            skillList.Add(allSkillList[7]);
        }
        if(level >= 45)
        {
            skillList.Add(allSkillList[8]);
        }
        if(level >= 60)
        {
            skillList.Add(allSkillList[9]);
        }
        if(level >= 65)
        {
            skillList.Add(allSkillList[10]);
        }
        if(level >= 70)
        {
            skillList.Add(allSkillList[11]);
        }
        if(level >= 80)
        {
            skillList.Add(allSkillList[12]);
        }
        if(level >= 90)
        {
            skillList.Add(allSkillList[13]);
        }
    }

    private void SetWarriorStatus()
    {
        hp = 20 + (level - 1) * 2;
        mp = 5 + (level - 1) / 3;
        str = 9 + (level - 1) * 5 / 4;
        mgc = 1 + (level - 1) / 3;
        spd = 3 + (level - 1) * 2 / 3;
        def = 10 + (level - 1) * 4 / 3;
        res = 2 + (level - 1) / 2;
        skillList = new List<Skill>();
        if(level >= 10)
        {
            skillList.Add(allSkillList[0]);
        }
        if(level >= 20)
        {
            skillList.Add(allSkillList[1]);
        }
        if(level >= 30)
        {
            skillList.Add(allSkillList[2]);
        }
        if(level >= 40)
        {
            skillList.Add(allSkillList[3]);
        }
        if(level >= 50)
        {
            skillList.Add(allSkillList[4]);
        }
        if(level >= 60)
        {
            skillList.Add(allSkillList[5]);
        }
    }

    private void SetMageStatus()
    {
        hp = 10 + (level - 1);
        mp = 20 + (level - 1) * 3 / 2;
        str = 1 + (level - 1) / 3;
        mgc = 10 + (level - 1) * 4 / 3;
        spd = 7 + (level - 1) * 4 / 3;
        def = 3 + (level - 1) * 3 / 5;
        res = 8 + (level - 1) * 6 / 5;
        skillList = new List<Skill>();
        if(level >= 5)
        {
            skillList.Add(allSkillList[0]);
        }
        if(level >= 10)
        {
            skillList.Add(allSkillList[1]);
        }
        if(level >= 15)
        {
            skillList.Add(allSkillList[2]);
        }
        if(level >= 20)
        {
            skillList.Add(allSkillList[3]);
        }
        if(level >= 25)
        {
            skillList.Add(allSkillList[4]);
        }
        if(level >= 30)
        {
            skillList.Add(allSkillList[5]);
        }
        if(level >= 35)
        {
            skillList.Add(allSkillList[6]);
        }
        if(level >= 40)
        {
            skillList.Add(allSkillList[7]);
        }
        if(level >= 45)
        {
            skillList.Add(allSkillList[8]);
        }
        if(level >= 50)
        {
            skillList.Add(allSkillList[9]);
        }
        if(level >= 55)
        {
            skillList.Add(allSkillList[10]);
        }
        if(level >= 60)
        {
            skillList.Add(allSkillList[11]);
        }
        if(level >= 70)
        {
            skillList.Add(allSkillList[12]);
        }
        if(level >= 75)
        {
            skillList.Add(allSkillList[13]);
        }
        if(level >= 80)
        {
            skillList.Add(allSkillList[14]);
        }
        if(level >= 90)
        {
            skillList.Add(allSkillList[15]);
        }
        if(level >= 100)
        {
            skillList.Add(allSkillList[16]);
        }
    }

    private void SetPriestStatus()
    {
        hp = 12 + (level - 1) * 5 / 4;
        mp = 15 + (level - 1) * 5 / 4;
        str = 4 + (level - 1) * 2 / 3;
        mgc = 6 + (level - 1) * 5 / 4;
        spd = 5 + (level - 1);
        def = 4 + (level - 1) * 2 / 3;
        res = 10 + (level - 1) * 4 / 3;
        skillList = new List<Skill>();
        if(level >= 5)
        {
            skillList.Add(allSkillList[0]);
        }
        if(level >= 10)
        {
            skillList.Add(allSkillList[1]);
        }
        if(level >= 15)
        {
            skillList.Add(allSkillList[2]);
        }
        if(level >= 20)
        {
            skillList.Add(allSkillList[3]);
        }
        if(level >= 25)
        {
            skillList.Add(allSkillList[4]);
        }
        if(level >= 30)
        {
            skillList.Add(allSkillList[5]);
        }
        if(level >= 35)
        {
            skillList.Add(allSkillList[6]);
        }
        if(level >= 40)
        {
            skillList.Add(allSkillList[7]);
        }
        if(level >= 45)
        {
            skillList.Add(allSkillList[8]);
        }
        if(level >= 50)
        {
            skillList.Add(allSkillList[9]);
        }
        if(level >= 55)
        {
            skillList.Add(allSkillList[10]);
        }
        if(level >= 60)
        {
            skillList.Add(allSkillList[11]);
        }
        if(level >= 65)
        {
            skillList.Add(allSkillList[12]);
        }
        if(level >= 70)
        {
            skillList.Add(allSkillList[13]);
        }
        if(level >= 80)
        {
            skillList.Add(allSkillList[14]);
        }
        if(level >= 90)
        {
            skillList.Add(allSkillList[15]);
        }
        if(level >= 100)
        {
            skillList.Add(allSkillList[16]);
        }
    }

    public int GetLevel()
    {
        return level;
    }

    public void SetHP(int hp)
    {
        this.hp = hp;
    }

    public int GetHP()
    {
        return hp;
    }
    
    public void SetMP(int mp)
    {
        this.mp = mp;
    }

    public int GetMP()
    {
        return mp;
    }

    public void SetSTR(int str)
    {
        this.str = str;
    }

    public int GetSTR()
    {
        return str;
    }

    public void SetMGC(int mgc)
    {
        this.mgc = mgc;
    }

    public int GetMGC()
    {
        return mgc;
    }

    public void SetSPD(int spd)
    {
        this.spd = spd;
    }

    public int GetSPD()
    {
        return spd;
    }

    public void SetDEF(int def)
    {
        this.def = def;
    }

    public int GetDEF()
    {
        return def;
    }

    public void SetRES(int res)
    {
        this.res = res;
    }

    public int GetRES()
    {
        return res;
    }

    public void SetSkillList(List<Skill> skillList)
    {
        this.skillList = skillList;
    }

    public List<Skill> GetSkillList()
    {
        return skillList;
    }
}
