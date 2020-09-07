using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    public static UIMainMenuManager instance;
    public GameObject MainMenuObject;
    public GameObject OptionsObject;
    public GameObject LevelSelectionObject;
    public GameObject SkinSelectionObject;

    private void Awake()
    {
        if (instance)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void OpenOptions()
    {
        OptionsObject.SetActive(true);
        MainMenuObject.SetActive(false);
        
    }

    public void CloseOptions()
    {
        OptionsObject.SetActive(false);
        MainMenuObject.SetActive(true);
    }

    // Start is called before the first frame update
    public void OpenLevelSelection()
    {
        LevelSelectionObject.SetActive(true);
    }

    public void CloseLevelSelection()
    {
        LevelSelectionObject.SetActive(false);
    }

    public void OpenSkinSelector()
    {
        SkinSelectionObject.SetActive(true);
    }
    
    public void CloseSkinSelector()
    {
        SkinSelectionObject.SetActive(false);
    }
}
