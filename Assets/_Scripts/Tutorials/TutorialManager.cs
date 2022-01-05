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

    /*  Unity functions */
    private void Awake() {
        Initialize();
    }

    private void Start() {

        EventBus.Register(this);
    
        _debugText.text = "TutorialManager: at start";

    }

    private void Update() {
        if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch)) {

            _debugText.text = "TutorialManager: raising event, name of starting tutorial: " + _listOfTutorialModules[_currentTutorialModule].name;
            StartTutorialModule(_currentTutorialModule);
        }
    }

    /*  Other functions */
    void Initialize() {
        if(_listOfTutorialModules != null) {
            foreach (GameObject tutorial in _listOfTutorialModules) {
                _numberOfTutorialModules++;
            }
        }
    }

    private void StartTutorialModule(int tutorialModuleId) {
        string nameOfNextTutorialModule = _listOfTutorialModules[tutorialModuleId].name;
        _debugText.text = "TutorialManager: module finished event, starting next module, name: " + nameOfNextTutorialModule;
        EventBus<TutorialModuleStartedEvent>.Raise(new TutorialModuleStartedEvent()
            {
                nameOfModuleThatIsStarting = nameOfNextTutorialModule
            });
    }

    /*  Events  */
    public void OnEvent(TutorialModuleFinishedEvent e) {
        _currentTutorialModule++;

        if(_currentTutorialModule < _numberOfTutorialModules) {
            StartTutorialModule(_currentTutorialModule);
        } else {
            _debugText.text = "TutorialManager: All tutorials finished!";
        }
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }
}
