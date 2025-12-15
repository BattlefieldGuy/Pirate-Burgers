using UnityEngine;

public class ToolBelt : MonoBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    private ToolAttach activeToolAttach;
    private Transform toolBeltTransform;
    private bool lastLeftHanded;

    void Update()
    {
        if (HandManager.instance.LeftHanded != lastLeftHanded)
        {
            lastLeftHanded = HandManager.instance.LeftHanded;

            if (!lastLeftHanded)
                activeToolAttach = rightHand.GetComponent<ToolAttach>();
            else
                activeToolAttach = leftHand.GetComponent<ToolAttach>();

            toolBeltTransform = activeToolAttach.toolTransformBeforeAttach;
        }
    }

    public void ToolToBeltTransform(GameObject tool)
    {
        tool.transform.position = toolBeltTransform.position;
        tool.transform.rotation = toolBeltTransform.rotation;
        tool.transform.SetParent(this.transform);
    }
}
