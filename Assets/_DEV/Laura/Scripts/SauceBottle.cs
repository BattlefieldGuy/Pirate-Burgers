using UnityEngine;

public class SauceBottle : MonoBehaviour
{
    [SerializeField] private GameObject sauceHitbox;
    [SerializeField] private Transform hitboxSpawnPosition;

    // Call the function below when the user squeezes a held sauce bottle.
    public void SquirtSauce()
    {
        Instantiate(sauceHitbox, hitboxSpawnPosition.position, hitboxSpawnPosition.rotation);
    }
}
