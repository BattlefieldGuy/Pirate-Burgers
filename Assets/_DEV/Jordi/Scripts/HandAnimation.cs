using UnityEngine;
using UnityEngine.XR;

public class HandAnimation : MonoBehaviour
{
    public XRNode handNode;

    private InputDevice device;
    private Animator animator;

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(handNode);
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(handNode);
            return;
        }

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            animator.SetFloat("Grabbing", gripValue);
        }
    }
}
