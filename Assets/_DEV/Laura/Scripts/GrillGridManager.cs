using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class GrillGridManager : MonoBehaviour
{
    /// <summary>
    ///  This script should be assigned to the grill, and the functions inside should be called whenever an
    ///  ingredient collides with the grill. Call AddItemToGrill() with the ingredient that was collided,
    ///  and whenever an ingredient is picked up off the grill, call RemoveItemFromGrill() with said ingredient.
    /// </summary>

    #region -- Variables --

    // Amount of ingredients allowed on the grill. Should be divisible by INGREDIENTSPERROW.
    private static int INGREDIENTLIMIT = 5;

    // Ingredient positioning variables. DISTANCEBETWEENROWS is equal to how much the z position of every
    // ingredient is changed based on the row they're in.
    [SerializeField] private int INGREDIENTSPERROW = 3;
    [SerializeField] private float DISTANCEBETWEENROWS = 5f;

    // The top left point of the grill, and the top right point of the grill.
    [SerializeField] Transform grillLeft;
    [SerializeField] Transform grillRight;

    // All created transforms for gridPositions will be parented to this object.
    [SerializeField] Transform gridPosParent;

    // Stores the items, and where said items can be positioned.
    private GameObject[] gridItems = new GameObject[INGREDIENTLIMIT];
    private Transform[] gridPositions = new Transform[INGREDIENTLIMIT];

    #endregion

    #region -- Test Stuff --

    // Testing/debugging functions. Only use these as such, not for the final game.

    [SerializeField] GameObject burgerPrefab;

    public void SpawnBurger(CallbackContext context)
    {
        if (context.performed && !context.canceled && GrillHasSpace())
            Debug.Log(AddItemToGrill(Instantiate(burgerPrefab)));
    }

    public void RemoveBurger(CallbackContext context)
    {
        if (context.performed && !context.canceled)
        {
            Debug.Log(RemoveItemFromGrill(gridItems[0]));
        }
    }
    #endregion

    #region -- Position Management --

    // Sets all of the grid positions beforehand, so the food can snap to the closest position when touching the grill.
    private void Start()
    {
        for (int i = 0; i < INGREDIENTLIMIT; i++)
        {
            Transform newPos = new GameObject().transform;
            newPos.SetParent(gridPosParent);
            newPos.name = "Position " + (i + 1);
            int index = i;
            int rowNumber = 0;
            while (index >= INGREDIENTSPERROW)
            {
                index -= INGREDIENTSPERROW;
                rowNumber++;
            }
            newPos.position = Vector3.Lerp(grillLeft.position, grillRight.position, (float)index / ((float)INGREDIENTSPERROW - 1f));
            newPos.position -= DISTANCEBETWEENROWS * rowNumber * transform.forward;
            gridPositions[i] = newPos;
        }
    }

    // Every frame, updates the position of every ingredient assigned to the grill based on the slot they were
    // assigned to, so they don't fall off.
    private void Update()
    {
        for (int i = 0; i < gridItems.Length; i++)
            if (gridItems[i] != null)
                gridItems[i].transform.position = gridPositions[i].position;
    }

    #endregion

    #region -- Grid Management Functions --

    // Call this function when a grillable ingredient is placed on the grill. The function assigns the closest available
    // position to the ingredient, then makes sure it stays there until picked up.
    public bool AddItemToGrill(GameObject item)
    {
        if (gridItems.Contains(item)) return false;

        Vector3 itemPos = item.transform.position;
        int index = -1;
        float distance = -1;
        List<int> possibleIndexes = new();
        for (int i = 0; i < gridItems.Length; i++)
            if (gridItems[i] == null) possibleIndexes.Add(i);

        foreach (int i in possibleIndexes)
        {
            if (distance == -1 || Vector3.Distance(itemPos, gridPositions[i].position) < distance)
            {
                distance = Vector3.Distance(itemPos, gridPositions[i].position);
                index = i;
            }
        }

        if (index != -1)
        {
            gridItems[index] = item;
            return true;
        }
        return false;
    }

    // Call this function when an ingredient is picked up off the grill, so it stops being locked to a position on the grill.
    // Returns whether the item given was successfully removed.
    public bool RemoveItemFromGrill(GameObject item)
    {
        if (gridItems.Contains(item)) gridItems[System.Array.IndexOf(gridItems, item)] = null;
        else return false;
        return true;
    }

    // Returns whether there's a free spot on the grill for an ingredient to be added.
    public bool GrillHasSpace()
    {
        for (int i = 0; i < gridItems.Length; i++)
            if (gridItems[i] == null) return true;
        return false;
    }

    #endregion
}
