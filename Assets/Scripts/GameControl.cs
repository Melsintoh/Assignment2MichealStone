using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour {
    public GameObject card;
    public int shuffle = 0;
    List<int> faceCollections = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
    public static System.Random ran = new System.Random();
    int[] faceUp = { -1, -2 };
    public bool flippedTwo;
    public GameObject firstFlipped;

    void Start() {
        int Length = faceCollections.Count;
        float xPostion = -3;
        float yPostion = 3;
        for (int i = 0; i < 16; i++) {
            shuffle = ran.Next(0, faceCollections.Count);
            var temp = Instantiate(card, new Vector3(xPostion, yPostion, 0), Quaternion.identity);

            temp.GetComponent<CardMain>().faceCollection = faceCollections[shuffle];
            faceCollections.Remove(faceCollections[shuffle]);
            if ((i + 1) % 4 == 0) {
                yPostion -= 2;
                xPostion = -3;
            } else {
                xPostion += 2;
            }
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void showFACE(int index, GameObject cardOrg) {
        if (faceUp[0] == -1) {
            faceUp[0] = index;
            firstFlipped = cardOrg;
        } else if (faceUp[1] == -2) {
            faceUp[1] = index;
            firstFlipped.GetComponent<CardMain>().match = checker();
            Debug.Log(firstFlipped.GetComponent<CardMain>().faceCollection);
        }
    }

    public void TakeFaceUp(int index) {
        if (faceUp[0] == index) {
            faceUp[0] = -1;
        } else if (faceUp[1] == index) {
            faceUp[1] = -2;
        }

    }

    public bool flippedPair() {
        flippedTwo = false;
        if (faceUp[0] >= 0 && faceUp[1] >= 0) {
            flippedTwo = true;
        }
        return flippedTwo;
    }

    public bool checker() {
        if (faceUp[0] == faceUp[1]) {
            faceUp[0] = -1;
            faceUp[1] = -2;
            return true;
        }
        return false;

    }

    public void resetFaces(int index) {
        if (faceUp[0] == index) {
            faceUp[0] = -1;
        } else if (faceUp[1] == index) {
            faceUp[1] = -2;
        }
    }
}
