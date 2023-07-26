using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class CalendarController : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject prefab;
    [SerializeField] private TextMeshProUGUI dateText;
    [SerializeField] private TextMeshProUGUI monthText;
    [SerializeField] private TextMeshProUGUI prevMonthText;
    [SerializeField] private TextMeshProUGUI nextMonthText;

    private DateTime SelectDate;
    private DateTime D_Date;
    private int startday;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 42; i++)
        {
            GameObject button = Instantiate(prefab, canvas.transform);
            button.GetComponent<Button>();
        }
        SelectDate = DateTime.Now;
        CalendarSetting();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToPrevMonth()
    {
        SelectDate = SelectDate.AddMonths(-1);
        CalendarSetting();
    }

    public void ToNextMonth()
    {
        SelectDate = SelectDate.AddMonths(1);
        CalendarSetting();
    }

    private void CalendarSetting()
    {
        int days = 1;
        int overday = 1;

        D_Date = new DateTime(SelectDate.Year, SelectDate.Month, 1);  //SelectDateの月の最初の日付
        int year = SelectDate.Year; //年
        int month = SelectDate.Month; //月
        int day = SelectDate.Day; //日
        //最初の日付の曜日を取得
        DayOfWeek firstDate = D_Date.DayOfWeek;
        //何日まであるか
        int monthEnd = DateTime.DaysInMonth(year, month);
        //前月が何日まであるか
        SelectDate = SelectDate.AddMonths(-1);
        month = SelectDate.Month;
        SelectDate = SelectDate.AddMonths(1);
        int lastmonth = DateTime.DaysInMonth(year, month);
        switch (firstDate)
        {
            case DayOfWeek.Sunday:
                startday = 0;
                break;
            case DayOfWeek.Monday:
                startday = 1;
                break;
            case DayOfWeek.Tuesday:
                startday = 2;
                break;
            case DayOfWeek.Wednesday:
                startday = 3;
                break;
            case DayOfWeek.Thursday:
                startday = 4;
                break;
            case DayOfWeek.Friday:
                startday = 5;
                break;
            case DayOfWeek.Saturday:
                startday = 6;
                break;
        }
        int lastmonthdays = lastmonth - startday + 1;

        for (int i = 0; i < 42; i++)
        {
            if (i >= startday)
            {
                if (days <= monthEnd)
                {
                    //文字を入れる
                    Transform DAY = canvas.transform.GetChild(i);
                    DateTime tmp = D_Date;//一時変数
                    DayOfWeek num = tmp.DayOfWeek;
                    //土曜日青・日曜日赤
                    switch (num)
                    {
                        case DayOfWeek.Sunday:
                            DAY.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.red;
                            break;
                        case DayOfWeek.Saturday:
                            DAY.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.blue;
                            break;
                        default:
                            DAY.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.black;
                            break;

                    }
                    DAY.GetChild(0).GetComponent<TextMeshProUGUI>().text = D_Date.Day.ToString();
                    //以下3行追加
                    GameObject button = canvas.transform.GetChild(i).gameObject;
                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    button.GetComponent<Button>().onClick.AddListener(() => { set_Date(tmp); });
                    D_Date = D_Date.AddDays(1);
                    days++;
                }
                else
                {
                    Transform DAY = canvas.transform.GetChild(i);
                    DAY.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.gray;
                    DAY.GetChild(0).GetComponent<TextMeshProUGUI>().text = overday.ToString();
                    GameObject button = canvas.transform.GetChild(i).gameObject;
                    button.GetComponent<Button>().onClick.RemoveAllListeners();
                    overday++;
                }
            }
            else
            {
                Transform DAY = canvas.transform.GetChild(i);
                DAY.GetChild(0).GetComponent<TextMeshProUGUI>().color = Color.gray;
                DAY.GetChild(0).GetComponent<TextMeshProUGUI>().text = lastmonthdays.ToString();
                GameObject button = canvas.transform.GetChild(i).gameObject;
                button.GetComponent<Button>().onClick.RemoveAllListeners();
                lastmonthdays++;
            }
        }

        monthText.text = SelectDate.Year + "/" + SelectDate.Month;
        int m = SelectDate.Month - 1;
        if(m == 0)
        {
            m = 12;
        }
        prevMonthText.text = "< " + m;
        m = SelectDate.Month + 1;
        if(m == 13)
        {
            m = 1;
        }
        nextMonthText.text = m + " >";
    }

    void set_Date(DateTime date)
    {
        dateText.text = date.Year + "/" + date.Month + "/" + date.Day;
    }

}
