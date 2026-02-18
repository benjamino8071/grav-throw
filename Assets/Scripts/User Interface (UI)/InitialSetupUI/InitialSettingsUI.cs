using UnityEngine;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class InitialSettingsUI : SUISuper
{
    [SerializeField] private MenuUISuper menuToShow;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public override void Hide()
    {
        gameObject.SetActive(false);
        
        menuToShow.Show();
    }
}
