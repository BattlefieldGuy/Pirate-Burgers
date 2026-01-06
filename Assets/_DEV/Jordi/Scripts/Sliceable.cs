using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [field: SerializeField] public GameObject PiecePrefab { get; set; }

    private bool HasBeenCut;

    //slice the ingredient in pieces
    public void OnSlice()
    {
        if(!HasBeenCut)
        {
            HasBeenCut = true;
            GameObject clone = Instantiate(PiecePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            clone.transform.parent = null;
            this.gameObject.SetActive(false);
        }
    }
}
