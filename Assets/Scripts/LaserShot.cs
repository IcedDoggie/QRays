using UnityEngine;
using System.Collections;
using DG.Tweening;
using DG.Tweening.Plugins;



public class LaserShot : MonoBehaviour {

	public GameController _gameController;
	public GameObject screenShakeObject;
    public GameObject shot1;
    public GameObject shot2;
    public GameObject wave;

	public int eachShotMana;

    public AudioSource gerobeamEffect;
    public AudioSource singlebeamEffect;

    private GameObject NowShot;
	private bool Lflag = false;
    private bool pauseFlag = false;
	private bool extraFlagToFixInputBugOnFire2 = false;
	private Vector3 defaultPosition;




    void Start()
    {
        gerobeamEffect = gameObject.AddComponent<AudioSource>();
        gerobeamEffect.clip = Resources.Load("UL_FS_Low_Boom") as AudioClip;

        singlebeamEffect = gameObject.AddComponent<AudioSource>();
        singlebeamEffect.clip = Resources.Load("UL_Short_burst_1") as AudioClip;

        defaultPosition = new Vector3(-5.53f, -2.08f, 0.122f);
		NowShot = null;
    }

    // Update is called once per frame
    void Update()
    {

		if (Input.GetButtonDown("Fire1") 
            && !_gameController.isGameOver 
            && _gameController.essenceBar.value > 0 
            && !Input.GetButton("Fire2") 
            && !_gameController.StoryCanvas.isActiveAndEnabled
            && pauseFlag == false)
        {
            #region primary Fire
            //			Handheld.Vibrate();
            singlebeamEffect.Play();
			if(screenShakeObject.layer != 8)
			{
				screenShakeObject.layer = 8;
				screenShakeObject.transform.DOShakePosition(0.5f, 0.05f, 500, 800, false, true).OnComplete(setDefaultPosition);
			}


			Vector3 laserpostion = this.transform.position;
			laserpostion.y -= 0.3f;

			GameObject s1 = (GameObject)Instantiate(shot1, laserpostion, this.transform.rotation);
			s1.GetComponent<BeamParam>().SetBeamParam(this.GetComponent<BeamParam>());

			GameObject wav = (GameObject)Instantiate(wave, laserpostion, this.transform.rotation);
			wav.transform.localScale *= 0.25f;
			wav.transform.Rotate(Vector3.left, 90.0f);
			wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;

			if(!_gameController.GameMenu.isActiveAndEnabled)
			StartCoroutine (manaDecrease (eachShotMana));
            #endregion
        }

        else if (!_gameController.isGameOver 
            && _gameController.essenceBar.value > 0 
            && !Input.GetButtonDown("Fire1") 
            && !_gameController.GameMenu.isActiveAndEnabled
            && !_gameController.StoryCanvas.isActiveAndEnabled
            && pauseFlag == false)
		{

			Vector3 laserpostion1 = this.transform.position;
			laserpostion1.y -= 0.3f;

			if (Input.GetButtonDown("Fire2") && !pauseFlag)
			{
                #region Activate 2ndary fire
                //				Handheld.Vibrate();
                GameObject wav = (GameObject)Instantiate(wave, laserpostion1, this.transform.rotation);
				wav.transform.Rotate(Vector3.left, 90.0f);
				wav.GetComponent<BeamWave>().col = this.GetComponent<BeamParam>().BeamColor;
                
                //sound effect
                gerobeamEffect.Play();
				extraFlagToFixInputBugOnFire2 = true;
                //Fire
                NowShot = (GameObject)Instantiate(shot2, laserpostion1, this.transform.rotation);
                #endregion
            }

            //it's Not "GetButtonDown"
			if (Input.GetButton ("Fire2") && !pauseFlag && extraFlagToFixInputBugOnFire2)
			{
				Lflag = true;
                //sound effect
                gerobeamEffect.Play();
                gerobeamEffect.loop = true;
                gerobeamEffect.priority = 60;

                if (Lflag)
				    _gameController.essenceBar.value -= 1;

				if(screenShakeObject.layer != 8)
				{
					screenShakeObject.layer = 8;
					screenShakeObject.transform.DOShakePosition (0.5f, 0.20f, 500, 800, false, false).OnComplete (setDefaultPosition);
				}

				BeamParam bp = this.GetComponent<BeamParam>();
				if(NowShot.GetComponent<BeamParam>().bGero	&& NowShot != null)
					NowShot.transform.parent = transform;

				Vector3 s = new Vector3(bp.Scale,bp.Scale,bp.Scale);

				NowShot.transform.localScale = s;
				NowShot.GetComponent<BeamParam>().SetBeamParam(bp);

				RaycastHit hit;

                #region Raycasting detection
                if (Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
				{
					if (hit.collider.tag == "Enemy")
					{
						//BL.StopLength(hit.distance);
			
						hit.collider.GetComponent<EnemyMover>().DieSoon();
					}
					if(hit.collider.tag == "item_C")
					{
						hit.collider.GetComponent<Item> ().getHit ();
					}
                    if(hit.collider.tag == "Boss")
                    {
                        hit.collider.GetComponent<BossMover>().DieSoon();
                    }


				}
                #endregion


            }
        }

		if ( (Input.GetButtonUp ("Fire2") 
            || _gameController.essenceBar.value <= 0) )
		{
			if(NowShot != null)
			{
				extraFlagToFixInputBugOnFire2 = false;
				Lflag = false;
                gerobeamEffect.Stop();
                NowShot.GetComponent<BeamParam>().bEnd = true;
			}
		}

        if (Input.GetButtonDown("Fire3"))
        {
            if(pauseFlag == true && Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseFlag = false;
				_gameController.VRPauseTxt.enabled = false;
            }

            else if (pauseFlag == false && Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseFlag = true;
//                gerobeamEffect.Stop();
				if(NowShot != null)
				{
					Lflag = false;
					extraFlagToFixInputBugOnFire2 = false;
					_gameController.VRPauseTxt.enabled = true;
					gerobeamEffect.Stop();
					NowShot.GetComponent<BeamParam>().bEnd = true;
					screenShakeObject.transform.DOKill ();
//					Destroy (NowShot);
				}
            }
               
        }
						
    }

	void setDefaultPosition()
	{
		screenShakeObject.layer = 0;
		screenShakeObject.transform.position = defaultPosition;
	}

	IEnumerator manaDecrease(int valueDecrease)
	{
		int temp = 0;
		if(!Lflag)
		{
			while(valueDecrease != temp)
			{
				valueDecrease -= 1;
				_gameController.essenceBar.value -= 1;

				yield return null;
			}
		}

	}


}

