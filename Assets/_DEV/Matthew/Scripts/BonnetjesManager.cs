using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BonnetjesManager : MonoBehaviour
{
    /// <summary>
    /// This scrip is responsible for managing the receipts and other this related to receipts.
    /// 
    /// It houses a list of possible items that can be ordered, each with main and secondary ingredients.
    /// every item is represented by the Item class, which contains the item's name and its ingredients.
    /// 
    /// Once an item is created, it is sent to the ReceiptList script to be displayed as a receipt.
    /// </summary>


    [System.Serializable]// ingredients list
    public class Item// Item structure
    {
        public string name;
        public List<string> MainIngredients;
        public List<string> SecondaryIngredients;
    }


    // Items
    public List<Item> ItemList = new List<Item>();


    //refs
    private ReceiptList receiptList;


    #region -- RECEIPT VARIABLES --
    // Receipt Variables are used to adjust receipt generation during playtime
    [Header("Receipt Variables")]
    [Space(5), Tooltip("Set Starter receipt interval"), SerializeField]
    private float receiptInterval = 5f;

    [Space(5), Range(0f, 20f), SerializeField]
    private float intervalOffsetRange = 5f;

    #endregion

    #region -- CUSTOMER VARIABLES --
    // Customers Variables are used for testing purposes and can also be used to adjust receipt generation during playtime
    [Space(30)]
    [Header("Customer Variables")]
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

    #endregion



    void Start()
    {
        receiptList = FindFirstObjectByType<ReceiptList>();
        StartCoroutine(enumerator());//temp
    }



    public void MakeItem()
    {
        // Create a new item with random ingredients
        //pick random item
        Item item_ = ItemList[Random.Range(0, ItemList.Count)];

        if (receiptList != null)
            receiptList.AddOrder(item_);
    }

    #region - ENUMARATORS -

    //Temp enum
    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(1f);
        MakeItem();
        //StartCoroutine(enumerator());
    }

    #endregion
}