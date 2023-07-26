using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalerMatchSetting : MonoBehaviour
{
    CanvasScaler scaler;

    private void Awake()
    {
        scaler = GetComponent<CanvasScaler>();
        AutoSetMatch();

    }

    private void AutoSetMatch()
    {
        if (scaler)
        {
            // 9/16 = 0.5625
            // 0.5625より小さかったらアスペクト比が9:16より小さい
            float ratio = (float)Screen.width / (float)Screen.height;
            scaler.matchWidthOrHeight = ratio < 0.5625 ? 0 : 1;
        }
    }

}
