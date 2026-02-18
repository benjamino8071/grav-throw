using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Represents slider for a Float Variable SO
/// As the slider may be used for different purposes,
/// setting child components are for the VISUALS only.
/// May also use this script to get child components.
///
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class SliderFloat : MonoBehaviour
{
    [SerializeField] private bool doNotRoundValueText;
    
    [SerializeField] private SUISuper parentSetting;
    
    [SerializeField] private FloatVariableSO floatVariableSO;
    
    [SerializeField] private Slider slider;
    [SerializeField] private TextMeshProUGUI sliderValueText;
    
    private void Awake()
    {
        slider.onValueChanged.AddListener(OnSliderChanged);
        parentSetting.OnRestoreDefaults += ParentSetting_OnRestoreDefaults;
    }

    private void ParentSetting_OnRestoreDefaults(object sender, EventArgs e)
    {
        SetDefaultSliderValue();
    }

    private void Start()
    {
        LoadSliderValue();
    }

    private void OnSliderChanged(float sliderValue)
    {
        floatVariableSO.SetValue(sliderValue);
        
        UpdateSliderValueText();
    }

    private void UpdateSliderValueText()
    {
        if (doNotRoundValueText)
        {
            string sliderValText = floatVariableSO.GetValue().ToString("F2");
            sliderValueText.SetText(sliderValText);
        }
        else
        {
            string sliderValText = Mathf.Round(floatVariableSO.GetValue()).ToString();
            sliderValueText.SetText(sliderValText);
        }
    }
    
    private void LoadSliderValue()
    {
        floatVariableSO.LoadValue();
        
        slider.value = floatVariableSO.GetValue();
        
        UpdateSliderValueText();
    }
    
    private void SetDefaultSliderValue()
    {
        floatVariableSO.SetDefaultValue();
        
        slider.value = floatVariableSO.GetValue(); //First-time opening game, set slider to half
        UpdateSliderValueText();
    }

    public Slider GetSlider()
    {
        return slider;
    }
}
