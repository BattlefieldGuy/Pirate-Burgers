using System.Collections.Generic;
using UnityEngine;

public class OrderFinished : MonoBehaviour
{
    public List<GameObject> itemsInZone = new List<GameObject>();

    [SerializeField] private ParticleSystem poof;
    [SerializeField] private AudioSource yay;
    BellPress BellPress;

    private OrderChecker orderChecker;

    [SerializeField]
    private ScoreUpdate scoreUpdate; //temp
    void Start()
    {
        BellPress = FindFirstObjectByType<BellPress>();
        orderChecker = GetComponent<OrderChecker>();
    }

    private void OnTriggerEnter(Collider other)
    {
        itemsInZone.Add(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (BellPress.PressedBell && orderChecker.CheckMatchingOrders())
        {
            if (poof != null) poof.Play();
            if (yay != null) yay.Play();
            Debug.Log("YAY");
            //gold coins in face
            foreach (GameObject _item in itemsInZone)
            {
                Destroy(_item);
            }

            orderChecker.ClearItems();//temp
            scoreUpdate.AddScore();
        }
        else if (BellPress.PressedBell && !orderChecker.CheckMatchingOrders())
        {
            //boze klant audio
            Debug.Log("nay");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        itemsInZone.Remove(other.gameObject);
    }
}
