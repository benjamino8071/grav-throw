using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class ListVariableSO<T> : ScriptableObject
{
    [SerializeField] private string ppParamNam;
    
    [SerializeField] private List<T> theList;
    private int listPtr;

    [SerializeField] private int defPtrVal;
    
    #region POINTER LOADER

    public void LoadPointer()
    {
        if (PlayerPrefs.HasKey(ppParamNam))
        {
            listPtr = PlayerPrefs.GetInt(ppParamNam);
        }
        else
        {
            SetDefaultPtr();
        }
    }

    #endregion
    
    #region SETTERS

    public void SetDefaultPtr()
    {
        listPtr = defPtrVal;
        PlayerPrefs.SetInt(ppParamNam, listPtr);
    }
    
    public void SetPointer(int newPtr)
    {
        listPtr = newPtr;
        PlayerPrefs.SetInt(ppParamNam, listPtr);
    }
    
    public void SetList(List<T> newList)
    {
        theList = newList;
    }
    
    //False if pointer is already pointing at the start of the list (so cannot be decremented further), True otherwise
    public bool DecrementPointer()
    {
        if (IsPtrAtStartOfList())
            return false;
        
        listPtr--;
        PlayerPrefs.SetInt(ppParamNam, listPtr);
        return true;
    }

    //False if pointer is already pointing at the start of the list (so cannot be decremented further), True otherwise
    public bool IncrementPointer()
    {
        if (IsPtrAtEndOfList())
            return false;
        
        listPtr++;
        PlayerPrefs.SetInt(ppParamNam, listPtr);
        return true;
    }

    #endregion

    #region GETTERS

    public T GetCurrentValue()
    {
        return theList[listPtr];
    }

    public int GetPointerValue()
    {
        return listPtr;
    }

    public List<T> GetList()
    {
        return theList;
    }

    #endregion

    #region BOOLEAN RESULTS

    public bool IsPtrAtStartOfList()
    {
        return listPtr == 0;
    }

    public bool IsPtrAtEndOfList()
    {
        return listPtr == theList.Count - 1;
    }

    #endregion
}