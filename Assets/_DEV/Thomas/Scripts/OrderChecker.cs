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

    [SerializeField] Item Receiptitem;

    [SerializeField] Item Activeorder;

    bool MatchingOrder = false;

    void CheckMatchingOrders()
    {
        if (HasReceipt)
        {
            if (Activeorder == Receiptitem)
            {
                MatchingOrder = true;
            }
            else
            {
                MatchingOrder = false;
            }
        }
    }

    void UpdateLocal()
    {
        if (loggedReceipt != null)
        {
            Receiptitem = loggedReceipt.foodItem;
        }
        else
        {
            Receiptitem = null;
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