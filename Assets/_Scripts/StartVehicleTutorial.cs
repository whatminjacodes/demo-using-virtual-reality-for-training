using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StartVehicleTutorial : MonoBehaviour
{
    public ItemInitialization _startButtonData;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _startLEDWhite;
    [SerializeField] private GameObject _startLEDRed;

    [SerializeField] private TMP_Text _tutorialText;
    public TMP_Text _debugText;

    int _numOfClicks = 1;
    int _numberOfStepsBeforeFinished = 5;

    private SphereCollider _startButtonCollider;

    /*  Events  */
    public delegate void StartVehicleTutorialFinishedDelegate();
    public static StartVehicleTutorialFinishedDelegate StartVehicleTutorialFinishedEvent;
    
    private void Awake() {
        TutorialManager.TutorialModuleStartedEvent += OnTutorialModuleStarted;    
    }

    void Update()
    {
        if(_numOfClicks == _numberOfStepsBeforeFinished) {
            _tutorialText.text = "Tutorialmodule finished";
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch)) {
           _debugText.text = "Right Trigger (Up)";
           _tutorialText.text = "Going to " + _numOfClicks + " part.";

           _numOfClicks++;
        }
    }

    /* 
     *  Initialize Tutorial objects
     */
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

    private void OnTutorialModuleStarted() {
        _debugText.text = "StartVehicleTutorial: OnTutorialModuleStarted";

        Initialize();

       // _startButtonCollider.enabled = true;
    }
}
