using UnityEngine;

public class BellPress : MonoBehaviour
{
    public bool PressedBell;
    [SerializeField] private AudioSource dingding;
    private void OnTriggerEnter(Collider other)
    {
        PressedBell = true;

        if (dingding != null)
            dingding.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        PressedBell = false;
    }
}
