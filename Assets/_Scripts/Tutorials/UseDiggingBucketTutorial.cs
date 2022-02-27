using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

/*
 *      Use Digging Bucket Tutorial Module
 *      - recognizes when tutorial module has been started
 *      - asks user to rotate the right lever so it touches the sphere visible in the training scene
 *      - then asks user to rotate the left lever so it touches another sphere
 */
public class UseDiggingBucketTutorial : MonoBehaviour,  IEventReceiver<TutorialModuleStartedEvent>, 
                                                        IEventReceiver<RightLeverGrabbedEvent>,
                                                        IEventReceiver<RightLeverLetGoEvent>,  
                                                        IEventReceiver<LeftLeverGrabbedEvent>,
                                                        IEventReceiver<LeftLeverLetGoEvent>, 
                                                        IEventReceiver<ExcavatorArmFrontMovedToCorrectLocationEvent>,
                                                        IEventReceiver<ExcavatorArmBackMovedToCorrectLocationEvent>
{

    [Header("Needed GameObjects")]
    [SerializeField] private GameObject _rightController;
    [SerializeField] private GameObject _leftController;

    [SerializeField] private GameObject _rightLever;
    [SerializeField] private GameObject _leftLever;

    [SerializeField] private GameObject _part1Sphere;
    [SerializeField] private GameObject _part2Sphere;

    [Header("Needed UI elements")]
    [SerializeField] private TMP_Text _tutorialText;
    [SerializeField] private TMP_Text _debugText;
    
    // Event checks
    private bool _tutorialStartedFromEvent = false;
    private bool _rightLeverGrabbed = false;
    private bool _leftLeverGrabbed = false;
    private bool _firstPartFinished = false;
    private bool _secondPartFinished = false;

    // Lever rotation
    private Quaternion _initialRightLeverRotation;
    private Quaternion _initialLeftLeverRotation;
    private Vector3 _startPos;
    private Vector3 _startPosLeft;
    private bool _offsetSet;
    private bool _offsetSetLeft;
 
    /*  Unity methods  */
    private void Start() {
        RegisterEvents();
        Initialize();
        _debugText.text = "UseDiggingBucketTutorial: registering events at start";
    }

    private void Update() {
        if(_tutorialStartedFromEvent == true) {
            if(!_firstPartFinished) {
                _tutorialText.text = DiggingBucketDialogue.DIGGING_BUCKET_BEGIN_DIALOGUE;
                if(_rightLeverGrabbed) {
                    RotateRightLever();
                            
                } else {
                    _offsetSet = false;
                }
            } else {
                _debugText.text = "UseDiggingBucketTutorial: first part finished";
                if(!_secondPartFinished) {
                    _tutorialText.text = DiggingBucketDialogue.DIGGIN_BUCKET_ANOTHER_LEVER_DIALOGUE;
                    if(_leftLeverGrabbed) {
                        RotateLeftLever();
                                
                    } else {
                        _offsetSetLeft = false;
                    }
                } else {
                    FinishAndCloseTutorial();
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

    private void SetOffsetsRight()
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
        SetOffsetsRight();
 
        Vector3 closestPoint = Vector3.Normalize(_rightController.transform.position - _rightLever.transform.position);
        Quaternion rotationAmount = Quaternion.FromToRotation(_startPos, closestPoint) * _initialRightLeverRotation;
        _rightLever.transform.rotation = Quaternion.Euler(rotationAmount.eulerAngles.x, 0, 0);
    }

    private void SetOffsetsLeft()
    {
        if (_offsetSetLeft) {
            return;
        }
 
        _startPosLeft = Vector3.Normalize(_leftController.transform.position - _leftLever.transform.position);
        _initialLeftLeverRotation = _leftLever.transform.rotation;
 
        _offsetSetLeft = true;
    }
 
    private void RotateLeftLever()
    {
        _debugText.text = "UseDiggingBucketTutorial: rotating left lever";
        SetOffsetsLeft();
 
        Vector3 closestPoint = Vector3.Normalize(_leftController.transform.position - _leftLever.transform.position);
        Quaternion rotationAmount = Quaternion.FromToRotation(_startPosLeft, closestPoint) * _initialLeftLeverRotation;
        _leftLever.transform.rotation = Quaternion.Euler(rotationAmount.eulerAngles.x, 0, 0);
    }

    private void FinishAndCloseTutorial() {
        _debugText.text = "UseDiggingBucketTutorial: tutorial finished";
        _tutorialText.text = DiggingBucketDialogue.DIGGIN_BUCKET_FINISHED_DIALOGUE;

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

    public void OnEvent(LeftLeverGrabbedEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, left lever grabbed";
        _leftLeverGrabbed = true; 
    }

    public void OnEvent(LeftLeverLetGoEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, left lever let go";
        _leftLeverGrabbed = false; 
    }

    public void OnEvent(ExcavatorArmFrontMovedToCorrectLocationEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, excavator arm front in correct spot";
        _part1Sphere.SetActive(false);
        _part2Sphere.SetActive(true);
        _firstPartFinished = true; 
    }

    public void OnEvent(ExcavatorArmBackMovedToCorrectLocationEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, excavator arm back in correct spot";
        _secondPartFinished = true; 
    }
}
