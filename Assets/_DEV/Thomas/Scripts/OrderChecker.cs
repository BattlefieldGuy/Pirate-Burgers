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

    public Item Activeorder;

    public bool MatchingOrder = false;

    public bool CheckMatchingOrders()
    {
        if (HasReceipt)
        {
            if (Activeorder.MainIngredients == ReceiptItem.MainIngredients && Activeorder.SecondaryIngredients == ReceiptItem.SecondaryIngredients)
            {
                MatchingOrder = true;
                return true;
            }
            else
            {
                MatchingOrder = false;
                return false;
            }
        }
        return false;
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered");
        if (other.gameObject.layer == LayerMask.NameToLayer("Dish"))
        {
            Debug.Log("Foood");
            Activeorder = other.gameObject.GetComponent<Recipe>().Dish;
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