using UnityEngine;

public class Friteuse : MonoBehaviour
{
    [SerializeField] private AudioSource FriteuseAudioSource;

    private bool VFXOn = false;

    private void OnTriggerEnter(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if (friteuseBak != null)
        {
            if (friteuseBak.FriteuseItems.Count > 0)
            {
                foreach (GameObject friet in friteuseBak.FriteuseItems)
                {
                    friet.GetComponent<GrillFoodActivater>().Enable();
                }
                if (!VFXOn)
                {
                    // Start grill VFX
                    friteuseBak.GetComponentInChildren<ParticleSystem>().Play();
                    VFXOn = true;
                }
                if (!FriteuseAudioSource.isPlaying)
                {
                    FriteuseAudioSource.Play();
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if(friteuseBak != null)
        {
            foreach (GameObject friet in friteuseBak.FriteuseItems)
            {
                friet.GetComponent<GrillFoodActivater>().Enable();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        FriteuseBak friteuseBak = other.gameObject.GetComponent<FriteuseBak>();
        if (friteuseBak != null)
        {
            foreach (GameObject friet in friteuseBak.FriteuseItems)
            {
                friet.GetComponent<GrillFoodActivater>().Disable();
            }
            if (VFXOn)
            {
                // Stop grill VFX
                friteuseBak.GetComponentInChildren<ParticleSystem>().Stop();
                VFXOn = false;
            }
            if (FriteuseAudioSource.isPlaying)
            {
                FriteuseAudioSource.Stop();
            }
        }
    }
}