using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public bool ResetScore = false;

	public WaveControl waveCounterClass;
	public GameObject DeathGUI;

	public GUIText DeathWaveText; 
	public GUIText DeathScoreText;
	public GUIText DeathHighScoreText;
	public GUIText waveText;
	public GUIText scoreText;
	public GUIText highScoreText;
	public GUIText restartText;

	private int score;
	private bool dead = false;
	
	public bool Dead {
		// Seta a variavel 'dead', e aparece as frases " 'Score', 'Wave' e 'Press (R to restart) [...]'"

		set{
			dead = value;
			SetHighScore ();

			DeathHighScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");

			DeathScoreText.text = "Score: " + score;
			DeathWaveText.text = "Wave: " + (waveCounterClass.WaveCounter - 1);

		}
	}

	// Use this for initialization
	void Start () {
		score = 0;
		UpdateScore ();

		if(!PlayerPrefs.HasKey("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore",0);
		}

		if(ResetScore)
		{
			PlayerPrefs.SetInt("HighScore",0);
			ResetScore = false;
		}

		highScoreText.text = "Highscore: " + PlayerPrefs.GetInt("HighScore");

		Debug.Log("Nivel Iniciado");
	}
	
	// Update is called once per frame
	void Update () {
		if(!dead)
		{
			UpdateWave ();
		}
		else
		{

			DeathGUI.SetActive(true);

			if(Input.GetKeyDown(KeyCode.R))
			{
				// Caso o player tenha morrido, e o botao R seja pressionado, o nivel eh recarregado
				Debug.Log("Recarregar Nivel");
				Application.LoadLevel(Application.loadedLevel);
			}
		}
		if(Input.GetKey("escape"))
		{
			//Caso o 'esc' tenha sido pressionado, o jogo fecha.
			Application.Quit();
		}
	}

	public void AddScore (int newScoreValue)
	{
		//Adiciona um valor passado por parametro ao Score do jogo
		score += newScoreValue;
		UpdateScore ();
	}

	void UpdateScore ()
	{
		// Atualiza o texto do Score
		scoreText.text = "Score: " + score;

	}
	void UpdateWave ()
	{
		// Atualiza o texto da Wave
		waveText.text = "Wave: " + (waveCounterClass.WaveCounter - 1);
	}

	void SetHighScore ()
	{
		if(score > PlayerPrefs.GetInt("HighScore"))
		{
			PlayerPrefs.SetInt("HighScore",score );
		}
	}

}
