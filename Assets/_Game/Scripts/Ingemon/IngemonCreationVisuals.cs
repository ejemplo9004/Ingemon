using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngemonCreationVisuals : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    private bool isFaded;

    public IEnumerator FadeEgg(RawImage image, Color start, Color end, float duration)
    {
        isFaded = false;
        if(start.a > end.a) particles.Play(); 
        for (float t = 0f; t < duration; t+= Time.deltaTime)
        {
            float normalizedTime = t / duration;
            image.color = Color.Lerp(start, end, normalizedTime);
            yield return null;
        }
        image.color = end;
        isFaded = true;
    }
    public bool IsFaded => isFaded;
}
