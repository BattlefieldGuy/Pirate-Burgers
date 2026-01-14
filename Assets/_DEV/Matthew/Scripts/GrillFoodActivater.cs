using UnityEngine;

public class GrillFoodActivater : MonoBehaviour
{
    /// <summary>
    /// This is the script that makes the cooking feel a little better.
    /// 
    /// text, rigidbody and cooking manager components are controlled from here.
    /// </summary>


    #region --- VARIABLES ---
    //these components are used to activate and deactivate the cooking
    private Rigidbody rb;

    private CookingManager cookingManager;

    private GameObject text;

    #endregion

    private void Start()
    {
        // retrieve necessary components
        rb = GetComponent<Rigidbody>();
        cookingManager = GetComponent<CookingManager>();
        text = transform.GetChild(1).gameObject;
    }

    #region --- FUNCTIONS ---

    public void Enable()
    {
        cookingManager.enabled = true;
        text.SetActive(true);
        rb.constraints = RigidbodyConstraints.FreezeAll;
        this.transform.rotation = new Quaternion(0, 0, 0, 1);

    }

    public void Disable()
    {
        text.SetActive(false);
        rb.constraints = RigidbodyConstraints.None;
        cookingManager.enabled = false;
    }

    #endregion
}