using System.Collections.Generic;
using UnityEngine;

public class CustomerRatingSystem : MonoBehaviour
{
    [Header("Customer Traits (1–10)")]
    [Range(1, 10)] public int PickinessSlider = 1;
    [Range(1, 10)] public int TippingSlider = 1;
    [Range(1, 10)] public int PatienceSlider = 1;

    [Header("Dish Settings")]
    public float BaseDishTime = 90f;

    [Header("Recipe (Required Ingredients)")]
    public Dictionary<string, int> Recipe = new Dictionary<string, int>()
    {
        { "Meat", 1 },
        { "Bread", 2 },
        { "Cheese", 1 }
    };

    public Dictionary<string, int> PreparedDish = new Dictionary<string, int>();

    [HideInInspector] public float dishPreparationTime;

    [Header("Scores (Output)")]
    public int PickinessScore;
    public int TippingScore;
    public int PatienceScore;
    public int FinalCustomerRating;
    public int CoinReward;

    public void GenerateRandomCustomer()
    {
        PickinessSlider = Random.Range(1, 11);
        TippingSlider = Random.Range(1, 11);
        PatienceSlider = Random.Range(1, 11);
    }

    public void AddIngredient(string ingredientName)
    {
        if (PreparedDish.ContainsKey(ingredientName))
            PreparedDish[ingredientName]++;
        else
            PreparedDish.Add(ingredientName, 1);
    }

    public bool HasAnyIngredients()
    {
        return PreparedDish.Count > 0;
    }

    public void ResetDish()
    {
        PreparedDish.Clear();
        dishPreparationTime = 0f;
    }

    int CalculatePickiness()
    {
        int penalty = 0;

        foreach (var item in Recipe)
        {
            int required = item.Value;
            int prepared = PreparedDish.ContainsKey(item.Key) ? PreparedDish[item.Key] : 0;

            int difference = Mathf.Abs(required - prepared);
            penalty += difference * PickinessSlider * 15;
        }

        return Mathf.Clamp(100 - penalty, 0, 100);
    }

    int CalculateTipping()
    {
        return TippingSlider * 10;
    }

    int CalculatePatience()
    {
        float maxWait = BaseDishTime * (PatienceSlider / 10f);

        if (dishPreparationTime <= maxWait)
            return 100;

        float overtime = dishPreparationTime - maxWait;
        return Mathf.Clamp(100 - Mathf.RoundToInt(overtime * 3f), 0, 100);
    }

    int GetTotalRecipeDifference()
    {
        int total = 0;

        foreach (var item in Recipe)
        {
            int required = item.Value;
            int prepared = PreparedDish.ContainsKey(item.Key) ? PreparedDish[item.Key] : 0;
            total += Mathf.Abs(required - prepared);
        }

        return total;
    }

    public void CalculateCustomerRating()
    {
        PickinessScore = CalculatePickiness();
        TippingScore = CalculateTipping();
        PatienceScore = CalculatePatience();

        int recipeDifference = GetTotalRecipeDifference();
        if (recipeDifference > 0)
        {
            float multiplier = Mathf.Clamp01(1f - (recipeDifference * 0.25f));
            PatienceScore = Mathf.RoundToInt(PatienceScore * multiplier);
        }

        FinalCustomerRating = Mathf.RoundToInt(
            PickinessScore * 0.3f +
            TippingScore * 0.3f +
            PatienceScore * 0.4f
        );

        FinalCustomerRating = Mathf.Clamp(FinalCustomerRating, 0, 100);

        CoinReward = Mathf.Max(1,
            Mathf.RoundToInt(TippingSlider * 10f * (FinalCustomerRating / 100f))
        );
    }

    public string GetIngredientSummary()
    {
        List<string> tooMuch = new List<string>();
        List<string> tooLittle = new List<string>();

        foreach (var item in Recipe)
        {
            int required = item.Value;
            int prepared = PreparedDish.ContainsKey(item.Key) ? PreparedDish[item.Key] : 0;

            if (prepared > required)
                tooMuch.Add($"{item.Key} (+{prepared - required})");
            else if (prepared < required)
                tooLittle.Add($"{item.Key} (-{required - prepared})");
        }

        if (tooMuch.Count == 0 && tooLittle.Count == 0)
            return "Perfect recipe!";

        string result = "";

        if (tooLittle.Count > 0)
            result += "<b>Too little:</b>\n" + string.Join(", ", tooLittle) + "\n";

        if (tooMuch.Count > 0)
            result += "<b>Too much:</b>\n" + string.Join(", ", tooMuch);

        return result.Trim();
    }

    public string GetCustomerSubtitle()
    {
        List<string> tooMuch = new List<string>();
        List<string> tooLittle = new List<string>();

        foreach (var item in Recipe)
        {
            int required = item.Value;
            int prepared = PreparedDish.ContainsKey(item.Key) ? PreparedDish[item.Key] : 0;

            if (prepared > required)
                tooMuch.Add(item.Key);
            else if (prepared < required)
                tooLittle.Add(item.Key);
        }

        //Reactie op de dish
        string timeReaction = "";
        float maxWait = BaseDishTime * (PatienceSlider / 10f);
        float overtime = dishPreparationTime - maxWait;

        float tolerance = 1f;
        if (overtime > tolerance)
        {
            if (overtime < 20f)
                timeReaction = "I was waiting a bit too long…";
            else
                timeReaction = "But this is taking forever!";
        }

        string ingredientReaction;

        if (tooMuch.Count == 0 && tooLittle.Count == 0)
            ingredientReaction = "Wow, this is exactly how I wanted it!";
        else if (tooLittle.Count > 0 && tooMuch.Count == 0)
            ingredientReaction = $"Hmm… this needs more {string.Join(" and ", tooLittle)}.";
        else if (tooMuch.Count > 0 && tooLittle.Count == 0)
            ingredientReaction = $"Whoa! That’s way too much {string.Join(" and ", tooMuch)}.";
        else
            ingredientReaction = $"Uh… too much {string.Join(" and ", tooMuch)}, but not enough {string.Join(" and ", tooLittle)}.";

        if (!string.IsNullOrEmpty(timeReaction))
            return ingredientReaction + " " + timeReaction;

        return ingredientReaction;
    }
}
