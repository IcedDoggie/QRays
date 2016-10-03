using UnityEngine;
using System.Collections;

public class YouLoseIfEnemyHitsThis : MonoBehaviour {

	public GameController gameController;

	public GameObject getAttackedFlash;

	
	
	void OnTriggerEnter (Collider other) 
	{
		// End the game if an enemy not in the dying state hits us.
		if (other.tag == "Enemy") {

			EnemyMover badGuy = other.gameObject.GetComponent<EnemyMover>();
			badGuy.tag = "Untagged";



	
			if (!badGuy.IsDying()) {
				Destroy (other.gameObject);
                
				gameController.hp -= 1;
				StartCoroutine (attackedCounting (.25f));

				if(gameController.hp >= 0)
					gameController.health.transform.GetChild (gameController.hp).gameObject.SetActive (false);
				

				if(gameController.hp <= 0)
				gameController.GameOver(false);
			}
		}

        else if(other.tag == "Boss"){
            BossMover badGuy = other.gameObject.GetComponent<BossMover>();
            badGuy.tag = "Untagged";

            if (!badGuy.IsDying()) {
                Destroy(other.gameObject);
                //gameController.hp = 0;
                StartCoroutine(attackedCounting(.25f));
                //to empty up all the health units
                while(gameController.hp != 0)
                {
                    gameController.hp -= 1;
                    gameController.health.transform.GetChild(gameController.hp).gameObject.SetActive(false);
                }
                gameController.GameOver(false);
            }


        }
        
	}	

	IEnumerator attackedCounting(float flashTime)
	{
		getAttackedFlash.SetActive (true);

		yield return new WaitForSeconds(flashTime);

		getAttackedFlash.SetActive (false);
	}

}
