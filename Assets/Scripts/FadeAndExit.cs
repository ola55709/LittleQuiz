using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeAndExit : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    public void ExitGame()
    {
        StartCoroutine(FadeOutAndQuit());
    }

    IEnumerator FadeOutAndQuit()
    {
        float timer = 0f;
        Color c = fadeImage.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            c.a = timer / fadeDuration;
            fadeImage.color = c;
            yield return null;
        }

        c.a = 1f;
        fadeImage.color = c;

        yield return new WaitForSeconds(0.2f);

        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}