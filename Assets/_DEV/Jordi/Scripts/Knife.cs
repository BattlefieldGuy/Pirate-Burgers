using System.Linq;
using UnityEngine;

public class Knife : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Sliceable"))
        {
            //get the script inside the sliceable object with the interface ISliceable on it
            ISliceable _sliceable = collision.gameObject.GetComponents<MonoBehaviour>().OfType<ISliceable>().FirstOrDefault();
            //if not null call OnSlice
            _sliceable?.OnSlice();
        }
    }
}
