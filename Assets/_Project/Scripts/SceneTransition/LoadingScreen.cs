using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class LoadingScreen : UiPanel
{
    [SerializeField] private TextMeshProUGUI textPercentage;
    [SerializeField] private Image progressBar;

    private float lastProgressValue = 0f;

    public void UpdateProgress(float progress, bool forceSet = false)
    {
        float newValue;
        if (forceSet)
        {
            newValue = progress;
        }
        else
        {
            newValue = Mathf.Lerp(lastProgressValue, progress, 0.02f);
        }
        
        textPercentage.text = string.Concat(Math.Round(newValue * 100), "%");
        progressBar.fillAmount = newValue;

        lastProgressValue = newValue;
    }
}
