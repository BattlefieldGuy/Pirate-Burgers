using UnityEngine;
using System.Collections.Generic;

public class Ambience : MonoBehaviour
{
    private float timer = 0f;

    [SerializeField] private List<AudioClip> parrots;
    [SerializeField] private AudioSource source;
    void Start() 
    {
        timer = Random.Range(1f, 15f);
    }
    
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            int randomIndex = Random.Range(0, parrots.Count);
            source.clip = parrots[randomIndex];
            source.Play();
            timer = Random.Range(1f, 15f);
        }
    }
}
