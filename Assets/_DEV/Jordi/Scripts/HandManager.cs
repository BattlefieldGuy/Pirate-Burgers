using UnityEngine;

public class HandManager : MonoBehaviour
{
    public static HandManager instance;

    public bool LeftHanded;

    public void BoolChange()
    {
        if(LeftHanded)
            LeftHanded = false;
        else
            LeftHanded = true;
    }

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
