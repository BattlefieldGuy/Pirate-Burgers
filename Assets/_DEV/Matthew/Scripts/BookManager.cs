using System.Collections;
using UnityEngine;


public class BookManager : MonoBehaviour
{
    [SerializeField]
    private Texture2D[] pages;

    public int currentPage = 0;//temp public

    [SerializeField, Header("Pages")]
    private MeshRenderer pageMesh1;
    [SerializeField]
    private MeshRenderer pageMesh2;


    //temp
    [Header("Temp")]
    public float WaitTime;

    public bool buttonDisplayName; //"run" or "generate" for example
    public bool buttonDisplayName2; //supports multiple buttons

    void Start()
    {
        StartCoroutine(WaitAndSwitch(WaitTime));
    }

    void Update()
    {
        if (buttonDisplayName)
            StartCoroutine(SwitchPageRight());
        else if (buttonDisplayName2)
            StartCoroutine(SwitchPageLeft());
        buttonDisplayName = false;
        buttonDisplayName2 = false;
    }


    public void SwitchRight() => StartCoroutine(SwitchPageRight());
    private IEnumerator SwitchPageRight()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            yield return new WaitForEndOfFrame();
            UpdateVisuals();
        }
        else
        {
            currentPage = 0;
            UpdateVisuals();
        }
    }

    public void SwitchLeft() => StartCoroutine(SwitchPageLeft());
    private IEnumerator SwitchPageLeft()
    {
        if (currentPage > 0)
        {
            currentPage--;
            yield return new WaitForEndOfFrame();
            UpdateVisuals();
        }
        else
        {
            currentPage = pages.Length - 1;
            UpdateVisuals();
        }
    }

    private void UpdateVisuals()
    {
        pageMesh1.material.mainTexture = pages[currentPage];
        pageMesh2.material.mainTexture = pages[currentPage];
    }

    //temp
    private IEnumerator WaitAndSwitch(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        //SwitchPage();
        StartCoroutine(WaitAndSwitch(waitTime));
    }
}
