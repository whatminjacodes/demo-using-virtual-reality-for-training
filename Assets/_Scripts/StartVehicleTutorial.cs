using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVehicleTutorial : MonoBehaviour
{
    public ItemInitialization _startButtonData;

    [SerializeField] private GameObject _startButton;
    [SerializeField] private GameObject _startLEDWhite;
    [SerializeField] private GameObject _startLEDRed;

    private SphereCollider _startButtonCollider;

    /*  Events  */
    public delegate void StartVehicleTutorialFinishedDelegate();
    public static StartVehicleTutorialFinishedDelegate StartVehicleTutorialFinishedEvent;
    
    void Start()
    {
        Initialize();
    }


    void Update()
    {
        
    }

    /* 
     *  Initialize Tutorial objects
     */
    private void Initialize() {
        if(_startButton != null) {
            // Position & rotation
            _startButton.transform.localPosition =  new Vector3(0,0,0);
            Quaternion defaultRotation = Quaternion.Euler(_startButtonData.defaultRotation.x, _startButtonData.defaultRotation.y, _startButtonData.defaultRotation.z);
           _startButton.transform.localRotation = defaultRotation;

            // Disable collider
           _startButtonCollider = _startButton.GetComponent<SphereCollider>();
        
            if (_startButtonCollider != null) {
                _startButtonCollider.enabled = false;
            }
            else {
                Debug.LogError("Start button collider missing!");
            }

            // Set LEDs
            _startLEDWhite.SetActive(true);
            _startLEDRed.SetActive(false);
        }
    }

    private void OnStartTutorialStarted() {
        _startButtonCollider.enabled = true;
    }

    
}
