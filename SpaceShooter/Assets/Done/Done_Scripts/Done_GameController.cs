using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyInfo{
	
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	public bool spawnShips;

	public EnemyInfo(Difficulty diff)
	{
		switch (diff) {
		case Difficulty.Easy:
			hazardCount = 10;
			spawnWait = 0.75f;
			startWait = 1;
			waveWait = 4;
			spawnShips = false;
			break;
		case Difficulty.Medium:
			hazardCount = 10;
			spawnWait = 0.75f;
			startWait = 1;
			waveWait = 4;
			spawnShips = true;
			break;
		case Difficulty.Hard: // on veut 1.5 x plus de mechant donc 1.5 * 10 = 15, mais on veut aussi que sa prennent le meme nombre de temps.
			hazardCount = 15; //la a place de 10 seconde (10 * 1) sa va en prendre 15 (15 * 1) donc on reduit aussi le spawnwait a 	0.66.
			spawnWait = 0.66f;
			startWait = 1;
			waveWait = 4;
			spawnShips = true;
			break;
		default:
			hazardCount = 10;
			spawnWait = 0.75f;
			startWait = 1;
			waveWait = 4;
			spawnShips = true;
			break;
		}
	}
}

public class Done_GameController : MonoBehaviour
{
	public GameObject[] hazards;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public Text scoreText;
	public Text gameOverText;
	public GameObject restartButton;
	public GameObject ReturnMenuButton;

	//------------MainMenuObjects---------------
	public GameObject mainMenuBackGround;
	public GameObject mainMenuGameNameText;
	public GameObject mainMenuStartButton;
	public GameObject mainMenuSettingsButton;
	public GameObject playerShip;
	//------------MainMenuObjects---------------

	//------------SettingsObjects---------------
	public GameObject toggleControlButton;
	public GameObject accelerometerText;
	public GameObject accelerometerInputField;
	public GameObject returnToMainMenuButton;
	public GameObject difficultyText;
	public GameObject difficultyDropDown;
	//------------SettingsObjects---------------
	
	private bool gameOver;
	private bool restart;
	private int score;
	private EnemyInfo enemyInfo;
	
	public void Start ()
	{
		restartButton.SetActive (false);
		ReturnMenuButton.SetActive (false);
		toggleMainMenu (true);
		toggleSettingsMenu (false);
		Toolbox.Instance.inGame = false;
	}

	public void StartGame()
	{
		toggleMainMenu (false);
		toggleSettingsMenu (false);
		Toolbox.Instance.inGame = true;
		enemyInfo = new EnemyInfo (Toolbox.Instance.currentDifficulty);


		if (GameObject.Find("Done_Player") == null) {
			Instantiate (playerShip, new Vector3 (0, 0, 0), Quaternion.identity); 
			//instantie le gameobject envoyer comme parametre , le prefabs (pas le choix l'original est destroy)
			//ensuite on get la nouveau vaisseau et on set les touchpad pour ceux deja creer.
			var player = GameObject.Find ("Done_Player(Clone)").GetComponent<Done_PlayerController>();
			player.touchPad = GameObject.Find ("Movement Zone").GetComponent<SimpleTouchPad>();
			player.firingTouchPad = GameObject.Find ("Fire Zone").GetComponent<SimpleFiringTouchPad>();
		}

		restartButton.SetActive (false);
		ReturnMenuButton.SetActive (false);
		gameOverText.text = "";
		gameOver = false;
		restart = false;
		score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void toggleMainMenu(bool show)
	{
		mainMenuBackGround.SetActive (show);
		mainMenuGameNameText.SetActive (show);
		mainMenuStartButton.SetActive (show);
		mainMenuSettingsButton.SetActive (show);
	}

	void toggleSettingsMenu(bool show)
	{
		toggleControlButton.SetActive (show);
		accelerometerText.SetActive (show);
		accelerometerInputField.SetActive (show);
		returnToMainMenuButton.SetActive (show);
		difficultyDropDown.SetActive(show);
		difficultyText.SetActive(show);
	}
	
	void Update ()
	{

	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < enemyInfo.hazardCount; i++)
			{
				GameObject hazard;
				if (enemyInfo.spawnShips)
					hazard = hazards [Random.Range (0, hazards.Length)];
				else
					hazard = hazards [Random.Range (0, hazards.Length - 1)];
				
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (enemyInfo.spawnWait);
			}
			yield return new WaitForSeconds (enemyInfo.waveWait);
			
			if (gameOver)
			{
				restartButton.SetActive (true);
				ReturnMenuButton.SetActive (true);
				break;
			}
		}
	}
	
	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		/*foreach (var item in GameObject.FindGameObjectsWithTag ("progressBar")) {
			Destroy (item);
		}*/
	}

	public void restartGame(){
		Toolbox.Instance.inGame = false;
		StartGame();
	}

	public void settingsMenu()
	{
		restartButton.SetActive (false);
		ReturnMenuButton.SetActive (false);
		toggleMainMenu (false);
		toggleSettingsMenu (true);
		mainMenuBackGround.SetActive (true);
		Toolbox.Instance.inGame = false;
	}
}