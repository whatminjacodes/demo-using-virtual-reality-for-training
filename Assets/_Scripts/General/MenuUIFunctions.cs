using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuUIFunctions : MonoBehaviour
{
    [SerializeField] private TMP_Text _monitorText;

    public void OnEnterClicked() {
        if(_monitorText != null) {
            _monitorText.text = "Enter clicked";
        }
    }
}
