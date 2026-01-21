using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RunManager : MonoBehaviour
{
    [Header("Timer variables"), SerializeField]
    public float dayLengthInMinutes = 20f;
    private float multiplier;
    [SerializeField] private int HoursInAShift = 8;
    private DigitalClock clock;

    [SerializeField] private float timeInSeconds;
    public float TimeEscalated = 0f;

    public static bool dayStarted = false, dayEnded = false;

    // Run variables
    private static int totalOrders = 0, goodOrders = 0, badOrders = 0;
    
    private EndOfDayFeedback feedbackScript;
    [SerializeField] AudioSource EndOfDaySound;

    #region --- UNITY METHODS ---

    void Start()
    {
        feedbackScript = FindFirstObjectByType<EndOfDayFeedback>();
        clock = FindFirstObjectByType<DigitalClock>();
        StartDay();
        multiplier = dayLengthInMinutes / (HoursInAShift * 60f);
        print("Multiplier is: " + multiplier);
    }

    void Update()
    {
        if(dayStarted && !dayEnded)
        {
            CountdownEvent();
            CountdownChecks();
        }
    }

    #endregion

    #region --- TIMER FUNCTIONS ---

    private void CalculateTimeInSeconds() => timeInSeconds = dayLengthInMinutes * 60;

    public void StartDay()
    {
        CalculateTimeInSeconds();
        dayStarted = true;
    }

    private void CountdownEvent()
    {
        if (timeInSeconds > 0)
        {
            timeInSeconds -= Time.deltaTime;
            TimeEscalated += (Time.deltaTime / multiplier) /60;
        }

        if (timeInSeconds <= 4 && !clock.alarmDisabled)
        {
            clock.Fuckoff();
        }

    }

    private void CountdownChecks()
    {
        if(timeInSeconds <= 0)
        {
            dayEnded = true;
            OnDayEnd();
        }
        if(timeInSeconds <= dayLengthInMinutes * 30)
        {
            OnDayHalfway();
        }
    }

    private void OnDayHalfway()
    {
        // Odds are this function isn't necessary, but if you want things to happen the moment the day is 50% done,
        // put that code here. Like Parrot voicelines for example, if we somehow manage to cram those in, lol.
    }

    private void OnDayEnd()
    {
        EndOfDaySound.Play();
        if (feedbackScript != null)
        {
            feedbackScript.TotalOrders = totalOrders;
            feedbackScript.GoodOrders = goodOrders;
            feedbackScript.BadOrders = badOrders;
            feedbackScript.enabled = true;
        }
        else Debug.LogError("There is no EndOfDayFeedback component in the scene to log results to!");
    }

    #endregion

    #region --- DATA MANAGEMENT ---

    // After an order is finished, call RunManager.RecordOrder() to save it.
    public static void RecordOrder(bool goodOrder)
    {
        totalOrders++;
        if (goodOrder)
            goodOrders++;
        else badOrders++;
    }

    // If you need to get the recorded orders, call RunManager.GetOrderRecords().
    public static int[] GetOrderRecords()
    {
        return new int[3]
        {
            totalOrders,
            goodOrders,
            badOrders
        };
    }

    #endregion


    #region - DEBUGGING -

    private void DBDayEnd()
    {
        SceneManager.LoadScene("S_MainMenu");
    }

    #endregion

}