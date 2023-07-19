using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//this.enabled = false; disables the script
//this.GetComponent<SCRIPTNAME>().enabled = false; disables SCRIPTNAME, SCRIPTNAME has to be 
//attached to the same game object that the current script is on
//to enable, enabled = true

//leave the forest? yes no
public class yes_no_button : MonoBehaviour {
    public Button yesButton;
    public Button noButton;
    public GameObject indicatorImage;

    public dialogue_2 dialogue2Script;
    public player_movement playerMovement;
    public scene_switch_3 sceneSwitch;

    private int currentIndex = 0;
    private Button[] buttons;

    public void startScript() {
        //make array of buttons
        buttons = new Button[] {yesButton, noButton};
        //initially set to yes button
        indicatorPosition(currentIndex);
        gameObject.SetActive(true);
        enabled = true;
    }

    private void Update() {
        //move from left index (yes button) to right index (no button)
        if (Input.GetKeyDown(KeyCode.A)) {
            //.Clamp restrict currentIndex within a range
            //currentIndex - 1 value to clamp
            //0 min val
            //1 max val
            //Clamp is needed because if you keep pressing a or d, u can go out of range
            //but if u stay within the range using clamp, it is not possible to go out of index
            currentIndex = Mathf.Clamp(currentIndex - 1, 0, 1);
            indicatorPosition(currentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.D)) {
            currentIndex = Mathf.Clamp(currentIndex + 1, 0, 1);
            indicatorPosition(currentIndex);
        }

        //hmm I'm having trouble with onclick buttons in unity. :( 
        //I set the buttons to receive yesClick() and noClick(),
        //but it didn't work. so i triggered them this way
        if ((Input.GetKeyDown(KeyCode.Return)) && (currentIndex == 0)) {
            yesClick();
        }
        if ((Input.GetKeyDown(KeyCode.Return)) && (currentIndex == 1)) {
            noClick();
        }

    }

    private void indicatorPosition(int index) {
        RectTransform indicatorTransform = indicatorImage.GetComponent<RectTransform>();
        RectTransform buttonTransform = buttons[index].GetComponent<RectTransform>();
        indicatorTransform.position = new Vector2(buttonTransform.position.x, buttonTransform.position.y + 0.6f);
    }

    public void yesClick() {
        dialogue2Script.enabled = false;
        sceneSwitch.nextScene();
    }

    public void noClick() {
        dialogue2Script.endDialogue();
        gameObject.SetActive(false);
        playerMovement.setEngagement(true);
    }
}
