using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;

public class GameManager : MonoBehaviour, IEventReceiver<TutorialFinishedEvent>
{
    [SerializeField] private TMP_Text _debugText;
    
    /*  Unity methods   */
    private void Start()
    {
        RegisterEvents();
        StartCoroutine(StartTutorial());
    }

    /*  GameManager methods */
    IEnumerator StartTutorial() {
        yield return new WaitForSeconds(1);
        EventBus<StartTutorialEvent>.Raise(new StartTutorialEvent() {});
    }

    /*  Events  */
    public void OnEvent(TutorialFinishedEvent e) {
        _debugText.text = "GameManager: Tutorial finished";
    }

    public void RegisterEvents() {
        EventBus.Register(this);
    }

    public void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }
}
