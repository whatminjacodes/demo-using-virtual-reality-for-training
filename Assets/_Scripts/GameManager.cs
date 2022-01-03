using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public ExcavatorScriptableObject excavatorDefaultValues;
    
    [SerializeField] private GameObject _excavator;
    [SerializeField] private Transform[] _movingParts;
    
    void Start()
    {
       // if(_rightLever != null) {
            Quaternion rotationAmount = Quaternion.Euler(excavatorDefaultValues.rightLeverMaxRotation.x, excavatorDefaultValues.rightLeverMaxRotation.y, excavatorDefaultValues.rightLeverMaxRotation.z);
           // _rightLever.rotation = rotationAmount;
       // }
    }

    void Update()
    {
        
    }

    private void Initialize() {
        
    }
}
