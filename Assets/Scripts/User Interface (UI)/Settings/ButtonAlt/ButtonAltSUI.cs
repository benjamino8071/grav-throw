using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ButtonAltSUI : MonoBehaviour
{
    [SerializeField] private MenuUISuper parentUI;
    
    [SerializeField] private MenuUISuper uiToShow;
    [SerializeField] private Button button;

    private void Awake()
    {
        button.onClick.AddListener(() =>
        {
            SoundEffectsMenu.Instance.PlayMenuButtonPressedSound();
            Debug.Log(gameObject.name);
            Debug.Log(parentUI);
            Debug.Log(uiToShow);
            parentUI.Hide();
            uiToShow.Show();
        });
    }
}
