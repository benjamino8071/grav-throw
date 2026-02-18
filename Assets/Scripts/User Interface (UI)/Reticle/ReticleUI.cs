using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Ensures the colour and size of the reticle is updated based on customisation set by player in the settings menu.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
public class ReticleUI : MonoBehaviour
{
    [SerializeField] private Material reticleMaterial;
    [SerializeField] private Image reticleImage;

    [SerializeField] private FloatVariableSO reticleSizeSO;
    
    private void Start()
    {
        Show();
    }

    private void Update()
    {
        UpdateColour();
        UpdateSize();
    }

    private void UpdateColour()
    {
        reticleImage.color = reticleMaterial.color;
    }

    private void UpdateSize()
    {
        reticleImage.rectTransform.sizeDelta = new Vector2(reticleSizeSO.GetValue(), reticleSizeSO.GetValue());
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
