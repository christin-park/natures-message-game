using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class input_field_ui : MonoBehaviour {
    public InputField inputField;

    private void Start() {
        HideUI();
        inputField.onEndEdit.AddListener(OnEndEdit);
    }

    private void OnEndEdit(string userInput) {
        inputField.text = string.Empty;
        HideUI();
    }

    public void ShowUI() {
        gameObject.SetActive(true);
        inputField.Select();
        inputField.ActivateInputField();
    }

    public void HideUI() {
        gameObject.SetActive(false);
    }
}
