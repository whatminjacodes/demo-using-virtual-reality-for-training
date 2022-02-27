using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonRotator : MonoBehaviour
{
    [SerializeField] private float minYRotation = 0f;
    [SerializeField] private float maxYRotation = -90f;
    
    private bool triggerClicked = false;
    private Transform rightHand;

    private Quaternion initialObjectRotation;
    private Quaternion initialControllerRotation;

    public Text _uiText;

    private bool set = false;

    private Collider collidedObject;

    private Quaternion currentRot;
    private Vector3 startPos;
    private bool offsetSet;

    void Update () {
        if (triggerClicked) {

            if(set == false)
                {
                    initialObjectRotation= transform.rotation;
                    initialControllerRotation = rightHand.rotation;
                    set = true;
                }

                Quaternion controllerAngularDifference = initialControllerRotation * Quaternion.Inverse(rightHand.rotation);
                transform.rotation = controllerAngularDifference * initialObjectRotation;
        }         
        else
        {
            set = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("OnTriggerEnter");
        _uiText.text = "Enter";
    }

    private void OnTriggerStay(Collider other) {
        Debug.Log("OnTriggerStay");
        _uiText.text = "Stay";
    }

    private void OnTriggerExit(Collider other) {
        Debug.Log("OnTriggerEnter");
        collidedObject = null;
        offsetSet = false;
        _uiText.text = "Exit";
    }

    void SetOffsets()
    {
        if (offsetSet)
            return;
 
        if(collidedObject != null) {
            startPos = Vector3.Normalize(collidedObject.gameObject.transform.position - this.transform.position);
            currentRot = this.transform.rotation;
    
            offsetSet = true;
        }
    }

    void Rotate()
    {
        SetOffsets();
 
        Vector3 closestPoint = Vector3.Normalize(collidedObject.gameObject.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.FromToRotation(startPos, closestPoint) * currentRot;
    }
}
