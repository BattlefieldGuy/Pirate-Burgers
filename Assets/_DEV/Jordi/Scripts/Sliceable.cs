using System.Collections;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [field: SerializeField] public GameObject PiecePrefab { get; set; }

    [SerializeField] private bool destroyIngredient;

    private bool HasBeenCut;

    //slice the ingredient in pieces
    public void OnSlice()
    {
        if(!HasBeenCut)
        {
            HasBeenCut = true;
            GameObject clone = Instantiate(PiecePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
            clone.transform.parent = null;
            if (destroyIngredient)
                Destroy(this.gameObject);
            else
                StartCoroutine(CutCooldown());
        }
    }

    IEnumerator CutCooldown()
    {
        yield return new WaitForSeconds(.5f);
        HasBeenCut = false;
    }
}
