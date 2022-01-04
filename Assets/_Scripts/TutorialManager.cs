using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _listOfTutorialModules;

    private int _numberOfTutorialModules = 0;
    private int _currentTutorialModule = 0;

    /*  Events  */
    public delegate void TutorialModuleStartedDelegate();
    public static event TutorialModuleStartedDelegate TutorialModuleStartedEvent;

    private void Awake() {
        Initialize();
    }

    private void Start() {
        Debug.Log("TutorialManager: Start");

        if(_listOfTutorialModules != null) {
            _listOfTutorialModules[_currentTutorialModule].SetActive(true);

            if(TutorialModuleStartedEvent != null) {
                TutorialModuleStartedEvent();
                Debug.Log("TutorialManager: TutorialModuleStartedEvent triggered");
            }

            // TODO: Somehow listen to tutorial finished event
        }
    }


    void Initialize() {
        if(_listOfTutorialModules != null) {
            foreach (GameObject tutorial in _listOfTutorialModules) {
                tutorial.SetActive(false);
                _numberOfTutorialModules++;
            }
        }
    }
}
