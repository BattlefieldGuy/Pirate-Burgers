using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToolAttach : MonoBehaviour
{
    [SerializeField] private float attachRadius = 0.05f;
    [SerializeField] private LayerMask toolLayer;
    [SerializeField] private float attachCooldownTime;

    private bool canAttach;
    private GameObject AttachedTool;

    private void Awake()
    {
        AttachedTool = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        canAttach = true;
    }

    private void FixedUpdate()
    {
        if(canAttach)
        {
            Collider[] hits = Physics.OverlapSphere(this.transform.position, attachRadius, toolLayer);
            foreach (var hit in hits)
            {
                if(!hit.transform.IsChildOf(AttachedTool.transform))
                {
                    canAttach = false;
                    AttachedTool.transform.SetParent(null);
                    AttachedTool.GetComponent<Rigidbody>().isKinematic = false;
                    SetToolLayer(AttachedTool, 31);
                    GameObject tool = hit.transform.root.gameObject;
                    SetToolLayer(tool, 0);
                    tool.GetComponent<Rigidbody>().isKinematic = true;
                    tool.transform.position = this.transform.position;
                    tool.transform.SetParent(this.transform);
                    AttachedTool = tool;
                    if (AttachedTool.GetComponent<XRGrabInteractable>())
                        AttachedTool.GetComponent<XRGrabInteractable>().enabled = false;
                    StartCoroutine(AttachCooldown());
                }
            }
        }
    }

    private void SetToolLayer(GameObject obj, int layer)
    {
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
