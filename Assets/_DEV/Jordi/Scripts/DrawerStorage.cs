using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DrawerStorage : MonoBehaviour
{
    public GameObject DrawerIngredient;

    private Transform drawer;
    private GameObject spawnedObject;
    private GameObject handInTrigger;

    private void Start()
    {
        drawer = transform.parent;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RealHand") && this.GetComponentInParent<Drawer>().IsOpen)
        {
            spawnedObject = Instantiate(DrawerIngredient, this.transform.position, this.transform.rotation);
            handInTrigger = other.transform.parent.gameObject;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Vector3 _drawerRight = drawer.right;
        Vector3 _drawerPos = this.transform.position;
        Vector3 _drawerToHand = handInTrigger.transform.position - _drawerPos;
        float _distanceAlongRight = Vector3.Dot(_drawerToHand, _drawerRight);

        if(spawnedObject)
            spawnedObject.transform.position = _drawerPos + _drawerRight * _distanceAlongRight;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RealHand"))
        {
            if (spawnedObject.GetComponent<XRGrabInteractable>().isSelected == true)
                spawnedObject = null;
            else
                Destroy(spawnedObject);
        }
    }
}
