using DG.Tweening;
using TMPro;
using UnityEngine;
using static BonnetjesManager;

public class ReceiptData : MonoBehaviour
{
    /// <summary>
    /// Sets the card information (in a basic way)
    /// Card updates along with the public variables
    /// </summary>

    Transform startPoint;

    public Item foodItem;
    public int Ordernumber = 0;

    [SerializeField] TMP_Text FoodName;
    [SerializeField] TMP_Text Title;
    [SerializeField] TMP_Text SecondIng;

    [SerializeField] float ordertime = 0;

    private RunManager runManager;


    public void Awake()
    {
        transform.localPosition = new Vector3(transform.localPosition.x, 0, 0);
        startPoint = FindFirstObjectByType<ReceiptList>().transform.Find("Start");
        runManager = FindFirstObjectByType<RunManager>();
        ordertime = Time.time;
    }

    public void Fuckoff()
    {
        //For non-tween nerds: move downwards using an InQuad (check a quide on easing styles) ease over .5seconds and destroy afterwards
        //All localspace btw
        float sendTime = Time.time - ordertime;
        runManager.AddOrder(true, sendTime);
        transform.DOLocalMove(new Vector3(transform.localPosition.x, -10, transform.localPosition.z), 0.5f).OnComplete(() => Destroy(gameObject)).SetEase(Ease.InQuad);
    }

    public string IngredientsCombined(Item item)
    {
        string ReturnString = "";
        foreach (string Ingredient in item.MainIngredients)
        {
            ReturnString += Ingredient + ", ";
        }
        for (int i = 0; i < item.SecondaryIngredients.Count; i++)
        {
            ReturnString += item.SecondaryIngredients[i];
            if (i != item.SecondaryIngredients.Count - 1)
            {
                ReturnString += ", ";
            }
        }
        return ReturnString;
    }

    public void UpdateCardInfo()
    {
        Title.text = "Order : " + Ordernumber.ToString();
        FoodName.text = foodItem.name;
        SecondIng.text = IngredientsCombined(foodItem);

        //Move to position based on ordernumber using an InOutSine ease over .5seconds
        transform.DOLocalMoveX(startPoint.localPosition.x + (1.9f * Ordernumber), 0.5f).SetEase(Ease.InOutSine);
        transform.DOLocalRotate(new Vector3(0, -46.673f + (6f * Ordernumber), 0), 0.5f).SetEase(Ease.InOutSine);
    }
}
