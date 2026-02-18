/*
 * @author Ben Conway
 * @date May 2024
 */
using UnityEngine;

[CreateAssetMenu()]
public class FloatVariableSO : ScriptableObject
{
    [SerializeField] private string ppParamNam;
    
    [SerializeField] private float val;

    [SerializeField] private float defVal;
    
    #region VALUE LOADER

    public void LoadValue()
    {
        if (PlayerPrefs.HasKey(ppParamNam))
        {
            val = PlayerPrefs.GetFloat(ppParamNam);
        }
        else
        {
            SetDefaultValue();
        }
    }

    #endregion
    
    #region SETTERS

    public void SetDefaultValue()
    {
        val = defVal;
        PlayerPrefs.SetFloat(ppParamNam, val);
    }

    public void SetValue(float newVal)
    {
        val = newVal;
        PlayerPrefs.SetFloat(ppParamNam, val);
    }

    #endregion

    #region GETTERS

    public float GetValue()
    {
        return val;
    }

    #endregion
}
