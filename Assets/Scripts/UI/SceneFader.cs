using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    public Image img;
    public AnimationCurve curve;
    public bool m_FadeInStart;

    private bool m_Transitioning;
    public static SceneFader instance;

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


    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        m_Transitioning = false;

        if (SceneManager.GetActiveScene().buildIndex != 0)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        m_Transitioning = true;
        float t = 1f;

        while (t > 0f)
        {
            t -= Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
            yield return 0;
        }
        m_Transitioning = false;

    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    public void FadeTo(int index)
    {
        string pathToScene = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(pathToScene);
        StartCoroutine(FadeOut(sceneName));
    }

    IEnumerator FadeOut(string scene)
    {
        m_Transitioning = true;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            float a = curve.Evaluate(t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, a);
            yield return 0;
        }

        SceneManager.LoadScene(scene);
        m_Transitioning = false;
        StartCoroutine(FadeIn());
    }
}
