using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardMain : MonoBehaviour {
    GameObject control;
    SpriteRenderer spriteRenderer;
    public Sprite[] face;
    public Sprite back;
    public int faceCollection;
    public bool match = false;

    void Start() {
        control = GameObject.Find("GameControl");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnMouseDown() {
        if (match == false) {
            if (spriteRenderer.sprite == back) {
                if (control.GetComponent<GameControl>().flippedPair() == false) {
                    spriteRenderer.sprite = face[faceCollection];
                    control.GetComponent<GameControl>().showFACE(faceCollection, this.gameObject);
                    control.GetComponent<GameControl>().flippedPair();
                    match = control.GetComponent<GameControl>().firstFlipped.GetComponent<CardMain>().match;
                    Debug.Log("Match " + match + " in " + faceCollection);
                }
            }
        }
    }

    float countDown = 1;

    private void Update() {
        if (match == false && spriteRenderer.sprite != back && control.GetComponent<GameControl>().flippedTwo) {
            countDown -= Time.deltaTime;
            if (countDown <= 0) {
                countDown = 1;
                control.GetComponent<GameControl>().resetFaces(faceCollection);
                spriteRenderer.sprite = back;
            }
        }
    }
}
