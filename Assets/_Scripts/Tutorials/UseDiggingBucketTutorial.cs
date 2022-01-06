using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class UseDiggingBucketTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>, IEventReceiver<RightLeverGrabbedEvent>,
IEventReceiver<RightLeverLetGoEvent>
{

    [Header("Needed GameObjects")]
    [SerializeField] private GameObject _rightController;
    [SerializeField] private GameObject _rightLever;
    [SerializeField] private GameObject _leftLever;

    [SerializeField] private GameObject _part1Sphere;
    [SerializeField] private GameObject _part2Sphere;
    [SerializeField] private Transform _part1Location;
    [SerializeField] private Transform _part2Location;

    [Header("Needed UI elements")]
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private TMP_Text _debugText;
    
    // Event checks
    private bool tutorialStartedFromEvent = false;
    private bool rightLeverGrabbed = false;

    /*  Unity methods  */
    private void Start() {
        RegisterEvents();
        _debugText.text = "UseDiggingBucketTutorial: registering events at start";
    }

    private void Update() {
        if(tutorialStartedFromEvent == true) {
            if(rightLeverGrabbed) {
               /* if(initialRotationsSet == false)
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
            }*/
            }
        }
    }

    private void OnDestroy() {
        UnRegisterEvents();
    }

    /*  Tutorial methods   */
    private void Initialize() {
        _part1Sphere.SetActive(true);
        _part2Sphere.SetActive(false);
    }

    private void FinishAndCloseTutorial() {
        _debugText.text = "UseDiggingBucketTutorial: tutorial finished";

        EventBus<TutorialModuleFinishedEvent>.Raise(new TutorialModuleFinishedEvent()
        {
            nameOfModuleThatWasFinished = this.gameObject.name
        });

        UnRegisterEvents();
        this.gameObject.SetActive(false);
    }

    /*  Events  */
    private void RegisterEvents() {
        EventBus.Register(this);
    }

    private void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    public void OnEvent(TutorialModuleStartedEvent e) {
        if(e.nameOfModuleThatIsStarting == this.gameObject.name) {
            _debugText.text = "DiggingBucketTutorial: on event, tutorial starting";
            tutorialStartedFromEvent = true;
        }
    }

    public void OnEvent(RightLeverGrabbedEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, right lever grabbed";
        rightLeverGrabbed = true; 
    }

    public void OnEvent(RightLeverLetGoEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, right lever let go";
        rightLeverGrabbed = false; 
    }
}
