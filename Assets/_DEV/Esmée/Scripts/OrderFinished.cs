using UnityEngine;

public class OrderFinished : MonoBehaviour
{
    [SerializeField] private bool correctOrder;
    BellPress BellPress;

    private OrderChecker orderChecker;
    void Start()
    {
        BellPress = FindFirstObjectByType<BellPress>();
        orderChecker = GetComponent<OrderChecker>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (BellPress.PressedBell && orderChecker.CheckMatchingOrders())
        {
            //order dissapears with customer
            Debug.Log("YAY");
        }
        else if (BellPress.PressedBell && !orderChecker.CheckMatchingOrders())
        {
            //error message ofzo/boze klant
            Debug.Log("nay");
        }
        else
        {
            //niks
            Debug.Log("wachtiewacht");
        }
    }
}
