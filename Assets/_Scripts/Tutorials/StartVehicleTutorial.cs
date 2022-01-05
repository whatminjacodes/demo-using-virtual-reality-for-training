using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class StartVehicleTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    public ItemInitialization _startButtonData;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _startLEDWhite;
    [SerializeField] private GameObject _startLEDRed;

    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private TMP_Text _debugText;
    [SerializeField] private TMP_Text _debugControllerText;

    int _numOfClicks = 1;
    int _numberOfStepsBeforeFinished = 5;

    bool tutorialStartedFromEvent = false;

    private SphereCollider _startButtonCollider;

    private void Start() {
        RegisterEvents();
        _debugText.text = "StartVehicleTutorial: registering events at start";
    }

    private void Initialize() {
        /* if(_startButton != null) {
            // Position & rotation
            _startButton.transform.localPosition =  new Vector3(0,0,0);
            Quaternion defaultRotation = Quaternion.Euler(_startButtonData.defaultRotation.x, _startButtonData.defaultRotation.y, _startButtonData.defaultRotation.z);
           _startButton.transform.localRotation = defaultRotation;

            // Disable collider
           _startButtonCollider = _startButton.GetComponent<SphereCollider>();
        
            if (_startButtonCollider != null) {
                _startButtonCollider.enabled = false;
            }
            else {
                Debug.LogError("Start button collider missing!");
            }

            // Set LEDs
            _startLEDWhite.SetActive(true);
            _startLEDRed.SetActive(false);
        }*/
    }

    void Update()
    {
        if(tutorialStartedFromEvent == true) {

            if(_numOfClicks == _numberOfStepsBeforeFinished) {
                _tutorialText.text = "Tutorial module finished";

                FinishAndCloseTutorial();
            }
            else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
                _debugControllerText.text = "Right Trigger (Up)";
                _tutorialText.text = "Going to " + _numOfClicks + " part.";
                _numOfClicks++;
            }
        }
    }

    /*  Events  */
    public void OnEvent(TutorialModuleStartedEvent e) {
        _debugText.text = "StartVehicleTutorial: on event, e: " + e.nameOfModuleThatIsStarting;
        if(e.nameOfModuleThatIsStarting == this.gameObject.name) {
            _debugText.text = "StartVehicleTutorial: on event, tutorial starting";
            tutorialStartedFromEvent = true;
            Initialize();
        }
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    /*  Finishing tutorial */
    public void FinishAndCloseTutorial() {
        _debugText.text = "StartVehicleTutorial: finishing tutorial";
        EventBus<TutorialModuleFinishedEvent>.Raise(new TutorialModuleFinishedEvent()
        {
            nameOfModuleThatWasFinished = this.gameObject.name
        });

        UnRegisterEvents();
        this.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        UnRegisterEvents();
    }
}
