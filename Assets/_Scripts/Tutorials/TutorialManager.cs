using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

/*
 *      Tutorial Manager
 *      - starts new tutorial modules depending on the order they have been set in the inspector
 *      - informs GameManager when all modules have been finished
 */
public class TutorialManager : MonoBehaviour, IEventReceiver<StartTutorialEvent>, 
                                              IEventReceiver<TutorialModuleFinishedEvent>
{
    [SerializeField] private GameObject[] _listOfTutorialModules;

    private int _numberOfTutorialModules = 0;
    private int _currentTutorialModule = 0;

    [SerializeField] private TMP_Text _debugText;

    /*  Unity functions */
    private void Start() {
        EventBus.Register(this);
    }
    
    /*  Other functions */
    IEnumerator Initialize() {
        yield return new WaitForSeconds(1);
    
        _debugText.text = "TutorialManager: initializing";
        if(_listOfTutorialModules != null) {
            foreach (GameObject tutorial in _listOfTutorialModules) {
                _numberOfTutorialModules++;
            }
        }
        _debugText.text = "TutorialManager: raising event, name of starting tutorial: " + _listOfTutorialModules[_currentTutorialModule].name;
        StartTutorialModule(_currentTutorialModule);
    }

    private void StartTutorialModule(int tutorialModuleId) {
        string nameOfNextTutorialModule = _listOfTutorialModules[tutorialModuleId].name;

        _debugText.text = "TutorialManager: start tutorial function, starting next module, name: " + nameOfNextTutorialModule;
        EventBus<TutorialModuleStartedEvent>.Raise(new TutorialModuleStartedEvent()
        {
            nameOfModuleThatIsStarting = nameOfNextTutorialModule
        });
    }

    private void FinishTutorial() {
        EventBus<TutorialFinishedEvent>.Raise(new TutorialFinishedEvent() {});
    }

    /*  Events  */
    public void OnEvent(StartTutorialEvent e) {
        StartCoroutine(Initialize());
    }

    public void OnEvent(TutorialModuleFinishedEvent e) {
        _currentTutorialModule++;

        if(_currentTutorialModule < _numberOfTutorialModules) {
            StartTutorialModule(_currentTutorialModule);
        } else {
            _debugText.text = "TutorialManager: All tutorials finished!";
            FinishTutorial();
        }
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }
}
