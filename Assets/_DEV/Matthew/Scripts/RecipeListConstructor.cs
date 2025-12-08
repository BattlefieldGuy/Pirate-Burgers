using UnityEngine;
using static BonnetjesManager;

public class RecipeListConstructor : MonoBehaviour
{
    /// <summary>
    /// 
    /// This script holds all recipes and makes a usable list for the game, this is done by checking wich recipes can and cannot be made acording to the players progresion.
    /// every recipe has there set of required machines, these are the defining factors that decide if the recipe is able to be prepared.
    /// 
    /// on game start this usable list is created so that the receipt manager can generate random items.
    /// 
    /// </summary>

    #region -- Variabels --

    [Header("Main List"), SerializeField]
    private Item recipeList;


    private Item usableList;
    #endregion


    void Start()
    {

    }

    void Update()
    {

    }


    private void ConstructUsableList()
    {
        // Construct usable list
    }

    private void SendUsableList(Item _list)
    {
        // find object with receipt manager
        // Send usable list to receipt manager
    }
}