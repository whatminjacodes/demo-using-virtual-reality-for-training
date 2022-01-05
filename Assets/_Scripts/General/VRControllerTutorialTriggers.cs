using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using pEventBus;

public class VRControllerTutorialTriggers : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    public TMP_Text debugText;
    public TMP_Text debugText2;

    [SerializeField] private GameObject _enterButtonGameObject;

    bool startButtonEventSent = false;

    string currentTutorialModuleName = "";

    private void Start() {
        RegisterEvents();
    }

    public void OnEvent(TutorialModuleStartedEvent e) {
        currentTutorialModuleName = e.nameOfModuleThatIsStarting;
    }

    private void OnTriggerStay(Collider other) {
        if(currentTutorialModuleName == Constants.START_VEHICLE_TUTORIAL_NAME) {
            debugText.text = "ontriggerstay: " + other.gameObject.name;

            debugText2.text = "xrot: " + this.gameObject.transform.rotation.x + "yrot: " + this.gameObject.transform.rotation.y + "zrot: " + this.gameObject.transform.rotation.z;
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
            {
                debugText.text = "Right Trigger (Down), on trigger stay event";
                
                if(other.gameObject.name == Constants.START_BUTTON_NAME && !startButtonEventSent) {
                    debugText.text = "Triggers: start button event sent";

                    EventBus<StartButtonGrabbedEvent>.Raise(new StartButtonGrabbedEvent()
                    {
                    });
                    startButtonEventSent = true;
                }
            }
        }
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    private void OnTriggerExit(Collider other) {
        debugText.text = "ontriggerexit: " + other.gameObject.name;

        //startButtonEventSent = false;
        /*if(other.gameObject.name == _enterButtonGameObject.name) {
            debugText.text = "ontriggerenter: " + other.gameObject.name;

            EventBus<MainMenuEnterTouchedEvent>.Raise(new MainMenuEnterTouchedEvent()
            {
            });
        }*/
    }
}
