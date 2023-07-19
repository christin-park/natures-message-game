using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class input_window : MonoBehaviour {
    private void Awake() {
        Hide();
    }
    public void Show() {
        gameObject.SetActive(true);
    }

    public void Hide() {
        gameObject.SetActive(false);
    }
}
