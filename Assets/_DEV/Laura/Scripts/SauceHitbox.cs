using System.Collections;
using UnityEngine;

public class SauceHitbox : MonoBehaviour
{
    /// <summary>
    ///     
    /// Script for the hitbox spawned by the sauce bottle, when this hits the DishManager hitbox, it'll add sauce to the meal.
    /// 
    /// </summary>

    // Amount of time in seconds that the hitbox remains active for.
    private static float LIFETIME = 0.1f;

    private void Start() => StartCoroutine(LifetimeDelay());

    private IEnumerator LifetimeDelay()
    {
        // Destroys itself after a set period so the hitbox doesn't linger indefinitely, but long enough to guarantee it can
        // check for contacts.
        yield return new WaitForSeconds(LIFETIME);
        Destroy(transform.parent.gameObject);
    }
}
