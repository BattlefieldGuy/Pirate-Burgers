using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Knife : MonoBehaviour
{
    public XRGrabInteractable GrabInteractable;

    [SerializeField] private float velocityThreshold;
    private bool knifeCanCut;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        //checks the velocity of the knive towards the forward
        float _forwardVelocity = Vector3.Dot(ControllerVelocityManager.Instance.Velocity, transform.forward);
        if (_forwardVelocity > velocityThreshold)
            knifeCanCut = true;
        else
            knifeCanCut = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Sliceable") && knifeCanCut)
        {
            //call the OnSlice function to cut ingredient
            collision.transform.GetComponent<Sliceable>().OnSlice();
        }
    }
}
