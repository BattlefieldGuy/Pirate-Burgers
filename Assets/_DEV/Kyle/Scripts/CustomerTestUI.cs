using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CustomerTestUI : MonoBehaviour
{
    [Header("Rating System")]
    public CustomerRatingSystem rating;

    [Header("UI Score Sliders (Visual Only)")]
    public Slider pickinessSlider;
    public Slider tippingSlider;   
    public Slider patienceSlider;

    [Header("Stars")]
    public Image star1;
    public Image star2;
    public Image star3;
    public float starPopDelay = 0.25f;

    [Header("UI Text")]
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI resultText;

    [Header("Ingredient Counts")]
    public TextMeshProUGUI meatCountText;
    public TextMeshProUGUI breadCountText;
    public TextMeshProUGUI cheeseCountText;

    [Header("End Summary")]
    public TextMeshProUGUI summaryText;

    [Header("Customer Subtitle")]
    public TextMeshProUGUI customerSubtitleText;

    float timer;
    bool isCooking;

    void Start()
    {
        ResetStars();
        UpdateIngredientTexts();
        summaryText.text = "";
        customerSubtitleText.text = "";
    }

    void Update()
    {
        if (isCooking)
        {
            timer += Time.deltaTime;
            rating.dishPreparationTime = timer;

            int minutes = Mathf.FloorToInt(timer / 60f);
            float seconds = timer % 60f;

            timeText.text = $"Cooking Time: {minutes}m {seconds:00.0}s";
        }
    }


    public void StartCooking()
    {
        rating.GenerateRandomCustomer();

        isCooking = true;
        timer = 0f;
        rating.ResetDish();

        ResetStars();
        UpdateIngredientTexts();
        resultText.text = "Cooking...";
        summaryText.text = "";
        customerSubtitleText.text = "";
    }

    public void AddMeat() { rating.AddIngredient("Meat"); UpdateIngredientTexts(); }
    public void AddBread() { rating.AddIngredient("Bread"); UpdateIngredientTexts(); }
    public void AddCheese() { rating.AddIngredient("Cheese"); UpdateIngredientTexts(); }

    public void ServeDish()
    {
        isCooking = false;

        if (!rating.HasAnyIngredients())
        {
            pickinessSlider.value = 0;
            tippingSlider.value = 0;
            patienceSlider.value = 0;

            ResetStars();
            resultText.text = "No dish served!";
            summaryText.text = "";
            customerSubtitleText.text = "…Did you forget my order?";
            return;
        }

        rating.CalculateCustomerRating();

        pickinessSlider.value = rating.PickinessScore;
        tippingSlider.value = rating.TippingScore;
        patienceSlider.value = rating.PatienceScore;

        StopAllCoroutines();
        StartCoroutine(PlayStarAnimation(rating.FinalCustomerRating));

        resultText.text = $"Coins: {rating.CoinReward}";
        summaryText.text = rating.GetIngredientSummary();
        customerSubtitleText.text = rating.GetCustomerSubtitle();
    }

    IEnumerator PlayStarAnimation(int score)
    {
        ResetStars();

        int stars = score >= 80 ? 3 : score >= 40 ? 2 : 1;

        star1.enabled = true;
        yield return new WaitForSeconds(starPopDelay);

        if (stars >= 2)
        {
            star2.enabled = true;
            yield return new WaitForSeconds(starPopDelay);
        }

        if (stars >= 3)
            star3.enabled = true;
    }

    void ResetStars()
    {
        star1.enabled = false;
        star2.enabled = false;
        star3.enabled = false;
    }

    void UpdateIngredientTexts()
    {
        meatCountText.text = rating.PreparedDish.ContainsKey("Meat") ? rating.PreparedDish["Meat"].ToString() : "0";
        breadCountText.text = rating.PreparedDish.ContainsKey("Bread") ? rating.PreparedDish["Bread"].ToString() : "0";
        cheeseCountText.text = rating.PreparedDish.ContainsKey("Cheese") ? rating.PreparedDish["Cheese"].ToString() : "0";
    }
}
