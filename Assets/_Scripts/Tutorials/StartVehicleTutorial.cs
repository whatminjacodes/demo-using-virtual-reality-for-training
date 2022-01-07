using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class StartVehicleTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>, IEventReceiver<StartButtonGrabbedEvent>,
IEventReceiver<StartButtonLetGoEvent>
{
    [Header("Needed GameObjects")]
    [SerializeField] private GameObject _rightController;
    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _startLEDWhite;
    [SerializeField] private GameObject _startLEDRed;

    [Header("Needed UI elements")]
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private TMP_Text _debugText;
    [SerializeField] private TMP_Text _debugControllerText;

    // Event checks
    private bool tutorialStartedFromEvent = false;
    private bool startButtonGrabbed = false;

    // Start button rotation
    private Quaternion initialObjectRotation;
    private Quaternion initialControllerRotation;
    private bool initialRotationsSet = false;

    /*  Unity methods   */
    private void Start() {
        _debugText.text = "StartVehicleTutorial: registering events at start";
        RegisterEvents();
    }

    private void Update()
    {
        if(tutorialStartedFromEvent == true) {
            if(startButtonGrabbed) {
                if(initialRotationsSet == false)
                {
                    initialObjectRotation = _startButton.transform.localRotation;
                    initialControllerRotation = _rightController.transform.rotation;
                    initialRotationsSet = true;
                }

                Quaternion controllerAngularDifference = initialControllerRotation * Quaternion.Inverse(_rightController.transform.rotation);
                var rotationAmount = controllerAngularDifference * initialObjectRotation;
                 _startButton.transform.localRotation = Quaternion.Inverse(Quaternion.Euler(0, rotationAmount.eulerAngles.y, 0));
                
                // TODO: Check when rotated enough and then set lights on and finish tutorial then
                SetLEDLightsOn(true);
                FinishAndCloseTutorial();

            } else {
                initialRotationsSet = false;
            }
        }
    }

    private void OnDestroy() {
        UnRegisterEvents();
    }

    /*  Tutorial methods   */
    private void SetLEDLightsOn(bool vehicleStarted) {
        if(vehicleStarted) {
            _startLEDWhite.SetActive(true);
            _startLEDRed.SetActive(false);
        } else {
            _startLEDWhite.SetActive(false);
            _startLEDRed.SetActive(true);
        }
    }

    public void FinishAndCloseTutorial() {
        tutorialStartedFromEvent = false;
        startButtonGrabbed = false;

        _debugText.text = "StartVehicleTutorial: finishing tutorial";
        EventBus<TutorialModuleFinishedEvent>.Raise(new TutorialModuleFinishedEvent()
        {
            nameOfModuleThatWasFinished = this.gameObject.name
        });

        UnRegisterEvents();
        this.gameObject.SetActive(false);
    }

    /*  Events  */
    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    public void OnEvent(TutorialModuleStartedEvent e) {
        if(e.nameOfModuleThatIsStarting == this.gameObject.name) {
            _debugText.text = "StartVehicleTutorial: on event, tutorial starting";
            tutorialStartedFromEvent = true;
        }
    }

    public void OnEvent(StartButtonGrabbedEvent e) {
        _debugText.text = "StartVehicleTutorial: on event, start button grabbed";
        startButtonGrabbed = true; 
    }

    public void OnEvent(StartButtonLetGoEvent e) {
        _debugText.text = "StartVehicleTutorial: on event, start button let go";
        startButtonGrabbed = false; 
    }
}
