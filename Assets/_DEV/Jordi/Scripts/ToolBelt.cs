using UnityEngine;

public class ToolBelt : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private Vector3 ToolBeltOffset;

    private ToolAttach activeToolAttach;
    private bool lastLeftHanded;
    private Camera cam;
    public Transform ToolTransformOnPickup;

    private void Awake()
    {
        cam = Camera.main;
    }

    void Update()
    {
        if (HandManager.instance.LeftHanded != lastLeftHanded || activeToolAttach == null)
        {
            lastLeftHanded = HandManager.instance.LeftHanded;

            if (!lastLeftHanded)
                activeToolAttach = rightHand.GetComponent<ToolAttach>();
            else
                activeToolAttach = leftHand.GetComponent<ToolAttach>();
        }
        ToolBeltPosition();
    }

    public void ToolToBeltTransform(GameObject tool)
    {
        tool.transform.position = activeToolAttach.ToolToBeltTransform.position;
        tool.transform.rotation = activeToolAttach.ToolToBeltTransform.rotation;
        tool.transform.SetParent(activeToolAttach.ToolToBeltTransform.transform);
    }

    public void PickUp(GameObject toolAttach)
    {
        ToolTransformOnPickup = toolAttach.transform;
    }

    public void ToolToBeltOnDrop(GameObject _tool)
    {
        if(activeToolAttach.attachedTool != _tool)
        {
            _tool.transform.position = ToolTransformOnPickup.position;
            _tool.transform.rotation = ToolTransformOnPickup.rotation;
            _tool.transform.SetParent(ToolTransformOnPickup.transform);
        }
    }

    public void ToolBeltPosition()
    {
        this.transform.position = new Vector3(cam.transform.position.x + ToolBeltOffset.x, 
            this.transform.position.y, cam.transform.position.z + ToolBeltOffset.z);
    }
}
