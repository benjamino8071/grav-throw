using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Provides visual functionality for different menu interfaces.
/// 
/// Show() and Hide() are virtual functions to enable overriding.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class MenuUISuper : MonoBehaviour
{
    [SerializeField] private Selectable selectOnShow;
    
    private Action onHideAction;
    
    public virtual void Show(Action onHideAction=null)
    {
        this.onHideAction = onHideAction;
        
        gameObject.SetActive(true);

        if (selectOnShow)
        {
            selectOnShow.Select();
        }
    }

    public virtual void Hide()
    {
        gameObject.SetActive(false);
        
        onHideAction?.Invoke();
    }
}