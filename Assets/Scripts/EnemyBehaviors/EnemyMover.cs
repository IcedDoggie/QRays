using UnityEngine;
using System.Collections;

public class EnemyMover : MonoBehaviour {


	public float speedMin;
	public float speedMax;
	private EnemySpawner enemyspawner;

//	private int randomJump;
	
	
	enum EnemyState { Normal, Dying};
	
	private EnemyState _state;

	// Use this for initialization.
	void Start () {

		enemyspawner = FindObjectOfType<EnemySpawner> ();
		_state = EnemyState.Normal;
//		this.GetComponent<Animator> ().SetBool ("EDie", false);
        
		this.GetComponent<Rigidbody>().velocity = transform.forward * enemyspawner.thisSpeed;
        
		StartCoroutine (randomState ());

	


	}
	
	
	/// <summary>
	/// Check and see if our enemy is in a dying state. We need this because occasionally
	/// momentum drives a "dead" enemy through the end zone.
	/// </summary>
	/// <returns><c>true</c> if this enemy is dying; otherwise, <c>false</c>.</returns>
	public bool IsDying() {
		return (_state == EnemyState.Dying);
	}
	
	
	/// <summary>
	/// Remove the game object after a short moment so we can watch it get knocked around.
	/// </summary>
	public void DieSoon() {
		if (_state == EnemyState.Normal) {
			// Let's let the enemy get knocked back a bit.
			StopCoroutine(randomState());
			_state = EnemyState.Dying;
			this.GetComponent<Animator> ().SetBool ("EDie", true);
			this.GetComponent<Rigidbody> ().velocity = Vector3.zero;
			GameController gameController = FindObjectOfType<GameController>();
			ItemSpawner itemSpawner = FindObjectOfType<ItemSpawner> ();
			gameController.GotOne();
			itemSpawner.dropItem (gameObject);


            this.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1.5f);
		}
	}

	IEnumerator randomState()
	{
        while (_state != EnemyState.Dying)
        {

            //			randomJump = Random.Range (0, 10);
            yield return new WaitForSeconds(3f);

            this.GetComponent<Animator>().SetBool("EJump", true);


            yield return new WaitForSeconds(0.5f);
            this.GetComponent<Animator>().SetBool("EJump", false);
        }
	}

	
	
}

