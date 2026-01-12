using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StackBurger : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {

        var grab = other.GetComponentInParent<XRGrabInteractable>();
        var rb = other.attachedRigidbody;

        if (grab == null || rb == null)
            return;

        other.transform.position = snapPoint.position;

        other.transform.SetParent(snapPoint);

        GetComponentInChildren<BoxCollider>().enabled = false;
        other.GetComponentInParent<BoxCollider>().enabled = false;



        grab.enabled = false;


        rb.useGravity = false;
        rb.isKinematic = true;

    }

}



