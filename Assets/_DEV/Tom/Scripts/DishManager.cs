using UnityEngine;

public class DishManager : MonoBehaviour
{
    /// <summary>
    /// this script detects what type of ingredient enters the plate's trigger and adds it to the recipe list.
    /// it will only allow secondary ingredients on the plate if there is a main ingredient on the plate, not if the plate is empty.
    /// </summary>
    private Recipe recipe;
    private bool hasMain = false;

    private void Start()
    {
        recipe = this.gameObject.GetComponent<Recipe>();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("MainIngredient"))
        {
            if(!hasMain)
            {
            recipe.AddIngredient(other.gameObject.name, true);
            hasMain = true;
            }
        }
        else
        {
            if(hasMain)
            {
            recipe.AddIngredient(other.gameObject.name, false);
            }
        }
    }
}