using System.Runtime.CompilerServices;
using UnityEngine;
using static BonnetjesManager;

public class OrderChecker : MonoBehaviour
{

    /// <summary>
    /// checks if the current order matches the receipt
    /// </summary>

    [SerializeField] ReceiptData _LoggedReceipt;
    public ReceiptData loggedReceipt
    {
        get => _LoggedReceipt;
        set
        {
            if (!ReferenceEquals(_LoggedReceipt, value))
            {
                _LoggedReceipt = value;
                HasReceipt = _LoggedReceipt != null;
                UpdateLocal();
            }
        }
    }

    bool HasReceipt = false;

    public Item ReceiptItem;

    public Item ActiveOrder;

    public bool CheckMatchingOrders()
    {
        bool orderMatches = true;
            for(int i = 0; i < ActiveOrder.MainIngredients.Count; i++)
            {
                if (!ReceiptItem.MainIngredients.Contains(ActiveOrder.MainIngredients[i])) orderMatches = false;
            };  
            for (int i = 0; i < ReceiptItem.MainIngredients.Count; i++)
            {
                if (!ActiveOrder.MainIngredients.Contains(ReceiptItem.MainIngredients[i])) orderMatches = false;
            };
            for (int i = 0; i < ActiveOrder.SecondaryIngredients.Count; i++)
            {
                if (!ReceiptItem.SecondaryIngredients.Contains(ActiveOrder.SecondaryIngredients[i])) orderMatches = false;
            };
            for (int i = 0; i < ReceiptItem.SecondaryIngredients.Count; i++)
            {
               if (!ActiveOrder.SecondaryIngredients.Contains(ReceiptItem.SecondaryIngredients[i])) orderMatches = false;
            };
        return orderMatches;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.layer == LayerMask.NameToLayer("Dish"))
        {
            Debug.Log("Foood");
            ActiveOrder = other.gameObject.GetComponent<Recipe>().Dish;
            print("post-added");
        }
        else if (other.gameObject.layer == LayerMask.NameToLayer("Receipt"))
        {
            ReceiptItem = other.gameObject.GetComponent<ReceiptData>().foodItem;
        }
    }

    void UpdateLocal()
    {
        if (loggedReceipt != null)
        {
            ReceiptItem = loggedReceipt.foodItem;
        }
        else
        {
            ReceiptItem = null;
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        HasReceipt = _LoggedReceipt != null;
        UpdateLocal();
    }
#endif
}