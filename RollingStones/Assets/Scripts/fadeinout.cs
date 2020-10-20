using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class fadeinout : MonoBehaviour
{
    public static fadeinout Fadeinout;
    public Image Fade;
    public float FadeTime = 2f;
    float start;
    float end;
    float time = 0f;
    bool isPlaying = false;
    
    void Sart()
    {

    }
    public void FadeInImage()
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        StartCoroutine("fadeinplay");    //코루틴 실행
    }

    public void FadeOutImage()
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        StartCoroutine("fadeoutplay");
    }

    IEnumerator fadeinplay()
    {
        Fade = GetComponent<Image>();
        isPlaying = true;
        Color fadecolor = Fade.color;
        while (fadecolor.a < 1f)//갈수록 투명

        {
            fadecolor.a += Time.deltaTime / FadeTime;
            Fade.color = fadecolor;
            yield return null;
        }
        Fade.color = fadecolor;
        isPlaying = false;
    }
    IEnumerator fadeoutplay()
    {
        Fade = GetComponent<Image>();
        isPlaying = true;
        Color fadecolor = Fade.color;
        while (fadecolor.a > 0f)//갈수록 투명

        {
            fadecolor.a += Time.deltaTime / FadeTime;
            Fade.color = fadecolor;
            yield return null;
        }
        Fade.color = fadecolor;
        isPlaying = false;
    }
}
