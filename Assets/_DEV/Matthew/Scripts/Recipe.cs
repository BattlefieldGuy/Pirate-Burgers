using UnityEngine;
using static BonnetjesManager;

public class Recipe : MonoBehaviour
{
    /// <summary>
    /// 
    /// This scrip will be put on every recipe that is instantiated in the game.
    /// It will hold the data about the recipe, such as its name, main ingredients and secondary ingredients.
    /// This can then be used at the counter to check if the player has made the correct recipe.
    /// 
    /// </summary>

    public Item Dish;

    public void AddIngredient(string _ingredient, bool _isMain)
    {
        if (_isMain)
        {
            Dish.MainIngredients.Add(_ingredient);
        }
        else
        {
            Dish.SecondaryIngredients.Add(_ingredient);
        }
    }
}