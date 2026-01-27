using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutToMM : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    void Start()
    {
        StartCoroutine(WaitASec());
    }


    IEnumerator FadeIn()
    {
        Renderer rend = sphere.transform.GetComponent<Renderer>();

        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            rend.material.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }

    IEnumerator WaitASec()
    {

        yield return new WaitForSeconds(15f);
        StartCoroutine(FadeIn());
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene("S_MainMenu");
    }

}
