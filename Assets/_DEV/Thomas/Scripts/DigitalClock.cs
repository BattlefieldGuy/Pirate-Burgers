using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DigitalClock : MonoBehaviour
{
    ///<summary>
    /// simple digital clock display script, attaches itself to RunManager to get time data
    /// Time is calculated as seconds = minutes
    ///</summary>

    
    [Header("Time Data")]
    [SerializeField] private float offset = 0;

    [SerializeField] private RunManager IngameTime;
    private float ConvertedTime; // the time of IngameTime affected by how many hours in a shift
    
    int Hour10s = 2; // the 10 and 20 of the hour
    int Hour1s = 3; // the 1-9 of the hour
    int Min10s = 0; // the 10, 20, 30, 40, 50 of the minute
    int Min1s = 0; // the 1-9 of the minute

    private bool AlarmFinished = true; //prevents alarm from starting on the first hour
    public bool alarmDisabled = false;
    private AudioSource source;

    
    [Header("Visual")]
    [SerializeField] private List<TMP_Text> TimeDisplay;

    void Start()
    {
        IngameTime = FindFirstObjectByType(typeof(RunManager)) as RunManager;
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        FormatTime();
    }

    void FormatTime()
    {
        float TotalTime = offset + IngameTime.TimeEscalated;
        int totalMinutes = (int)TotalTime;
        int hours = (totalMinutes / 60) % 24;
        int minutes = totalMinutes % 60;
        string FormattedTime = string.Format("{0:00}:{1:00}", hours, minutes);
        UpdateInts(FormattedTime);
    }

    void Alarm()
    {
        if (!AlarmFinished && !alarmDisabled)
        {
            AlarmFinished = true;
            source.Play();
        }
    }

    public void Fuckoff() //disables the alarm for the end of the day
    {
        alarmDisabled = true;
    }
    
    void UpdateInts(string Time)
    {
        char[] timeChars = Time.ToCharArray();
        int ignorei = 0;
        for (int i = 0; i < timeChars.Length; i++)
        {
            if (timeChars[i] != ':')
            {
                TimeDisplay[i - ignorei].text = timeChars[i].ToString();
            }
            else
            {
                ignorei++;
            }
        }
        
        if (timeChars[3] == '0' && timeChars[4] == '0')
        {
            Alarm();
        }
        else
        {
            AlarmFinished = false;
        }
    }
    
}
