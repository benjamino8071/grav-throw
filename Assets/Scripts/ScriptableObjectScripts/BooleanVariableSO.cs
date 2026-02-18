/*
 * @author Ben Conway
 * @date May 2024
 */
using System;
using UnityEngine;

[CreateAssetMenu]
public class BooleanVariableSO : ScriptableObject
{
    [SerializeField] private string ppParamNam;
    
    [SerializeField] private bool val;

    [SerializeField] private bool defVal;
    
    #region VALUE LOADER

    public void LoadValue()
    {
        if (PlayerPrefs.HasKey(ppParamNam))
        {
            val = Convert.ToBoolean(PlayerPrefs.GetInt(ppParamNam));
        }
        else
        {
            SetDefaultValue();
        }
    }

    #endregion
    
    #region SETTERS

    public void ChangeValue()
    {
        val = !val;
        
        PlayerPrefs.SetInt(ppParamNam, Convert.ToInt32(val));
    }
    
    public void SetDefaultValue()
    {
        val = defVal;
        
        PlayerPrefs.SetInt(ppParamNam, Convert.ToInt32(val));
    }

    public void SetToTrue()
    {
        val = true;
        
        PlayerPrefs.SetInt(ppParamNam, Convert.ToInt32(val));
    }

    public void SetToFalse()
    {
        val = false;
        
        PlayerPrefs.SetInt(ppParamNam, Convert.ToInt32(val));
    }

    #endregion

    #region GETTERS

    public bool GetValue()
    {
        return val;
    }

    #endregion
}
