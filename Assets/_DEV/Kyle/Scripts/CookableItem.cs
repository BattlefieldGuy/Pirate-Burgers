using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class CookableItem : MonoBehaviour
{
    public List<Renderer> renderers = new List<Renderer>();
    public TMP_Text statusText;
    public Transform lookTarget;

    void Update()
    {
        if (lookTarget != null)
        {
            statusText.transform.LookAt(lookTarget);
            statusText.transform.Rotate(0, 180f, 0);
        }
    }

    public void SetColor(Color color)
    {
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }

    public void SetText(string text)
    {
        statusText.text = text;
    }
}
