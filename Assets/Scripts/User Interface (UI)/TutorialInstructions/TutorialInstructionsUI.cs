using System;
using UnityEngine;

/// <summary>
/// Controls visibility of instructions display (on the HUD) depending on whether other interfaces are being shown.
/// Helps to reduce clutter  on-screen.
///
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class TutorialInstructionsUI : MonoBehaviour
{
    [SerializeField] private ContinueUISuper continueUISuper;

    [SerializeField] private PauseUI pauseUIoffline;
    private void Start()
    {
        Show();
        
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

    private void ContinueUISuper_OnLevelComplete(object sender, EventArgs e)
    {
        Hide();
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
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
