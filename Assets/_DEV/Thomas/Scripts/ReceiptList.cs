using System.Collections.Generic;
using UnityEngine;
using static BonnetjesManager;

public class ReceiptList : MonoBehaviour
{
    //Testing Variables, feel free to remove later on
    [SerializeField]
    List<string> foodnames = new List<string>()
    {
        "Borgir",
        "Salmon (raw)",
        "Whale (whole)",
        "Squid heart",
        "Whale liver"
    };

    [SerializeField] List<Sprite> foodSprites = new List<Sprite>();

    [Header("I was too lazy to learn how to make custom inspectors so have a boolean lmao")]
    public bool testButton;
    public bool RemoveFirstOrder;
    //The rest

    [System.Serializable]
    public class Receipt
    {
        public ReceiptData receiptData;
        public int Ordernumber;
    }

    [SerializeField] List<Receipt> receipts = new List<Receipt>();

    [SerializeField] GameObject receiptPrefab;
    [SerializeField] Transform StartingPoint; //Somehow different than the ReceiptData's starting point don't question it :)))))))))

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
        receipts[ordernumber].receiptData.Fuckoff();
        receipts.RemoveAt(ordernumber);
        UpdateAllReceipts();
    }

    public void AddOrder(string FoodName, Sprite sprite) //Change parameters as needed
    {
        GameObject newReceiptGO = Instantiate(receiptPrefab, transform);
        ReceiptData newReceiptData = newReceiptGO.GetComponent<ReceiptData>();
        Receipt newReceipt = new Receipt()
        {
            receiptData = newReceiptData,
            Ordernumber = receipts.Count
        };
        newReceiptData.Foodname = FoodName;
        newReceiptData.FoodSprite = sprite;
        newReceiptData.Ordernumber = newReceipt.Ordernumber;
        receipts.Add(newReceipt);
        newReceiptData.UpdateCardInfo();
        newReceiptData.transform.parent = transform;
        newReceiptData.transform.localPosition = StartingPoint.localPosition;

    }

    public void AddRandomOrder()
    {
        string RandomTitle = foodnames[Random.Range(0, foodnames.Count)];
        Sprite RandomSprite = foodSprites[Random.Range(0, foodSprites.Count)];

        AddOrder(RandomTitle, RandomSprite);
    }

    public void Update()
    {
        if (testButton)
        {
            testButton = false;
            AddRandomOrder();
        }
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
