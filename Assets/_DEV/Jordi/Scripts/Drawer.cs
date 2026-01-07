using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Drawer : MonoBehaviour
{
    [Header("Settings")]
    public float BackwardsDistance = 0f;
    public float ForwardDistance = 0.4f;

    public GameObject Handle;

    private Vector3 localMoveAxis = Vector3.forward;
    private XRGrabInteractable grabInteractable;
    private Transform drawerRoot;
    private Vector3 beginLocalPos;
    private Vector3 currentLocalPos;
    private Transform interactor;
    private float beginOffset;

    private void Awake()
    {
        grabInteractable = GetComponentInChildren<XRGrabInteractable>();
        drawerRoot = transform.parent;
    }

    private void Start()
    {
        beginLocalPos = transform.localPosition;
        currentLocalPos = transform.localPosition;
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        interactor = args.interactorObject.transform;
        //converts the WorldSpace to the LocalSpace of DrawerRoot
        Vector3 _localHandPos = drawerRoot.InverseTransformPoint(interactor.position);
        //a float that increases if moving forward and decreases when backwards
        beginOffset = Vector3.Dot(_localHandPos - currentLocalPos, localMoveAxis.normalized);
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        currentLocalPos = transform.localPosition;
        grabInteractable.transform.position = Handle.transform.position;
        interactor = null;
    }

    private void Update()
    {
        if (interactor == null)
            return;
        //converts the WorldSpace to the LocalSpace of DrawerRoot
        Vector3 _localHandPos = drawerRoot.InverseTransformPoint(interactor.position);
        //drawer cant go past minimum or maximum distance
        float _distance = Mathf.Clamp(Vector3.Dot(_localHandPos - beginLocalPos, localMoveAxis.normalized) - beginOffset, BackwardsDistance, ForwardDistance);
        transform.localPosition = beginLocalPos + localMoveAxis.normalized * _distance;
    }
}
