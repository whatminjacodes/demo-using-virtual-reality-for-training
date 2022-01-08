using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

public class GameManager : MonoBehaviour, IEventReceiver<AButtonPressedEvent>,
                                          IEventReceiver<BButtonPressedEvent>,
                                          IEventReceiver<TriggerPressedEvent>
{
    [SerializeField] private string[] _controllerInstructionDialogue;
    private int _nextControllerDialogue = 0;
    private bool _controllerInstructionsFinished = false;

    [SerializeField] private TMP_Text _monitorText;
    [SerializeField] private TMP_Text _debugText;
    
    void Start()
    {
        RegisterEvents();
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize() {
        yield return new WaitForSeconds(1);

        if(_monitorText != null) {
             _monitorText.text = _controllerInstructionDialogue[_nextControllerDialogue];
             _nextControllerDialogue++;
        }
    }

    private void StartTutorial() {
       EventBus<StartTutorialEvent>.Raise(new StartTutorialEvent() {});
    }

    public void OnEvent(AButtonPressedEvent e) {
        _debugText.text = "GameManager: A button pressed event";
        if(_controllerInstructionsFinished) {
            StartTutorial();
            
        } else {
            _monitorText.text = _controllerInstructionDialogue[_nextControllerDialogue];
            _nextControllerDialogue++;
        }
        
    }

    public void OnEvent(BButtonPressedEvent e) {
        _debugText.text = "GameManager: B button pressed event";
        _monitorText.text = _controllerInstructionDialogue[_nextControllerDialogue];
        _nextControllerDialogue++;
    }

    public void OnEvent(TriggerPressedEvent e) {
        _debugText.text = "GameManager: trigger pressed event";
        _monitorText.text = _controllerInstructionDialogue[_nextControllerDialogue];
        _nextControllerDialogue++;
        _controllerInstructionsFinished = true;
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }
}
