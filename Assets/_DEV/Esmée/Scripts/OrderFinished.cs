using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class OrderFinished : MonoBehaviour
{
    public List<GameObject> itemsInZone = new List<GameObject>();

    [SerializeField] private ParticleSystem poof;
    [SerializeField] private AudioSource yay;
    [SerializeField] private AudioClip DingDing;
    [SerializeField] private AudioClip Wrong;
    BellPress BellPress;

    private OrderChecker orderChecker;
    private ReceiptList receiptList;
    private RunManager runManager;

    [SerializeField]
    private ScoreUpdate scoreUpdate; //temp
    void Start()
    {
        yay = GetComponent<AudioSource>();
        BellPress = FindFirstObjectByType<BellPress>();
        orderChecker = GetComponent<OrderChecker>();
        receiptList = FindFirstObjectByType<ReceiptList>();
        runManager = FindFirstObjectByType<RunManager>();
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
            if (yay != null)
            {
                yay.PlayOneShot(DingDing);
            }
            Debug.Log("YAY");
            //gold coins in face
            foreach (GameObject _item in itemsInZone)
            {
                if (_item.GetComponent<ReceiptData>() != null)
                {
                    int _orderNumber = _item.GetComponent<ReceiptData>().Ordernumber;
                    receiptList.ClearReceipt(_orderNumber);
                }
                else
                    _item.transform.DOLocalMove(new Vector3(transform.localPosition.x, -10, transform.localPosition.z), 0.5f).OnComplete(() => Destroy(gameObject)).SetEase(Ease.InQuad);
            }

            orderChecker.ClearItems();
        }
        else if (BellPress.PressedBell && !orderChecker.CheckMatchingOrders() && orderChecker.ReceiptItem != null && orderChecker.ActiveOrder != null)
        {
            //boze klant audio
            Debug.Log("nay");
            if (yay != null)
            {
                yay.PlayOneShot(Wrong);
            }
            foreach (GameObject _item in itemsInZone)
            {
                if (_item.GetComponent<ReceiptData>() != null)
                {
                    int _orderNumber = _item.GetComponent<ReceiptData>().Ordernumber;
                    runManager.AddOrder(false, _orderNumber);
                }
                else
                    _item.transform.DOLocalMove(new Vector3(transform.localPosition.x, -10, transform.localPosition.z), 0.5f).OnComplete(() => Destroy(gameObject)).SetEase(Ease.InQuad);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        itemsInZone.Remove(other.gameObject);
    }
}
