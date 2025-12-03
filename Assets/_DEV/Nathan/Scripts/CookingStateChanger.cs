using UnityEngine;
using System.Collections;

public class CookingStateChanger : MonoBehaviour
{
    public int cookingState = 0;

    [SerializeField] float cookingTime = 2f;

    public GameObject rawBurger, cookedBurger, burnedBurger;

    


    private void Update()
    {
        SwitchCookingState();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "EditorOnly")
        {
            Debug.Log("collision works");
            StartCoroutine(RawToCooked());
        }
    }
    private void SwitchCookingState()
    {
        switch (cookingState)
        {
            case 0:
                rawBurger.SetActive(true);
                cookedBurger.SetActive(false);
                burnedBurger.SetActive(false);
                break;

            case 1:
                cookedBurger.SetActive(true);
                rawBurger.SetActive(false);
                burnedBurger.SetActive(false);
                break;

            case 2:
                burnedBurger.SetActive(true);
                cookedBurger.SetActive(false);
                rawBurger.SetActive(false);
                break;
        }
    }

    IEnumerator RawToCooked()
    {
        yield return new WaitForSeconds(cookingTime);
        cookingState = 1;
        StartCoroutine(CookedToBurned());
    }

    IEnumerator CookedToBurned()
    {
        yield return new WaitForSeconds(cookingTime);
        cookingState = 2;
    }

}
