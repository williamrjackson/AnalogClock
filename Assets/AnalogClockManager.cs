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

        int nHour = dt.Hour;
        int nMin = dt.Minute;
        int nSec = dt.Second;

        SetHandRotation(hourHand, GetHourInDegrees(nHour, nMin));
        SetHandRotation(minuteHand, GetMinuteInDegrees(nMin, nSec));
    }

    private void SetHandRotation(Transform hand, float degrees)
    {
        hand.localEulerAngles = new Vector3(hand.localEulerAngles.x, hand.localEulerAngles.y, degrees);
    }

    private float GetMinuteInDegrees(int minute, int seconds)
    {
        float fMinute = minute + seconds / 60f;
        return Mathf.Lerp(0f, 360f, fMinute / 60f);
    }

    private float GetHourInDegrees(int hour, int minute)
    {
        return (hour * 30f) + minute * .5f;
    }
}
