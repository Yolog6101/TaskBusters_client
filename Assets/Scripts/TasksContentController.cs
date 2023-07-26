using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;

public class TasksContentController : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject content;

    public void generate(int num){
        int i;
        Vector2 sdPrefab = taskPrefab.GetComponent<RectTransform>().sizeDelta;
        for(i = 0; i < num; i++)
        {
            GameObject obj = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity);
            obj.name="Task"+i.ToString();
            //obj.transform.SetParent(content.transform);
            obj.transform.SetParent(content.transform,false);
            //相対サイズにすると大きくなるので相対サイズをOFF
            //参考 https://monaski.hatenablog.com/entry/2015/11/03/133612
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
        }
        Vector2 sdContent = content.GetComponent<RectTransform>().sizeDelta;
        sdContent.y = sdPrefab.y * i;
        content.GetComponent<RectTransform>().sizeDelta = sdContent;

    }
   
}
