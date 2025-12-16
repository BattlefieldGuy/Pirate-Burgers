using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RealHand"))
        {
            door.transform.DORotate(new Vector3(0, -80, 0), 3);

            StartCoroutine(FadeIn());
            StartCoroutine(WaitASec());
        }
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
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("S_MainMenu");
    }


}


