using UnityEngine;
using System.Collections.Generic;

public class CookingManager : MonoBehaviour
{
    public List<CookableItem> items = new List<CookableItem>();

    [Header("Colors")]
    public Color rawColor = Color.white;
    public Color mediumColor = new Color(0.8f, 0.5f, 0.3f);
    public Color wellDoneColor = new Color(0.5f, 0.25f, 0.1f);
    public Color burntColor = Color.black;

    [Header("Stage Times")]
    public float rawTime = 2f;
    public float mediumTime = 3f;
    public float wellDoneTime = 3f;

    float timer;
    int stage; // 0=Raw,1=Medium,2=WellDone,3=Burnt

    void Start()
    {
        ApplyText();
    }

    void Update()
    {
        timer += Time.deltaTime;

        Color from = GetFromColor();
        Color to = GetToColor();
        float duration = GetStageTime();

        float t = duration > 0 ? timer / duration : 1f;
        t = Mathf.Clamp01(t);

        foreach (var item in items)
        {
            if (item == null) continue;
            item.SetColor(Color.Lerp(from, to, t));
        }

        if (timer >= duration && stage < 3)
        {
            stage++;
            timer = 0f;
            ApplyText();
        }
    }

    void ApplyText()
    {
        foreach (var item in items)
        {
            if (item == null) continue;

            switch (stage)
            {
                case 0: item.SetText("Raw"); break;
                case 1: item.SetText("Medium"); break;
                case 2: item.SetText("Well Done"); break;
                case 3: item.SetText("Overcooked"); break;
            }
        }
    }

    Color GetFromColor()
    {
        switch (stage)
        {
            case 0: return rawColor;
            case 1: return mediumColor;
            case 2: return wellDoneColor;
            default: return burntColor;
        }
    }

    Color GetToColor()
    {
        switch (stage)
        {
            case 0: return mediumColor;
            case 1: return wellDoneColor;
            case 2: return burntColor;
            default: return burntColor;
        }
    }

    float GetStageTime()
    {
        switch (stage)
        {
            case 0: return rawTime;
            case 1: return mediumTime;
            case 2: return wellDoneTime;
            default: return 0f;
        }
    }
}
