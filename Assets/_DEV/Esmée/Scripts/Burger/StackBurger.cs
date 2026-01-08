using UnityEngine;

public class StackBurger : MonoBehaviour
{
    //private void OnTriggerEnter(Collider other)
    //{

    //    Transform ingredientRoot = other.attachedRigidbody.transform;

    //    ingredientRoot.SetParent(transform.parent);


    //    //other.transform.parent = transform;
    //    other.attachedRigidbody.isKinematic = true;
    //    other.attachedRigidbody.useGravity = false;

    //}

    //void OnTriggerExit(Collider other)
    //{
    //    other.transform.parent = null;
    //}





    private bool occupied = false;

    private void OnTriggerEnter(Collider other)
    {
        if (occupied) return;
        if (other.attachedRigidbody == null) return;

        Transform ingredient = other.attachedRigidbody.transform;

        ingredient.position = transform.position;

        // Parent het ingredient aan DIT ingredient
        ingredient.SetParent(transform.parent);

        // Zet physics uit
        other.attachedRigidbody.isKinematic = true;
        other.attachedRigidbody.useGravity = false;

        occupied = true;
    }
}



