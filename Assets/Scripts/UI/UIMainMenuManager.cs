using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    public static UIMainMenuManager instance;

    public static string MainMenuSceneName;
    public static string OptionsSceneName;
    public static string LevelSelectionSceneName;

    public string MainMenuName;
    public string OptionsName;
    public string LevelSelectionName;

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
        MainMenuSceneName = MainMenuName;
        OptionsSceneName = OptionsName;
        LevelSelectionSceneName = LevelSelectionName;
    }

    // Start is called before the first frame update
    public void LoadLevelSelection()
    {
        SceneFader.instance.FadeTo(Config.SCENES_LEVELSELECTIONNAME);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
