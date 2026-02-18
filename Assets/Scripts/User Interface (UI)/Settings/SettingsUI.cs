using UnityEngine;

/// <summary>
/// Offers basic visual functionality for settings interface.
///
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class SettingsUI : MenuUISuper
{
    [SerializeField] private MenuUISuper menuToShow;
    
    private void Start()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
                
        menuToShow.Show();
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
    }
}
