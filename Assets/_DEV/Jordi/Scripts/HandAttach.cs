using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class HandAttach : MonoBehaviour
{
    [SerializeField] private float attachRadius = 0.1f;
    [SerializeField]private LayerMask handLayer;

    private Rigidbody equippedToolRB;
    private GameObject GrabGameObject;
    private GameObject equippedTool;
    private XRGrabInteractable equippedToolGrabScript;
    private bool hasEquipped;

    private void Awake()
    {
        GrabGameObject = this.transform.parent.GetComponentInChildren<NearFarInteractor>().gameObject;
    }

    private void FixedUpdate()
    {
        if(!hasEquipped)
        {
            Collider[] hits = Physics.OverlapSphere(this.transform.position, attachRadius, handLayer);
            foreach (var hit in hits)
            {
                hasEquipped = true;
                equippedTool = hit.transform.root.gameObject;
                GrabGameObject.SetActive(false);
                equippedTool.transform.position = this.transform.position;
                equippedTool.transform.parent = transform;
                equippedToolRB.isKinematic = true;
            }
        }
        else
        {
            if(equippedToolGrabScript.isSelected)
            {
                hasEquipped = false;
                equippedTool.transform.parent = null;
                equippedToolRB.isKinematic = false;
                GrabGameObject.SetActive(true);
                equippedTool = null;
            }
        }
    }

    private void Update()
    {
        if(!equippedToolRB && equippedTool)
        {
            equippedToolRB = equippedTool.GetComponent<Rigidbody>();
        }
    }
}
