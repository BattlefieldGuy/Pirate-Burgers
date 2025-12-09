using System.Collections.Generic;
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



    #region -- Variabels 

    // Player Progression - Unlocked Machines, value is submitted by game manager
    public List<string> UnlockedMachines;

    // Main Recipe List - List of all recipes in the game with every atribute, customize in inspector
    [Header("Main List"), SerializeField]
    private List<Item> recipeList;

    // List to send to the Receipt Manager - List of usable recipes based on player progression
    public List<Item> usableList;



    private BonnetjesManager bonnetjesManager;


    #endregion


    void Start()
    {
        ConstructUsableList();
    }

    void Update()
    {

    }


    private void ConstructUsableList()
    {
        // Construct usable list
        foreach (Item recipe in recipeList)
        {
            bool canBeMade = true;
            // Check if all required machines are unlocked
            foreach (string machine in recipe.RequiredMachines)
            {
                if (!UnlockedMachines.Contains(machine))
                {
                    canBeMade = false;
                    break;
                }
            }
            // If recipe can be made, add to usable list
            if (canBeMade)
            {
                Debug.Log(canBeMade);
                usableList?.Add(recipe);
            }
        }
    }

    private void SendUsableList(List<Item> _list)
    {
        // find object with receipt manager
        bonnetjesManager = FindFirstObjectByType<BonnetjesManager>();

        if (bonnetjesManager != null)
            bonnetjesManager.ItemList = _list;

        // Send usable list to receipt manager
    }
}