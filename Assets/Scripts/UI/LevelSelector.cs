using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public Button[] levelButton;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);

        if (levelButton.Length > 0)
            for (int i = 0; i < levelButton.Length; i++)
            {
                if (i + 1 > levelReached)
                    levelButton[i].interactable = false;
            }
    }

    public void Select(string levelName)
    {
        SceneFader.instance.FadeTo(levelName);
    }
}
