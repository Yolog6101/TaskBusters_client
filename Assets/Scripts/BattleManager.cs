using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private GameObject hero;
    [SerializeField] private GameObject warrior;
    [SerializeField] private GameObject mage;
    [SerializeField] private GameObject priest;
    [SerializeField] private GameObject enemy;
    [SerializeField] private SelectedEnemy selectedEnemy;
    private List<BattleStatus> statusList = new List<BattleStatus>();
    private BattleEnemyStatus enemyStatus;
    [SerializeField] private TextMeshProUGUI heroHPText;
    [SerializeField] private TextMeshProUGUI heroMPText;
    [SerializeField] private TextMeshProUGUI warriorHPText;
    [SerializeField] private TextMeshProUGUI warriorMPText;
    [SerializeField] private TextMeshProUGUI mageHPText;
    [SerializeField] private TextMeshProUGUI mageMPText;
    [SerializeField] private TextMeshProUGUI priestHPText;
    [SerializeField] private TextMeshProUGUI priestMPText;
    [SerializeField] private TextMeshProUGUI message;
    [SerializeField] private TextMeshProUGUI commandNameText;
    [SerializeField] private GameObject skillContent;
    [SerializeField] private GameObject skillButtonPrefab;
    [SerializeField] private GameObject commandWindow;
    [SerializeField] private GameObject skillCommandWindow;
    [SerializeField] private GameObject targetCommandWindow;
    // 0:ターン開始処理 1:勇者コマンド選択 2:戦士コマンド選択 3:魔導士コマンド選択 4:神官コマンド選択 5:敵コマンド選択 6:戦闘処理 7:戦闘終了
    private int mode;
    List<(int, int)> spdList;
    List<(int, int, int, Skill)> commandList;
    List<bool> aliveList = new List<bool>(){true, true, true, true};
    [SerializeField] private int waitTime;

    // Start is called before the first frame update
    void Start()
    {
        hero.GetComponent<BattleStatus>().FirstSettings();//データ取得
        warrior.GetComponent<BattleStatus>().FirstSettings();
        mage.GetComponent<BattleStatus>().FirstSettings();
        priest.GetComponent<BattleStatus>().FirstSettings();
        statusList.Add((hero.GetComponent<BattleStatus>()));
        statusList.Add((warrior.GetComponent<BattleStatus>()));
        statusList.Add((mage.GetComponent<BattleStatus>()));
        statusList.Add((priest.GetComponent<BattleStatus>()));
        enemyStatus = enemy.GetComponent<BattleEnemyStatus>();
        enemyStatus.FirstSettings();
        StatusUpdate();
        enemy.GetComponent<Transform>().transform.Find("EnemyImage").gameObject.GetComponent<Image>().sprite = selectedEnemy.GetEnemyStatus().GetSprite();
        message.text = selectedEnemy.GetEnemyStatus().GetEnemyName() + "が現れた！";
        SetMode(0);
    }

    public void StatusUpdate()
    {
        //表示関連
        heroHPText.text = "HP " + statusList[0].GetHP();
        heroMPText.text = "MP " + statusList[0].GetMP();
        warriorHPText.text = "HP " + statusList[1].GetHP();
        warriorMPText.text = "MP " + statusList[1].GetMP();
        mageHPText.text = "HP " + statusList[2].GetHP();
        mageMPText.text = "MP " + statusList[2].GetMP();
        priestHPText.text = "HP " + statusList[3].GetHP();
        priestMPText.text = "MP " + statusList[3].GetMP();
    }

    public async void SetMode(int mode)
    {
        this.mode = mode;
        if(mode == 0)
        {
            await StartTurn();
            SetMode(1);
        }
        else if(mode == 1)
        {
            if(aliveList[mode - 1])
            {
                int i;
                BattleStatus status = statusList[0];
                List<Skill> list = status.GetSkillList();
                int count = list.Count;
                commandWindow.SetActive(true);
                foreach (Transform c in skillContent.transform) {
                    GameObject.Destroy (c.gameObject);
                }
                commandNameText.text = "勇者";
                Vector2 sdPrefab = skillButtonPrefab.GetComponent<RectTransform>().sizeDelta;
                for(i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity, skillContent.transform);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
                    obj.transform.Find("SkillNameText").gameObject.GetComponent<TextMeshProUGUI>().text = list[i].GetSkillName();
                    obj.transform.Find("SkillMPText").gameObject.GetComponent<TextMeshProUGUI>().text = "MP " + list[i].GetMP();
                }
                Vector2 sdContent = skillContent.GetComponent<RectTransform>().sizeDelta;
                sdContent.y = sdPrefab.y * i;
                skillContent.GetComponent<RectTransform>().sizeDelta = sdContent;
            }
        }
        else if(mode == 2)
        {
            if(aliveList[mode - 1])
            {
                int i;
                BattleStatus status = statusList[1];
                List<Skill> list = status.GetSkillList();
                int count = list.Count;
                foreach (Transform c in skillContent.transform) {
                    GameObject.Destroy (c.gameObject);
                }
                commandNameText.text = "戦士";
                Vector2 sdPrefab = skillButtonPrefab.GetComponent<RectTransform>().sizeDelta;
                for(i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity, skillContent.transform);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
                    obj.transform.Find("SkillNameText").gameObject.GetComponent<TextMeshProUGUI>().text = list[i].GetSkillName();
                    obj.transform.Find("SkillMPText").gameObject.GetComponent<TextMeshProUGUI>().text = "MP " + list[i].GetMP();
                }
                Vector2 sdContent = skillContent.GetComponent<RectTransform>().sizeDelta;
                sdContent.y = sdPrefab.y * i;
                skillContent.GetComponent<RectTransform>().sizeDelta = sdContent;
            }
        }
        else if(mode == 3)
        {
            if(aliveList[mode - 1])
            {
                int i;
                BattleStatus status = statusList[2];
                List<Skill> list = status.GetSkillList();
                int count = list.Count;
                foreach (Transform c in skillContent.transform) {
                    GameObject.Destroy (c.gameObject);
                }
                commandNameText.text = "魔導士";
                Vector2 sdPrefab = skillButtonPrefab.GetComponent<RectTransform>().sizeDelta;
                for(i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity, skillContent.transform);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
                    obj.transform.Find("SkillNameText").gameObject.GetComponent<TextMeshProUGUI>().text = list[i].GetSkillName();
                    obj.transform.Find("SkillMPText").gameObject.GetComponent<TextMeshProUGUI>().text = "MP " + list[i].GetMP();
                }
                Vector2 sdContent = skillContent.GetComponent<RectTransform>().sizeDelta;
                sdContent.y = sdPrefab.y * i;
                skillContent.GetComponent<RectTransform>().sizeDelta = sdContent;
            }            
        }
        else if(mode == 4)
        {
            if(aliveList[mode - 1])
            {
                int i;
                BattleStatus status = statusList[3];
                List<Skill> list = status.GetSkillList();
                int count = list.Count;
                foreach (Transform c in skillContent.transform) {
                    GameObject.Destroy (c.gameObject);
                }
                commandNameText.text = "神官";
                Vector2 sdPrefab = skillButtonPrefab.GetComponent<RectTransform>().sizeDelta;
                for(i = 0; i < count; i++)
                {
                    GameObject obj = Instantiate(skillButtonPrefab, Vector3.zero, Quaternion.identity, skillContent.transform);
                    obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
                    obj.transform.Find("SkillNameText").gameObject.GetComponent<TextMeshProUGUI>().text = list[i].GetSkillName();
                    obj.transform.Find("SkillMPText").gameObject.GetComponent<TextMeshProUGUI>().text = "MP " + list[i].GetMP();
                }
                Vector2 sdContent = skillContent.GetComponent<RectTransform>().sizeDelta;
                sdContent.y = sdPrefab.y * i;
                skillContent.GetComponent<RectTransform>().sizeDelta = sdContent;
            }
        }
        else if(mode == 5)
        {
            BattleEnemyStatus status = enemyStatus;
            List<Skill> list = status.GetSkillList();
            int count = list.Count;
            System.Random r = new System.Random();
            int n;
            Skill skill;
            List<int> aliver = new List<int>();
            for(int i = 0; i < 2; i++)
            {
                n = r.Next(0, count + 1);
                for(int j = 0; j < aliveList.Count; j++)
                {
                    if(aliveList[j])
                    {
                        aliver.Add(j);
                    }
                }
                if(n < count){
                    skill = list[n];
                    n = r.Next(0, aliver.Count);
                    n = aliver[n];
                    commandList.Add((mode - 1 + i, 1, n, skill));
                }
                else
                {
                    skill = null;
                    n = r.Next(0, aliver.Count);
                    n = aliver[n];
                    commandList.Add((mode - 1 + i, 0, n, skill));
                }
                commandWindow.SetActive(false);
            }
            SetMode(6);
        }
        else if(mode == 6)
        {
            await BattleFunc();
            if(enemyStatus.GetHP() == 0)
            {
                SetMode(7);
            }
            else if(!aliveList.Contains(true))
            {
                SetMode(8);
            }
            else
            {
                SetMode(0);
            }
        }
        else if(mode == 7)
        {
            await ClearFunc();
        }
        else if(mode == 8)
        {
            await GameOver();
        }
    }

    private async System.Threading.Tasks.Task GameOver()
    {
        SetMessage("全滅した...");
        await System.Threading.Tasks.Task.Delay(waitTime);
        SceneManager.LoadScene("BossesScene");
    }

    private async System.Threading.Tasks.Task ClearFunc()
    {
        enemy.SetActive(false);
        SetMessage(enemyStatus.GetEnemyName() + "を倒した！");
        await System.Threading.Tasks.Task.Delay(waitTime);
        SceneManager.LoadScene("BossesScene");
    }

    private async System.Threading.Tasks.Task BattleFunc()
    {
        int n;
        int damage = 0;
        int atk;
        int defense;
        int character;
        int commandType;
        int target;
        Skill skill;
        for(int i = 0; i < spdList.Count; i++)
        {
            n = spdList[i].Item1;
            if(n != 4)
            {
                if(aliveList[n])
                {
                    (character, commandType, target, skill) = commandList.Find(x => x.Item1 == n);
                    if(commandType == 0)
                    {
                        if(statusList[n].GetSTRRank() == 1)
                        {
                            atk = (int)(statusList[n].GetSTR() * 1.25);
                        }
                        else if(statusList[n].GetSTRRank() == 2)
                        {
                            atk = (int)(statusList[n].GetSTR() * 1.5);
                        }
                        else if(statusList[n].GetSTRRank() == -1)
                        {
                            atk = (int)(statusList[n].GetSTR() * 0.8);
                        }
                        else if(statusList[n].GetSTRRank() == -2)
                        {
                            atk = (int)(statusList[n].GetSTR() * 0.67);
                        }
                        else
                        {
                            atk = statusList[n].GetSTR();
                        }
                        if(enemyStatus.GetDEFRank() == 1)
                        {
                            defense = (int)(enemyStatus.GetDEF() * 1.25);
                        }
                        else if(enemyStatus.GetDEFRank() == 2)
                        {
                            defense = (int)(enemyStatus.GetDEF() * 1.5);
                        }
                        else if(enemyStatus.GetDEFRank() == -1)
                        {
                            defense = (int)(enemyStatus.GetDEF() * 0.8);
                        }
                        else if(enemyStatus.GetDEFRank() == -2)
                        {
                            defense = (int)(enemyStatus.GetDEF() * 0.67);
                        }
                        else
                        {
                            defense = enemyStatus.GetDEF();
                        }
                        damage = (int)((atk / 2) - (defense / 4));
                        damage = Math.Max(damage, 1);
                        if(enemyStatus.GetHP() > damage)
                        {
                            enemyStatus.SetHP(enemyStatus.GetHP() - damage);
                            StatusUpdate();
                            SetMessage(statusList[n].GetCharacterName() + "の攻撃！\n" + damage + "のダメージ！");
                            await System.Threading.Tasks.Task.Delay(waitTime);
                        }
                        else
                        {
                            enemyStatus.SetHP(0);
                            StatusUpdate();
                            SetMessage(statusList[n].GetCharacterName() + "の攻撃！\n" + damage + "のダメージ！");
                            await System.Threading.Tasks.Task.Delay(waitTime);
                            break;
                        }
                    }
                    else if(commandType == 1)
                    {
                        if(skill.GetSkillType() == 0)
                        {
                            if(statusList[n].GetSTRRank() == 1)
                            {
                                atk = (int)(statusList[n].GetSTR() * skill.GetPower() * 1.25);
                            }
                            else if(statusList[n].GetSTRRank() == 2)
                            {
                                atk = (int)(statusList[n].GetSTR() * skill.GetPower() * 1.5);
                            }
                            else if(statusList[n].GetSTRRank() == -1)
                            {
                                atk = (int)(statusList[n].GetSTR() * skill.GetPower() * 0.8);
                            }
                            else if(statusList[n].GetSTRRank() == -2)
                            {
                                atk = (int)(statusList[n].GetSTR() * skill.GetPower() * 0.67);
                            }
                            else
                            {
                                atk = (int)(statusList[n].GetSTR() * skill.GetPower());
                            }
                            if(enemyStatus.GetDEFRank() == 1)
                            {
                                defense = (int)(enemyStatus.GetDEF() * 1.25);
                            }
                            else if(enemyStatus.GetDEFRank() == 2)
                            {
                                defense = (int)(enemyStatus.GetDEF() * 1.5);
                            }
                            else if(enemyStatus.GetDEFRank() == -1)
                            {
                                defense = (int)(enemyStatus.GetDEF() * 0.8);
                            }
                            else if(enemyStatus.GetDEFRank() == -2)
                            {
                                defense = (int)(enemyStatus.GetDEF() * 0.67);
                            }
                            else
                            {
                                defense = enemyStatus.GetDEF();
                            }
                            if(skill.GetElement() == 0)
                            {
                                damage = (int)((atk / 2) - (defense / 4));
                            }
                            else if(skill.GetElement() == 1)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetFire());
                            }
                            else if(skill.GetElement() == 2)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetIce());
                            }
                            else if(skill.GetElement() == 3)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetThunder());
                            }
                            damage = Math.Max(damage, 1);
                            if(skill.GetMP() > statusList[n].GetMP())
                            {
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                            else if(enemyStatus.GetHP() > damage)
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                enemyStatus.SetHP(enemyStatus.GetHP() - damage);
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + damage + "のダメージ！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                            else
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                enemyStatus.SetHP(0);
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + damage + "のダメージ！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                                break;
                            }
                        }
                        else if(skill.GetSkillType() == 1)
                        {
                            if(statusList[n].GetMGCRank() == 1)
                            {
                                atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.25);
                            }
                            else if(statusList[n].GetMGCRank() == 2)
                            {
                                atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.5);
                            }
                            else if(statusList[n].GetMGCRank() == -1)
                            {
                                atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.8);
                            }
                            else if(statusList[n].GetMGCRank() == -2)
                            {
                                atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.67);
                            }
                            else
                            {
                                atk = (int)(statusList[n].GetMGC() * skill.GetPower());
                            }
                            if(enemyStatus.GetRESRank() == 1)
                            {
                                defense = (int)(enemyStatus.GetRES() * 1.25);
                            }
                            else if(enemyStatus.GetRESRank() == 2)
                            {
                                defense = (int)(enemyStatus.GetRES() * 1.5);
                            }
                            else if(enemyStatus.GetRESRank() == -1)
                            {
                                defense = (int)(enemyStatus.GetRES() * 0.8);
                            }
                            else if(enemyStatus.GetRESRank() == -2)
                            {
                                defense = (int)(enemyStatus.GetRES() * 0.67);
                            }
                            else
                            {
                                defense = enemyStatus.GetRES();
                            }
                            if(skill.GetElement() == 0)
                            {
                                damage = (int)((atk / 2) - (defense / 4));
                            }
                            else if(skill.GetElement() == 1)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetFire());
                            }
                            else if(skill.GetElement() == 2)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetIce());
                            }
                            else if(skill.GetElement() == 3)
                            {
                                damage = (int)(((atk / 2) - (defense / 4)) * enemyStatus.GetThunder());
                            }
                            damage = Math.Max(damage, 1);
                            if(skill.GetMP() > statusList[n].GetMP())
                            {
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                            else if(enemyStatus.GetHP() > damage)
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                enemyStatus.SetHP(enemyStatus.GetHP() - damage);
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + damage + "のダメージ！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                            else
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                enemyStatus.SetHP(0);
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + damage + "のダメージ！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                                break;
                            }
                        }
                        else if(skill.GetSkillType() == 2)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(statusList[n].GetMGCRank() == 1)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.25);
                                }
                                else if(statusList[n].GetMGCRank() == 2)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.5);
                                }
                                else if(statusList[n].GetMGCRank() == -1)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.8);
                                }
                                else if(statusList[n].GetMGCRank() == -2)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.67);
                                }
                                else
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower());
                                }
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        damage = Math.Min(statusList[target].GetMaxHP(), statusList[target].GetHP() + atk);
                                        damage = damage - statusList[target].GetHP();
                                        statusList[target].SetHP(damage + statusList[target].GetHP());
                                        StatusUpdate();
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "のHPが" + damage + "回復した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            damage = Math.Min(statusList[j].GetMaxHP(), statusList[j].GetHP() + atk);
                                            damage = damage - statusList[j].GetHP();
                                            statusList[j].SetHP(damage + statusList[j].GetHP());
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "のHPが" + damage + "回復した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 1)
                            {
                                if(statusList[n].GetMGCRank() == 1)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.25);
                                }
                                else if(statusList[n].GetMGCRank() == 2)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 1.5);
                                }
                                else if(statusList[n].GetMGCRank() == -1)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.8);
                                }
                                else if(statusList[n].GetMGCRank() == -2)
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower() * 0.67);
                                }
                                else
                                {
                                    atk = (int)(statusList[n].GetMGC() * skill.GetPower());
                                }
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(aliveList[target])
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    statusList[target].SetHP(atk);
                                    aliveList[target] = true;
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "は生き返った！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                if(aliveList[target])
                                {
                                    damage = Math.Min(statusList[target].GetMaxMP(), statusList[target].GetMP() + 30);
                                    damage = damage - statusList[target].GetMP();
                                    statusList[target].SetMP(damage + statusList[target].GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "のMPが" + damage + "回復した！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 3)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        if(statusList[target].GetSTRRank() != 2)
                                        {
                                            statusList[target].SetSTRRank(statusList[target].GetSTRRank() + 1);
                                            statusList[target].SetSTRRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の力が1段階上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[target].SetSTRRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の力はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetSTRRank() != 2)
                                            {
                                                statusList[j].SetSTRRank(statusList[j].GetSTRRank() + 1);
                                                statusList[j].SetSTRRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の力が1段階上昇した！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[j].SetSTRRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の力はもう上がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 1)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        if(statusList[target].GetMGCRank() != 2)
                                        {
                                            statusList[target].SetMGCRank(statusList[target].GetMGCRank() + 1);
                                            statusList[target].SetMGCRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔力が1段階上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[target].SetMGCRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔力はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetMGCRank() != 2)
                                            {
                                                statusList[j].SetMGCRank(statusList[j].GetMGCRank() + 1);
                                                statusList[j].SetMGCRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔力が1段階上昇した！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[j].SetMGCRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔力はもう上がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        if(statusList[target].GetSPDRank() != 2)
                                        {
                                            statusList[target].SetSPDRank(statusList[target].GetSPDRank() + 1);
                                            statusList[target].SetSPDRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の速さが1段階上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[target].SetSPDRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の速さはもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetSPDRank() != 2)
                                            {
                                                statusList[j].SetSPDRank(statusList[j].GetSPDRank() + 1);
                                                statusList[j].SetSPDRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の速さが1段階上昇した！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[j].SetSPDRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の速さはもう上がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 3)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        if(statusList[target].GetDEFRank() != 2)
                                        {
                                            statusList[target].SetDEFRank(statusList[target].GetDEFRank() + 1);
                                            statusList[target].SetDEFRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の守備が1段階上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[target].SetDEFRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の守備はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetDEFRank() != 2)
                                            {
                                                statusList[j].SetDEFRank(statusList[j].GetDEFRank() + 1);
                                                statusList[j].SetDEFRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の守備が1段階上昇した！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[j].SetDEFRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の守備はもう上がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 4)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(aliveList[target])
                                    {
                                        statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                        if(statusList[target].GetRESRank() != 2)
                                        {
                                            statusList[target].SetRESRank(statusList[target].GetRESRank() + 1);
                                            statusList[target].SetRESRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔防が1段階上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[target].SetRESRankTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔防はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetRESRank() != 2)
                                            {
                                                statusList[j].SetRESRank(statusList[j].GetRESRank() + 1);
                                                statusList[j].SetRESRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔防が1段階上昇した！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[j].SetRESRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔防はもう上がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                        else
                                        {
                                            SetMessage("しかし何も起こらなかった！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 4)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(enemyStatus.GetSTRRank() != -2)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetSTRRank(enemyStatus.GetSTRRank() - 1);
                                    enemyStatus.SetSTRRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の力が1段階下がった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetSTRRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の力はもう下がらない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 1)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(enemyStatus.GetMGCRank() != -2)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetMGCRank(enemyStatus.GetMGCRank() - 1);
                                    enemyStatus.SetMGCRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔力が1段階下がった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetMGCRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔力はもう下がらない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(enemyStatus.GetSPDRank() != -2)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetSPDRank(enemyStatus.GetSPDRank() - 1);
                                    enemyStatus.SetSPDRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の速さが1段階下がった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetSPDRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の速さはもう下がらない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 3)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(enemyStatus.GetDEFRank() != -2)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetDEFRank(enemyStatus.GetDEFRank() - 1);
                                    enemyStatus.SetDEFRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の守備が1段階下がった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetDEFRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の守備はもう下がらない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 4)
                            {
                                if(skill.GetMP() > statusList[n].GetMP())
                                {
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(enemyStatus.GetRESRank() != -2)
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetRESRank(enemyStatus.GetRESRank() - 1);
                                    enemyStatus.SetRESRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔防が1段階下がった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                    enemyStatus.SetRESRankTurn(5);
                                    StatusUpdate();
                                    SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔防はもう下がらない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 5)
                        {
                            if(skill.GetMP() > statusList[n].GetMP())
                            {
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                            else if(skill.GetElement() == 1)
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                                for(int j = 0; j < statusList.Count; j++)
                                {
                                    if(aliveList[j])
                                    {
                                        if(statusList[j].GetFireCorrect() == 0)
                                        {
                                            statusList[j].SetFireCorrect(skill.GetPower());
                                            statusList[j].SetFireCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の炎耐性が上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[j].SetFireCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の炎耐性はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage("しかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                                for(int j = 0; j < statusList.Count; j++)
                                {
                                    if(aliveList[j])
                                    {
                                        if(statusList[j].GetIceCorrect() == 0)
                                        {
                                            statusList[j].SetIceCorrect(skill.GetPower());
                                            statusList[j].SetIceCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の氷耐性が上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[j].SetIceCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の氷耐性はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage("しかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 3)
                            {
                                statusList[n].SetMP(statusList[n].GetMP() - skill.GetMP());
                                StatusUpdate();
                                SetMessage(statusList[n].GetCharacterName() + "の" + skill.GetSkillName() + "！\n");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                                for(int j = 0; j < statusList.Count; j++)
                                {
                                    if(aliveList[j])
                                    {
                                        if(statusList[j].GetThunderCorrect() == 0)
                                        {
                                            statusList[j].SetThunderCorrect(skill.GetPower());
                                            statusList[j].SetThunderCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の雷耐性が上昇した！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                        else
                                        {
                                            statusList[j].SetThunderCorrectTurn(5);
                                            StatusUpdate();
                                            SetMessage(statusList[j].GetCharacterName() + "の雷耐性はもう上がらない！");
                                            await System.Threading.Tasks.Task.Delay(waitTime);
                                        }
                                    }
                                    else
                                    {
                                        SetMessage("しかし何も起こらなかった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 6)
                        {
                            statusList[n].SetGuard((float)0.8);
                            statusList[target].SetIsProtected(true);
                            SetMessage(statusList[n].GetCharacterName() + "は" + statusList[target].GetCharacterName() + "をかばっている！");
                            await System.Threading.Tasks.Task.Delay(waitTime);
                        }
                    }
                    else if(commandType == 2)
                    {
                        SetMessage(statusList[n].GetCharacterName() + "は身を守っている！");
                        await System.Threading.Tasks.Task.Delay(waitTime);
                    }
                }
            }
            else
            {
                for(n = 4; n <= 5; n++)
                {
                    (character, commandType, target, skill) = commandList.Find(x => x.Item1 == n);
                    if(statusList[target].GetIsProtected())
                    {
                        target = 1;
                    }
                    if(commandType == 0)
                    {
                        if(enemyStatus.GetSTRRank() == 1)
                        {
                            atk = (int)(enemyStatus.GetSTR() * 1.25);
                        }
                        else if(enemyStatus.GetSTRRank() == 2)
                        {
                            atk = (int)(enemyStatus.GetSTR() * 1.5);
                        }
                        else if(enemyStatus.GetSTRRank() == -1)
                        {
                            atk = (int)(enemyStatus.GetSTR() * 0.8);
                        }
                        else if(enemyStatus.GetSTRRank() == -2)
                        {
                            atk = (int)(enemyStatus.GetSTR() * 0.67);
                        }
                        else
                        {
                            atk = enemyStatus.GetSTR();
                        }
                        if(statusList[target].GetDEFRank() == 1)
                        {
                            defense = (int)(statusList[target].GetDEF() * 1.25);
                        }
                        else if(statusList[target].GetDEFRank() == 2)
                        {
                            defense = (int)(statusList[target].GetDEF() * 1.5);
                        }
                        else if(statusList[target].GetDEFRank() == -1)
                        {
                            defense = (int)(statusList[target].GetDEF() * 0.8);
                        }
                        else if(statusList[target].GetDEFRank() == -2)
                        {
                            defense = (int)(statusList[target].GetDEF() * 0.67);
                        }
                        else
                        {
                            defense = statusList[target].GetDEF();
                        }
                        damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetGuard());
                        damage = Math.Max(damage, 1);
                        if(statusList[target].GetHP() > damage)
                        {
                            statusList[target].SetHP(statusList[target].GetHP() - damage);
                            StatusUpdate();
                            SetMessage(enemyStatus.GetEnemyName() + "の攻撃！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！");
                            await System.Threading.Tasks.Task.Delay(waitTime);
                        }
                        else
                        {
                            statusList[target].SetHP(0);
                            aliveList[target] = false;
                            CharacterDead(target);
                            StatusUpdate();
                            SetMessage(enemyStatus.GetEnemyName() + "の攻撃！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！" + statusList[target].GetCharacterName() + "は死んでしまった！");
                            await System.Threading.Tasks.Task.Delay(waitTime);
                        }
                    }
                    else if(commandType == 1)
                    {
                        if(skill.GetSkillType() == 0)
                        {
                            if(enemyStatus.GetSTRRank() == 1)
                            {
                                atk = (int)(enemyStatus.GetSTR() * skill.GetPower() * 1.25);
                            }
                            else if(enemyStatus.GetSTRRank() == 2)
                            {
                                atk = (int)(enemyStatus.GetSTR() * skill.GetPower() * 1.5);
                            }
                            else if(enemyStatus.GetSTRRank() == -1)
                            {
                                atk = (int)(enemyStatus.GetSTR() * skill.GetPower() * 0.8);
                            }
                            else if(enemyStatus.GetSTRRank() == -2)
                            {
                                atk = (int)(enemyStatus.GetSTR() * skill.GetPower() * 0.67);
                            }
                            else
                            {
                                atk = (int)(enemyStatus.GetSTR() * skill.GetPower());
                            }
                            if(skill.GetRange() == 0)
                            {
                                if(statusList[target].GetDEFRank() == 1)
                                {
                                    defense = (int)(statusList[target].GetDEF() * 1.25);
                                }
                                else if(statusList[target].GetDEFRank() == 2)
                                {
                                    defense = (int)(statusList[target].GetDEF() * 1.5);
                                }
                                else if(statusList[target].GetDEFRank() == -1)
                                {
                                    defense = (int)(statusList[target].GetDEF() * 0.8);
                                }
                                else if(statusList[target].GetDEFRank() == -2)
                                {
                                    defense = (int)(statusList[target].GetDEF() * 0.67);
                                }
                                else
                                {
                                    defense = statusList[target].GetDEF();
                                }
                                if(skill.GetElement() == 0)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 1)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetFireCorrect() * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 2)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetIceCorrect() * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 3)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetThunderCorrect() * statusList[target].GetGuard());
                                }
                                damage = Math.Max(damage, 1);
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(statusList[target].GetHP() > damage)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    statusList[target].SetHP(statusList[target].GetHP() - damage);
                                    StatusUpdate();
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    statusList[target].SetHP(0);
                                    aliveList[target] = false;
                                    CharacterDead(target);
                                    StatusUpdate();
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！" + statusList[target].GetCharacterName() + "は死んでしまった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetRange() == 1)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            int k = 0;
                                            if(statusList[j].GetIsProtected())
                                            {
                                                k = 1;
                                            }
                                            else
                                            {
                                                k = j;
                                            }
                                            if(statusList[k].GetDEFRank() == 1)
                                            {
                                                defense = (int)(statusList[k].GetDEF() * 1.25);
                                            }
                                            else if(statusList[k].GetDEFRank() == 2)
                                            {
                                                defense = (int)(statusList[k].GetDEF() * 1.5);
                                            }
                                            else if(statusList[k].GetDEFRank() == -1)
                                            {
                                                defense = (int)(statusList[k].GetDEF() * 0.8);
                                            }
                                            else if(statusList[k].GetDEFRank() == -2)
                                            {
                                                defense = (int)(statusList[k].GetDEF() * 0.67);
                                            }
                                            else
                                            {
                                                defense = statusList[k].GetDEF();
                                            }
                                            if(skill.GetElement() == 0)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 1)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetFireCorrect() * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 2)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetIceCorrect() * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 3)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetThunderCorrect() * statusList[k].GetGuard());
                                            }
                                            damage = Math.Max(damage, 1);
                                            if(statusList[k].GetHP() > damage)
                                            {
                                                statusList[k].SetHP(statusList[k].GetHP() - damage);
                                                StatusUpdate();
                                                SetMessage(statusList[k].GetCharacterName() + "に" + damage + "のダメージ！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[k].SetHP(0);
                                                aliveList[k] = false;
                                                CharacterDead(target);
                                                StatusUpdate();
                                                SetMessage(statusList[k].GetCharacterName() + "に" + damage + "のダメージ！" + statusList[k].GetCharacterName() + "は死んでしまった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 1)
                        {
                            if(enemyStatus.GetMGCRank() == 1)
                            {
                                atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 1.25);
                            }
                            else if(enemyStatus.GetMGCRank() == 2)
                            {
                                atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 1.5);
                            }
                            else if(enemyStatus.GetMGCRank() == -1)
                            {
                                atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 0.8);
                            }
                            else if(enemyStatus.GetMGCRank() == -2)
                            {
                                atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 0.67);
                            }
                            else
                            {
                                atk = (int)(enemyStatus.GetMGC() * skill.GetPower());
                            }
                            if(skill.GetRange() == 0)
                            {
                                if(statusList[target].GetRESRank() == 1)
                                {
                                    defense = (int)(statusList[target].GetRES() * 1.25);
                                }
                                else if(statusList[target].GetRESRank() == 2)
                                {
                                    defense = (int)(statusList[target].GetRES() * 1.5);
                                }
                                else if(statusList[target].GetRESRank() == -1)
                                {
                                    defense = (int)(statusList[target].GetRES() * 0.8);
                                }
                                else if(statusList[target].GetRESRank() == -2)
                                {
                                    defense = (int)(statusList[target].GetRES() * 0.67);
                                }
                                else
                                {
                                    defense = statusList[target].GetRES();
                                }
                                if(skill.GetElement() == 0)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 1)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetFireCorrect() * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 2)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetIceCorrect() * statusList[target].GetGuard());
                                }
                                else if(skill.GetElement() == 3)
                                {
                                    damage = (int)(((atk / 2) - (defense / 4)) * statusList[target].GetThunderCorrect() * statusList[target].GetGuard());
                                }
                                damage = Math.Max(damage, 1);
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(statusList[target].GetHP() > damage)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    statusList[target].SetHP(statusList[target].GetHP() - damage);
                                    StatusUpdate();
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    statusList[target].SetHP(0);
                                    aliveList[target] = false;
                                    CharacterDead(target);
                                    StatusUpdate();
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "に" + damage + "のダメージ！" + statusList[target].GetCharacterName() + "は死んでしまった！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetRange() == 1)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            int k = 0;
                                            if(statusList[j].GetIsProtected())
                                            {
                                                k = 1;
                                            }
                                            else
                                            {
                                                k = j;
                                            }
                                            if(statusList[k].GetRESRank() == 1)
                                            {
                                                defense = (int)(statusList[k].GetRES() * 1.25);
                                            }
                                            else if(statusList[k].GetRESRank() == 2)
                                            {
                                                defense = (int)(statusList[k].GetRES() * 1.5);
                                            }
                                            else if(statusList[k].GetRESRank() == -1)
                                            {
                                                defense = (int)(statusList[k].GetRES() * 0.8);
                                            }
                                            else if(statusList[k].GetRESRank() == -2)
                                            {
                                                defense = (int)(statusList[k].GetRES() * 0.67);
                                            }
                                            else
                                            {
                                                defense = statusList[k].GetRES();
                                            }
                                            if(skill.GetElement() == 0)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 1)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetFireCorrect() * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 2)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetIceCorrect() * statusList[k].GetGuard());
                                            }
                                            else if(skill.GetElement() == 3)
                                            {
                                                damage = (int)(((atk / 2) - (defense / 4)) * statusList[k].GetThunderCorrect() * statusList[k].GetGuard());
                                            }
                                            damage = Math.Max(damage, 1);
                                            if(statusList[k].GetHP() > damage)
                                            {
                                                statusList[k].SetHP(statusList[k].GetHP() - damage);
                                                StatusUpdate();
                                                SetMessage(statusList[k].GetCharacterName() + "に" + damage + "のダメージ！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                statusList[k].SetHP(0);
                                                aliveList[k] = false;
                                                CharacterDead(target);
                                                StatusUpdate();
                                                SetMessage(statusList[k].GetCharacterName() + "に" + damage + "のダメージ！" + statusList[k].GetCharacterName() + "は死んでしまった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 2)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(enemyStatus.GetMGCRank() == 1)
                                {
                                    atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 1.25);
                                }
                                else if(enemyStatus.GetMGCRank() == 2)
                                {
                                    atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 1.5);
                                }
                                else if(enemyStatus.GetMGCRank() == -1)
                                {
                                    atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 0.8);
                                }
                                else if(enemyStatus.GetMGCRank() == -2)
                                {
                                    atk = (int)(enemyStatus.GetMGC() * skill.GetPower() * 0.67);
                                }
                                else
                                {
                                    atk = (int)(enemyStatus.GetMGC() * skill.GetPower());
                                }
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    damage = Math.Min(enemyStatus.GetMaxHP(), enemyStatus.GetHP() + atk);
                                    damage = damage - enemyStatus.GetHP();
                                    enemyStatus.SetHP(damage + enemyStatus.GetHP());
                                    StatusUpdate();
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "のHPが" + damage + "回復した！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                damage = Math.Min(enemyStatus.GetMaxMP(), enemyStatus.GetMP() + 30);
                                damage = damage - enemyStatus.GetMP();
                                enemyStatus.SetMP(damage + enemyStatus.GetMP());
                                StatusUpdate();
                                SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "のMPが" + damage + "回復した！");
                                await System.Threading.Tasks.Task.Delay(waitTime);
                            }
                        }
                        else if(skill.GetSkillType() == 3)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    if(enemyStatus.GetSTRRank() != 2)
                                    {
                                        enemyStatus.SetSTRRank(enemyStatus.GetSTRRank() + 1);
                                        enemyStatus.SetSTRRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の力が1段階上昇した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetSTRRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の力はもう上がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 1)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    if(enemyStatus.GetMGCRank() != 2)
                                    {
                                        enemyStatus.SetMGCRank(enemyStatus.GetMGCRank() + 1);
                                        enemyStatus.SetMGCRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔力が1段階上昇した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMGCRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔力はもう上がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    if(enemyStatus.GetSPDRank() != 2)
                                    {
                                        enemyStatus.SetSPDRank(enemyStatus.GetSPDRank() + 1);
                                        enemyStatus.SetSPDRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の速さが1段階上昇した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetSPDRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の速さはもう上がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 3)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    if(enemyStatus.GetDEFRank() != 2)
                                    {
                                        enemyStatus.SetDEFRank(enemyStatus.GetDEFRank() + 1);
                                        enemyStatus.SetDEFRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の守備が1段階上昇した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetDEFRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の守備はもう上がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                            else if(skill.GetElement() == 4)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    if(enemyStatus.GetRESRank() != 2)
                                    {
                                        enemyStatus.SetRESRank(enemyStatus.GetRESRank() + 1);
                                        enemyStatus.SetRESRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔防が1段階上昇した！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetRESRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + enemyStatus.GetEnemyName() + "の魔防はもう上がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                            }
                        }
                        else if(skill.GetSkillType() == 4)
                        {
                            if(skill.GetElement() == 0)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(statusList[target].GetSTRRank() != -2)
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetSTRRank(statusList[target].GetSTRRank() - 1);
                                        statusList[target].SetSTRRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の力が1段階下がった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetSTRRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の力はもう下がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetSTRRank() != -2)
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetSTRRank(statusList[j].GetSTRRank() - 1);
                                                statusList[j].SetSTRRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の力が1段階下がった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetSTRRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の力はもう下がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 1)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(statusList[target].GetMGCRank() != -2)
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetMGCRank(statusList[target].GetMGCRank() - 1);
                                        statusList[target].SetMGCRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔力が1段階下がった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetMGCRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔力はもう下がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetMGCRank() != -2)
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetMGCRank(statusList[j].GetMGCRank() - 1);
                                                statusList[j].SetMGCRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔力が1段階下がった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetMGCRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔力はもう下がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 2)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(statusList[target].GetSPDRank() != -2)
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetSPDRank(statusList[target].GetSPDRank() - 1);
                                        statusList[target].SetSPDRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の速さが1段階下がった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetSPDRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の速さはもう下がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetSPDRank() != -2)
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetSPDRank(statusList[j].GetSPDRank() - 1);
                                                statusList[j].SetSPDRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の速さが1段階下がった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetSPDRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の速さはもう下がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 3)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(statusList[target].GetDEFRank() != -2)
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetDEFRank(statusList[target].GetDEFRank() - 1);
                                        statusList[target].SetDEFRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の守備が1段階下がった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetDEFRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の守備はもう下がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetDEFRank() != -2)
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetDEFRank(statusList[j].GetDEFRank() - 1);
                                                statusList[j].SetDEFRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の守備が1段階下がった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetDEFRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の守備はもう下がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                            else if(skill.GetElement() == 4)
                            {
                                if(skill.GetMP() > enemyStatus.GetMP())
                                {
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\nしかしMPが足りない！");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                }
                                else if(skill.GetRange() == 0)
                                {
                                    if(statusList[target].GetRESRank() != -2)
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetRESRank(statusList[target].GetRESRank() - 1);
                                        statusList[target].SetRESRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔防が1段階下がった！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                    else
                                    {
                                        enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                        statusList[target].SetRESRankTurn(5);
                                        StatusUpdate();
                                        SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n" + statusList[target].GetCharacterName() + "の魔防はもう下がらない！");
                                        await System.Threading.Tasks.Task.Delay(waitTime);
                                    }
                                }
                                else if(skill.GetRange() == 1)
                                {
                                    enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                    SetMessage(enemyStatus.GetEnemyName() + "の" + skill.GetSkillName() + "！\n");
                                    await System.Threading.Tasks.Task.Delay(waitTime);
                                    for(int j = 0; j < statusList.Count; j++)
                                    {
                                        if(aliveList[j])
                                        {
                                            if(statusList[j].GetRESRank() != -2)
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetRESRank(statusList[j].GetRESRank() - 1);
                                                statusList[j].SetRESRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔防が1段階下がった！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                            else
                                            {
                                                enemyStatus.SetMP(enemyStatus.GetMP() - skill.GetMP());
                                                statusList[j].SetRESRankTurn(5);
                                                StatusUpdate();
                                                SetMessage(statusList[j].GetCharacterName() + "の魔防はもう下がらない！");
                                                await System.Threading.Tasks.Task.Delay(waitTime);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void CharacterDead(int character)
    {
        statusList[character].SetSTRRank(0);
        statusList[character].SetSTRRankTurn(0);
        statusList[character].SetMGCRank(0);
        statusList[character].SetMGCRankTurn(0);
        statusList[character].SetSPDRank(0);
        statusList[character].SetSPDRankTurn(0);
        statusList[character].SetDEFRank(0);
        statusList[character].SetDEFRankTurn(0);
        statusList[character].SetRESRank(0);
        statusList[character].SetRESRankTurn(0);
    }

    public void SetMessage(string str)
    {
        message.text = str;
    }

    public void SelectAttack()
    {
        commandList.Add((mode - 1, 0, 4, null));
        skillCommandWindow.SetActive(false);
        SetMode(mode + 1);
    }

    public void SelectSkill(string skillName)
    {
        List<Skill> list = statusList[mode - 1].GetSkillList();
        Skill skill = list.Find(x => x.GetSkillName() == skillName);
        if(skill.GetRange() == 0 && (skill.GetSkillType() == 2 || skill.GetSkillType() == 3 || skill.GetSkillType() == 6))
        {
            commandList.Add((mode - 1, 1, -1, skill));
            skillCommandWindow.SetActive(false);
            targetCommandWindow.SetActive(true);
        }
        else
        {
            commandList.Add((mode - 1, 1, 4, skill));
            skillCommandWindow.SetActive(false);
            SetMode(mode + 1);
        }
    }

    public void SelectGuard()
    {
        commandList.Add((mode - 1, 2, 4, null));
        statusList[mode - 1].SetGuard((float)0.5);
        skillCommandWindow.SetActive(false);
        SetMode(mode + 1);
    }

    public void SelectTarget(string targetName)
    {
        if(targetName == "勇者")
        {
            commandList.Add((commandList[mode - 1].Item1, commandList[mode - 1].Item2, 0, commandList[mode - 1].Item4));
            commandList.RemoveAt(mode - 1);
        }
        else if(targetName == "戦士")
        {
            commandList.Add((commandList[mode - 1].Item1, commandList[mode - 1].Item2, 1, commandList[mode - 1].Item4));
            commandList.RemoveAt(mode - 1);
        }
        else if(targetName == "魔導士")
        {
            commandList.Add((commandList[mode - 1].Item1, commandList[mode - 1].Item2, 2, commandList[mode - 1].Item4));
            commandList.RemoveAt(mode - 1);
        }
        else if(targetName == "神官")
        {
            commandList.Add((commandList[mode - 1].Item1, commandList[mode - 1].Item2, 3, commandList[mode - 1].Item4));
            commandList.RemoveAt(mode - 1);
        }
        targetCommandWindow.SetActive(false);
        SetMode(mode + 1);
    }

    public async System.Threading.Tasks.Task StartTurn()
    {
        int spd = 0;
        spdList = new List<(int, int)>();
        for(int i = 0; i < 4; i++)
        {
            if(statusList[i].GetSTRRank() != 0)
            {
                statusList[i].SetSTRRankTurn(statusList[i].GetSTRRankTurn() - 1);
                if(statusList[i].GetSTRRankTurn() == 0)
                {
                   statusList[i].SetSTRRank(0);
                   SetMessage(statusList[i].GetCharacterName() + "の力は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetMGCRank() != 0)
            {
                statusList[i].SetMGCRankTurn(statusList[i].GetMGCRankTurn() - 1);
                if(statusList[i].GetMGCRankTurn() == 0)
                {
                   statusList[i].SetMGCRank(0);
                   SetMessage(statusList[i].GetCharacterName() + "の魔力は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetSPDRank() != 0)
            {
                statusList[i].SetSPDRankTurn(statusList[i].GetSPDRankTurn() - 1);
                if(statusList[i].GetSPDRankTurn() == 0)
                {
                   statusList[i].SetSPDRank(0);
                   SetMessage(statusList[i].GetCharacterName() + "の速さは元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetDEFRank() != 0)
            {
                statusList[i].SetDEFRankTurn(statusList[i].GetDEFRankTurn() - 1);
                if(statusList[i].GetDEFRankTurn() == 0)
                {
                   statusList[i].SetDEFRank(0);
                   SetMessage(statusList[i].GetCharacterName() + "の守備は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetRESRank() != 0)
            {
                statusList[i].SetRESRankTurn(statusList[i].GetRESRankTurn() - 1);
                if(statusList[i].GetRESRankTurn() == 0)
                {
                   statusList[i].SetRESRank(0);
                   SetMessage(statusList[i].GetCharacterName() + "の魔防は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetFireCorrect() != 0)
            {
                statusList[i].SetFireCorrectTurn(statusList[i].GetFireCorrectTurn() - 1);
                if(statusList[i].GetFireCorrectTurn() == 0)
                {
                   statusList[i].SetFireCorrect(0);
                   SetMessage(statusList[i].GetCharacterName() + "の炎耐性は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetIceCorrect() != 0)
            {
                statusList[i].SetIceCorrectTurn(statusList[i].GetIceCorrectTurn() - 1);
                if(statusList[i].GetIceCorrectTurn() == 0)
                {
                   statusList[i].SetIceCorrect(0);
                   SetMessage(statusList[i].GetCharacterName() + "の氷耐性は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetThunderCorrect() != 0)
            {
                statusList[i].SetThunderCorrectTurn(statusList[i].GetThunderCorrectTurn() - 1);
                if(statusList[i].GetThunderCorrectTurn() == 0)
                {
                   statusList[i].SetThunderCorrect(0);
                   SetMessage(statusList[i].GetCharacterName() + "の雷耐性は元に戻った！");
                   await System.Threading.Tasks.Task.Delay(waitTime);
                }
            }
            if(statusList[i].GetSPDRank() == 1)
            {
                spd = (int)(statusList[i].GetSPD() * 1.25);
            }
            else if(statusList[i].GetSPDRank() == 2)
            {
                spd = (int)(statusList[i].GetSPD() * 1.5);
            }
            else if(statusList[i].GetSPDRank() == -1)
            {
                spd = (int)(statusList[i].GetSPD() * 0.8);
            }
            else if(statusList[i].GetSPDRank() == -2)
            {
                spd = (int)(statusList[i].GetSPD() * 0.67);
            }
            else
            {
                spd = statusList[i].GetSPD();
            }
            spdList.Add((i, spd));
            statusList[i].SetGuard(1);
            statusList[i].SetIsProtected(false);
        }
        if(enemyStatus.GetSTRRank() != 0)
        {
            enemyStatus.SetSTRRankTurn(enemyStatus.GetSTRRankTurn() - 1);
            if(enemyStatus.GetSTRRankTurn() == 0)
            {
                enemyStatus.SetSTRRank(0);
                SetMessage(enemyStatus.GetEnemyName() + "の力は元に戻った！");
                await System.Threading.Tasks.Task.Delay(waitTime);
            }
        }
        if(enemyStatus.GetMGCRank() != 0)
        {
            enemyStatus.SetMGCRankTurn(enemyStatus.GetMGCRankTurn() - 1);
            if(enemyStatus.GetMGCRankTurn() == 0)
            {
                enemyStatus.SetMGCRank(0);
                SetMessage(enemyStatus.GetEnemyName() + "の魔力は元に戻った！");
                await System.Threading.Tasks.Task.Delay(waitTime);
            }
        }
        if(enemyStatus.GetSPDRank() != 0)
        {
            enemyStatus.SetSPDRankTurn(enemyStatus.GetSPDRankTurn() - 1);
            if(enemyStatus.GetSPDRankTurn() == 0)
            {
                enemyStatus.SetSPDRank(0);
                SetMessage(enemyStatus.GetEnemyName() + "の速さは元に戻った！");
                await System.Threading.Tasks.Task.Delay(waitTime);
            }
        }
        if(enemyStatus.GetDEFRank() != 0)
        {
            enemyStatus.SetDEFRankTurn(enemyStatus.GetDEFRankTurn() - 1);
            if(enemyStatus.GetDEFRankTurn() == 0)
            {
                enemyStatus.SetDEFRank(0);
                SetMessage(enemyStatus.GetEnemyName() + "の守備は元に戻った！");
                await System.Threading.Tasks.Task.Delay(waitTime);
            }
        }
        if(enemyStatus.GetRESRank() != 0)
        {
            enemyStatus.SetRESRankTurn(enemyStatus.GetRESRankTurn() - 1);
            if(enemyStatus.GetRESRankTurn() == 0)
            {
                enemyStatus.SetRESRank(0);
                SetMessage(enemyStatus.GetEnemyName() + "の魔防は元に戻った！");
                await System.Threading.Tasks.Task.Delay(waitTime);
            }
        }
        if(enemyStatus.GetSPDRank() == 1)
        {
            spd = (int)(enemyStatus.GetSPD() * 1.25);
        }
        else if(enemyStatus.GetSPDRank() == 2)
        {
            spd = (int)(enemyStatus.GetSPD() * 1.5);
        }
        else if(enemyStatus.GetSPDRank() == -1)
        {
            spd = (int)(enemyStatus.GetSPD() * 0.8);
        }
        else if(enemyStatus.GetSPDRank() == -2)
        {
            spd = (int)(enemyStatus.GetSPD() * 0.67);
        }
        else
        {
            spd = enemyStatus.GetSPD();
        }
        spdList.Add((4, spd));

        spdList.Sort((a, b) => b.Item2 - a.Item2);
        commandList = new List<(int, int, int, Skill)>();
    }
}

