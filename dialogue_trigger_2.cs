using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//triggers dialogue between white and transparent block to exit forest
public class dialogue_trigger_2 : MonoBehaviour {
    public dialogue_2 dialogue2Script;
    public player_movement playerMovement; 

    private bool detect;

    //OnTriggerEnter2D triggered when collision with collision set
    //expects Collider2D type named collision
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            detect = true;
            dialogue2Script.indicator.SetActive(detect);
            playerMovement.setEngagement(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            detect = false;
            dialogue2Script.indicator.SetActive(detect);
            dialogue2Script.endDialogue();
            playerMovement.setEngagement(false);
        }
    }

    private void Update() {
        if (detect && Input.GetKeyDown(KeyCode.Return)) {
            dialogue2Script.startDialogue();
        }
    }
}
