using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//from forest to end
//after pressing no when leaving forest, it switches
public class scene_switch_3 : MonoBehaviour {
    public bool fadingOut = false;
    public Image fadeImage;
    public float fadeSpeed = 1f;

    private void Start() {
        StartCoroutine(fadeIn());
    }

    public void nextScene() {
        StartCoroutine(TransitionToNextScene());
        StartCoroutine(fadeOut());
    }

    public IEnumerator TransitionToNextScene() {
        fadingOut = true;
        yield return StartCoroutine(fadeOut());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public IEnumerator fadeIn() {
        fadeImage.gameObject.SetActive(false);
        fadeImage.color = Color.black;

        while (fadeImage.color.a > 0) {
            fadeImage.color -= new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }

        fadeImage.gameObject.SetActive(false);
    }

    public IEnumerator fadeOut() {
        fadeImage.gameObject.SetActive(true);
        fadeImage.color = Color.clear;

        while (fadeImage.color.a < 1) {
            fadeImage.color += new Color(0, 0, 0, fadeSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
