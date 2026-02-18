using UnityEngine;

/// <summary>
/// If developer wishes to add more resolutions, the resolution must have either 4:3, 5:4, 16:9 or 16:10
/// aspect ratio.
/// They must then add this resolution to the scriptable object referenced with currResSO.
/// 
/// @author Ben Conway
/// @date May 2024
/// </summary>
[RequireComponent(typeof(ListWithIntPairsPlayerChoice))]
public class ResolutionSpecific : MonoBehaviour
{
    [SerializeField] private ListWithIntPairsPlayerChoice listWithIPPC;
    
    [SerializeField] private ListWithFullScreenModesSO displayModeSO; //For changing resolution
    
    [SerializeField] private StringVariableSO fourByThreeAndFiveByFourSO;
    [SerializeField] private StringVariableSO sixteenByNineSO;
    [SerializeField] private StringVariableSO sixteenByTenSO;
    
    [SerializeField] private ListWithIntPairsSO currResSO;
    
    /*
     * Will find out what the current aspect ratio is.
     * If it is different to aspect ratio in previous frame, then update aspect ratio
     */
    [SerializeField] private ListWithStringVariablesSO aspRatsSO;
    
    [SerializeField] private ListWithIntPairsSO l4By3And5By4ResSO;
    [SerializeField] private ListWithIntPairsSO l16By9ResSO;
    [SerializeField] private ListWithIntPairsSO l16By10ResSO;

    private ListWithIntPairsSO currResList = null;
    
    private void Update()
    {
        if (aspRatsSO.GetCurrentValue().Value == fourByThreeAndFiveByFourSO.Value)
        {
            //Only needs to change display if the aspect ratio has changed
            if (currResList != l4By3And5By4ResSO)
            {
                Display4By3And5by4Resolutions();
                currResList = l4By3And5By4ResSO;
            }
        }
        else if (aspRatsSO.GetCurrentValue().Value == sixteenByNineSO.Value)
        {
            //Only needs to change display if the aspect ratio has changed
            if (currResList != l16By9ResSO)
            {
                Display16By9Resolutions();
                currResList = l16By9ResSO;
            }
        }
        else if (aspRatsSO.GetCurrentValue().Value == sixteenByTenSO.Value)
        {
            //Only needs to change display if the aspect ratio has changed
            if (currResList != l16By10ResSO)
            {
                Display16By10Resolutions();
                currResList = l16By10ResSO;
            }
        }
        else
        {
            //Should never get here!
            Debug.LogError("ERROR: Did not set resolution list correctly!");
        }
        UpdateResolution();
    }

    private void Display4By3And5by4Resolutions()
    {
        currResSO.SetList(l4By3And5By4ResSO.GetList());
        currResSO.SetPointer(l4By3And5By4ResSO.GetPointerValue());
    }
    
    private void Display16By9Resolutions()
    {
        currResSO.SetList(l16By9ResSO.GetList());
        currResSO.SetPointer(l16By9ResSO.GetPointerValue());
    }

    private void Display16By10Resolutions()
    {
        currResSO.SetList(l16By10ResSO.GetList());
        currResSO.SetPointer(l16By10ResSO.GetPointerValue());
    }
    
    private void UpdateResolution()
    {
        int width = currResSO.GetCurrentValue().itemOne;
        int height = currResSO.GetCurrentValue().itemTwo;
        
        //From Display Mode
        FullScreenMode fullScreenMode = displayModeSO.GetCurrentValue();
        //
        
        Screen.SetResolution(width, height, fullScreenMode);
        
        UpdateResolutionVisual();
    }

    private void UpdateResolutionVisual()
    {
        string width = currResSO.GetCurrentValue().itemOne.ToString();
        string height = currResSO.GetCurrentValue().itemTwo.ToString();
        string resolutionText = width + "x" + height;
        
        listWithIPPC.UpdateVisual(resolutionText);
    }
}
