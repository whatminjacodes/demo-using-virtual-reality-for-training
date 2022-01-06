using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using pEventBus;

public class DiggingBucketSphereTriggers : MonoBehaviour
{
    private bool takavipuEventSent = false;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == Constants.DIGGING_TAKAVIPU2 && !takavipuEventSent) {

            EventBus<TakaVipu2MovedToCorrectLocationEvent>.Raise(new TakaVipu2MovedToCorrectLocationEvent() {});
            takavipuEventSent = true;
        }
    }
}
