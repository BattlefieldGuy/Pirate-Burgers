using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ReceiptData : MonoBehaviour
{
    /// <summary>
    /// Sets the card information (in a basic way)
    /// Card updates along with the public variables
    /// </summary>

    Transform startPoint;


    public string Foodname;
    public Sprite FoodSprite;
    public int Ordernumber;

    [SerializeField] Image image;
    [SerializeField] TMP_Text ordername;

    public void Awake()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0,0);
        startPoint = FindFirstObjectByType<ReceiptList>().transform.Find("Start");
    }

    public void Fuckoff()
    {
        //For non-tween nerds: move downwards using an InQuad (check a quide on easing styles) ease over .5seconds and destroy afterwards
        //All localspace btw
        transform.DOLocalMove(new Vector3(transform.localPosition.x,-10,transform.localPosition.z), 0.5f).OnComplete(() => Destroy(gameObject)).SetEase(Ease.InQuad);
    }

    public void UpdateCardInfo()
    {
        ordername.text = Foodname;
        image.sprite = FoodSprite;

        //Move to position based on ordernumber using an InOutSine ease over .5seconds
        transform.DOLocalMoveX(startPoint.localPosition.x + (1.9f * Ordernumber), 0.5f).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(new Vector3(0, -46.673f + (6f * Ordernumber),0), 0.5f).SetEase(Ease.InOutSine);
    }
}
