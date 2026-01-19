using UnityEngine;

public class ToolBelt : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private ToolAttach activeToolAttach;
    private bool lastLeftHanded;
    private Camera cam;

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

    public void ToolBeltPosition()
    {
        this.transform.position = new Vector3(cam.transform.position.x, this.transform.position.y, cam.transform.position.z);
    }
}
