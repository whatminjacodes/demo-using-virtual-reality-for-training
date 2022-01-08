using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using pEventBus;

public class InputManager : MonoBehaviour
{
   public TMP_Text debugText;
   
    void Update()
    {
        //--- Right Controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
           debugText.text = "A Button (Down)";
           EventBus<AButtonPressedEvent>.Raise(new AButtonPressedEvent() {});
        }
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
           debugText.text = "A Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
           debugText.text = "B Button (Down)";
           EventBus<BButtonPressedEvent>.Raise(new BButtonPressedEvent() {});
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            debugText.text = "B Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
           debugText.text = "Right Trigger (Down)";
           EventBus<TriggerPressedEvent>.Raise(new TriggerPressedEvent() {});
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
           debugText.text = "Right Trigger (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
           debugText.text = "Right Grip (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
           debugText.text = "Right Grip (Up)";
        }

        //--- Left Controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
            debugText.text = "X Button (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.LTouch))
        {
           debugText.text = "X Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
           debugText.text = "Y Button (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.LTouch))
        {
            debugText.text = "Y Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
            debugText.text = "Left Trigger (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.LTouch))
        {
           debugText.text = "Left Trigger (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
           debugText.text = "Left Grip (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch))
        {
           debugText.text = "Left Grip (Up)";
        }
    }
}