using System.Collections.Generic;
using UnityEngine;

public class GrillManager : MonoBehaviour
{
    /// <summary>
    /// This manager is responsible of hnandeling the grill grid manager and the cooking manager on every item.
    /// 
    /// </summary>


    #region --- VARIABLES ---

    public List<GameObject> grillingItems = new List<GameObject>();

    [SerializeField]
    private string grillFoodTag = "GrillFood";


    private GrillGridManager grillGridManager;

    #endregion

    void Start()
    {
        grillGridManager = GetComponentInChildren<GrillGridManager>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("TSSSSSS");
        // When an item get's placed on the grill it will start cooking
        if (other.CompareTag(grillFoodTag))
        {
            grillingItems.Add(other.gameObject);
            Debug.Log("Added item to grill: " + other.gameObject.name);
            CookingManager cookingManager = other.GetComponent<CookingManager>();
            if (cookingManager != null)
            {
                cookingManager.enabled = true;
                Debug.Log("Started cooking item: " + other.gameObject.name);
            }

            if (grillGridManager != null)
            {
                grillGridManager.AddItemToGrill(other.gameObject);
                Debug.Log("Added item to grill grid: " + other.gameObject.name);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When an item get's removed from the grill it will stop cooking
        if (grillingItems.Contains(other.gameObject))
        {
            grillingItems.Remove(other.gameObject);

            CookingManager cookingManager = other.GetComponent<CookingManager>();
            if (cookingManager != null)
            {
                cookingManager.enabled = false;
            }

            if (grillGridManager != null)
            {
                grillGridManager.RemoveItemFromGrill(other.gameObject);
            }
        }
    }
}
