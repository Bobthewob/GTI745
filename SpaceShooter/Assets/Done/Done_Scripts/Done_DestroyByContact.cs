using UnityEngine;
using System.Collections;

public class Done_DestroyByContact : MonoBehaviour
{
	public GameObject explosion;
	public GameObject playerExplosion;
	public int scoreValue;
	private Done_GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindGameObjectWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <Done_GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}

	void OnTriggerEnter (Collider other)
	{
		
		if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "EnemyShip")
		{
			return;
		}

		if(other.tag == "weapons" && gameObject.tag == "EnemyShip") // tag for player weapon
		{
            Debug.Log("collison that will charge");

            var player = GameObject.FindGameObjectWithTag ("Player");

            if (player != null)
            {
                Debug.Log("player not null");
                var t = player.GetComponent<Done_PlayerController>();
                if(t != null)
                {
                    Debug.Log("script not null");
                    t.ChargeWeapon();
                }
                //player.GetComponent<Done_PlayerController>().ChargeWeapon();
            }
            else
                Debug.Log("player null");


        }

        if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}

		if (other.tag == "Player")
		{
			Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		}
		
		gameController.AddScore(scoreValue);
		Destroy (other.gameObject);
		Destroy (gameObject);
	}

	public void Destruction(){

		if (explosion != null)
		{
			Instantiate(explosion, transform.position, transform.rotation);
		}
		gameController.AddScore(scoreValue);
		Destroy (gameObject);
	}
}