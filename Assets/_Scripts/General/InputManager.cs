using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
   public Text debugText;
   
    void Update()
    {
        //--- Right Controller
        if (OVRInput.GetDown(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
           debugText.text = "A Button (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
           debugText.text = "A Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
           debugText.text = "B Button (Down)";
        }
        else if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            debugText.text = "B Button (Up)";
        }

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
           debugText.text = "Right Trigger (Down)";
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