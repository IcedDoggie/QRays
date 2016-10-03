using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Audio;
using UnityEngine.UI.Extensions;

public class GameController : MonoBehaviour
{
    public EnemySpawner enemySpawner;
    public GameObject gvrMain;

    public bool isGameOver;

    public Canvas VRGameOverCanvas;
    public Canvas GameMenu;
    public Canvas StoryCanvas;
    public Canvas StoryVRCanvas;
    public Canvas VRCanvas;
	public Canvas OptionMenuCanvas;
	public GameObject content;
    public Text VRGameOverTxt;
    public Text VRScoreTxt;
	public Text VRPauseTxt;
    //    public Text MainMenu;


    private AudioSource playMusic;
    private AudioSource menuMusic;

    public GameObject health;
    public Slider essenceBar;

    public int hp = 3;



    //	public GameObject gameOverbutton;
    private int _currScore;
    private int _scoreToWin = 20;
    private bool _didIWin;

	private bool startb, quitb, btmb, resetb, optionb, optionBackb;

    void Start()
    {

        //StartCoroutine(UIAnimation());
        //StartCoroutine(autoSlidePage());
        //		MainMenu.text = "QRays";
        VRGameOverCanvas.enabled = false;
		OptionMenuCanvas.enabled = false;
        VRCanvas.enabled = false;
        playMusic = gameObject.GetComponent<AudioSource>();
        menuMusic = gameObject.AddComponent<AudioSource>();
        menuMusic.clip = Resources.Load("intro_music_gjt") as AudioClip;
        menuMusic.Play();
        menuMusic.loop = true;

		startb = false;
		quitb = false;
		btmb = false;
		resetb = false;
		optionb = false;
		optionBackb = false;

		StartCoroutine (detectInputNewGame ());
		StartCoroutine (detectInputQuitGame ());
		StartCoroutine (detectInputResetGame ());
		StartCoroutine (detectInputbackToMenu ());
		StartCoroutine (detectInputOption ());
		StartCoroutine (detectInputOptionBack ());

//		content.transform.GetChild (1).gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (1078f, 0.0f);
//		Debug.Log (content.transform.GetChild (1).name);
//		content.transform.GetChild (2).gameObject.GetComponent<RectTransform> ().localPosition = new Vector3 (3804.95f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void NewGame()
    {
	       GameMenu.enabled = false;

	       playMusic.Play();
	       ResetGame();
    }
	//
	public IEnumerator detectInputNewGame()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && startb)
			{
				NewGame ();
			}
			yield return null;
		}
	}
	public void pointerInStart()
	{
		startb = true;
	}
	public void pointerOutStart()
	{
		startb = false;
	}

    public void QuitGame()
    {
        Application.Quit();
    }
	//
	public IEnumerator detectInputQuitGame()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && quitb)
			{
				QuitGame ();
			}
			yield return null;
		}
	}
	public void pointerInQuit()
	{
		quitb = true;
	}
	public void pointerOutQuit()
	{
		quitb = false;
	}

	//
	public IEnumerator detectInputbackToMenu()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && btmb)
			{
				backToMenu ();
			}
			yield return null;
		}
	}
	public void pointerInBTM()
	{
		btmb = true;
	}
	public void pointerOutBTM()
	{
		btmb = false;
	}
	//
	public IEnumerator detectInputResetGame()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && resetb)
			{
				ResetGame ();
			}
			yield return null;
		}
	}
	public void pointerInReset()
	{
		resetb = true;
	}
	public void pointerOutReset()
	{
		resetb = false;
	}

	public void optionButton()
	{
		GameMenu.enabled = false;
		OptionMenuCanvas.enabled = true;
	}

	public IEnumerator detectInputOption()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && optionb)
			{
				optionButton ();
			}
			yield return null;
		}
	}

	public void pointerInOption()
	{
		optionb = true;
	}
	public void pointerOutOption()
	{
		optionb = false;
	}

	public void optionBackButton()
	{
		GameMenu.enabled = true;
		OptionMenuCanvas.enabled = false;
	}

	public IEnumerator detectInputOptionBack()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1") && optionBackb)
			{
				optionBackButton ();
			}
			yield return null;
		}
	}

	public void pointerInOptionBack()
	{
		optionBackb = true;
	}
	public void pointerOutOptionBack()
	{
		optionBackb = false;
	}

    /// <summary>
    /// Got an enemy! Increment the score and see if we win.
    /// </summary>
    public void GotOne()
    {

        _currScore++;


        VRScoreTxt.text = "" + _currScore;
        //		if (_currScore >= _scoreToWin) {
        //			GameOver(true);
        //		}
    }

    public void GotOneBoss()
    {
        _currScore = _currScore + 10;
        VRScoreTxt.text = "" + _currScore;
    }


    /// <summary>
    /// Game's over. 
    /// </summary>
    /// <param name="didIWin">Did the playeer win?</param>	

    public void GameOver(bool didIWin)
    {
        isGameOver = true;
        _didIWin = didIWin;
        string finalTxt = (_didIWin) ? "You won!" : "Too bad";
        if (GvrViewer.Instance.VRModeEnabled)
        {
            VRGameOverCanvas.enabled = true;
            VRGameOverTxt.text = finalTxt;
        }

    }
    /// <summary>
    /// Resets the interface, removes remaining game objects. Basically gets us to a point
    /// where we're ready to play again.
    /// </summary>
    public void ResetGame()
    {
        // Reset the interface
        //GameMenu.GetComponentInChildren<Image>().CrossFadeAlpha(0.1f, 2.0f, false);
        //GameMenu.GetComponentInChildren<Text>().CrossFadeAlpha(0.1f, 2.0f, false);
        //GameMenu.GetComponentInChildren<Image>().GetComponentInChildren<Text>().CrossFadeAlpha(0.1f, 2.0f, false);
        gvrMain.transform.position = new Vector3(-5.53f, -2.0f, 0.122f);
        StopCoroutine(enemySpawner.SpawnWaves());
        StartCoroutine(enemySpawner.SpawnWaves());

        menuMusic.Stop();

        VRCanvas.enabled = true;
        VRGameOverCanvas.enabled = false;

        
        isGameOver = false;
        _currScore = 0;
        hp = 3;
		enemySpawner.thisSpeed = 3.0f;

        VRScoreTxt.text = "--";
        essenceBar.value = 1000;

        health.transform.GetChild(0).gameObject.SetActive(true);
        health.transform.GetChild(1).gameObject.SetActive(true);
        health.transform.GetChild(2).gameObject.SetActive(true);
        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
		foreach (GameObject boss in bosses)
		{
			Destroy(boss);
		}

    }

    public void backToMenu()
    {
        StopCoroutine(enemySpawner.SpawnWaves());

        playMusic.Stop();
        menuMusic.Play();

        VRGameOverCanvas.enabled = false;
        VRCanvas.enabled = false;

        isGameOver = false;
        _currScore = 0;
        hp = 3;

        VRScoreTxt.text = "--";
        essenceBar.value = 1000;

        health.transform.GetChild(0).gameObject.SetActive(true);
        health.transform.GetChild(1).gameObject.SetActive(true);
        health.transform.GetChild(2).gameObject.SetActive(true);
        // Remove any remaining game objects
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        GameMenu.enabled = true;

    }



    IEnumerator UIAnimation()
    {
        while (true)
        {
            health.transform.DOPunchScale(new Vector3(.5f, .5f), 1.0f, 5, .5f);
            essenceBar.transform.DOPunchScale(new Vector3(.5f, .5f), 1.0f, 5, .5f);


            yield return new WaitForSeconds(5f);
        }
    }

    IEnumerator autoSlidePage()
    {
        yield return new WaitForSeconds(2f);
        StoryVRCanvas.enabled = true;
        Vector3 speed = new Vector3(0, 0, 0.0f);
        while (StoryCanvas.isActiveAndEnabled && gvrMain.transform.localPosition.z > 117)
        {
            Vector3 temp = new Vector3(0f, 0f, -0.02f) + speed;
            gvrMain.transform.localPosition += temp;
            if (Input.GetButton("Fire1") && StoryCanvas.enabled && gvrMain.transform.localPosition.z > 117)
            {

                speed = new Vector3(0, 0, -0.10f);
            }
            else
            {
                speed = new Vector3(0, 0, 0.0f);
            }

            //			if(horizontalScrollSnap.CurrentScreen() == horizontalScrollSnap._screens - 1)
            //			{
            //				StoryCanvas.enabled = false;
            //			}
            yield return null;
        }

        yield return new WaitForSeconds(3f);
        StoryVRCanvas.enabled = false;
        StoryCanvas.enabled = false;
        gvrMain.transform.position = new Vector3(-5.53f, -2.0f, 0.122f);

    }

}