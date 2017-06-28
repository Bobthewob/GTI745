using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour {

    public int melodieZoom = 20;
    public int normalZoom = 60;
    public float speed = 0.01f;

    private Vector3 holdMousePosition;
    private Vector3 initialMousePosition;
    private bool translateFirstTime = true;

    private bool rotateFirstTime = true;

    private bool isZoomed = false;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var direction = GetComponent<Camera>().transform.forward;

        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            GetComponent<Transform>().position = new Vector3(
                transform.position.x + direction.x * .5f, 
                transform.position.y + direction.y * .5f, 
                transform.position.z + direction.z * .5f
            );
        }

        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            GetComponent<Transform>().position = new Vector3(
                transform.position.x - direction.x * .5f, 
                transform.position.y - direction.y * .5f, 
                transform.position.z - direction.z * .5f
            );
        }

        if (Input.GetMouseButton(1))
        {
            if (rotateFirstTime)
            {
                rotateFirstTime = false;
                initialMousePosition = Input.mousePosition;
            }
            else
            {
                holdMousePosition = Input.mousePosition;

                float grandeur = (initialMousePosition - holdMousePosition).magnitude * speed;
                
                var hold3DMousePosition = GetComponent<Camera>().ViewportToWorldPoint(
                    new Vector3(
                        holdMousePosition.x,
                        holdMousePosition.y,
                        GetComponent<Camera>().nearClipPlane
                    )
                );
                var initial3DMousePosition = GetComponent<Camera>().ViewportToWorldPoint(
                    new Vector3(
                        initialMousePosition.x,
                        initialMousePosition.y,
                        GetComponent<Camera>().nearClipPlane
                    )
                );

                var directionMouse = initial3DMousePosition - hold3DMousePosition;
                directionMouse.Normalize();

                Vector3 rotationVector = Vector3.Cross(directionMouse, direction);
                
                var positionAfterRotation = Quaternion.AngleAxis(1f, rotationVector);

                GetComponent<Transform>().rotation = Quaternion.Inverse(positionAfterRotation) * GetComponent<Transform>().rotation;

                //GetComponent<Transform>().position = Quaternion. * GetComponent<Transform>().rotation;
            }
        }
        else
        {
            rotateFirstTime = true;
        }

        if (Input.GetMouseButton(2))
        {
            Debug.Log("Pressed middle click. test");
            if (translateFirstTime)
            {
                translateFirstTime = false;
                initialMousePosition = Input.mousePosition;
            }
            else
            {
                Debug.Log("Pressed middle click.");
                holdMousePosition = Input.mousePosition;

                float grandeur = (initialMousePosition - holdMousePosition).magnitude * speed;

                // translation
                var hold3DMousePosition = GetComponent<Camera>().ViewportToWorldPoint(
                    new Vector3(
                        holdMousePosition.x, 
                        holdMousePosition.y, 
                        GetComponent<Camera>().nearClipPlane
                    )
                );
                var initial3DMousePosition = GetComponent<Camera>().ViewportToWorldPoint(
                    new Vector3(
                        initialMousePosition.x, 
                        initialMousePosition.y, 
                        GetComponent<Camera>().nearClipPlane
                    )
                );
                
                var directionMouse = initial3DMousePosition - hold3DMousePosition;
                directionMouse.Normalize();

                GetComponent<Transform>().position = new Vector3(
                    transform.position.x + directionMouse.x * grandeur, 
                    transform.position.y + directionMouse.y * grandeur, 
                    transform.position.z + directionMouse.z * grandeur
                );

                initialMousePosition = Input.mousePosition;
            }
        }
        else
        {
            translateFirstTime = true;
        }

    }
}
