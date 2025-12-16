using UnityEngine;

public class DataEOD : MonoBehaviour
{
    private TMPro.TextMeshProUGUI tmpText;
    EndOfDayFeedback endOfDayFeedback;

    private void Awake()
    {
        tmpText = GetComponent<TMPro.TextMeshProUGUI>();
        endOfDayFeedback = FindFirstObjectByType<EndOfDayFeedback>();
    }
    void Start()
    {
        string dataText = tmpText.text;
        dataText = string.Format(dataText, endOfDayFeedback.TotalOrders, endOfDayFeedback.GoodOrders, endOfDayFeedback.BadOrders);
        tmpText.text = dataText;
    }
}
