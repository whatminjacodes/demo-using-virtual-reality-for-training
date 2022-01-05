using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    }

    void Update()
    {
        
    }

    private void Initialize() {
        
    }

   // private void 
}
