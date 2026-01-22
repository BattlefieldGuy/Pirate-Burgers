using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GrillManager : MonoBehaviour
{
    /// <summary>
    /// This manager is responsible of handeling the grill grid manager and the cooking manager on every item.
    /// 
    /// Once an item with the grill food tag enters the grill area, it will be put trough the AddFoodToGrill function. 
    /// This function adds the item to the grilling items list to keep track of, add the item to the grill grid manager 
    /// and enables the cooking activator on the item.
    /// 
    /// the cooking activator manages the cooking but also disables the rigidbody movement and rotation, and activates the text above the item.
    ///
    /// hi thomas here i added the funny sounds heehoo
    /// </summary>


    #region --- VARIABLES ---

    public List<GameObject> grillingItems = new List<GameObject>();

    [SerializeField]
    private VisualEffect grillVFX;

    [SerializeField]
    private string grillFoodTag = "SecondaryIgredient";

    private GrillGridManager grillGridManager;

    [SerializeField] private AudioSource grillAudioSource;

    private bool VFXOn = false;

    #endregion

    #region --- BASIC UNITY METHODS ---

    void Start()
    {
        grillGridManager = GetComponentInChildren<GrillGridManager>();
        if (grillGridManager == null)
        {
            grillAudioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        VFXController();
        AudioController();
    }

    #endregion

    #region --- TRIGGER HANDLERS ---

    private void OnTriggerEnter(Collider other)
    {
        // When an item get's placed on the grill it will start cooking
        if (other.CompareTag(grillFoodTag))
        {
            AddFoodToGrill(other.gameObject);
        }

        //if there is at least one item on the grill, play the grill sound
    }

    private void OnTriggerExit(Collider other)
    {
        // When an item get's removed from the grill it will stop cooking
        if (grillingItems.Contains(other.gameObject))
        {
            RemoveFoodFromGrill(other.gameObject);
        }
    }

    #endregion

    #region --- FOOD HANDELERS ---

    private void AddFoodToGrill(GameObject _food)
    {
        grillingItems.Add(_food.gameObject);
        GrillFoodActivater _grillFoodManager = _food.GetComponent<GrillFoodActivater>();
        if (_grillFoodManager != null)
        {
            _grillFoodManager.Enable();
        }

        if (grillGridManager != null)
        {
            grillGridManager.AddItemToGrill(_food.gameObject);
        }
    }

    private void RemoveFoodFromGrill(GameObject _food)
    {
        grillingItems.Remove(_food.gameObject);

        GrillFoodActivater _grillFoodManager = _food.GetComponent<GrillFoodActivater>();
        if (_grillFoodManager != null)
        {
            _grillFoodManager.Disable();
        }

        if (grillGridManager != null)
        {
            grillGridManager.RemoveItemFromGrill(_food.gameObject);
        }
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
                grillVFX.Play();
                VFXOn = true;
            }
        }
        else
        {
            if (VFXOn)
            {
                // Stop grill VFX
                grillVFX.Stop();
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
