using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class UseDiggingBucketTutorial : MonoBehaviour,  IEventReceiver<TutorialModuleStartedEvent>, 
                                                        IEventReceiver<RightLeverGrabbedEvent>,
                                                        IEventReceiver<RightLeverLetGoEvent>, 
                                                        IEventReceiver<TakaVipu2MovedToCorrectLocationEvent>
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
    private bool takavipu2OnCorrectSpot = false;

    Quaternion initialRightLeverRotation;

    // Start button rotation
    private Quaternion initialObjectRotation;
    private Quaternion initialControllerRotation;
    private bool initialRotationsSet = false;

     private Quaternion currentRot;
    private Vector3 startPos;
    private bool offsetSet;
 
    void SetOffsets()
    {
        if (offsetSet)
            return;
 
        startPos = Vector3.Normalize(_rightController.transform.position - _rightLever.transform.position);
        currentRot = _rightLever.transform.rotation;
 
        offsetSet = true;
    }
 
    void Rotate()
    {
        SetOffsets();
 
        Vector3 closestPoint = Vector3.Normalize(_rightController.transform.position - _rightLever.transform.position);
        var rotationAmountt = Quaternion.FromToRotation(startPos, closestPoint) * currentRot;
        _rightLever.transform.rotation = Quaternion.Euler(rotationAmountt.eulerAngles.x, 0, 0);
    }
 

    /*  Unity methods  */
    private void Start() {
        RegisterEvents();
        _debugText.text = "UseDiggingBucketTutorial: registering events at start";

        initialRightLeverRotation = _rightLever.transform.localRotation;
    }

    private void Update() {
        if(tutorialStartedFromEvent == true) {
            
           /* if(!takavipu2OnCorrectSpot) {

                _rightLever.transform.Rotate(-Vector3.right * 10 * Time.deltaTime);
            } */

            if(!takavipu2OnCorrectSpot) {
                if(rightLeverGrabbed) {
                    /*if(initialRotationsSet == false)
                    {
                        initialObjectRotation = _rightLever.transform.rotation;
                        initialControllerRotation = _rightController.transform.rotation;
                        initialRotationsSet = true;
                    }

                    Quaternion controllerAngularDifference = initialControllerRotation * Quaternion.Inverse(_rightController.transform.rotation);

                    var rotationAmount = controllerAngularDifference * initialObjectRotation;
                    _rightLever.transform.rotation = Quaternion.Inverse(Quaternion.Euler(rotationAmount.eulerAngles.x, 0, 0));

                    _tutorialText.text = "x: " + _rightLever.transform.rotation.x;

                } else {
                    initialRotationsSet = false;
                }*/

            // if (OVRInput.Get(grabButton) && IsCloseEnough())
                    Rotate();
                            
                }
                else{
                    offsetSet = false;
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

    public void OnEvent(TakaVipu2MovedToCorrectLocationEvent e) {
        _debugText.text = "DiggingBucketTutorial: on event, takavipu2 in correct spot";
        takavipu2OnCorrectSpot = true; 
    }
}
