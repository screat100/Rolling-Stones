using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class fadeinout : MonoBehaviour
{
    public static fadeinout Fadeinout;
    public Image Fade;
    public float FadeInTime = 0.5f;
    public float FadeOutTime = 0.5f;
    bool isPlaying = false;

    void Awake()
    {
        if (fadeinout.Fadeinout == null)
            fadeinout.Fadeinout = this;
    }

    void Sart()
    {
        Fade = GetComponent<Image>();
    }
    public void FadeInImage()//불투명
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        StartCoroutine("fadeinplay");    //코루틴 실행
    }

    public void FadeOutImage()//투명
    {
        if (isPlaying == true) //중복재생방지
        {
            return;
        }
        StartCoroutine("fadeoutplay");
    }

    IEnumerator fadeinplay()
    {
        isPlaying = true;
        Color fadecolor = Fade.color;
        while (fadecolor.a < 1f)//갈수록 불투명

        {
            fadecolor.a += Time.deltaTime / FadeInTime;
            Fade.color = fadecolor;
            yield return null;
        }
        Fade.color = fadecolor;
        isPlaying = false;
    }
    IEnumerator fadeoutplay()
    {
        isPlaying = true;
        Color fadecolor = Fade.color;
        while (fadecolor.a > 0f)//갈수록 투명
        {
            fadecolor.a -= Time.deltaTime / FadeOutTime;
            Fade.color = fadecolor;
            yield return null;
        }
        Fade.color = fadecolor;
        isPlaying = false;
    }
}
