using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToolAttach : MonoBehaviour
{
    public GameObject attachedTool;
    public Transform toolTransformBeforeAttach;
    public Transform OverlapSphereTransform;

    [SerializeField] private float attachRadius = 0.05f;
    [SerializeField] private LayerMask toolLayer;
    [SerializeField] private float attachCooldownTime;
    [SerializeField] private Transform toolSocket;
    [SerializeField] private ToolBelt toolBelt;

    private bool canAttach;

    private void Awake()
    {
        //get tool in hand and if null give warning
        foreach (Transform child in this.transform)
        {
            if (child.CompareTag("HandAttachable"))
                attachedTool = child.gameObject;
        }
        if (attachedTool == null)
            Debug.LogWarning("No tool in hand or not found");
    }

    private void Start()
    {
        canAttach = true;
    }

    private void FixedUpdate()
    {
        //if you can attach and object uses correct layer and is not the child of the hand
        //detach the currently used tool and attach the interacted tool
        if(canAttach)
        {
            Collider[] hits = Physics.OverlapSphere(OverlapSphereTransform.position, attachRadius, toolLayer);
            foreach (var hit in hits)
            {
                if(!hit.transform.IsChildOf(attachedTool.transform))
                {
                    canAttach = false;
                    Detach(attachedTool);
                    Attach(hit.transform.parent.gameObject);
                    StartCoroutine(AttachCooldown());
                }
            }
        }
    }

    private void Attach(GameObject _tool)
    {
        toolTransformBeforeAttach = _tool.transform;
        SetToolLayer(_tool, 0);
        _tool.GetComponent<Rigidbody>().isKinematic = true;
        _tool.transform.position = this.transform.position;
        _tool.transform.rotation = toolSocket.transform.rotation;
        _tool.transform.SetParent(this.transform);
        attachedTool = _tool;
        if (attachedTool.GetComponent<XRGrabInteractable>())
            attachedTool.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void Detach(GameObject _tool)
    {
        if (attachedTool.GetComponent<XRGrabInteractable>())
            attachedTool.GetComponent<XRGrabInteractable>().enabled = true;
        toolBelt.ToolToBeltTransform(_tool);
        SetToolLayer(attachedTool, 31);
    }

    private void SetToolLayer(GameObject obj, int layer)
    {
        //switches all layers of itself and the childs of the given object
        obj.layer = layer;
        foreach (Transform child in obj.transform)
        {
            SetToolLayer(child.gameObject, layer);
        }
    }


    IEnumerator AttachCooldown()
    {
        yield return new WaitForSeconds(attachCooldownTime);
        canAttach = true;
    }

    void OnDrawGizmos()
    {
        // Set the gizmo color
        Gizmos.color = Color.green;

        // Draw a sphere at the object's position
        Gizmos.DrawWireSphere(OverlapSphereTransform.position, attachRadius);
    }
}
