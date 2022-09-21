using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using UnityEngine.UI;

public static class Utils
{
    public static IEnumerator LerpAlpha(Image image, float endAlpha, float duration)
    {
        UnityEngine.Debug.Log("entre al lerp normal");

        float elapsedTime = 0f;
        float startAlpha = image.color.a;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }       
    }

    public static IEnumerator LerpAlpha(Image image, float endAlpha, float duration, Action<string> callback, string newLocationName)
    {
        UnityEngine.Debug.Log("entre al lerp de string");

        

        float elapsedTime = 0f;
        float startAlpha = image.color.a;
        while(elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            image.color = new Color(image.color.r, image.color.g, image.color.b, newAlpha);
            yield return null;
        }

        if (callback != null) callback(newLocationName);
    }

    public static IEnumerator LerpCanvasAlpha(CanvasGroup canvasGroup, float endAlpha, float duration, Action<string> callback, string newLocationName)
    {
        UnityEngine.Debug.Log("entre al lerp de string");

        Stopwatch stopWatch = new Stopwatch();
        stopWatch.Start();
        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;
        while (elapsedTime < duration)
        {
            
            //Debug.Log("while loop");
            //Debug.Log("deltatime en coroutine: " + Time.deltaTime);
            //elapsedTime += (float)stopWatch.Elapsed.TotalSeconds;
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }

        stopWatch.Stop();

        UnityEngine.Debug.Log("sali del while loop");

        if (callback != null) callback(newLocationName);
    }

    public static IEnumerator LerpCanvasAlpha(CanvasGroup canvasGroup, float startAlpha, float endAlpha, float duration, bool interactable)
    {
        canvasGroup.interactable = interactable;
        canvasGroup.blocksRaycasts = interactable;

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            float newAlpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / duration);
            canvasGroup.alpha = newAlpha;
            yield return null;
        }
    }
}
