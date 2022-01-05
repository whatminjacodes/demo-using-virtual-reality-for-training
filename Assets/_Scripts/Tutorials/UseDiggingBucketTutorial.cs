using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class UseDiggingBucketTutorial : MonoBehaviour, IEventReceiver<TutorialModuleStartedEvent>
{
    [SerializeField] private TMP_Text _tutorialText;
    public TMP_Text _debugText;

    private bool tutorialStartedFromEvent = false;

    private void Awake() {
        RegisterEvents();
        _debugText.text = "UseDiggingBucketTutorial: registering events at start";
    }

    private void Start() {
        
    }

    private void Update() {
        if(tutorialStartedFromEvent) {

        }
    }

    private void Initialize() {
        FinishAndCloseTutorial();
    }

    /*  Events  */
    public void OnEvent(TutorialModuleStartedEvent e) {
        if(e.nameOfModuleThatIsStarting == this.gameObject.name) {
            tutorialStartedFromEvent = true;
            Initialize();
            _debugText.text = "UseDiggingBucketTutorial: on event triggered, starting tutorial";
        }
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    /*  Finishing tutorial */
    public void FinishAndCloseTutorial() {
        _debugText.text = "UseDiggingBucketTutorial: tutorial finished";

        EventBus<TutorialModuleFinishedEvent>.Raise(new TutorialModuleFinishedEvent()
        {
            nameOfModuleThatWasFinished = this.gameObject.name
        });

        UnRegisterEvents();
        this.gameObject.SetActive(false);
    }

    private void OnDestroy() {
        UnRegisterEvents();
    }
}
