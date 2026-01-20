using UnityEngine;

public class Pickle : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private int randomPickleNumber;
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 10)
        {
            PickleNumber();

            if (randomPickleNumber == 67)
            {
                anim.SetTrigger("pickles");
            }
        }
    }

    private void PickleNumber()
    {
        randomPickleNumber = Random.Range(1, 99);
    }

    //sorry 'bout this
}
