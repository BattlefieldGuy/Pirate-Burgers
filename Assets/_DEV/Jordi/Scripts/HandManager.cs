using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public bool LeftHanded;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
