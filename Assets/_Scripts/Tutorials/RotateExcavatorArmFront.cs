using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Rotate Excavator Arm Front
 *      - has to be attached to the right lever
 *      - rotates the excavator arm relative to GameObject this script is attached to
 */
public class RotateExcavatorArmFront : MonoBehaviour
{
    [SerializeField] private GameObject _excavatorArmFront;
    Quaternion initialRotation;

    /*  Unity methods   */
    private void Start() {
        initialRotation = Quaternion.Inverse(_excavatorArmFront.transform.localRotation);
    }

    private void Update() {
        var xRotation = initialRotation.eulerAngles.x + this.gameObject.transform.eulerAngles.x;
        _excavatorArmFront.transform.localRotation = Quaternion.Inverse(Quaternion.Euler(xRotation, _excavatorArmFront.transform.eulerAngles.y, _excavatorArmFront.transform.eulerAngles.z));
    }
}
