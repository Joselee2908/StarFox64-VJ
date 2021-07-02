using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public LevelLoader lvlloader;
	
    void Update() {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) {
			lvlloader.loadScene("Level1");
		}
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) {
			lvlloader.loadScene("Level2");
		}
    }

    public void PlayButton() {
    	lvlloader.loadScene("Level1");
    }
	
	public void CreditsButton() {
    	lvlloader.loadScene("Credits");
    }
	
    public void QuitButton() {
    	Application.Quit();
    }
}
