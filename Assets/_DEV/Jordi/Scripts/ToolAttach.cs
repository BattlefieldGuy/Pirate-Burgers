using System.Collections;
using UnityEngine;

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
                if(hit.gameObject != AttachedTool)
                {
                    Debug.Log(hit.transform.root);
                    AttachedTool.transform.SetParent(null);
                    AttachedTool.GetComponent<Rigidbody>().isKinematic = false;
                    hit.transform.root.GetComponent<Rigidbody>().isKinematic = true;
                    hit.transform.root.SetParent(this.transform);
                    canAttach = false;
                    StartCoroutine(AttachCooldown());
                }
            }
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
