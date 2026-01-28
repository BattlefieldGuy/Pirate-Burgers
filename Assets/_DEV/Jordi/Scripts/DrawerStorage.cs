using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DrawerStorage : MonoBehaviour
{
    public GameObject DrawerIngredient;

    [SerializeField, Range(0, -0.2f)] private float leftDistance;
    [SerializeField, Range(0, 0.2f)] private float rightDistance;

    private Transform drawer;
    private GameObject spawnedObject;
    private GameObject handInTrigger;

    private void Start()
    {
        drawer = transform.root;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RealHand") || other.CompareTag("HandAttachable"))
        {
            if (this.GetComponentInParent<Drawer>() != null)
            {
                if (this.GetComponentInParent<Drawer>().IsOpen)
                {
                    spawnedObject = Instantiate(DrawerIngredient, this.transform.position, this.transform.rotation);
                    //spawnedObject.name.Replace("(Clone)", "").Trim();
                    spawnedObject.name = DrawerIngredient.name;
                    handInTrigger = other.transform.parent.gameObject;
                }
            }
            else
            {
                spawnedObject = Instantiate(DrawerIngredient, this.transform.position, this.transform.rotation);
                //spawnedObject.name.Replace("(Clone)", "").Trim();
                spawnedObject.name = DrawerIngredient.name;
                handInTrigger = other.transform.parent.gameObject;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("RealHand") || other.CompareTag("HandAttachable"))
        {
            Vector3 _drawerRight = drawer.right;
            Vector3 _drawerPos = this.transform.position;
            Vector3 _drawerToHand = handInTrigger.transform.position - _drawerPos;
            float _distanceAlongRight = Vector3.Dot(_drawerToHand, _drawerRight);

            if (spawnedObject)
            {
                float _distance = Mathf.Clamp(_distanceAlongRight, leftDistance, rightDistance);
                spawnedObject.transform.position = _drawerPos + _drawerRight * _distance;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RealHand") || other.CompareTag("HandAttachable"))
        {
            if (spawnedObject.GetComponent<XRGrabInteractable>().isSelected == true)
            {
                Rigidbody _rb = spawnedObject.GetComponent<Rigidbody>();
                spawnedObject.GetComponent<XRGrabInteractable>().selectExited.AddListener(_ => _rb.isKinematic = false);
                spawnedObject = null;
            }
            else
                Destroy(spawnedObject);

            handInTrigger = null;
        }
    }
}
