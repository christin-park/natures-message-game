using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//leave the forest? text
public class dialogue_2 : MonoBehaviour {
    //ref to gameobjects
    public GameObject window;
    public GameObject indicator;
    public GameObject yesNoButton;
    public TMP_Text dialogueText;

    //ref to writing speed
    public float writingSpeed;

    //ref to scripts
    public player_movement playerMovement;
    
    //vars
    public List<string> dialogues;
    private int index;
    private int charIndex;
    
    //start and updates
    private bool started;
    private bool wait;

    private void Awake() {
        indicator.SetActive(false);
        window.SetActive(false);
        yesNoButton.SetActive(false);
    }

    //start dialogue2, called from dialogue_trigger_2
    public void startDialogue() {
        if (started) {
            return;
        }
        started = true;
        window.SetActive(true);
        indicator.SetActive(false);
        getDialogue(0);
    }

    //sets up dialogue for writing
    private void getDialogue(int i) {
        index = i;
        charIndex = 0;
        dialogueText.text = string.Empty;
        StartCoroutine(writing());
    }

    //ends dialogue
    public void endDialogue() {
        started = false;
        wait = false;
        StopAllCoroutines();
        window.SetActive(false);
        yesNoButton.SetActive(false);
        setEngagement(false);
    }

    //continue dialogue
    public void continueDialogue() {
        index++;
        
        if (index < dialogues.Count) {
            getDialogue(index);
            if (index == 1) {
                yesNoButton.GetComponent<yes_no_button>().startScript();
                //yesNoButton.startScript(); not this cuz im referecing yesNoButton game component and not scrip
                enabled = false;
            }
        }
        else {
            indicator.SetActive(true);
            endDialogue();
        }
    }

    //write out char one by one
    IEnumerator writing() {
        yield return new WaitForSeconds(writingSpeed);

        string currentDialogue = dialogues[index];
        dialogueText.text += currentDialogue[charIndex];
        charIndex++;
        if (charIndex < currentDialogue.Length) {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(writing());
        }
        else {
            wait = true;
        }
    }

    void Update() {
        if (!started)
            return;
        if (wait && Input.GetKeyDown(KeyCode.Return)) {
            wait = false;
            continueDialogue();
        }
    }

    public void setEngagement(bool engaged) {
        playerMovement.setEngagement(engaged);
        window.SetActive(engaged);
        index = 0;
        started = engaged;

        if (engaged) {
            getDialogue(index);
        }
        else {
            endDialogue();
        }
    }
}
