using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{

	public Animator transition;
	public float transitionTime = 1f;
	public Text txt;

    // Update is called once per frame
    void Update()
    {
        
    }
	
	public void loadScene(string lvl) {
		StartCoroutine(LoadLevel(lvl));
	}
	
	IEnumerator LoadLevel(string lvl) {
		txt.text = "";
		if (lvl == "Level1") txt.text = "LEVEL 1";
		if (lvl == "Level2") txt.text = "LEVEL 2";
		transition.SetTrigger("Start");
		yield return new WaitForSeconds(transitionTime);
		SceneManager.LoadScene(lvl);
	}
}
