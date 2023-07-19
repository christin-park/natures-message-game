using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//from main_menu to how_to_play
//triggered by button press with mouse
public class scene_switch : MonoBehaviour
{
    public Image fadeImage;

    //compatible with button cuz play button is set to playgame()
    private void Start() {
        StartCoroutine(fadeIn());
    }
    
    //to allow buttons to detect onclick functions in unity, must be public
    public void playGame() {
        StartCoroutine(TransitionToNextScene());
        fadeOut();
    }

    private IEnumerator TransitionToNextScene() {
        yield return StartCoroutine(fadeOut());
        //fill -> build settings
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator fadeIn() {
        fadeImage.gameObject.SetActive(false);
        fadeImage.color = Color.black;

        while (fadeImage.color.a > 0) {
            //f indicates float type
            fadeImage.color -= new Color(0, 0, 0, 1f * Time.deltaTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator fadeOut() {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        while (fadeImage.color.a < 1) {
            fadeImage.color += new Color(0, 0, 0, 1f * Time.deltaTime);
            yield return null;
        }
    }
}

