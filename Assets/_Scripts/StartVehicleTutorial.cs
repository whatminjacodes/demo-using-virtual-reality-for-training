using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVehicleTutorial : MonoBehaviour
{
    public ItemInitialization _startButtonData;

    [SerializeField] private Transform _startButton;
    
    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void Initialize() {
        if(_startButton != null) {
           _startButton.position =  _startButtonData.defaultPosition;
           
           Quaternion defaultRotation = Quaternion.Euler(_startButtonData.defaultRotation.x, _startButtonData.defaultRotation.y, _startButtonData.defaultRotation.z);
           _startButton.rotation = defaultRotation;
        }
    }
}
