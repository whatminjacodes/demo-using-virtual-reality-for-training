using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

/*
 *      Digging Bucket Tutorial Sphere Triggers
 *      - recognizes when the excavator arms are in a correct position
 *      - sends an event when arm is in correct position
 */
public class DiggingBucketSpherePart2Triggers : MonoBehaviour
{
    private bool _excavatorArmBackEventSent = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == Constants.DIGGING_EXCAVATOR_ARM_BACK && !_excavatorArmBackEventSent) {

            EventBus<ExcavatorArmBackMovedToCorrectLocationEvent>.Raise(new ExcavatorArmBackMovedToCorrectLocationEvent() {});
            _excavatorArmBackEventSent = true;
        }
    }
}
