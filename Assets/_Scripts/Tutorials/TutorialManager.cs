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
    private void Start() {
        StartCoroutine(Initialize());
        

    }
    
    /*  Other functions */
    IEnumerator Initialize() {
        yield return new WaitForSeconds(2);

        EventBus.Register(this);
    
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
