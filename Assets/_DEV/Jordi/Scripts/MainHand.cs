using UnityEngine;

enum Hand
{
    left,
    right
}

public class MainHand : MonoBehaviour
{
    [SerializeField] private GameObject ControllerVisuals;
    [SerializeField] private GameObject ToolObject;
    [SerializeField] private Hand thisHand;

    private void Update()
    {
        //boolean that checks if lefthanded is true and returns false or true depending on which hand this is
        bool isRightHandActive = (HandManager.instance.LeftHanded) ? thisHand != Hand.right : thisHand == Hand.right;
        ControllerVisuals.SetActive(!isRightHandActive);
        ToolObject.SetActive(isRightHandActive);
    }
}
