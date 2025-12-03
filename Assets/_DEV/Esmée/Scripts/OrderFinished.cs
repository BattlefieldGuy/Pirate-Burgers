using UnityEngine;

public class OrderFinished : MonoBehaviour
{
    [SerializeField] private bool correctOrder;
    BellPress BellPress;
    void Start()
    {
        BellPress = FindFirstObjectByType<BellPress>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (BellPress.PressedBell && correctOrder)
        {
            //order dissapears with customer
            Debug.Log("YAY");
        }
        else if (BellPress.PressedBell && !correctOrder)
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
