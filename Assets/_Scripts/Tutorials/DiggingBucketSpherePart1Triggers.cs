using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

/*
 *      Digging Bucket Tutorial Sphere Triggers
 *      - recognizes when the excavator arms are in a correct position
 *      - sends an event when arm is in correct position
 */
public class DiggingBucketSpherePart1Triggers : MonoBehaviour
{
    private bool _excavatorArmFrontEventSent = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == Constants.DIGGING_EXCAVATOR_ARM_FRONT && !_excavatorArmFrontEventSent) {

            EventBus<ExcavatorArmFrontMovedToCorrectLocationEvent>.Raise(new ExcavatorArmFrontMovedToCorrectLocationEvent() {});
            _excavatorArmFrontEventSent = true;
        }
    }
}
