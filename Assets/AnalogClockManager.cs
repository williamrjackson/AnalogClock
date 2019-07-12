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

    const float HOURS_ON_CLOCK = 12f;
    const float MINUTES_ON_CLOCK = 60f;
    const float SECONDS_ON_CLOCK = 60f;
    const float HOUR_DEGREES = 360f / HOURS_ON_CLOCK;
    const float MINUTE_DEGREES = 360f / MINUTES_ON_CLOCK;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Refresh once every second
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
        // Update Z axis, retain others.
        hand.localEulerAngles = new Vector3(hand.localEulerAngles.x, hand.localEulerAngles.y, degrees);
    }

    private float GetHourInDegrees(int hour, int minute)
    {
        return ((hour * HOUR_DEGREES) + minute * HOUR_DEGREES / MINUTES_ON_CLOCK);
    }

    private float GetMinuteInDegrees(int minute, int seconds)
    {
        float fMinute = minute + (seconds / SECONDS_ON_CLOCK);
        return fMinute * MINUTE_DEGREES;
    }
}
