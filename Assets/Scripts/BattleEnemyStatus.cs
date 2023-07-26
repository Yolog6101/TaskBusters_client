using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyStatus : MonoBehaviour
{
    [SerializeField] private SelectedEnemy selectedEnemy;
    private EnemyStatus enemyStatus;
    private string enemyName;
    private int hp;
    private int maxHP;
    private int mp;
    private int maxMP;
    private int str;
    private int mgc;
    private int spd;
    private int def;
    private int res;
    private float fire;
    private float ice;
    private float thunder;
    private int strRank;
    private int mgcRank;
    private int spdRank;
    private int defRank;
    private int resRank;
    private int strRankTurn;
    private int mgcRankTurn;
    private int spdRankTurn;
    private int defRankTurn;
    private int resRankTurn;
    private List<Skill> skillList;

    // Start is called before the first frame update
    void Start()
    {
        /*enemyStatus = selectedEnemy.GetEnemyStatus();
        hp = enemyStatus.GetHP();
        maxHP = hp;
        mp = enemyStatus.GetMP();
        maxMP = mp;
        str = enemyStatus.GetSTR();
        mgc = enemyStatus.GetMGC();
        spd = enemyStatus.GetSPD();
        def = enemyStatus.GetDEF();
        res = enemyStatus.GetRES();
        fire = enemyStatus.GetFire();
        ice = enemyStatus.GetIce();
        thunder = enemyStatus.GetThunder();
        skillList = enemyStatus.GetSkillList();
        strRank = 0;
        mgcRank = 0;
        spdRank = 0;
        defRank = 0;
        resRank = 0;
        strRankTurn = 0;
        mgcRankTurn = 0;
        spdRankTurn = 0;
        defRankTurn = 0;
        resRankTurn = 0;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstSettings()
    {
        enemyStatus = selectedEnemy.GetEnemyStatus();
        enemyName = enemyStatus.GetEnemyName();
        hp = enemyStatus.GetHP();
        maxHP = hp;
        mp = enemyStatus.GetMP();
        maxMP = mp;
        str = enemyStatus.GetSTR();
        mgc = enemyStatus.GetMGC();
        spd = enemyStatus.GetSPD();
        def = enemyStatus.GetDEF();
        res = enemyStatus.GetRES();
        fire = enemyStatus.GetFire();
        ice = enemyStatus.GetIce();
        thunder = enemyStatus.GetThunder();
        skillList = enemyStatus.GetSkillList();
        strRank = 0;
        mgcRank = 0;
        spdRank = 0;
        defRank = 0;
        resRank = 0;
        strRankTurn = 0;
        mgcRankTurn = 0;
        spdRankTurn = 0;
        defRankTurn = 0;
        resRankTurn = 0;
    }

    public string GetEnemyName()
    {
        return enemyName;
    }

    public void SetHP(int hp)
    {
        this.hp = hp;
    }

    public int GetHP()
    {
        return hp;
    }

    public int GetMaxHP()
    {
        return maxHP;
    }
    
    public void SetMP(int mp)
    {
        this.mp = mp;
    }

    public int GetMP()
    {
        return mp;
    }

    public int GetMaxMP()
    {
        return maxMP;
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

    public void SetFire(float fire)
    {
        this.fire = fire;
    }

    public float GetFire()
    {
        return fire;
    }

    public void SetIce(float ice)
    {
        this.ice = ice;
    }

    public float GetIce()
    {
        return ice;
    }

    public void SetThunder(float thunder)
    {
        this.thunder = thunder;
    }

    public float GetThunder()
    {
        return thunder;
    }

    public void SetSkillList(List<Skill> skillList)
    {
        this.skillList = skillList;
    }

    public List<Skill> GetSkillList()
    {
        return skillList;
    }

    public void SetSTRRank(int strRank)
    {
        this.strRank = strRank;
    }

    public int GetSTRRank()
    {
        return strRank;
    }

    public void SetMGCRank(int mgcRank)
    {
        this.mgcRank = mgcRank;
    }

    public int GetMGCRank()
    {
        return mgcRank;
    }

    public void SetSPDRank(int spdRank)
    {
        this.spdRank = spdRank;
    }

    public int GetSPDRank()
    {
        return spdRank;
    }

    public void SetDEFRank(int defRank)
    {
        this.defRank = defRank;
    }

    public int GetDEFRank()
    {
        return defRank;
    }

    public void SetRESRank(int resRank)
    {
        this.resRank = resRank;
    }

    public int GetRESRank()
    {
        return resRank;
    }

    public void SetSTRRankTurn(int strRankTurn)
    {
        this.strRankTurn = strRankTurn;
    }

    public int GetSTRRankTurn()
    {
        return strRankTurn;
    }

    public void SetMGCRankTurn(int mgcRankTurn)
    {
        this.mgcRankTurn = mgcRankTurn;
    }

    public int GetMGCRankTurn()
    {
        return mgcRankTurn;
    }

    public void SetSPDRankTurn(int spdRankTurn)
    {
        this.spdRankTurn = spdRankTurn;
    }

    public int GetSPDRankTurn()
    {
        return spdRankTurn;
    }

    public void SetDEFRankTurn(int defRankTurn)
    {
        this.defRankTurn = defRankTurn;
    }

    public int GetDEFRankTurn()
    {
        return defRankTurn;
    }

    public void SetRESRankTurn(int resRankTurn)
    {
        this.resRankTurn = resRankTurn;
    }

    public int GetRESRankTurn()
    {
        return resRankTurn;
    }
}
