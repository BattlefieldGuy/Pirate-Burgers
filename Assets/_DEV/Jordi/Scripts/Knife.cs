using UnityEngine;

public class Knife : MonoBehaviour
{
    [SerializeField] private GameObject saladPiecePrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Sliceable"))
        {
            //slice in half
            for(int i = 0; i < 2; i++)
            {
                GameObject clone = Instantiate(saladPiecePrefab, collision.transform.position, collision.transform.rotation);
                collision.gameObject.SetActive(false);
                clone.transform.parent = null;
                clone.transform.localScale = new Vector3(.25f, .25f, .25f);
            }
        }
    }
}
