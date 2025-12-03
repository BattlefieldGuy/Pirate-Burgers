using UnityEngine;

public class Saveable_Ingredient : MonoBehaviour
{

    public string UUID;
    void Start()
    {
        UUID = SaveManager.CreateUniqueID();
    }
}
