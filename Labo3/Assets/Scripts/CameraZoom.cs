using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    public float speedZoom = 1f;
    public float speed = 0.01f;

    private Vector3 holdMousePosition;
    private Vector3 initialMousePosition;
    private bool translateFirstTime = true;

    private bool rotateFirstTime = true;

    private bool isZoomed = false;
    private Vector3 targetPosition;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var direction = GetComponent<Camera>().transform.forward;
        var cameraTransform = GetComponent<Transform>();
        var camera = GetComponent<Camera>();

        // Scroll Up
        if (Input.GetAxis("Mouse ScrollWheel") > 0) {
            zoomIn(cameraTransform, direction);
        }

        // Scroll down
        else if (Input.GetAxis("Mouse ScrollWheel") < 0) {
            zoomOut(cameraTransform, direction);
        }

        // Right click
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Pressed right click down.");
            rotateFirstTime = false;
            initialMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(1))
        {
            Debug.Log("Pressed right click up.");
            rotateFirstTime = true;
        }

        if (Input.GetMouseButton(1))
        {
            Debug.Log("Pressed right click hold.");
            if (!rotateFirstTime)
            {
                holdMousePosition = Input.mousePosition;

                float grandeur = (initialMousePosition - holdMousePosition).magnitude * speed;

                var hold3DMousePosition = camera.ViewportToWorldPoint(
                    new Vector3(
                        holdMousePosition.x,
                        holdMousePosition.y,
                        camera.nearClipPlane
                    )
                );

                var initial3DMousePosition = camera.ViewportToWorldPoint(
                    new Vector3(
                        initialMousePosition.x,
                        initialMousePosition.y,
                        camera.nearClipPlane
                    )
                );

                cameraTransform.RotateAround(Vector3.zero, (hold3DMousePosition - initial3DMousePosition), 1);

                /*var hold3DMousePosition = camera.ViewportToWorldPoint(
                    new Vector3(
                        holdMousePosition.x,
                        holdMousePosition.y,
                        camera.nearClipPlane
                    )
                );
                var initial3DMousePosition = camera.ViewportToWorldPoint(
                    new Vector3(
                        initialMousePosition.x,
                        initialMousePosition.y,
                        camera.nearClipPlane
                    )
                );

                var directionMouse = initial3DMousePosition - hold3DMousePosition;
                directionMouse.Normalize();

                Vector3 rotationVector = Vector3.Cross(directionMouse, direction);

                var positionAfterRotation = Quaternion.AngleAxis(1f, rotationVector);

                GetComponent<Transform>().rotation = Quaternion.Inverse(positionAfterRotation) * GetComponent<Transform>().rotation;*/

                //GetComponent<Transform>().position = Quaternion. * GetComponent<Transform>().rotation;
            }
        }

        // Middle click
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

    private void zoomIn(Transform cameraTransform, Vector3 direction)
    {
        cameraTransform.position = new Vector3(
            transform.position.x + direction.x * speedZoom,
            transform.position.y + direction.y * speedZoom,
            transform.position.z + direction.z * speedZoom
        );
    }

    private void zoomOut(Transform cameraTransform, Vector3 direction)
    {
        cameraTransform.position = new Vector3(
            transform.position.x - direction.x * speedZoom,
            transform.position.y - direction.y * speedZoom,
            transform.position.z - direction.z * speedZoom
        );
    }
}
