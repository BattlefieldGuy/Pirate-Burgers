using UnityEngine;

public class Salad : MonoBehaviour, ISliceable
{
    [field: SerializeField] public GameObject PiecePrefab { get; set; }

    //for loop will be replaced by just one instantiate
    public void OnSlice()
    {
        GameObject clone = Instantiate(PiecePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
        clone.transform.parent = null;
        this.gameObject.SetActive(false);
    }
}
