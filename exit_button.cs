using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//exit game
public class exit_button : MonoBehaviour {
    private Button button;
    private void Start() {
        Button exit_button = GetComponent<Button>();
    }

    public void QuitGame() {   
        Application.Quit();
    }
}
