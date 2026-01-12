using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StackBurger : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {

        other.transform.position = snapPoint.position;
        other.transform.SetParent(transform.parent);

        GetComponentInChildren<BoxCollider>().enabled = false;
        other.GetComponentInParent<XRGrabInteractable>().enabled = false;


        Debug.Log("MIAAAUW");

        other.attachedRigidbody.useGravity = false;
        other.attachedRigidbody.isKinematic = true;
    }

}



