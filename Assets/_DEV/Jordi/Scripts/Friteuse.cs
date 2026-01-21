using UnityEngine;

public class Friteuse : MonoBehaviour
{
    GrillFoodActivater grillFoodActivater;

    private void OnTriggerEnter(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if (friteuseBak != null)
        {
            foreach(GameObject friet in friteuseBak.grillingItems)
            {
                friet.GetComponent<GrillFoodActivater>().Enable();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if (friteuseBak != null)
        {
            foreach (GameObject friet in friteuseBak.grillingItems)
            {
                friet.GetComponent<GrillFoodActivater>().Enable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if (friteuseBak != null)
        {
            foreach (GameObject friet in friteuseBak.grillingItems)
            {
                friet.GetComponent<GrillFoodActivater>().Disable();
            }
        }
    }
}
