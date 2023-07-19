using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//triggers dialogue between white and red
public class dialogue_trigger : MonoBehaviour {
    public dialogue dialogueScript;
    public player_movement playerMovement;

    private bool detect;

    //OnTriggerEnter2D triggered when collision with collision set
    //expects Collider2D type named collision
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            detect = true;  
            dialogueScript.indicator.SetActive(detect);
            playerMovement.setEngagement(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            detect = false;
            dialogueScript.indicator.SetActive(detect);
            dialogueScript.endDialogue();
            playerMovement.setEngagement(false);
        }
    }

    private void Update() {
        if (detect && Input.GetKeyDown(KeyCode.Return)) {
            dialogueScript.startDialogue();
        }
    }
}
