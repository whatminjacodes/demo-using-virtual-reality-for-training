using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

/*
 *      Start Vehicle Tutorial Module
 *      - recognizes when tutorial module has been started
 *      - user rotates the start button, LED lights change color to indicate the excavator has been powered
 *      - it currently doesn't check if the user has rotated the button enough, wasn't really needed in this demo
 *      - when the user lets go of the start button, the tutorial module ends
 */
public class StartVehicleTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>, 
                                                   IEventReceiver<StartButtonGrabbedEvent>,
                                                   IEventReceiver<StartButtonLetGoEvent>,
                                                   IEventReceiver<AButtonPressedEvent>
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
    private bool _tutorialStartedFromEvent = false;
    private bool _startButtonGrabbed = false;
    private bool _excavatorHasPower = false;

    // Start button rotation
    private Quaternion _initialObjectRotation;
    private Quaternion _initialControllerRotation;
    private bool _initialRotationsSet = false;

    /*  Unity methods   */
    private void Start() {
        _debugText.text = "StartVehicleTutorial: registering events at start";
        RegisterEvents();
    }

    private void Update()
    {
        if(_tutorialStartedFromEvent == true) {
            if(_startButtonGrabbed) {
                if(_initialRotationsSet == false)
                {
                    _initialObjectRotation = _startButton.transform.localRotation;
                    _initialControllerRotation = _rightController.transform.rotation;
                    _initialRotationsSet = true;
                }

                Quaternion controllerAngularDifference = _initialControllerRotation * Quaternion.Inverse(_rightController.transform.rotation);
                var rotationAmount = controllerAngularDifference * _initialObjectRotation;
                 _startButton.transform.localRotation = Quaternion.Inverse(Quaternion.Euler(0, rotationAmount.eulerAngles.y, 0));

            } else {
                _initialRotationsSet = false;
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
        _tutorialStartedFromEvent = false;
        _startButtonGrabbed = false;

        _tutorialText.text = "";

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
            _tutorialText.text = StartVehicleDialogue.START_VEHICLE_BEGIN_DIALOGUE;
            _tutorialStartedFromEvent = true;
        }
    }

    public void OnEvent(StartButtonGrabbedEvent e) {
        _debugText.text = "StartVehicleTutorial: on event, start button grabbed";
        _startButtonGrabbed = true; 
    }

    public void OnEvent(StartButtonLetGoEvent e) {
        _debugText.text = "StartVehicleTutorial: on event, start button let go";
        _startButtonGrabbed = false; 
        _excavatorHasPower = true;
        SetLEDLightsOn(true);

        _tutorialText.text = StartVehicleDialogue.START_VEHICLE_FINISHED_DIALOGUE;
    }

    public void OnEvent(AButtonPressedEvent e) {
        if(_excavatorHasPower) {
            FinishAndCloseTutorial();
        }
    }
}
