using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medal : MonoBehaviour
{
    public AudioManager _audiomanager;
    public AudioClip soundEffect;

    public Material allInOneMaterial;
    public float timeBetweenShines;
    public AnimationCurve shineAnimationCurve;
    public float animationCurveTime;
    public float shineSpeed;

    private void Awake()
    {
        allInOneMaterial = GetComponent<Image>().material;
    }

    public void PlayMedalSounds()
    {
        _audiomanager.PlaySound(soundEffect, 1.4f);
    }

    public void StartShine()
    {
        StartCoroutine(ShineCoroutine());
    }

    IEnumerator ShineCoroutine()
    {
        yield return new WaitForSeconds(timeBetweenShines);

        while(allInOneMaterial.GetFloat("_ShineLocation") < 1)
        {
            float position = shineAnimationCurve.Evaluate(animationCurveTime);
            allInOneMaterial.SetFloat("_ShineLocation", position);
            animationCurveTime += Time.deltaTime * shineSpeed;
            yield return null;
        }

        animationCurveTime = 1f;
        yield return new WaitForSeconds(0.3f);

        while (allInOneMaterial.GetFloat("_ShineLocation") != 0)
        {
            float position = shineAnimationCurve.Evaluate(animationCurveTime);
            allInOneMaterial.SetFloat("_ShineLocation", position);
            animationCurveTime -= Time.deltaTime * shineSpeed;
            yield return null;
        }

        animationCurveTime = 0f;
        StartCoroutine(ShineCoroutine());
    }
}
