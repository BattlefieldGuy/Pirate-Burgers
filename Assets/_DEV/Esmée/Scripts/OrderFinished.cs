using UnityEngine;

public class OrderFinished : MonoBehaviour
{
    [SerializeField] private bool correctOrder;
    [SerializeField] private ParticleSystem poof;
    [SerializeField] private AudioSource yay;
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
            poof.Play();
            yay.Play();
            Debug.Log("YAY");
            //gold coins in face
            if (other != null) Destroy(other.gameObject);
        }
        else if (BellPress.PressedBell && !orderChecker.CheckMatchingOrders())
        {
            //boze klant audio
            Debug.Log("nay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        correctOrder = false;
    }
}
