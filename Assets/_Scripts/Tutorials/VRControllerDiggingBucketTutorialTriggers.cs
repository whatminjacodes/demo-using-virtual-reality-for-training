using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

/*
 *      Digging Bucket Tutorial Controller Triggers
 *      - recognized when controller is near levers and correct button is pressed down
 *      - sends events when controller grabs or lets go of a lever
 */
public class VRControllerDiggingBucketTutorialTriggers : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    // General
    private string currentTutorialModuleName = "";

    // Events
    private bool rightLeverGrabbedEventSent = false;

    // Debug
    public TMP_Text controllerDebugText;

    /*  Unity methods   */
    private void Start() {
        RegisterEvents();
    }

    /*  Triggers    */
    private void OnTriggerStay(Collider other) {
        if(currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {

            // Right lever trigger pressed down
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && !rightLeverGrabbedEventSent) {
                    SendRightLeverGrabbedEvent();
                }
            // Right lever trigger let go
            } else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && rightLeverGrabbedEventSent) {
                    SendRightLeverLetGoEvent();
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {
            if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && rightLeverGrabbedEventSent) {
                SendRightLeverLetGoEvent();
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
        controllerDebugText.text = "VRControllerDiggingBucketTutorialTriggers: Digging bucket tutorial started";
        currentTutorialModuleName = e.nameOfModuleThatIsStarting;
    }

    private void SendRightLeverGrabbedEvent() {
        EventBus<RightLeverGrabbedEvent>.Raise(new RightLeverGrabbedEvent() {});
        rightLeverGrabbedEventSent = true;
    }

    private void SendRightLeverLetGoEvent() {
        EventBus<RightLeverLetGoEvent>.Raise(new RightLeverLetGoEvent() {});
        rightLeverGrabbedEventSent = false;
    }
}
