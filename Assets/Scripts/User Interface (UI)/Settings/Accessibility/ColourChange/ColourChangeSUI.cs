using UnityEngine.UI;
using UnityEngine;

/// <summary>
/// Contains functionality for changing colour of an image object.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ColourChangeSUI : SUISuper
{
    [SerializeField] protected FloatVariableSO redValue;
    [SerializeField] protected FloatVariableSO greenValue;
    [SerializeField] protected FloatVariableSO blueValue;

    [SerializeField] protected Image image;
    
    [SerializeField] protected Material material;
    
    private void Start()
    {
        Hide();
    }
    
    private void Update()
    {
        UpdateColour();
    }

    protected virtual void UpdateColour()
    {
        float red = redValue.GetValue() / 255;
        float green = greenValue.GetValue() / 255;
        float blue = blueValue.GetValue() / 255;
        
        material.color = new Color(red, green, blue);
        
        image.color = new Color(red, green, blue);
    }
}
