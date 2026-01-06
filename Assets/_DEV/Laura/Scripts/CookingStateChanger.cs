using UnityEngine;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

// Keeps track of cooking ingredients and changes their state after enough time passes. Needs to be attached to an actual
// GameObject because Unity hates me and Coroutines can't be started from static functions.
public class CookingStateChanger : MonoBehaviour
{

    // Amount of time between cooking state changes.
    private readonly float cookingTime = 2f;

    // List of currently cooking foods are added here.
    private readonly Dictionary<GameObject, Coroutine> cookingIngredients = new();

    public bool StartCookingObject(GameObject ingredient)
    {
        // Make sure you're detaching the object from the player's hand in the script you're calling this from.
        // This script only keeps track of actually cooking the ingredients, positioning should be handled by a separate script.
        
        // Call this function when an object is placed on the grill/fryer/stove/furnace. Returns true if successfully added,
        // false if the object was already being cooked.
        if (!cookingIngredients.ContainsKey(ingredient))
        {
            cookingIngredients.Add(ingredient, StartCoroutine(CookTimer(ingredient)));
            return true;
        }
        return false;
    }

    public bool StopCookingObject(GameObject ingredient)
    {
        // Call this when the object is picked up off whatever it's cooking in. Returns whether the object was being cooked
        // in the first place.
        if (cookingIngredients.ContainsKey(ingredient))
        {
            StopCoroutine(cookingIngredients[ingredient]);
            cookingIngredients.Remove(ingredient);
            return true;
        }
        return false;
    }

    public bool ObjectIsCooking(GameObject ingredient)
    {
        return cookingIngredients.ContainsKey(ingredient);
    }

    private IEnumerator CookTimer(GameObject ingredient)
    {
        // Speaks for itself. Cooks the food over time.
        while (true)
        {
            yield return new WaitForSeconds(cookingTime);
            ChangeCookState(ingredient);
        }
    }

    private void ChangeCookState(GameObject ingredient)
    {
        // NEEDED: Update the ingredient's cooked state, something like "ingredient.CookState ++;". Call StopCookingObject()
        // and destroy the ingredient if it's beyond burnt.
    }
}
