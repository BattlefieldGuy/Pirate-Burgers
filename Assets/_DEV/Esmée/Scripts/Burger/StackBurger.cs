using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StackBurger : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;

    private Recipe recipe;

    private void OnTriggerEnter(Collider other)
    {

        var grab = other.GetComponentInParent<XRGrabInteractable>();
        var rb = other.attachedRigidbody;

        if (grab == null || rb == null)
            return;


        var y = other.transform.rotation.y; //hij neemt niet de rotatie van de hand mee ofzo dus y is altijd 0 waardoor hij raar rotate. maar iig niet gekantelt so

        other.transform.position = snapPoint.position;
        other.transform.SetParent(snapPoint);

        other.transform.rotation = Quaternion.Euler(0, y, 0);

        GetComponentInChildren<BoxCollider>().enabled = false;
        other.GetComponentInParent<BoxCollider>().enabled = false;

        recipe = GetComponentInParent<Recipe>();

        if (recipe != null)
        {
            if (gameObject.tag == "MainIngredient")
                recipe.AddIngredient(other.gameObject.name, true);
            else
                recipe.AddIngredient(other.gameObject.name, false);
        }


        Debug.Log(y);


        grab.enabled = false;


        rb.useGravity = false;
        rb.isKinematic = true;

    }

}





