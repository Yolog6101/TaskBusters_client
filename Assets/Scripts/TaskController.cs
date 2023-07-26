using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    [SerializeField] private GameObject taskPrefab;
    [SerializeField] private GameObject content;

    // Start is called before the first frame update
    void Start()
    {
        int i;
        Vector2 sdPrefab = taskPrefab.GetComponent<RectTransform>().sizeDelta;
        for(i = 0; i < 10; i++)
        {
            GameObject obj = Instantiate(taskPrefab, Vector3.zero, Quaternion.identity);
            obj.transform.SetParent(content.transform);
            obj.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -sdPrefab.y / 2 * (2 * i + 1));
        }
        Vector2 sdContent = content.GetComponent<RectTransform>().sizeDelta;
        sdContent.y = sdPrefab.y * i;
        content.GetComponent<RectTransform>().sizeDelta = sdContent;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
