using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using pEventBus;

public class VRControllerTriggers : MonoBehaviour
{
    public TMP_Text debugText;

    [SerializeField] private GameObject _enterButtonGameObject;

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == _enterButtonGameObject.name) {
            debugText.text = "ontriggerenter: " + other.gameObject.name;

            EventBus<MainMenuEnterTouchedEvent>.Raise(new MainMenuEnterTouchedEvent()
            {
            });
        }
    }
}
