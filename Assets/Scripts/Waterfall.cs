using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waterfall : MonoBehaviour
{
    private Material waterfallMaterial;
    private float yOffset;

    /// <summary>
    /// Cuanto se mueve la waterfall
    /// </summary>
    [SerializeField] private float distanceToTravel;
    
    /// <summary>
    /// Cada cuanto voy a actualizar el fondo. se divide por 60 antes de ocuparlo
    /// </summary>
    private float frames;
    [SerializeField] private float baseFrames;

    void Awake()
    {
        waterfallMaterial = GetComponent<Renderer>().material;
        StartCoroutine(AnimateLowFrame());
    }

    public void ResetWaterfallFrames()
    {
        frames = baseFrames;
    }

    public void SetWaterfallFrames(float multiplier)
    {
        frames = baseFrames * (1 + multiplier);
    }

    IEnumerator AnimateLowFrame()
    {
        while (true)
        {
            yOffset += distanceToTravel;
            waterfallMaterial.SetTextureOffset("_MainTex", new Vector2(0, yOffset));

            var wait = 1 / frames;
            yield return new WaitForSeconds(wait);
        }
    }
}
