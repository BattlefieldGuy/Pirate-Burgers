using System.Collections.Generic;
using UnityEngine;
using static BonnetjesManager;

public class ReceiptList : MonoBehaviour
{

    /// <summary>
    /// adds and removes receipts from the list and updates them accordingly
    /// </summary>

    [SerializeField] private CustomerManager customers;

    [Header("I was too lazy to learn how to make custom inspectors so have a boolean lmao")]
    public bool RemoveFirstOrder;
    //The rest

    [System.Serializable]
    public class Receipt
    {
        public ReceiptData receiptData;
        public int orderNumber;
        public Item item;
    }

    [SerializeField] List<Receipt> receipts = new List<Receipt>();

    [SerializeField] GameObject receiptPrefab;

    [SerializeField]
    Transform StartingPoint; //Somehow different than the ReceiptData's starting point don't question it :)))))))))

    void Start()
    {
        customers = FindFirstObjectByType(typeof(CustomerManager)) as CustomerManager;
    }


public void UpdateAllReceipts()
    {
        foreach (Receipt receipt in receipts)
        {
            receipt.receiptData.Ordernumber = receipts.IndexOf(receipt);
            receipt.receiptData.UpdateCardInfo();
        }
    }

    public void ClearReceipt(int ordernumber)
    {
        if (receipts.Count <= 0 && receipts.Count >= ordernumber) return;
        
        BonnetjesManager.Item toreceipt = new BonnetjesManager.Item()
        {
            name = receipts[ordernumber].item.name,
            MainIngredients = receipts[ordernumber].item.MainIngredients,
            SecondaryIngredients = receipts[ordernumber].item.SecondaryIngredients
        };
        customers.DeleteCustomerByReceipt(toreceipt);
        receipts[ordernumber].receiptData.Fuckoff();
        receipts.RemoveAt(ordernumber);
        UpdateAllReceipts();
    }

    public void AddOrder(Item _item) //Change parameters as needed
    {
        GameObject newReceiptGO = Instantiate(receiptPrefab, transform);
        ReceiptData newReceiptData = newReceiptGO.GetComponent<ReceiptData>();
        Receipt newReceipt = new Receipt()
        {
            orderNumber = receipts.Count,
            receiptData = newReceiptData,
            item = _item
        };
        newReceiptData.Ordernumber = newReceipt.orderNumber;
        newReceiptData.foodItem = _item;
        receipts.Add(newReceipt);
        newReceiptData.UpdateCardInfo();
        newReceiptData.transform.parent = transform;
        newReceiptData.transform.localPosition = StartingPoint.localPosition;
        BonnetjesManager.Item togive = new BonnetjesManager.Item()
        {
            name = _item.name,
            MainIngredients = _item.MainIngredients,
            SecondaryIngredients = _item.SecondaryIngredients
        };
        customers.SpawnCustomer(togive);

    }

    public void Update()
    {
        if (RemoveFirstOrder)
        {
            RemoveFirstOrder = false;
            ClearReceipt(0);
        }
    }


    public void Test(Item item_)
    {
        Debug.Log(item_.name);
        Debug.Log(item_.MainIngredients.ToString());
        Debug.Log(item_.SecondaryIngredients);
    }
}
