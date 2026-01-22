using UnityEngine;

public class Friteuse : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
            FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
            if (friteuseBak != null)
            {
                foreach(GameObject friet in friteuseBak.FriteuseItems)
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
            foreach (GameObject friet in friteuseBak.FriteuseItems)
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
            foreach (GameObject friet in friteuseBak.FriteuseItems)
            {
                friet.GetComponent<GrillFoodActivater>().Disable();
            }
        }
    }
}
