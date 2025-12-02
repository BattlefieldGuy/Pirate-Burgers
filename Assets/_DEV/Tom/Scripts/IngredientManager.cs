using UnityEngine;

public class IngredientManager : MonoBehaviour
{
    private int mainIngredient;
    private int[] sideIngredients = new int[10];

    private void OnTriggerEnter(Collider other)
    {
        if (mainIngredient == 0)
        {
            mainIngredient = other.GetComponent<Ingredients>().IngredientValue;
        }
    }
    private void StartConstruction()
    {

    }
}
