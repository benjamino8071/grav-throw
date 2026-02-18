using System;
using UnityEngine;

/// <summary>
/// Includes functionality for restoring settings to their default value - universal function in all settings.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class SUISuper : MenuUISuper
{
    public event EventHandler OnRestoreDefaults;
    
    [SerializeField] protected RestoreDefaultsSUI restoreDefaultsSui;
    
    public virtual void RestoreDefaults()
    {
        restoreDefaultsSui.Show(InvokeRestoreDefaults);
    }

    private void InvokeRestoreDefaults()
    {
        OnRestoreDefaults?.Invoke(this, EventArgs.Empty);
    }
}