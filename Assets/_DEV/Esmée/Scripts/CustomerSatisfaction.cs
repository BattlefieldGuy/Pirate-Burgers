using UnityEngine;

public class CustomerSatisfaction : MonoBehaviour
{


    [SerializeField] private Sprite happy;
    [SerializeField] private Sprite notHappy;

    private float jaiidd;

    void Start()
    {

    }

    void Update()
    {
        if (jaiidd >= 50)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = happy;
        }
        else if (jaiidd >= 25)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = notHappy;
        }

    }
}
