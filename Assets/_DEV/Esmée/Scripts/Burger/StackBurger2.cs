using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StackBurger2 : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;
    [SerializeField] private Collider col;


    private Recipe recipe;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other + "COLLIDED");
        var grab = GetComponent<XRGrabInteractable>();
        //var rb = other.attachedRigidbody;
        var rb = GetComponent<Rigidbody>();


        if (grab == null || rb == null)
            return;


        //var y = other.transform.rotation.y; //hij neemt niet de rotatie van de hand mee ofzo dus y is altijd 0 waardoor hij raar rotate. maar iig niet gekantelt so

        transform.position = snapPoint.position;
        other.transform.SetParent(snapPoint);

        //transform.rotation = Quaternion.Euler(0, y, 0);

        //GetComponentInChildren<BoxCollider>().enabled = true;

        //other.GetComponentInChildren<BoxCollider>().enabled = false;
        col.enabled = false;
        recipe = GetComponentInParent<Recipe>();

        if (recipe != null)
        {
            if (gameObject.tag == "MainIngredient")
                recipe.AddIngredient(other.gameObject.name, true);
            else
                recipe.AddIngredient(other.gameObject.name, false);
        }


        grab.enabled = false;

        rb.useGravity = false;
        rb.isKinematic = true;

    }
}
