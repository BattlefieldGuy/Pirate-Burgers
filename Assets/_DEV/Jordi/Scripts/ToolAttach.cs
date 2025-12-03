using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToolAttach : MonoBehaviour
{
    [SerializeField] private float attachRadius = 0.05f;
    [SerializeField] private LayerMask toolLayer;
    [SerializeField] private float attachCooldownTime;

    private bool canAttach;
    private GameObject attachedTool;
    private Quaternion toolRotation;

    private void Awake()
    {
        //get tool in hand and if null give warning
        attachedTool = transform.GetChild(0).gameObject;
        if (attachedTool == null)
            Debug.LogWarning("No tool in hand");
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
            Collider[] hits = Physics.OverlapSphere(this.transform.position, attachRadius, toolLayer);
            foreach (var hit in hits)
            {
                if(!hit.transform.IsChildOf(attachedTool.transform))
                {
                    canAttach = false;
                    Detach();
                    Attach(hit.transform.root.gameObject);
                    StartCoroutine(AttachCooldown());
                }
            }
        }
    }

    private void Attach(GameObject _tool)
    {
        SetToolLayer(_tool, 0);
        _tool.GetComponent<Rigidbody>().isKinematic = true;
        _tool.transform.position = this.transform.position;
        _tool.transform.rotation = toolRotation;
        _tool.transform.SetParent(this.transform);
        attachedTool = _tool;
        if (attachedTool.GetComponent<XRGrabInteractable>())
            attachedTool.GetComponent<XRGrabInteractable>().enabled = false;
    }

    private void Detach()
    {
        toolRotation = attachedTool.transform.rotation;
        attachedTool.transform.SetParent(null);
        attachedTool.GetComponent<Rigidbody>().isKinematic = false;
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
        Gizmos.DrawWireSphere(transform.position, attachRadius);
    }
}
