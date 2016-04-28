using UnityEngine;
using System.Collections;

public class GameUI : MonoBehaviour
{
	private ButterflyCycle butterfly;
	private bool menuMode;
	private bool gameMode;
	private int level;

	private float bestTimeLVL1, bestTimeLVL2, bestTimeLVL3;
	private float timer = 0;

	// Use this for initialization
	void Start ()
	{
		menuMode = true;
		butterfly = GameObject.FindGameObjectWithTag ("Butterfly").transform.parent.GetComponent<ButterflyCycle> ();
//
		if (PlayerPrefs.HasKey ("level1")) {
			bestTimeLVL1 = PlayerPrefs.GetFloat ("level1");
		}
		if (PlayerPrefs.HasKey ("level2")) {
			bestTimeLVL2 = PlayerPrefs.GetFloat ("level2");
		}
		if (PlayerPrefs.HasKey ("level3")) {
			bestTimeLVL3 = PlayerPrefs.GetFloat ("level3");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (gameMode) {
			timer += Time.deltaTime;
		}
	}

	private void OnGUI ()
	{
		var style = new GUIStyle ("label");
		style.fontSize = 16;
		style.normal.textColor = Color.white;

		if (menuMode) {
			if (GUI.Button (new Rect (25, 25, 100, 35), "Explore")) {
				level = 0;
				butterfly.CycleEnable = false;
				gameMode = true;
				menuMode = false;
			}
			if (GUI.Button (new Rect (25, 65, 100, 35), "Easy")) {
				level = 1;
				butterfly.CycleEnable = true;
				butterfly.Speed = 5;
				gameMode = true;
				menuMode = false;
			}
			if (GUI.Button (new Rect (25, 105, 100, 35), "Normal")) {
				level = 2;
				butterfly.CycleEnable = true;
				butterfly.Speed = 8;
				gameMode = true;
				menuMode = false;
			}
			if (GUI.Button (new Rect (25, 145, 100, 35), "Hard")) {
				level = 3;
				butterfly.CycleEnable = true;
				butterfly.Speed = 12.5f;
				butterfly.LevelHard = true;
				gameMode = true;
				menuMode = false;
			}
		}
		//----------------------------------------------------------------------
		if (gameMode && level == 0) {
			if (GUI.Button (new Rect (25, 25, 100, 35), "Menu")) {
				menuMode = true;
				gameMode = false;
			}
		}
		if (gameMode && level == 1) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " + (Mathf.Round (bestTimeLVL1 * 100) / 100).ToString ()), style);
			GUI.Label (new Rect (250, 25, 150, 40), "Catch me if you can!", style);
			GUI.Label (new Rect (25, 60, 150, 40), ("Current time: " + (Mathf.Round (timer * 100) / 100).ToString ()), style);

			if (GUI.Button (new Rect (500, 25, 100, 35), "Give up?")) {
				butterfly.CycleEnable = false;
				menuMode = true;
				gameMode = false;
				timer = 0;
			}
		}
		if (gameMode && level == 2) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " + (Mathf.Round (bestTimeLVL2 * 100) / 100).ToString ()), style);
			GUI.Label (new Rect (250, 25, 150, 40), "Catch me if you can!", style);
			GUI.Label (new Rect (25, 60, 150, 40), ("Current time: " + (Mathf.Round (timer * 100) / 100).ToString ()), style);

			if (GUI.Button (new Rect (500, 25, 100, 35), "Give up?")) {
				butterfly.CycleEnable = false;
				menuMode = true;
				gameMode = false;
				timer = 0;
			}
		}
		if (gameMode && level == 3) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " + (Mathf.Round (bestTimeLVL3 * 100) / 100).ToString ()), style);
			GUI.Label (new Rect (250, 25, 150, 40), "Catch me if you can!", style);
			GUI.Label (new Rect (25, 60, 150, 40), ("Current time: " + (Mathf.Round (timer * 100) / 100).ToString ()), style);

			if (GUI.Button (new Rect (500, 25, 100, 35), "Give up?")) {
				butterfly.CycleEnable = false;
				menuMode = true;
				gameMode = false;
				timer = 0;
			}
		}
		//----------------------------------------------------------------------
		if (level == 1 && !gameMode && !menuMode) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " +(Mathf.Round (bestTimeLVL1 * 100) / 100).ToString ()), style);
			if (GUI.Button (new Rect (25, 65, 100, 35), "Menu")) {
				menuMode = true;
			}
		}
		if (level == 2 && !gameMode && !menuMode) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " + (Mathf.Round (bestTimeLVL2 * 100) / 100).ToString ()), style);
			if (GUI.Button (new Rect (25, 65, 100, 35), "Menu")) {
				menuMode = true;
			}
		}
		if (level == 3 && !gameMode && !menuMode) {
			GUI.Label (new Rect (25, 25, 150, 40), ("Best time: " + (Mathf.Round (bestTimeLVL3 * 100) / 100).ToString ()), style);
			if (GUI.Button (new Rect (25, 65, 100, 35), "Menu")) {
				menuMode = true;
			}
		}

	}

	public void Reset ()
	{
		if (level == 1) {
			butterfly.CycleEnable = false;
			bestTimeLVL1 = timer;
			gameMode = false;
			timer = 0;
		}
		if (level == 2) {
			butterfly.CycleEnable = false;
			bestTimeLVL2 = timer;
			gameMode = false;
			timer = 0;
		}
		if (level == 3) {
			butterfly.CycleEnable = false;
			bestTimeLVL3 = timer;
			gameMode = false;
			timer = 0;
		}
	}

	void OnApplicationQuit ()
	{
		PlayerPrefs.SetFloat ("level1", bestTimeLVL1);
		PlayerPrefs.SetFloat ("level2", bestTimeLVL2);
		PlayerPrefs.SetFloat ("level3", bestTimeLVL3);
	}
}
