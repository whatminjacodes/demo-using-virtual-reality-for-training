using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class UseDiggingBucketTutorial : MonoBehaviour,  IEventReceiver<TutorialModuleStartedEvent>, 
                                                        IEventReceiver<RightLeverGrabbedEvent>,
                                                        IEventReceiver<RightLeverLetGoEvent>, 
                                                        IEventReceiver<ExcavatorArmFrontMovedToCorrectLocationEvent>
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
    private bool _tutorialStartedFromEvent = false;
    private bool _rightLeverGrabbed = false;
    private bool _firstPartFinished = false;

    // Start button rotation
    private Quaternion _initialRightLeverRotation;
    private Vector3 _startPos;
    private bool _offsetSet;
 
    /*  Unity methods  */
    private void Start() {
        RegisterEvents();
        _debugText.text = "UseDiggingBucketTutorial: registering events at start";
    }

    private void Update() {
        if(_tutorialStartedFromEvent == true) {
            if(!_firstPartFinished) {
                if(_rightLeverGrabbed) {
                    RotateRightLever();
                            
                } else {
                    _offsetSet = false;
                }
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

    private void SetOffsets()
    {
        if (_offsetSet) {
            return;
        }
 
        _startPos = Vector3.Normalize(_rightController.transform.position - _rightLever.transform.position);
        _initialRightLeverRotation = _rightLever.transform.rotation;
 
        _offsetSet = true;
    }
 
    private void RotateRightLever()
    {
        SetOffsets();
 
        Vector3 closestPoint = Vector3.Normalize(_rightController.transform.position - _rightLever.transform.position);
        Quaternion rotationAmount = Quaternion.FromToRotation(_startPos, closestPoint) * _initialRightLeverRotation;
        _rightLever.transform.rotation = Quaternion.Euler(rotationAmount.eulerAngles.x, 0, 0);
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
            _tutorialStartedFromEvent = true;
        }
    }

    public void OnEvent(RightLeverGrabbedEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, right lever grabbed";
        _rightLeverGrabbed = true; 
    }

    public void OnEvent(RightLeverLetGoEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, right lever let go";
        _rightLeverGrabbed = false; 
    }

    public void OnEvent(ExcavatorArmFrontMovedToCorrectLocationEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, takavipu2 in correct spot";
        _firstPartFinished = true; 
    }
}
