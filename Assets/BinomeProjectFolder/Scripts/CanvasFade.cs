using System;
using UnityEngine;

public class CanvasFade : MonoBehaviour
{
    [SerializeField] bool fadeIn = false, fadeOut = true;
    [SerializeField] CanvasGroup canvasGroup = null;
    [SerializeField] float currentTime = 0.0f, maxTime = 1.0f;

    public bool FadeIn { get => fadeIn; set => fadeIn = value; }
    public bool FadeOut { get => fadeOut; set => fadeOut = value; }

    public event Action OnFadeFinished = null;

    private void Start()
    {
        if (!canvasGroup) return;

        canvasGroup.alpha = 1.0f;

        fadeOut = true;
    }

    void Update()
    {
        if (!canvasGroup) return;

        if (fadeIn)
        {
            canvasGroup.alpha = currentTime;
        }

        if(fadeOut)
        {
            canvasGroup.alpha = maxTime - currentTime;
        }

        IncreaseTime();
    }

    void Reset()
    {
        if(fadeIn) canvasGroup.alpha = 1.0f;
        if(fadeOut) canvasGroup.alpha = 0.0f;

        OnFadeFinished?.Invoke();
        fadeIn = false;
        fadeOut = false;
        currentTime = 0.0f;
    }

    void IncreaseTime()
    {
        if(!fadeIn && !fadeOut) return; 

        currentTime += Time.deltaTime;

        if (currentTime > maxTime)
        {
            Reset();
        }
    }
}
