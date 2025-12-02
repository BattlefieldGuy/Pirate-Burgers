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

    [SerializeField]
    private Item ingredients;

    public void AddIngredient(GameObject _ingredient, bool _isMain)
    {
        if (_isMain)
        {
            ingredients.MainIngredients.Add(_ingredient.name);
        }
        else
        {
            ingredients.SecondaryIngredients.Add(_ingredient.name);
        }
    }
}