using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public class GameManager : MonoBehaviour
{
    public ExcavatorScriptableObject excavatorDefaultValues;
    
    [SerializeField] private GameObject _excavator;
    [SerializeField] private Transform[] _movingParts;

    /*  Events  */
    public delegate void TutorialStartedDelegate();
    public static event TutorialStartedDelegate TutorialStartedEvent;
    
    void Start()
    {
        StartCoroutine(Initialize());
    }

    void Update()
    {
        
    }

    IEnumerator Initialize() {
        yield return new WaitForSeconds(5);

        StartTutorial();
    }

   private void StartTutorial() {
       EventBus<StartTutorialEvent>.Raise(new StartTutorialEvent() {});
   }
}
