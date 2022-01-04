using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeatRotator : MonoBehaviour
{
    [SerializeField] private Transform _vrCamera;

    public Text _uiText;

    void FixedUpdate() {
        this.transform.Rotate(0, _vrCamera.rotation.y, 0, Space.Self);

         Quaternion defaultRotation = Quaternion.Euler(0, _vrCamera.rotation.y, 0);
        transform.localRotation = defaultRotation;

        _uiText.text = "Rotation y: " + _vrCamera.rotation.y;
    }
}
