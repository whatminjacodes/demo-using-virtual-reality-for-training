using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using pEventBus;

/*
 *      Start Vehicle Tutorial Controller Triggers
 *      - recognized when controller is near the start button and correct button is pressed down
 *      - sends events when controller grabs or lets go of a start button
 */
public class VRControllerStartVehicleTutorialTriggers : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    private string currentTutorialModuleName = "";
    private bool startButtonGrabbedEventSent = false;

    /*  Unity methods   */
    private void Start() {
        RegisterEvents();
    }

    private void OnTriggerStay(Collider other) {
        if(currentTutorialModuleName == Constants.START_VEHICLE_TUTORIAL_NAME) {
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.START_BUTTON_NAME && !startButtonGrabbedEventSent) {

                    EventBus<StartButtonGrabbedEvent>.Raise(new StartButtonGrabbedEvent() {});
                    startButtonGrabbedEventSent = true;
                }
            } else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                if(other.gameObject.name == Constants.START_BUTTON_NAME && startButtonGrabbedEventSent) {

                    EventBus<StartButtonLetGoEvent>.Raise(new StartButtonLetGoEvent() {});
                    startButtonGrabbedEventSent = false;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(currentTutorialModuleName == Constants.START_VEHICLE_TUTORIAL_NAME) {
            if(other.gameObject.name == Constants.START_BUTTON_NAME && startButtonGrabbedEventSent) {

                EventBus<StartButtonLetGoEvent>.Raise(new StartButtonLetGoEvent() {});
                startButtonGrabbedEventSent = false;
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
        currentTutorialModuleName = e.nameOfModuleThatIsStarting;
    }
}
