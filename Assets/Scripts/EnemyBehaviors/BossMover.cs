using UnityEngine;
using System.Collections;

public class BossMover : MonoBehaviour {

	public float maxZ, minZ, maxX, minX;
	public float teleportspeedmax = 2.0f, teleportspeedmin = 1.0f;

	private float thisSpeed;

    enum EnemyState {Normal, Attack, Dying};
    private EnemyState _state;
    // Use this for initialization
    void Start () {
        _state = EnemyState.Normal;
     
       // this.GetComponent<Rigidbody>().velocity = transform.forward * thisSpeed;

        //StartCoroutine (randomMove());

        if (GvrViewer.Instance.VRModeEnabled)
        {
            thisSpeed *= 0.85f;
        }
		StartCoroutine (bossTeleport ());
    }

    public bool IsDying()
    {
        return (_state == EnemyState.Dying);
    }

    public void DieSoon()
    {
        if (_state == EnemyState.Normal)
        {
            // Let's let the enemy get knocked back a bit.
            _state = EnemyState.Dying;
            this.GetComponent<Animator>().SetBool("EDie", true);
            this.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GameController gameController = FindObjectOfType<GameController>();
            gameController.GotOneBoss();

            this.GetComponent<AudioSource>().Play();
            Destroy(gameObject, 1.5f);
        }
    }

    IEnumerator randomState()
    {
        while (_state != EnemyState.Dying)
        {
            yield return new WaitForSeconds(3f);

            //this.GetComponent<Rigidbody>().velocity = 
        }
    }

	IEnumerator bossTeleport()
	{
		while (_state == EnemyState.Normal)
			
		{
			maxX -= 2.0f;
			transform.position = new Vector3 (maxX, transform.position.y, Random.Range (maxZ, minZ));
			yield return new WaitForSeconds (Random.Range(teleportspeedmin,teleportspeedmax));
		}
	}

    // Update is called once per frame
    void Update () {
	
	}
}
