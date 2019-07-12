using System;
using UnityEngine;

public class AnalogClockManager : MonoBehaviour
{
    [SerializeField]
    private Transform hourHand = null;
    [SerializeField]
    private Transform minuteHand = null;

    private float elapsedTime = 0f;
    private float refreshRate = 1f;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if (elapsedTime > refreshRate)
        {
            elapsedTime = 0f;
            SetClockHands();
        }
    }

    private void SetClockHands()
    {
        DateTime dt = DateTime.Now;

        int nHour = int.Parse(dt.ToString("hh"));
        int nMin = int.Parse(dt.ToString("mm"));
        int nSec = int.Parse(dt.ToString("ss"));

        hourHand.localEulerAngles = new Vector3(hourHand.localEulerAngles.x, hourHand.localEulerAngles.y, GetHourInDegrees(nHour, nMin));
        minuteHand.localEulerAngles = new Vector3(minuteHand.localEulerAngles.x, minuteHand.localEulerAngles.y, GetMinuteInDegrees(nMin, nSec));
    }

    private float GetMinuteInDegrees(int minute, int seconds)
    {
        float fMinute = minute + Mathf.InverseLerp(0, 60, seconds);
        return Mathf.Lerp(0, 360, Mathf.InverseLerp(0, 60, fMinute));
    }

    private float GetHourInDegrees(int hour, int minute)
    {
        return (hour * 30) + minute * .5f;
    }
}
