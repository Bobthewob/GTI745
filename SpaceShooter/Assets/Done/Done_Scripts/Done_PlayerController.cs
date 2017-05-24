using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class Done_Boundary 
{
	public float xMin, xMax, zMin, zMax;
}

public class Done_PlayerController : MonoBehaviour
{
	public float speed;
	public float tilt;
	public Done_Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;
	public SimpleTouchPad touchPad;
	public SimpleFiringTouchPad firingTouchPad;
	public bool firingState;
	public float fireRate;
	private float nextFire;
	private int weaponCharge = 0;
	private bool weaponNotReset = true;

    //-----------progressbarImages--------------------
    public GameObject firstCharge;
	public GameObject secondCharge;
	public GameObject thirdCharge;
	public GameObject fourthCharge;
	public GameObject fifthCharge;
	public GameObject weaponReadyText;
	public GameObject progBarContour;
    public GameObject fireSecondaryWeaponButton;
    //-----------progressbarImages--------------------

    private Quaternion calibrationQuaternion;

	public void Start(){
		CalibrateAccelerometer();
	}

	void Update ()
	{
		if (Toolbox.Instance.inGame && weaponNotReset) {
			InitializeWeapon ();
			weaponNotReset = false;
		}
			
		if (Time.time > nextFire && Input.touchCount == 3 && weaponCharge == 5) { // second weapon 
			var allEnemy = GameObject.FindGameObjectsWithTag ("Enemy");

			if (allEnemy.Length > 0) {
				foreach (var enemy in allEnemy) {
					var script = enemy.GetComponent<Done_DestroyByContact> ();
					if(script != null)
						script.Destruction ();
				}
			}
				
			allEnemy = GameObject.FindGameObjectsWithTag ("EnemyShip");

			if (allEnemy.Length > 0) {
				foreach (var enemy in allEnemy) {
					var script = enemy.GetComponent<Done_DestroyByContact> ();
					if(script != null)
						script.Destruction ();
				}
			}
				
			resetWeapon();
		}
		else if (!Toolbox.Instance.controllerMod && firingTouchPad.canFire () && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource> ().Play ();
		} else if (Toolbox.Instance.controllerMod && Input.touchCount > 0 && Time.time > nextFire && Toolbox.Instance.inGame) {
			nextFire = Time.time + fireRate;
			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play ();
		}

	}

	void CalibrateAccelerometer(){ 
		Vector3 accelerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion); //je crois quon prend l'inverse parce que il utilise Ax = B qui devient x = A^(-1)B
	}

	Vector3 FixAccelleration(Vector3 acceleration){
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	void FixedUpdate ()
	{
		Vector3 movement;

		if (Toolbox.Instance.controllerMod) {
			Vector3 accelerationRaw = Input.acceleration;
			Vector3 acceleration = FixAccelleration (accelerationRaw);
			movement = new Vector3 (acceleration.x, 0.0f, acceleration.y) * Toolbox.Instance.AccelFactor;
		} else {
			Vector2 direction = touchPad.getDirection();
			movement = new Vector3 (direction.x, 0.0f, direction.y);
		}

		GetComponent<Rigidbody>().velocity = movement * speed; 
		
		GetComponent<Rigidbody>().position = new Vector3 //s'assure que la position est à l'intérieur de notre scène je crois
		(
			Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
		);
		
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt); 
	}

	public void InitializeWeapon()
	{
		weaponCharge = 0;
        var prog = Instantiate(progBarContour);
		prog.transform.SetParent(GameObject.Find("Canvas").transform, false);
    }

	public void resetWeapon()
	{
        Debug.Log("reset weapon");
		weaponCharge = 0;
        var progBar = GameObject.FindGameObjectsWithTag ("progressBar");

		if (progBar.Length > 0) {
			foreach (var item in progBar) {
				if (item.name != "ProgressBarContour(Clone)")
					Destroy(item);
			}
		}

        var weaponButton = GameObject.FindGameObjectWithTag("FireSecondaryWeaponButton");

        if (weaponButton != null)
            Destroy(weaponButton);

        Debug.Log(weaponCharge.ToString());
    }

	public void ChargeWeapon()
	{
        Debug.Log("charge weapon : " + weaponCharge);

        if (weaponCharge == 5)
			return;
		
		++weaponCharge;
        
        GameObject bar;

		switch (weaponCharge) {
		case 1:
			bar = Instantiate (firstCharge);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
			break;
		case 2:
			bar = Instantiate (secondCharge);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
			break;
		case 3:
			bar = Instantiate (thirdCharge);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
			break;
		case 4:
			bar = Instantiate (fourthCharge);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
			break;
		case 5:
			bar = Instantiate (fifthCharge);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
			bar = Instantiate (weaponReadyText);
			bar.transform.SetParent (GameObject.Find("Canvas").transform, false);
            
            bar = Instantiate(fireSecondaryWeaponButton);
            bar.transform.SetParent(GameObject.Find("Canvas").transform, false);
            break;
		}
	}

    public void FireSecondaryWeapon()
    {
        var allEnemy = GameObject.FindGameObjectsWithTag("Enemy");

        if (allEnemy.Length > 0)
        {
            foreach (var enemy in allEnemy)
            {
                var script = enemy.GetComponent<Done_DestroyByContact>();
                if (script != null)
                    script.Destruction();
            }
        }

        allEnemy = GameObject.FindGameObjectsWithTag("EnemyShip");

        if (allEnemy.Length > 0)
        {
            foreach (var enemy in allEnemy)
            {
                var script = enemy.GetComponent<Done_DestroyByContact>();
                if (script != null)
                    script.Destruction();
            }
        }

        var playerShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Done_PlayerController>();
        playerShip.resetWeapon();
    }

    void OnDestroy() {
		var progBar = GameObject.FindGameObjectsWithTag ("progressBar");

		if (progBar.Length > 0) {
			foreach (var item in progBar) {
				Destroy (item);
			}
		}
	}
}
