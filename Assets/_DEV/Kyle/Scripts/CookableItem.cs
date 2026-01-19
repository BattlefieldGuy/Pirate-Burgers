using UnityEngine;
using TMPro;

public class CookableItem : MonoBehaviour
{
    public Renderer rend;
    public TMP_Text statusText;
    public Transform lookTarget;

    Material mat;

    void Awake()
    {
        mat = rend.material;
    }

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
        mat.color = color;
    }

    public void SetText(string text)
    {
        statusText.text = text;
    }
}
