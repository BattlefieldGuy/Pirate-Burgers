using UnityEngine;
using UnityEngine.XR;

public class ToolAnimation : MonoBehaviour
{
    public Animator Animator;
    public XRNode handNode;

    private InputDevice device;

    private void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(handNode);
    }

    private void Update()
    {
        if (!Animator) return;

        if (!device.isValid)
        {
            device = InputDevices.GetDeviceAtXRNode(handNode);
            return;
        }

        if (device.TryGetFeatureValue(CommonUsages.grip, out float gripValue))
        {
            Animator.SetFloat("Grab", gripValue);
        }
    }
}
