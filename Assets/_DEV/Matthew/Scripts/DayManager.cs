using UnityEngine;
using UnityEngine.SceneManagement;

public class DayManager : MonoBehaviour
{

    [Header("Day length"), SerializeField]
    private float dayLengthInMinutes = 20f;

    private float timeInSeconds;

    #region --- UNITY METHODS ---

    void Start()
    {
        CalculateTimeInSeconds();
    }

    void Update()
    {
        CountdownEvent();
    }

    #endregion

    #region --- TIMER FUNCTIONS ---

    private void CalculateTimeInSeconds()
    {
        timeInSeconds = dayLengthInMinutes * 60;
    }

    private void CountdownEvent()
    {
        bool _hasactivated = false;
        if (timeInSeconds > 0)
            timeInSeconds -= Time.deltaTime;
        else if (_hasactivated)
        {
            _hasactivated = true;
            DBDayEnd();
        }

    }

    #endregion

    #region --- DATA MANAGEMENT ---

    #endregion


    #region - DEBUGGING -

    private void DBDayEnd()
    {
        SceneManager.LoadScene("S_MainMenu");
    }

    #endregion

}