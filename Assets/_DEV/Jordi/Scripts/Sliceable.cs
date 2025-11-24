using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [field: SerializeField] public GameObject PiecePrefab { get; set; }

    public void OnSlice()
    {
        GameObject clone = Instantiate(PiecePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        clone.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
