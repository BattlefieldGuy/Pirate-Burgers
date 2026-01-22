using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FriteuseBak : MonoBehaviour
{
    /// <summary>
    ///     FRIET of PATAT???
    /// </summary>


    #region --- VARIABLES ---

    public List<GameObject> FriteuseItems = new List<GameObject>();

    [SerializeField]
    private ParticleSystem BubbleParticles;

    [SerializeField]
    private string FriesFoodTag = "Fries";

    [SerializeField] private AudioSource FriteuseAudioSource;

    private bool VFXOn = false;

    #endregion

    #region --- BASIC UNITY METHODS ---

    void Update()
    {
        VFXController();
        AudioController();
    }

    #endregion

    #region --- TRIGGER HANDLERS ---

    private void OnTriggerEnter(Collider other)
    {
        // When fries are inside the Friteusebak add to Friteusebak
        if (other.CompareTag(FriesFoodTag))
        {
            AddFoodToFriteuseBak(other.gameObject);
        }

        //if there is at least one item on the grill, play the grill sound
    }

    private void OnTriggerExit(Collider other)
    {
        // When an item get's removed from the Friteusebak it stops cooking
        if (FriteuseItems.Contains(other.gameObject))
        {
            RemoveFoodFromFriteuseBak(other.gameObject);
            other.GetComponent<GrillFoodActivater>().Disable();
        }
    }

    #endregion

    #region --- FOOD HANDELERS ---

    private void AddFoodToFriteuseBak(GameObject _food)
    {
        FriteuseItems.Add(_food);
        _food.transform.parent = this.transform;
    }

    private void RemoveFoodFromFriteuseBak(GameObject _food)
    {
        FriteuseItems.Remove(_food);
        _food.transform.SetParent(null);
    }

    #endregion

    #region --- VFX ---

    private void VFXController()
    {
        if (FriteuseItems.Count > 0)
        {
            if (!VFXOn)
            {
                // Start grill VFX
                BubbleParticles.Play();
                VFXOn = true;
            }
        }
        else
        {
            if (VFXOn)
            {
                // Stop grill VFX
                BubbleParticles.Stop();
                VFXOn = false;
            }
        }
    }

    private void AudioController()
    {
        if (FriteuseItems.Count > 0 && !FriteuseAudioSource.isPlaying)
        {
            FriteuseAudioSource.Play();
        }
        else if (FriteuseItems.Count == 0 && FriteuseAudioSource.isPlaying)
        {
            FriteuseAudioSource.Stop();
        }
    }

    #endregion
}
