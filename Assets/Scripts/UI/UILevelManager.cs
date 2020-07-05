using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelManager : MonoBehaviour
{
    public static UILevelManager instance;
    public GameObject m_LoseMenu;

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

    public void ShowLoseMenu()
    {
        m_LoseMenu.SetActive(true);
    }

    public void RestartGame()
    {
        Debug.Log(SceneManager.GetActiveScene().name);
        SceneFader.instance.FadeTo(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        SceneFader.instance.FadeTo(Config.SCENES_MAINMENUNAME);
    }
}
