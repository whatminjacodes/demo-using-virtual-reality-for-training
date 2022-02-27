using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using pEventBus;
using UnityEngine.SceneManagement;

public class MenuUIFunctions : MonoBehaviour, IEventReceiver<MainMenuEnterTouchedEvent>
{
    [SerializeField] private TMP_Text _monitorText;

    private void Start() {
        RegisterEvents();
    }

    public void OnEnterClicked() {
        if(_monitorText != null) {
            _monitorText.text = "Enter clicked";
        }
    }

    public void OnEvent(MainMenuEnterTouchedEvent e) {
        _monitorText.text = "On event, loading scene";

        this.gameObject.GetComponent<OVRScreenFade>().FadeIn();
        StartCoroutine(MyCoroutine());
        
    }

    IEnumerator MyCoroutine()
    {
        yield return new WaitForSeconds(2);
   
        SceneManager.LoadScene(Constants.TRAINING_SCENE_NAME, LoadSceneMode.Single);
    }

    private void RegisterEvents() {
        EventBus.Register(this);
    }

    private void UnRegisterEvents() {
        EventBus.UnRegister(this);
    }

    private void OnDestroy() {
        UnRegisterEvents();
    }

}
