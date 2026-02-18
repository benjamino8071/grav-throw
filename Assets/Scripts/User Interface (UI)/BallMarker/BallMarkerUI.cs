using System;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// Allows other scripts to control visibility of the ball marker.
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class BallMarkerUI : MonoBehaviour
{
    [SerializeField] private RectTransform child;

    [FormerlySerializedAs("continueUI")] [SerializeField] private ContinueUISuper continueUISuper;

    [SerializeField] private BooleanVariableSO showBallMarkerSO;
    
    private void Start()
    {
        showBallMarkerSO.SetToTrue();
        
        //FOR LEVEL SCENES
        if (continueUISuper != null)
        {
            continueUISuper.OnLevelComplete += ContinueUISuperOnOnLevelComplete;
        }
    }

    private void Update()
    {
        if (showBallMarkerSO.GetValue())
        {
            ShowChild();
        }
        else
        {
            Hide();
        }
    }

    private void ContinueUISuperOnOnLevelComplete(object sender, EventArgs e)
    {
        showBallMarkerSO.SetToFalse();
        Hide();
    }

    public void ShowChild()
    {
        child.gameObject.SetActive(true);
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        child.gameObject.SetActive(false);
    }
}
