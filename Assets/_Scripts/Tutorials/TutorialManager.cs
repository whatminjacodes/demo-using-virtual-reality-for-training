using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

public class TutorialManager : MonoBehaviour, IEventReceiver<TutorialModuleFinishedEvent>
{
    [SerializeField] private GameObject[] _listOfTutorialModules;

    private int _numberOfTutorialModules = 0;
    private int _currentTutorialModule = 0;

    [SerializeField] private TMP_Text _debugText;

    /*  Events  */
   // public delegate void TutorialModuleStartedDelegate();
   // public static event TutorialModuleStartedDelegate TutorialModuleStartedEvent;

    private void Awake() {
        Initialize();
    }

    private void Start() {

        EventBus.Register(this);
    
        Debug.Log("TutorialManager: Start");

        if(_listOfTutorialModules != null) {
            _listOfTutorialModules[_currentTutorialModule].SetActive(true);
            _debugText.text = "Tutorial manager at start";
            //if(TutorialModuleStartedEvent != null) {
               // TutorialModuleStartedEvent();
                Debug.Log("TutorialManager: name of starting tutorial: " + _listOfTutorialModules[_currentTutorialModule].name);
            //}

            


            //_currentTutorialModule++;
            // TODO: Somehow listen to tutorial finished event
        }
    }

    private void Update() {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch)) {

            _debugText.text = "Tutorial manager raising start event";
           // _debugControllerText.text = "A button (UP)";
           // _tutorialText.text = "Going to " + _numOfClicks + " part.";
            //_debugText.text = "startvehicle tutorial ongoing";

                EventBus<TutorialModuleStartedEvent>.Raise(new TutorialModuleStartedEvent()
                {
                    nameOfModuleThatIsStarting = "a"
                });
            }
    }


    void Initialize() {
        if(_listOfTutorialModules != null) {
            foreach (GameObject tutorial in _listOfTutorialModules) {
                //tutorial.SetActive(false);
                _numberOfTutorialModules++;
            }
        }
    }

    public void OnEvent(TutorialModuleFinishedEvent e)
    {
        Debug.Log("TutorialManager: Module finished");
       // _listOfTutorialModules[_currentTutorialModule].SetActive(false);
        _debugText.text = "Tutorial manager starting a new tutorial at finished event";
        _currentTutorialModule++;

        //if(_listOfTutorialModules)
        _listOfTutorialModules[_currentTutorialModule].SetActive(true);

        EventBus<TutorialModuleStartedEvent>.Raise(new TutorialModuleStartedEvent()
            {
                nameOfModuleThatIsStarting = "a"
            });
    }
}
