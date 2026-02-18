using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Allows player to set red, green and blue values for the colour of the ball marker display.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class BallMarkerColourSUI : ColourChangeSUI
{
    //Uses image variable inherited from ColourChangeUI as the boxColourImage
    [SerializeField] private Image boxColourImage;
    [SerializeField] private Image arrowColourImage;
    
    private void Start()
    {
        Hide();
    }
    
    private void Update()
    {
        UpdateColour();
    }
    
    protected override void UpdateColour()
    {
        float red = redValue.GetValue() / 255;
        float green = greenValue.GetValue() / 255;
        float blue = blueValue.GetValue() / 255;
        
        material.color = new Color(red, green, blue);
        
        boxColourImage.color = new Color(red, green, blue);
        arrowColourImage.color = new Color(red, green, blue);
    }
}
