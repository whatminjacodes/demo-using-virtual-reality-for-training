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
    private string _currentTutorialModuleName = "";

    // Events
    private bool _rightLeverGrabbedEventSent = false;
    private bool _leftLeverGrabbedEventSent = false;

    // Debug
    public TMP_Text _controllerDebugText;

    /*  Unity methods   */
    private void Start() {
        RegisterEvents();
    }

    /*  Triggers    */
    private void OnTriggerStay(Collider other) {
        if(_currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {

            // Right controller trigger pressed down
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && !_rightLeverGrabbedEventSent) {
                    SendRightLeverGrabbedEvent();
                }
            // Right controller trigger let go
            } else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && _rightLeverGrabbedEventSent) {
                    SendRightLeverLetGoEvent();
                }
            }

            // Left controller trigger pressed down
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                if(other.gameObject.name == Constants.DIGGING_LEVER_LEFT && !_leftLeverGrabbedEventSent) {
                    _controllerDebugText.text = "Left: sending event";
                    SendLeftLeverGrabbedEvent();
                }
            // Left controller trigger let go
            } else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch)) {
                if(other.gameObject.name == Constants.DIGGING_LEVER_LEFT && _leftLeverGrabbedEventSent) {
                    SendLeftLeverLetGoEvent();
                }
            }
        }
    }

    // Controller moved away from the lever
    private void OnTriggerExit(Collider other) {
        if(_currentTutorialModuleName == Constants.DIGGING_BUCKET_TUTORIAL_NAME) {
            if(other.gameObject.name == Constants.DIGGING_LEVER_RIGHT && _rightLeverGrabbedEventSent) {
                SendRightLeverLetGoEvent();
            }
            if(other.gameObject.name == Constants.DIGGING_LEVER_LEFT && _leftLeverGrabbedEventSent) {
                SendLeftLeverLetGoEvent();
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
        _controllerDebugText.text = "VRControllerDiggingBucketTutorialTriggers: Digging bucket tutorial started";
        _currentTutorialModuleName = e.nameOfModuleThatIsStarting;
    }

    private void SendRightLeverGrabbedEvent() {
        EventBus<RightLeverGrabbedEvent>.Raise(new RightLeverGrabbedEvent() {});
        _rightLeverGrabbedEventSent = true;
    }

    private void SendRightLeverLetGoEvent() {
        EventBus<RightLeverLetGoEvent>.Raise(new RightLeverLetGoEvent() {});
        _rightLeverGrabbedEventSent = false;
    }

    private void SendLeftLeverGrabbedEvent() {
        EventBus<LeftLeverGrabbedEvent>.Raise(new LeftLeverGrabbedEvent() {});
        _leftLeverGrabbedEventSent = true;
    }

    private void SendLeftLeverLetGoEvent() {
        EventBus<LeftLeverLetGoEvent>.Raise(new LeftLeverLetGoEvent() {});
        _leftLeverGrabbedEventSent = false;
    }
}
