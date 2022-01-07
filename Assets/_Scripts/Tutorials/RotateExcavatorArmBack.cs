using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *      Rotate Excavator Arm Back
 *      - has to be attached to the left lever
 *      - rotates the excavator arm relative to GameObject this script is attached to
 */
public class RotateExcavatorArmBack : MonoBehaviour
{
    [SerializeField] private GameObject _excavatorArmBack;
    Quaternion initialRotation;

    /*  Unity methods   */
    private void Start() {
        initialRotation = Quaternion.Inverse(_excavatorArmBack.transform.localRotation);
    }

    private void Update() {
        var xRotation = initialRotation.eulerAngles.x - this.gameObject.transform.eulerAngles.x;
        _excavatorArmBack.transform.localRotation = Quaternion.Inverse(Quaternion.Euler(xRotation, _excavatorArmBack.transform.eulerAngles.y, _excavatorArmBack.transform.eulerAngles.z));
    }
}
