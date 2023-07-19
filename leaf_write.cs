using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//trigger leaf u can write on
public class leaf_write : MonoBehaviour {
    public GameObject leafWrite;

    private void Start() {
        hideLeaf();
    }

    public static void showLeaf() {
        leafWrite.SetActive(true);
    }

    public static void hideLeaf() {
        leafWrite.SetActive(false);
    }
}
