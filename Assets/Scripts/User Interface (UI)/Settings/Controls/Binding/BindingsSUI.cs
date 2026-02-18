using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class BindingsSUI : SUISuper
{
    [SerializeField] private RectTransform rebindBindingCover;

    [FormerlySerializedAs("keybindModifier")] [SerializeField] private BindingModifier bindingModifier;
    
    private void Start()
    {
        HideRebindBindingCover();
        Hide();
    }

    public void ShowRebindBindingCover()
    {
        rebindBindingCover.gameObject.SetActive(true);
    }

    public void HideRebindBindingCover()
    {
        rebindBindingCover.gameObject.SetActive(false);
    }

    public override void RestoreDefaults()
    {
        restoreDefaultsSui.Show(RestoreDefaultsActual);
    }

    private void RestoreDefaultsActual()
    {
        bindingModifier.RestoreDefaultKeybinds();
    }
}
