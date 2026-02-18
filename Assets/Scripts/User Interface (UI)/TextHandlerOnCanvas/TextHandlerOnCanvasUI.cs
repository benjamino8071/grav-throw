using TMPro;
using UnityEngine;

/// <summary>
/// Provides functionality for keeping text font and size correct based on customisation from the player.
/// 
/// Note UpdateTextSize is currently not in use.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class TextHandlerOnCanvasUI : MonoBehaviour
{
    [SerializeField] private ListWithTextFontsSO listWithTextFontsSO;

    private void Awake()
    {
        listWithTextFontsSO.LoadPointer();
    }

    private void Update()
    {
        UpdateTextFont();
    }

    private void UpdateTextFont()
    {
        TMP_FontAsset newFont = listWithTextFontsSO.GetCurrentValue();

        TextMeshProUGUI[] textObjects = GetComponentsInChildren<TextMeshProUGUI>();

        foreach (TextMeshProUGUI textObject in textObjects)
        {
            textObject.font = newFont;
        }
    }
    
    private void UpdateTextSize(float sliderValue)
    {
        TextMeshProUGUI[] textObjects = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>();
        
        foreach (TextMeshProUGUI textObject in textObjects)
        {
            textObject.fontSize = sliderValue;
        }
    }
}
