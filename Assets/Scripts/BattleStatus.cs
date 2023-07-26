using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStatus : MonoBehaviour
{
    [SerializeField] private CharacterStatus characterStatus;
    private string characterName;
    private int hp;
    private int maxHP;
    private int mp;
    private int maxMP;
    private int str;
    private int mgc;
    private int spd;
    private int def;
    private int res;
    private int strRank;
    private int mgcRank;
    private int spdRank;
    private int defRank;
    private int resRank;
    private float fireCorrect;
    private float iceCorrect;
    private float thunderCorrect;
    private int strRankTurn;
    private int mgcRankTurn;
    private int spdRankTurn;
    private int defRankTurn;
    private int resRankTurn;
    private int fireCorrectTurn;
    private int iceCorrectTurn;
    private int thunderCorrectTurn;
    private List<Skill> skillList;
    private float guard;
    private bool isProtected;

    // Start is called before the first frame update
    void Start()
    {
        /*hp = characterStatus.GetHP();
        maxHP = hp;
        mp = characterStatus.GetMP();
        maxMP = mp;
        str = characterStatus.GetSTR();
        mgc = characterStatus.GetMGC();
        spd = characterStatus.GetSPD();
        def = characterStatus.GetDEF();
        res = characterStatus.GetRES();
        skillList = characterStatus.GetSkillList();
        strRank = 0;
        mgcRank = 0;
        spdRank = 0;
        defRank = 0;
        resRank = 0;
        fireCorrect = 0;
        iceCorrect = 0;
        thunderCorrect = 0;
        strRankTurn = 0;
        mgcRankTurn = 0;
        spdRankTurn = 0;
        defRankTurn = 0;
        resRankTurn = 0;
        fireCorrectTurn = 0;
        iceCorrectTurn = 0;
        thunderCorrectTurn = 0;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FirstSettings()
    {
        characterStatus.SetLevel(PlayerBasedata.GetUserLevel());
        characterName = characterStatus.GetCharacterName();
        hp = characterStatus.GetHP();
        maxHP = hp;
        mp = characterStatus.GetMP();
        maxMP = mp;
        str = characterStatus.GetSTR();
        mgc = characterStatus.GetMGC();
        spd = characterStatus.GetSPD();
        def = characterStatus.GetDEF();
        res = characterStatus.GetRES();
        skillList = characterStatus.GetSkillList();
        strRank = 0;
        mgcRank = 0;
        spdRank = 0;
        defRank = 0;
        resRank = 0;
        fireCorrect = 0;
        iceCorrect = 0;
        thunderCorrect = 0;
        strRankTurn = 0;
        mgcRankTurn = 0;
        spdRankTurn = 0;
        defRankTurn = 0;
        resRankTurn = 0;
        fireCorrectTurn = 0;
        iceCorrectTurn = 0;
        thunderCorrectTurn = 0;
        
    }

    public string GetCharacterName()
    {
        return characterName;
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

    public void SetFireCorrect(float fireCorrect)
    {
        this.fireCorrect = fireCorrect;
    }

    public float GetFireCorrect()
    {
        return fireCorrect;
    }

    public void SetIceCorrect(float iceCorrect)
    {
        this.iceCorrect = iceCorrect;
    }

    public float GetIceCorrect()
    {
        return iceCorrect;
    }

    public void SetThunderCorrect(float thunderCorrect)
    {
        this.thunderCorrect = thunderCorrect;
    }

    public float GetThunderCorrect()
    {
        return thunderCorrect;
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

    public void SetFireCorrectTurn(int fireCorrectTurn)
    {
        this.fireCorrectTurn = fireCorrectTurn;
    }

    public int GetFireCorrectTurn()
    {
        return fireCorrectTurn;
    }

    public void SetIceCorrectTurn(int iceCorrectTurn)
    {
        this.iceCorrectTurn = iceCorrectTurn;
    }

    public int GetIceCorrectTurn()
    {
        return iceCorrectTurn;
    }

    public void SetThunderCorrectTurn(int thunderCorrectTurn)
    {
        this.thunderCorrectTurn = thunderCorrectTurn;
    }

    public int GetThunderCorrectTurn()
    {
        return thunderCorrectTurn;
    }

    public void SetGuard(float guard)
    {
        this.guard = guard;
    }

    public float GetGuard()
    {
        return guard;
    }

    public void SetIsProtected(bool isProtected)
    {
        this.isProtected = isProtected;
    }

    public bool GetIsProtected()
    {
        return isProtected;
    }
}
