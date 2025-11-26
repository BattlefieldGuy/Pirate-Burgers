using UnityEngine;

public class HandAttach : MonoBehaviour
{
    [SerializeField] private float attachRadius = 0.1f;

    private bool hasEquipped;

    private void FixedUpdate()
    {
        if(!hasEquipped)
        {
            Collider[] hits = Physics.OverlapSphere(this.transform.position, attachRadius);
            foreach (var hit in hits)
            {
                if(hit.transform.root.CompareTag("HandAttachable"))
                {
                    hasEquipped = true;
                    Transform root = hit.transform.root;
                    Rigidbody rb = root.GetComponent<Rigidbody>(); 
                    if (rb != null) 
                        rb.isKinematic = true; 
                    root.transform.position = this.transform.position;
                    root.transform.parent = transform;
                }
            }
        }
    }
}
