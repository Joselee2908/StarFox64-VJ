using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsMenu : MonoBehaviour
{
	public LevelLoader lvlloader;
	public GameObject title;
	public GameObject dev;
	public GameObject insp;
	public GameObject specialthanks;
	public GameObject thanksforplaying;
	public GameObject button;
	
    void Start() {
        title.SetActive(true);
		button.SetActive(false);
		Invoke("changeOne", 10f);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
			lvlloader.loadScene("Menu");
		}
		if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
			lvlloader.loadScene("Level1");
		}
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
			lvlloader.loadScene("Level2");
		}
    }

	void changeOne() {
		title.SetActive(false);
		dev.SetActive(true);
		button.SetActive(true);
		Invoke("changeTwo", 10f);
	}

	void changeTwo() {
		dev.SetActive(false);
		insp.SetActive(true);
		Invoke("changeThree", 10f);
	}

	void changeThree() {
		insp.SetActive(false);
		specialthanks.SetActive(true);
		Invoke("changeFour", 10f);
	}

	void changeFour() {
		specialthanks.SetActive(false);
		thanksforplaying.SetActive(true);
	}

    public void BackButton(){
    	lvlloader.loadScene("Menu");
    }
}
