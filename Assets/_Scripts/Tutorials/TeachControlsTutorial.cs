using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

public class TeachControlsTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>,
                                                    IEventReceiver<AButtonPressedEvent>,
                                                    IEventReceiver<BButtonPressedEvent>,
                                                    IEventReceiver<TriggerPressedEvent>
{
    private bool _controllerInstructionsFinished = false;

    [SerializeField] private TMP_Text _monitorText;
    [SerializeField] private TMP_Text _debugText;

    private void Start()
    {
        RegisterEvents();
    }

    IEnumerator Initialize() {
        yield return new WaitForSeconds(1);

        if(_monitorText != null) {
             _monitorText.text = ControllerInstructionDialogue.HELLO_DIALOGUE;
        }
    }

    public void FinishAndCloseTutorial() {
        _debugText.text = "TeachControlsTutorial: finishing tutorial";
        EventBus<TutorialModuleFinishedEvent>.Raise(new TutorialModuleFinishedEvent()
        {
            nameOfModuleThatWasFinished = this.gameObject.name
        });

        UnRegisterEvents();
        this.gameObject.SetActive(false);
    }

    public void OnEvent(TutorialModuleStartedEvent e) {
        if(e.nameOfModuleThatIsStarting == this.gameObject.name) {
            _debugText.text = "TeachControlsTutorial: on event, tutorial starting";
            StartCoroutine(Initialize());
        }
    }

    public void OnEvent(AButtonPressedEvent e) {
        // TODO: shoud check if the correct button is pressed
        _debugText.text = "TeachControlsTutorial: A button pressed event";
        if(_controllerInstructionsFinished) {
            FinishAndCloseTutorial();
            
        } else {
            _monitorText.text = ControllerInstructionDialogue.B_BUTTON_DIALOGUE;
        }
    }

    public void OnEvent(BButtonPressedEvent e) {
        _debugText.text = "TeachControlsTutorial: B button pressed event";
        _monitorText.text = ControllerInstructionDialogue.TRIGGER_DIALOGUE;
    }

    public void OnEvent(TriggerPressedEvent e) {
        _debugText.text = "TeachControlsTutorial: trigger pressed event";
        _monitorText.text = ControllerInstructionDialogue.FINISHED_DIALOGUE;
        _controllerInstructionsFinished = true;
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }
}
