using System;
using UnityEngine;

public class AnalogClockManager : MonoBehaviour
{
    [SerializeField]
    private Transform hourHand = null;
    [SerializeField]
    private Transform minuteHand = null;
    [SerializeField]
    [Tooltip("Optional Second Hand Transform")]
    private Transform secondHand = null;

    [SerializeField]
    [Range(0f, 1f)]
    private float refreshRate = 0f;
    private float elapsedTime = 0f;

    const float HOURS_ON_CLOCK = 12f;
    const float MINUTES_ON_CLOCK = 60f;
    const float SECONDS_ON_CLOCK = 60f;
    const float MILLISECONDS_ON_CLOCK = 1000f;
    const float HOUR_DEGREES = 360f / HOURS_ON_CLOCK;
    const float MINUTE_DEGREES = 360f / MINUTES_ON_CLOCK;
    const float SECOND_DEGREES = 360f / SECONDS_ON_CLOCK;

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        // Refresh once after each refreshRate interval
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
        int nMilSec = dt.Millisecond;

        SetHandRotation(hourHand, GetHourInDegrees(nHour, nMin));
        SetHandRotation(minuteHand, GetMinuteInDegrees(nMin, nSec));
        if (secondHand != null)
        {
            SetHandRotation(secondHand, GetSecondInDegrees(nSec, nMilSec));
        }
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
    private float GetSecondInDegrees(int second, int milliseconds)
    {
        float fSecond = second + (milliseconds / MILLISECONDS_ON_CLOCK);
        return fSecond * MINUTE_DEGREES;
    }
}
