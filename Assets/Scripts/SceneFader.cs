using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneFader : MonoBehaviour
{
    public Image fadeImage;
    public AnimationCurve fadeCurve;

    void Start()
    {
        StartCoroutine(FadeIn());
    }

    public void FadeTo(string scene)
    {
        StartCoroutine(FadeOut(scene));
    }

    IEnumerator FadeIn()
    {
        var t = 1f;

        while (t > 0)
        {
            t -= Time.deltaTime;
            var alpha = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }
    }

    IEnumerator FadeOut(string scene)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime;
            var alpha = fadeCurve.Evaluate(t);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return 0;
        }

        SceneManager.LoadSceneAsync(scene);
    }

}
