using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class FriteuseBak : MonoBehaviour
{
    /// <summary>
    ///     FRIET of PATAT???
    /// </summary>


    #region --- VARIABLES ---

    public List<GameObject> grillingItems = new List<GameObject>();

    [SerializeField]
    private ParticleSystem BubbleParticles;

    [SerializeField]
    private string grillFoodTag = "Fries";

    [SerializeField] private AudioSource grillAudioSource;

    private bool VFXOn = false;

    #endregion

    #region --- BASIC UNITY METHODS ---

    void Update()
    {
        VFXController();
        AudioController();
        Debug.Log(grillAudioSource.isPlaying);
    }

    #endregion

    #region --- TRIGGER HANDLERS ---

    private void OnTriggerEnter(Collider other)
    {
        // When an item get's placed on the grill it will start cooking
        if (other.CompareTag(grillFoodTag))
        {
            AddFoodToFriteuseBak(other.gameObject);
        }

        //if there is at least one item on the grill, play the grill sound
    }

    private void OnTriggerExit(Collider other)
    {
        // When an item get's removed from the grill it will stop cooking
        if (grillingItems.Contains(other.gameObject))
        {
            RemoveFoodFromFriteuseBak(other.gameObject);
            other.GetComponent<GrillFoodActivater>().Disable();
        }
    }

    #endregion

    #region --- FOOD HANDELERS ---

    private void AddFoodToFriteuseBak(GameObject _food)
    {
        grillingItems.Add(_food);
        _food.transform.parent = this.transform;
    }

    private void RemoveFoodFromFriteuseBak(GameObject _food)
    {
        grillingItems.Remove(_food);
        _food.transform.SetParent(null);
    }

    #endregion

    #region --- VFX ---

    private void VFXController()
    {
        if (grillingItems.Count > 0)
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
        if (grillingItems.Count > 0 && !grillAudioSource.isPlaying)
        {
            grillAudioSource.Play();
        }
        else if (grillingItems.Count == 0 && grillAudioSource.isPlaying)
        {
            grillAudioSource.Stop();
        }
    }

    #endregion
}
