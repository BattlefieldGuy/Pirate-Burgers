using System.Collections;
using UnityEngine;


[ExecuteInEditMode]
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
            SwitchPageLeft();
        buttonDisplayName = false;
        buttonDisplayName2 = false;
    }


    public IEnumerator SwitchPageRight()
    {
        if (currentPage < pages.Length - 1)
        {
            currentPage++;
            yield return new WaitForEndOfFrame();
            pageMesh1.material.mainTexture = pages[currentPage];
            pageMesh2.material.mainTexture = pages[currentPage];
        }
        else
        {
            currentPage = 0;
        }
    }

    public IEnumerator SwitchPageLeft()
    {
        if (currentPage >= 0)
        {
            currentPage--;
            yield return new WaitForEndOfFrame();
            pageMesh1.material.mainTexture = pages[currentPage];
            pageMesh2.material.mainTexture = pages[currentPage];
        }
        else
        {
            currentPage = pages.Length - 1;
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
