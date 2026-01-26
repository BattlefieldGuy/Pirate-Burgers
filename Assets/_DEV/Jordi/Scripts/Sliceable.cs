using System.Collections;
using UnityEngine;

public class Sliceable : MonoBehaviour
{
    [field: SerializeField] public GameObject PiecePrefab { get; set; }

    [SerializeField] private int TimesCanCut;

    private bool HasBeenCut;

    //slice the ingredient in pieces
    public void OnSlice()
    {
        if(TimesCanCut > 0)
        {
            if (!HasBeenCut)
            {
                HasBeenCut = true;
                TimesCanCut--;
                GameObject clone = Instantiate(PiecePrefab, this.gameObject.transform.position, this.gameObject.transform.rotation);
                clone.transform.parent = null;
                StartCoroutine(CutCooldown());
            }
        }
        else
            Destroy(this);
    }

    IEnumerator CutCooldown()
    {
        yield return new WaitForSeconds(.5f);
        HasBeenCut = false;
    }
}
