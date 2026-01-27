using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FriteuseBak : MonoBehaviour
{
    /// <summary>
    ///     FRIET of PATAT???
    /// </summary>


    #region --- VARIABLES ---

    public List<GameObject> FriteuseItems = new List<GameObject>();

    [SerializeField]
    private string FriesFoodTag = "Fries";

    #endregion

    #region --- TRIGGER HANDLERS ---

    private void OnTriggerEnter(Collider other)
    {
        // When fries are inside the Friteusebak add to Friteusebak
        if (other.CompareTag(FriesFoodTag))
        {
            AddFoodToFriteuseBak(other.gameObject);
        }

        //if there is at least one item on the grill, play the grill sound
    }

    private void OnTriggerExit(Collider other)
    {
        // When an item get's removed from the Friteusebak it stops cooking
        if (FriteuseItems.Contains(other.gameObject))
        {
            RemoveFoodFromFriteuseBak(other.gameObject);
            other.GetComponent<GrillFoodActivater>().Disable();
        }
    }

    #endregion

    #region --- FOOD HANDELERS ---

    private void AddFoodToFriteuseBak(GameObject _food)
    {
        FriteuseItems.Add(_food);
        _food.transform.parent = this.transform;
        _food.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void RemoveFoodFromFriteuseBak(GameObject _food)
    {
        FriteuseItems.Remove(_food);
        _food.transform.SetParent(null);
        _food.GetComponent<Rigidbody>().isKinematic = false;
    }

    #endregion
}
