using UnityEngine;

public class OrderTimer : MonoBehaviour
{
    public float OrderTimeLimit = 60f;
    public float TimeSpent;

    [Range(0f,1f)]
    public float WarningPercentage = 0.75f;

    public bool running = false;

    public enum TimerPhase
    {
        Normal,
        Warning,
        OutOfTime
    }

    public TimerPhase CurrentPhase = TimerPhase.Normal; //doe hiermee wat je wil

    public void Update()
    {
        if (running)
        {
            TimeSpent += Time.deltaTime;
        }

        switch (TimeSpent)
        {
            case float t when (t >= OrderTimeLimit):
                CurrentPhase = TimerPhase.OutOfTime;
                break;
            case float t when (t >= OrderTimeLimit * WarningPercentage):
                CurrentPhase = TimerPhase.Warning;
                break;
            default:
                CurrentPhase = TimerPhase.Normal;
                break;
        }
    }
}
