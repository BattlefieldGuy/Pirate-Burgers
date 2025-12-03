using UnityEngine;

public class Saveable_Ingredient : MonoBehaviour
{
    /// <summary>
    /// Registers this ingredient with the SaveManager to allow saving and loading of this object
    /// While guaranteeing a start with its own UUID, but may be overridden to replace deleted objects. This is why there's a confirmation timer
    /// </summary>

    [SerializeField] float ConfirmationDuraction = .5f;
    bool Locked;

    public string UUID;
    void Start()
    {
        //put a print here before which did start
        if (UUID == "")
        {
            print("Asking this stupid shit to GIVE ME A FUCKING ID RAH");
            UUID = SaveManager.CreateUniqueID();
        }
    }

    public void ApplyInstantiatedVelocity(Vector3 linear, Vector3 angular)
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity = linear;
        rb.angularVelocity = angular;
    }

    void Update()
    {
        if (!Locked)
        {
            if (ConfirmationDuraction > 0)
            {
                ConfirmationDuraction -= Time.deltaTime;
            }
            else
            {
                Locked = true;
                SaveManager.AddIngredientID(UUID);
            }
        }
    }

    private void confirmDestroy()
    {
        SaveManager.deleteIngredientID(UUID);
    }
}
