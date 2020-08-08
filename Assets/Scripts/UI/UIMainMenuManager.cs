using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenuManager : MonoBehaviour
{
    public static UIMainMenuManager instance;
    public GameObject OptionsObject;

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

    }

    public void CloseOptions()
    {
        OptionsObject.SetActive(false);

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
