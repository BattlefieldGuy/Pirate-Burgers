using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

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
    [SerializeField] private NearFarInteractor interactor;
    [SerializeField] private NearFarInteractor grabberInteractor;

    private void Update()
    {
        //boolean that checks if lefthanded is true and returns false or true depending on which hand this is
        bool isRightHandActive = (HandManager.instance.LeftHanded) ? thisHand != Hand.right : thisHand == Hand.right;
        ControllerVisuals.SetActive(!isRightHandActive);
        ToolObject.SetActive(isRightHandActive);
        interactor.gameObject.SetActive(!isRightHandActive);
        if (transform.GetComponentInChildren<ToolAttach>() && transform.GetComponentInChildren<ToolAttach>().attachedTool.name == "Grabber")
            grabberInteractor.gameObject.SetActive(true);
        else
            grabberInteractor.gameObject.SetActive(false);
    }
}
