using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EndOfDayFeedback : MonoBehaviour
{
    //dit gehele script mag eigenlijk pas geactiveerd worden zodra de dag om is (timer?) 

    [SerializeField] private List<string> goodDay;
    [SerializeField] private List<string> badDay;
    private TextMeshProUGUI tmp;

    public int TotalOrders; //hier moet dan even iets van de uiteindelijke scores in de plaats
    public int GoodOrders;
    public int BadOrders;


    //evt nog avr time per bon, is nog geen logica voor dus voeg het nu nog niet toe

    void Start()
    {
        tmp = GetComponentInChildren<TextMeshProUGUI>();
        GenerateRandomText();
    }

    private void GenerateRandomText()
    {
        if (TotalOrders >= 10)
        {
            string randomFeedbackText = goodDay[Random.Range(0, goodDay.Count)];
            tmp.text = randomFeedbackText;
        }
        else
        {
            string randomFeedbackText = badDay[Random.Range(0, badDay.Count)];
            tmp.text = randomFeedbackText;
        }
    }
}
