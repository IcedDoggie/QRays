using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Item : MonoBehaviour {

	private GameController gameController;
	public int hp = 10;
	public int regenMana = 200;

	private AudioSource powerUpSound;

	void Start()
	{
		gameController = FindObjectOfType<GameController> ();
		Destroy (gameObject, 10.0f);
	}
		

	public void getHit()
	{
		hp -= 1;

		if(hp < 0)
		{
			transform.GetChild (0).GetChild (1).GetComponent<SkinnedMeshRenderer> ().enabled = false;
			gameObject.GetComponent<CapsuleCollider> ().enabled = false;
			powerUpSound = gameObject.AddComponent<AudioSource> ();
			powerUpSound.clip = Resources.Load("Power_Up_Ray-Mike_Koenig-800933783") as AudioClip;
			powerUpSound.Play ();
			StartCoroutine (manaIncrease (regenMana));
		}
	}
	IEnumerator manaIncrease(int valueIncrease)
	{
		int temp = 0;

		while(valueIncrease >= temp)
		{
			valueIncrease -= 4;
			gameController.essenceBar.value += 4;

			yield return null;
		}

		Destroy (gameObject);

	}

}
