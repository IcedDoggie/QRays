using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using DG.Tweening;
/// <summary>
/// Enemy spawner. Creates a new enemy once every few seconds, assuming we're not in a game over state.
/// </summary>
public class EnemySpawner : MonoBehaviour {
	
	public float maxZSpawn;
	public float minZSpawn;
	public float xSpawn;
	public float thisSpeed = 3.0f;


	public GameObject[] harzards;
	public int hazardCount = 10;
	public float spawnWait = 4.0f;
	public float waveWait = 5.0f;
	public Text waveText;

	private float _nextLaunchTime;
	private float _ySpawn = -3.0f;
	private GameController _gameController;
	private EnemyMover enemymover;
	private Quaternion _launchRotation;

	private float startWait = 2.0f;
	private int wave = 1;

	private System.Random random = new System.Random();

    //Post CBT Changes
    private bool stageChangeFlag = false;
    private Image fadeOutScreen;

    void Start()
	{
		
	}

	public void NewGame () {
//        SetNextLaunch();

			_gameController = this.GetComponent<GameController>();
			enemymover = FindObjectOfType<EnemyMover> ();
			_launchRotation = Quaternion.Euler(new Vector3(0.0f, -90.0f, 0.0f));
            fadeOutScreen = _gameController.VRCanvas.GetComponent<Image>();
       

    }
	public IEnumerator detectInputNewGame()
	{
		while(true)
		{
			if(Input.GetButtonDown("Fire1"))
			{
				NewGame ();
			}
			yield return null;
		}
	}
	public void pointerInStart()
	{
		StartCoroutine (detectInputNewGame ());
	}
	public void pointerOutStart()
	{
		StopCoroutine (detectInputNewGame ());
	}

	void Update () {
//		if (Time.time > _nextLaunchTime && !_gameController.isGameOver) {
//			Vector3 launchPosition = new Vector3(xSpawn, _ySpawn, Random.Range(minZSpawn, maxZSpawn));
//			Instantiate(enemyPrefab, launchPosition, _launchRotation);
//			SetNextLaunch();
//		}
	}

	public IEnumerator SpawnWaves()
	{
        wave = 1;
		bool bossDie = true;
		yield return new WaitForSeconds (startWait);
		while (!_gameController.isGameOver)
		{
            
            
			StartCoroutine(UpdateWave ());
            if (wave % 3 != 0)
            {
				bossDie = true;
                for (int i = 0; i < hazardCount; i++)
                {
                    float temp = Random.Range(0.0f, 100.0f);
                    //GameObject harzard = harzards [spawnMonster];
                    //GameObject harzard = harzards [Random.Range(0, harzards.Length)];
                    //GameObject harzard = harzards[Random.Range(0, harzards.Length)];
                    GameObject hazardNormal = harzards[0];
                    Vector3 spawnPosition = new Vector3(xSpawn, _ySpawn, Random.Range(minZSpawn, maxZSpawn));
                    Instantiate(hazardNormal, spawnPosition, _launchRotation);

                    yield return new WaitForSeconds(spawnWait);
                }
            }
            else if (wave % 3 == 0)
            {
				bossDie = false;
                float temp = Random.Range(0.0f, 100.0f);
                GameObject hazardBoss = harzards[1];
                Vector3 spawnPosition = new Vector3(xSpawn, _ySpawn, Random.Range(minZSpawn, maxZSpawn));
                Instantiate(hazardBoss, spawnPosition, _launchRotation);

				//Debug.Log (hazardBoss.GetComponent<Animator> ().GetBool ("EDie"));
				if (hazardBoss.GetComponent<Animator> ().GetBool ("EDie"))
					bossDie = true;



				thisSpeed += 1.0f;
				yield return new WaitForSeconds (waveWait);
            }
            hazardCount += 2;

            if (_gameController.VRCanvas.GetComponent<Image>().color.a == 0 ||
                _gameController.VRCanvas.GetComponent<Image>().color.a == -255)
            {
                
            }

            yield return new WaitForSeconds(waveWait);

  
            
        }
	}

	IEnumerator UpdateWave()
	{
		float fade = 0.0f;
        waveText.text = "Wave " + wave;
        //if (wave > 1)
        //{
        //    _gameController.VRCanvas.GetComponent<Image>().DOFade(255, 1000);
        //    stageChangeFlag = true;
        //}

        while (waveText.color.a < 1)
		{
			fade += 0.05f;
			waveText.color = new Color (waveText.color.r,waveText.color.g,waveText.color.b,  fade);
            fadeOutScreen.color = new Color(fadeOutScreen.color.r,
                fadeOutScreen.color.g, fadeOutScreen.color.b, fade);
            yield return null;
		
		}

		yield return new WaitForSeconds (3.0f);
        
        wave += 1;
        //if(stageChangeFlag == true)
        //{
        //    _gameController.VRCanvas.GetComponent<Image>().DOFade(-255, 500);
        //    stageChangeFlag = false;
        //}
            
        while (waveText.color.a > 0)
		{
			fade -= 0.05f;
			waveText.color = new Color (waveText.color.r,waveText.color.g,waveText.color.b,  fade);
            fadeOutScreen.color = new Color(fadeOutScreen.color.r,
                fadeOutScreen.color.g, fadeOutScreen.color.b, fade);
			yield return null;
		}
         

    }
}
