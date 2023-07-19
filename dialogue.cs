using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//talk to red
public class dialogue : MonoBehaviour
{
    //ref to gameobjects
    public GameObject window;
    public GameObject indicator;
    public GameObject leafPlane;
    public GameObject redNPC;
    public GameObject tempLeaf;
    public TMP_Text dialogueText;
    
    //ref to writing speed (makes parameter in unity)
    public float writingSpeed;

    //ref to scripts (next game, im writing my sciprts likeThis and not like_this :D)
    public leaf_write leafWrite;
    public player_movement playerMovement;
    public loading_bar loadingBar;

    //dialogue box (each sentence is an element in the list)
    public List<string> dialogues;
    private int index;
    private int charIndex;
    
    //start and updates
    private bool started;
    private bool wait;
    private int counter = 0;

    //animators
    private Animator leafPlaneAnimator;
    private Animator redAnimator;
    
    //awake is called when script object is initialized, doesnt have to be enabled
    //diff from start cuz it is called on the frame when the script is enabled
    private void Awake() {
        tempLeaf.SetActive(true);
        indicator.SetActive(false);
        window.SetActive(false);
        leafPlane.SetActive(false);
        leafPlaneAnimator = leafPlane.GetComponent<Animator>();
        redAnimator = redNPC.GetComponent<Animator>();
    }

    //a check to see whether dialogue is current active
    //started is boolean, so IsActive() returns a true/false variable
    //for excample if (dialogue.IsActive()) (meaning dialogue scrip is active), then do x
    //started is updated throughout this script, so dialogue activeness (is this a word lol) is enabled/reenabled
    public bool isActive() {
        return started;
    }

    //start dialogue, called from dialogue_trigger
    public void startDialogue() {
        if (started) {
            return;
        }
        if (counter > 0) {
            window.SetActive(true);
            indicator.SetActive(false);
            getDialogue(14);
            return;
        }
        ++counter;
        
        started = true;
        window.SetActive(true);
        indicator.SetActive(false);
        getDialogue(0);
    }

    //sets up dialogue for writing
    public void getDialogue(int i) {
        //for example GetDialogue(9) starts at index 9
        index = i;
        //reset character index cuz itll end up at a diff place
        charIndex = 0;
        //clear dialogue string
        dialogueText.text = string.Empty;
        StartCoroutine(writing());
    }

    //ends dialogue
    public void endDialogue() {
        //dialogue scrip is no longer active
        started = false;
        wait = false;
        //this is related to IEnumerators
        StopAllCoroutines();
        window.SetActive(false);
        playerMovement.setEngagement(false);
    }

    //continue dialogue
    public void continueDialogue() {
        index++;
        tempLeaf.SetActive(false);
        //.Count num elements in list
        if (index < dialogues.Count) {
            getDialogue(index);
            if ((index == 5) && Input.GetKeyDown(KeyCode.Return)) { //leaf :D
                leaf_write.showLeaf();
                window.SetActive(false);
                indicator.SetActive(false);
                playerMovement.setEngagement(true);
                return;
            }
            else if ((index == 6) && Input.GetKeyDown(KeyCode.Return)) { //del leaf D:
                leaf_write.hideLeaf();
                
                ++index;
            }
            else if ((index == 11) && Input.GetKeyDown(KeyCode.Return)) { //throw leaf :D
                leafPlane.SetActive(true);
                StartCoroutine(DelayedTriggerLeaf());
                StartCoroutine(DelayedTriggerUndoThrow());
                redAnimator.SetTrigger("TriggerThrow");
                loadingBar.startFilling();
                loadingBar.progressBar.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Return)) {
                }
            }

            if ((index >= 7) || (index <= 10)) { //back to reg dialogue
                window.SetActive(true);
                getDialogue(index);
            }

            if (index == 12) {
                Debug.Log("tis ithe problem;");
            }

            if (index == 13) { //del throw leaf D:
                wait = false;
                loadingBar.progressBar.SetActive(false);
                loadingBar.slider.gameObject.SetActive(false);
                loadingBar.flipPlayer();
            }

            if ((index == 15) && Input.GetKeyDown(KeyCode.Return)) {
                indicator.SetActive(true);
                endDialogue();
                leaf_write.hideLeaf();
            }
        }
        else {
            indicator.SetActive(true);
            endDialogue();
            leaf_write.hideLeaf();
        }
        playerMovement.setEngagement(false);

    }

    //write out char one by one
    //IEnumator lets u iterate over itemss
    //yield return stops method and asks value from caller.
    //WaitForSeconds is a new object with with the value of writingSpeed
    IEnumerator writing() {
        yield return new WaitForSeconds(writingSpeed);
        string currentDialogue = dialogues[index];
        //write char
        dialogueText.text += currentDialogue[charIndex];
        charIndex++;
        //.Length num char in string
        if (charIndex < dialogues[index].Length) {
            yield return new WaitForSeconds(writingSpeed);
            StartCoroutine(writing());
        }
        else {
            wait = true;
        }
    }

    //throw red's leaf
    IEnumerator DelayedTriggerLeaf() {
        yield return new WaitForSeconds(1f);
        leafPlaneAnimator.SetTrigger("TriggerLeaf");
    }

    //red throw
    IEnumerator DelayedTriggerUndoThrow() {
        yield return new WaitForSeconds(6.0f);
        redAnimator.SetTrigger("UndoTriggerThrow");
    }

    private void Update() {
        if (!started)
            return;

        loading_bar loadingBar = FindObjectOfType<loading_bar>();
        if (loadingBar != null && loadingBar.isActiveAndEnabled) {
            return;
        }

        if (wait && Input.GetKeyDown(KeyCode.Return)) {
            wait = false;
            continueDialogue();
        }
    }
}
 