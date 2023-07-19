using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//fills loading bar. duration u hold enter determines
//how hard u throw the leaf plane
public class loading_bar : MonoBehaviour {
    //refs
    public Slider slider;
    public float fillSpeed = 1.0f;
    public Animator leafAnimatorWhite;
    public Animator whiteAnimator;
    public GameObject leafPlaneWhite;
    public GameObject progressBar;
    public Transform playerTransform;
    public dialogue dialogue;

    //vars
    private bool isFilling;
    private float fillStartTime;
    public bool wait;
    private bool started;

    private void Awake() {
        gameObject.SetActive(false);
        leafPlaneWhite.SetActive(false);
        progressBar.SetActive(false);
 
    }

    //start filling bar
    public void startProgress() {
        isFilling = true;
        fillStartTime = Time.time;
    }   


    //call from dialogue
    public void startFilling() {
        if (started)
            return;
        started = true;
        gameObject.SetActive(true);
        flipPlayer();
        leafPlaneWhite.SetActive(true);
        whiteAnimator.SetTrigger("triggerThrow");
        
    }

    //clamp to not go past the slider max
    public void stopFilling() {   
        started = false;
        isFilling = false;
        float fillDuration = Time.time - fillStartTime;
        float fillAmount = fillDuration / 2.5f;
        slider.value = Mathf.Clamp(fillAmount, 0f, 4f);
        PlayAnimation(fillAmount);
        dialogue.continueDialogue();
    }

    //throw the plane right
    public void flipPlayer() {
        if (playerTransform != null) {
            Vector3 scale = playerTransform.localScale;
            scale.x *= -1;
            playerTransform.localScale = scale;
        }
    }

    private void finishDialogue() {
        dialogue.continueDialogue();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (!started) {
                finishDialogue();
                return;
            }
            startProgress();
        }

        //updates how full the slider is
        if (isFilling) {
            float fillDuration = Time.time - fillStartTime;
            float fillAmount = fillDuration / 2.5f;
            slider.value = Mathf.Clamp(fillAmount, 0f, 4f);
            if (Input.GetKeyUp(KeyCode.Return)) {
                stopFilling();
            }
        }
    }

    //diff animation based on fill amount
    private void PlayAnimation(float fillAmount) {
        if (fillAmount <= 4f) {
            leafAnimatorWhite.SetTrigger("leaf7_5");
            whiteAnimator.SetTrigger("triggerThrow2");
        }
        else if (fillAmount < 3f) {
            leafAnimatorWhite.SetTrigger("leaf5");
            whiteAnimator.SetTrigger("triggerThrow2");
        }
        else if (fillAmount < 2f) {
            leafAnimatorWhite.SetTrigger("leaf2_5");
            whiteAnimator.SetTrigger("triggerThrow2");
        }
        else if (fillAmount < 1f) {
            leafAnimatorWhite.SetTrigger("leaf_drop");
            whiteAnimator.SetTrigger("triggerThrow2");
        }
    }
}
