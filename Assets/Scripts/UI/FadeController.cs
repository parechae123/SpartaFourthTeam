using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TitleFade : MonoBehaviour
{
    [SerializeField] private Image titleImage;
    [SerializeField] private float fadeDuration = 2f;

    private void Start()
    {
        StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn()
    {
        float time = 0f;
        Color c = titleImage.color;
        c.a = 0f;           
        titleImage.color = c;

        while (time < fadeDuration)
        {
            time += Time.deltaTime;
            c.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
            titleImage.color = c;
            yield return null;
        }

        c.a = 1f; 
        titleImage.color = c;
    }
}
