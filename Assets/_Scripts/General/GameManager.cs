using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;
using TMPro;
using UnityEngine.SceneManagement;

/*
 *      Game Manager
 *      - starts the chosen tutorial
 *      - gets notified when all tutorial modules have been finished
 *      - goes back to menu scene when the tutorial is finished
 */
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

    IEnumerator FinishTutorial() {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(Constants.MENU_SCENE_NAME, LoadSceneMode.Single);
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
