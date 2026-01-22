using UnityEngine;

public class prullenbak : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("RealHand") || !other.CompareTag("HandAttachable"))
            Destroy(other.gameObject);
    }
}
