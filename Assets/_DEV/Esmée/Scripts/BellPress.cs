using UnityEngine;

public class BellPress : MonoBehaviour
{
    public bool PressedBell;
    private void OnTriggerEnter(Collider other)
    {
        PressedBell = true;
    }

    private void OnTriggerExit(Collider other)
    {
        PressedBell = false;
    }
}
