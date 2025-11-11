using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeCounter : MonoBehaviour
{
    [SerializeField] TimeTrigger startTrigger = null, endTrigger = null;
    [SerializeField] float currentTime = 0.0f;
    [SerializeField] bool canTick = false;
    [SerializeField] Text counterText = null;

    void Update()
    {
        UpdateCounter();
    }

    private void OnEnable()
    {
        if (!startTrigger || !endTrigger) return;

        startTrigger.onTrigger += StartCounter;
        endTrigger.onTrigger += EndCounter;
    }
    private void OnDisable()
    {
        if (!startTrigger || !endTrigger) return;
        
        startTrigger.onTrigger -= StartCounter;
        endTrigger.onTrigger -= EndCounter;
    }

    void UpdateCounter()
    {
        if (!canTick || !counterText) return;

        currentTime += Time.deltaTime;
        int _seconds = (int)currentTime % 60;
        int _minutes = (int)currentTime / 60;
        string _result = string.Format("{0:00}:{1:00}", _minutes, _seconds);
        counterText.text = _result;
    }

    void StartCounter()
    {
        canTick = true;
    }

    void EndCounter()
    {
        canTick = false;
    }
}
