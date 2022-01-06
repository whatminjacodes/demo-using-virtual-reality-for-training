using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

public class VRControllerDiggingBucketTutorialTriggers : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    private string currentTutorialModuleName = "";
    private bool rightLeverGrabbedEventSent = false;

    public TMP_Text controllerDebugText;

    /*  Unity methods   */
    private void Start() {
        RegisterEvents();
    }

    private void OnTriggerStay(Collider other) {
        if(currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {
            controllerDebugText.text = "Digging bucket tutorial started";
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && !rightLeverGrabbedEventSent) {
                    controllerDebugText.text = "Right lever grabbed ontriggerstay";
                    EventBus<RightLeverGrabbedEvent>.Raise(new RightLeverGrabbedEvent() {});
                    rightLeverGrabbedEventSent = true;
                }
            } else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && rightLeverGrabbedEventSent) {
                    controllerDebugText.text = "Right lever let go ontriggerstay";
                    EventBus<RightLeverLetGoEvent>.Raise(new RightLeverLetGoEvent() {});
                    rightLeverGrabbedEventSent = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {
            if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && rightLeverGrabbedEventSent) {
                controllerDebugText.text = "Right lever ontriggerexit";
                EventBus<RightLeverLetGoEvent>.Raise(new RightLeverLetGoEvent() {});
                rightLeverGrabbedEventSent = false;
            }
        }
    }

    /*  Events  */
    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    public void OnEvent(TutorialModuleStartedEvent e) {
        controllerDebugText.text = "tutorialmodulestarted event on controller";
        currentTutorialModuleName = e.nameOfModuleThatIsStarting;
    }
}
