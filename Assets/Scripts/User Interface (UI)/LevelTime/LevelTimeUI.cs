using System;
using TMPro;
using UnityEngine;

/// <summary>
/// Handles visual display of timer during a level (of the the tutorial).
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class LevelTimeUI : MonoBehaviour
{
    [SerializeField] private PauseUI pauseUIoffline;
    [SerializeField] private ContinueUISuper continueUISuper;
    
    [SerializeField] private FloatVariableSO timerSO;
    
    [SerializeField] private TextMeshProUGUI timeValueText;

    private void Awake()
    {
        pauseUIoffline.OnPaused += PauseUIofflineOnOnPaused;
        pauseUIoffline.OnUnPaused += PauseUIofflineOnOnUnPaused;
        continueUISuper.OnLevelComplete += ContinueUISuper_OnLevelComplete;
    }

    private void PauseUIofflineOnOnUnPaused(object sender, EventArgs e)
    {
        Show();
    }

    private void PauseUIofflineOnOnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void Start()
    {
        Show();
        timerSO.SetDefaultValue();
    }

    private void Update()
    {
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timerSO.SetValue(timerSO.GetValue()+Time.deltaTime);
        timeValueText.SetText(timerSO.GetValue().ToString("0.00")+"s");
    }
    
    private void ContinueUISuper_OnLevelComplete(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        pauseUIoffline.OnPaused -= PauseUIofflineOnOnPaused;
        pauseUIoffline.OnUnPaused -= PauseUIofflineOnOnUnPaused;
        continueUISuper.OnLevelComplete -= ContinueUISuper_OnLevelComplete;
    }
}
