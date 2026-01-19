using System.Collections;
using UnityEngine;

public class BookManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] pages;

    private int currentPage = 0;

    [SerializeField, Header("Pages")]
    private MeshRenderer pageMesh1;
    [SerializeField]
    private MeshRenderer pageMesh2;

    //temp
    [Header("Temp")]
    public float WaitTime;

    void Start()
    {
        StartCoroutine(WaitAndSwitch(WaitTime));
    }

    void Update()
    {

    }


    public void SwitchPageRight()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            pageMesh1.material.mainTexture = pages[currentPage];
            pageMesh2.material.mainTexture = pages[currentPage];
        }
        else
        {
            currentPage = 0;
        }
    }

    public void SwitchPageLeft()
    {
        if (currentPage >= 0)
        {
            currentPage--;
            pageMesh1.material.mainTexture = pages[currentPage];
            pageMesh2.material.mainTexture = pages[currentPage];
        }
        else
        {
            currentPage = pages.Length;
        }
    }

    //temp
    private IEnumerator WaitAndSwitch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //SwitchPage();
        StartCoroutine(WaitAndSwitch(waitTime));
    }
}
