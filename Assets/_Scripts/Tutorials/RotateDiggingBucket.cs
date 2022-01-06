using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateDiggingBucket : MonoBehaviour
{
    [SerializeField] private GameObject _secondVipu;

    Quaternion initialRotation;

    private void Start() {
        initialRotation = Quaternion.Inverse(_secondVipu.transform.localRotation);

        Debug.Log("initialrotx: " + initialRotation + ", rightlever rot x: " + this.gameObject.transform.eulerAngles.x);
        Debug.Log("calculated rot: " + initialRotation + this.gameObject.transform.eulerAngles.x);
    }

    private void Update() {
        var xRotation = initialRotation.eulerAngles.x + this.gameObject.transform.eulerAngles.x;

        Debug.Log("initialrotx: " + initialRotation + ", rightlever rot x: " + this.gameObject.transform.eulerAngles.x);
        Debug.Log("calculated rot: " + xRotation);
        //_secondVipu.transform.Rotate(xRotation, _secondVipu.transform.eulerAngles.y, _secondVipu.transform.eulerAngles.z);

        _secondVipu.transform.localRotation = Quaternion.Inverse(Quaternion.Euler(xRotation, _secondVipu.transform.eulerAngles.y, _secondVipu.transform.eulerAngles.z));
    }
}
