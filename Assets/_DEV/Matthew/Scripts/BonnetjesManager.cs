using System.Collections.Generic;
using UnityEngine;

public class BonnetjesManager : MonoBehaviour
{
    // Ingridients
    public List<string> MainIngridients = new List<string>();

    public List<string> SecondaryIngridients = new List<string>();

    // Customers Variables
    public int CurrentCustomers = 0;

    [SerializeField] private int maxCustomers = 5;

    // Custom Variables
    [SerializeField] private float spawnIntervalMin = 10f;
    [SerializeField] private float spawnIntervalMax = 15f;

    // Money
    [SerializeField] private float customerMoneyValue;
    [SerializeField] private float customerMoneyMin = 10f;
    [SerializeField] private float customerMoneyMax = 10f;

    // Patience
    [SerializeField] private float customerPatienceValue;
    [SerializeField] private float customerPatienceMin = 30f;
    [SerializeField] private float customerPatienceMax = 60f;

    // Pickiness
    [SerializeField] private float customerPickinessValue;
    [SerializeField] private float customerPickinessMin = 0.1f;
    [SerializeField] private float customerPickinessMax = 0.5f;



    void Start()
    {

    }

    void Update()
    {

    }


    public void MakeItem()
    {
        // Create a new item with random ingridients
    }
}