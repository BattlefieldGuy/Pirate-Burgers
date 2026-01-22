using System.Collections;
using UnityEngine;

public class GrabPlate : MonoBehaviour
{
    [SerializeField] private GameObject plate;

    private GameObject spawnedObject;
    private GameObject handInTrigger;
    [SerializeField] private Transform spawnpoint;

    private bool canSpawn = true;

    private void OnTriggerEnter(Collider other)
    {
        if (!canSpawn) return;

        if (other.CompareTag("RealHand") || other.CompareTag("HandAttachable"))
        {
            spawnedObject = Instantiate(plate, spawnpoint.transform.position, spawnpoint.transform.rotation);
            handInTrigger = other.transform.parent.gameObject;
        }
        StartCoroutine(WaitForNextPlate());
    }

    private IEnumerator WaitForNextPlate()
    {
        canSpawn = false;
        yield return new WaitForSeconds(5);
        canSpawn = true;
    }
}

