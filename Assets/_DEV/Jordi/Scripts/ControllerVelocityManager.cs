using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class ControllerVelocityManager : MonoBehaviour
{
    public static ControllerVelocityManager Instance;

    public Vector3 Velocity;
    public bool LeftHanded;

    private InputDevice leftDevice;
    private InputDevice rightDevice;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(this.gameObject);
    }

    void Start()
    {
        FindDevices();
    }

    private void Update()
    {
        //if devices are not found try finding again till found
        if (!leftDevice.isValid || !rightDevice.isValid)
        {
            FindDevices();
        }

        //check the velocity of your main hand
        if (!LeftHanded)
            CheckVelocity(rightDevice);
        else
            CheckVelocity(leftDevice);
    }

    private void FindDevices()
    {
        // Find devices on LeftHand / RightHand
        var leftHanded = new List<InputDevice>();
        var rightHanded = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.LeftHand, leftHanded);
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, rightHanded);

        if (leftHanded.Count > 0) leftDevice = leftHanded[0];
        if (rightHanded.Count > 0) rightDevice = rightHanded[0];
    }

    private void CheckVelocity(InputDevice device)
    {
        if (device.TryGetFeatureValue(CommonUsages.deviceVelocity, out Velocity))
        {

        }
    }
}
