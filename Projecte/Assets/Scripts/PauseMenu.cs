using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool GameIsPaused = false;
	
	public GameObject pauseMenuUI;
	public GameObject instructionsMenuUI;
	public GameObject deadMenuUI;
	public LevelLoader lvlloader;
	public string level;

    void Start() {
        GameIsPaused = false;
		pauseMenuUI.SetActive(false);
		instructionsMenuUI.SetActive(false);
		deadMenuUI.SetActive(false);
		Time.timeScale = 1f;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && !deadMenuUI.active) {
			if (GameIsPaused) Resume();
			else Pause();
		}
		if((Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1)) && level == "Level2") lvlloader.loadScene("Level1");
        if((Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2)) && level == "Level1") lvlloader.loadScene("Level2");
    }

	public void Resume() {
		pauseMenuUI.SetActive(false);
		instructionsMenuUI.SetActive(false);
		Time.timeScale = 1f;
		GameIsPaused = false;
	}
	
	void Pause() {
		pauseMenuUI.SetActive(true);
		Time.timeScale = 0f;
		GameIsPaused = true;
	}
	
	public void LoadMenu() {
		Time.timeScale = 1f;
		lvlloader.loadScene("Menu");
	}
	
	public void Restart() {
		Time.timeScale = 1f;
		SceneManager.LoadScene(level);
	}
	
	public void GameOver() {
		Time.timeScale = 0f;
		deadMenuUI.SetActive(true);
	}
}
