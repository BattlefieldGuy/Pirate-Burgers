using UnityEngine;

public class StackBurger : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;

    private void OnTriggerEnter(Collider other)
    {

        other.transform.SetParent(transform.parent);
        other.transform.position = snapPoint.position;
        //GetComponent<BoxCollider>().enabled = false;
        Debug.Log("MIAAAUW");

        other.attachedRigidbody.useGravity = false;
    }


    /*ontriggerexit werkt niet met vr er moet dan iets komen wat met de exit de items unparent. ik word gek dus hou het hierbij voor nu 
     
     de items stacken alleen tis allemaal heel vervelend door het parenten
    
     bord is wel gefixt niks kan er onder parenten*/
}



