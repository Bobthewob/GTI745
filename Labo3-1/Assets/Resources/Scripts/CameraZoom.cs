﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Linq;

public class CameraZoom : MonoBehaviour
{

    public float speedZoom = 1f;
    public float speed = 0.01f;
    public InputField cubeNameInput;

    private Vector3 holdMousePosition;
    private Vector3 initialMousePosition;
    private bool translateFirstTime = true;
    private bool rotateFirstTime = true;
	private bool playSelect = false;

    private bool isZoomed = false;
    private Vector3 targetPosition;
    private Vector3 pointFirstCollider = Vector3.zero;
    private bool rotateAroundObject = false;
    private GameObject firstCubeMerge = null;
    private GameObject secondCubeMerge = null;
    private GameObject cubeSelection = null;
	private GameObject SelectedMoveCube = null;
    private const string STAR_TAG = "Star";

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var menuRectangle = GameObject.Find("Panel").GetComponent<RectTransform>().rect;

        if (Manager.Instance.transitionIn)
        {
            cameraTransitionIn();
        }
        else if (Manager.Instance.transitionOut)
        {
           // Debug.Log("Transition Out");
            cameraTransitionOut();
        }

        if (!menuRectangle.Contains(Input.mousePosition))
        { //if we have the cursor over the menu we do not want to move the camera in any way

            var direction = GetComponent<Camera>().transform.forward;
            var cameraTransform = GetComponent<Transform>();
            var camera = GetComponent<Camera>();

            switch (Manager.Instance.cursorType)
            {
                case cursorType.FreeView:

                    // Scroll Up
                    if (Input.GetAxis("Mouse ScrollWheel") > 0)
                    {
                        zoomIn(cameraTransform, direction);
                    }

                    // Scroll down
                    else if (Input.GetAxis("Mouse ScrollWheel") < 0)
                    {
                        zoomOut(cameraTransform, direction);
                    }

                    // Right click
                    if (Input.GetMouseButtonDown(1))
                    {
                        Debug.Log("Pressed right click down.");
                        rotateFirstTime = false;
                        initialMousePosition = Input.mousePosition;

                        var ray = camera.ScreenPointToRay(initialMousePosition);
                        RaycastHit hitInfo;
                        Physics.Raycast(ray, out hitInfo);
                        pointFirstCollider = hitInfo.point;

                        if (pointFirstCollider == Vector3.zero)
                            rotateAroundObject = false;
                        else
                            rotateAroundObject = true;
                    }

                    if (Input.GetMouseButtonUp(1))
                    {
                        Debug.Log("Pressed right click up.");
                        rotateFirstTime = true;
                        rotateAroundObject = false;

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

                            var mouseDirectionVector = (hold3DMousePosition - initial3DMousePosition);
                            mouseDirectionVector.Normalize();
                            var rotationVector = Vector3.Cross(mouseDirectionVector, camera.transform.forward);

                            //nous tournons autour d'un object (l'object a /t/ trouver au premier click de la souris avecc un raycasting du curseur);
                            if (rotateAroundObject)
                            {
                                cameraTransform.RotateAround(pointFirstCollider, rotationVector, 0.5f * grandeur);
                            }
                            else //nous tournons autour de rien
                            {
                                cameraTransform.RotateAround(camera.transform.position + (camera.transform.forward * 10), rotationVector, 0.5f * grandeur);
                            }
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

				if (Input.GetMouseButtonUp(0)) //left click up
                    {
					
                        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        Physics.Raycast(ray, out hitInfo);
                        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG)
                        {
                            if (cubeSelection != null)
                            {
							cubeSelection.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                                //cubeSelection.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }

                            cubeSelection = hitInfo.collider.gameObject;

							if (cubeSelection != null) {
								Manager.Instance.selectedCube = cubeSelection.GetComponent<cubeScript>().cube;
								cubeNameInput.text = Manager.Instance.selectedCube.name;

							cubeSelection.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterialSelected");
								//cubeSelection.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);
							}
                        }
                        else if(hitInfo.collider == null){
                            if (cubeSelection != null) {
                                Manager.Instance.selectedCube = null;
								cubeSelection.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                                //cubeSelection.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            }
                        }
                    }
                    break;
                case cursorType.CreateCube:
                    if (Input.GetMouseButtonUp(0)) //left click up
                    {
					var newCube = (GameObject)Instantiate(Resources.Load("Cube"));

                        var ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);

                        newCube.transform.position = ray.origin + (ray.direction * 15);
                    }
                    break;
                case cursorType.MergeCube:
                    //left click
                    if (Input.GetMouseButtonDown(0))
                    {
                        var ray = camera.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hitInfo;
                        Physics.Raycast(ray, out hitInfo);
                        if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG)
                        {
                            var selectedCube = hitInfo.collider.gameObject;
                            firstCubeMerge = selectedCube;
						    selectedCube.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterialMerge");
                            //selectedCube.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                        }

                    }
                    else if (Input.GetMouseButtonUp(0))
                    {
                        if (firstCubeMerge != null && secondCubeMerge != null)
                        {
						    firstCubeMerge.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                            //firstCubeMerge.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            firstCubeMerge.transform.localScale += new Vector3(1, 1, 1);
                            cubeScript c1Info = firstCubeMerge.GetComponent<cubeScript>();
                            cubeScript c2Info = secondCubeMerge.GetComponent<cubeScript>();

                            foreach (var partition in c2Info.cube)
                            {
                                c1Info.cube.children.Add(partition);
                            }

                            // Update la lumiere
                            c1Info.UpdateLight();
                            
                            Destroy(secondCubeMerge);
                            Manager.Instance.rootCubes.Remove(c2Info.cube);
                        }
                    }

                    if (Input.GetMouseButton(0))
                    {
                        if (firstCubeMerge != null)
                        {
                            var ray = camera.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hitInfo;
                            Physics.Raycast(ray, out hitInfo);
                            if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG)
                            {
                                var selectedCube = hitInfo.collider.gameObject;
                                if (selectedCube != firstCubeMerge)
                                {
                                    if (secondCubeMerge != null)
                                    {
                                        if (selectedCube != secondCubeMerge)
										secondCubeMerge.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                                            //secondCubeMerge.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                                    }

                                    secondCubeMerge = selectedCube;
								    selectedCube.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterialMerge");
                                    //selectedCube.GetComponent<MeshRenderer>().material.color = new Color(0, 0, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (firstCubeMerge != null)
                        {
						    firstCubeMerge.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                            //firstCubeMerge.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            firstCubeMerge = null;
                        }

                        if (secondCubeMerge != null)
                        {
						    secondCubeMerge.GetComponent<MeshRenderer> ().material = (Material)Resources.Load ("SunMaterial");
                            //secondCubeMerge.GetComponent<MeshRenderer>().material.color = new Color(1, 1, 1);
                            secondCubeMerge = null;
                        }
                    }
                    break;
			case cursorType.MoveCube:
				if (Input.GetMouseButtonDown (0)) { //first left click
					holdMousePosition = Input.mousePosition;

					var ray = camera.ScreenPointToRay (Input.mousePosition);
					RaycastHit hitInfo;
					Physics.Raycast (ray, out hitInfo);
					if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG) {
						SelectedMoveCube = hitInfo.collider.gameObject;
					} else {
						SelectedMoveCube = null;
					}
				} else if (Input.GetMouseButton (0)) { // left click
					
					if (SelectedMoveCube != null) {
						var currentMousePosition = Input.mousePosition;

						float grandeur = (currentMousePosition - holdMousePosition).magnitude * speed;

						// translation
						var hold3DMousePosition = GetComponent<Camera> ().ViewportToWorldPoint (
							                          new Vector3 (
								                          currentMousePosition.x,
								                          currentMousePosition.y,
								                          GetComponent<Camera> ().nearClipPlane
							                          )
						                          );
						var initial3DMousePosition = GetComponent<Camera> ().ViewportToWorldPoint (
							                             new Vector3 (
								                             holdMousePosition.x,
								                             holdMousePosition.y,
								                             GetComponent<Camera> ().nearClipPlane
							                             )
						                             );

						var directionMouse = hold3DMousePosition - initial3DMousePosition;
						directionMouse.Normalize ();

						Debug.Log (SelectedMoveCube.GetComponent<Transform> ().position);
						Debug.Log (directionMouse);

						SelectedMoveCube.GetComponent<Transform> ().position = new Vector3 (
							SelectedMoveCube.GetComponent<Transform> ().position.x + directionMouse.x * grandeur,
							SelectedMoveCube.GetComponent<Transform> ().position.y + directionMouse.y * grandeur,
							SelectedMoveCube.GetComponent<Transform> ().position.z + directionMouse.z * grandeur
						);

						holdMousePosition = Input.mousePosition;
					}
				} else if (Input.GetAxis ("Mouse ScrollWheel") < 0) {

					var ray = camera.ScreenPointToRay (Input.mousePosition);
					RaycastHit hitInfo;
					Physics.Raycast (ray, out hitInfo);
					if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG) {
						SelectedMoveCube = hitInfo.collider.gameObject;

						var directionVectorFoward = (SelectedMoveCube.GetComponent<Transform> ().position - gameObject.transform.position);
						directionVectorFoward.Normalize ();

						Debug.Log (directionVectorFoward);

						SelectedMoveCube.GetComponent<Transform> ().position = new Vector3 (
							SelectedMoveCube.GetComponent<Transform> ().position.x + directionVectorFoward.x * 0.5f,
							SelectedMoveCube.GetComponent<Transform> ().position.y + directionVectorFoward.y * 0.5f,
							SelectedMoveCube.GetComponent<Transform> ().position.z + directionVectorFoward.z * 0.5f
						);
					}

				} else if (Input.GetAxis ("Mouse ScrollWheel") > 0) {

					var ray = camera.ScreenPointToRay (Input.mousePosition);
					RaycastHit hitInfo;
					Physics.Raycast (ray, out hitInfo);
					if (hitInfo.collider != null && hitInfo.collider.gameObject.tag == STAR_TAG) {
						SelectedMoveCube = hitInfo.collider.gameObject;

						var directionVectorBackward = (gameObject.transform.position - SelectedMoveCube.GetComponent<Transform> ().position);
						directionVectorBackward.Normalize ();

						SelectedMoveCube.GetComponent<Transform> ().position = new Vector3 (
							SelectedMoveCube.GetComponent<Transform> ().position.x + directionVectorBackward.x * 0.5f,
							SelectedMoveCube.GetComponent<Transform> ().position.y + directionVectorBackward.y * 0.5f,
							SelectedMoveCube.GetComponent<Transform> ().position.z + directionVectorBackward.z * 0.5f
						);
					}
				}
				break;
            }
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

	public void deleteSelectedCube(){
		if (cubeSelection != null) {
			Manager.Instance.rootCubes.Remove (cubeSelection.GetComponent<cubeScript> ().cube);
			Manager.Instance.selectedCube = null;
			GameObject.Destroy (cubeSelection);
		}
	}

    public void cameraTransitionIn()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        if (Manager.Instance.selectedCube != null)
        {
            Vector3 lTargetDir = Manager.Instance.selectedCube.star.transform.position - cam.transform.position;
            speed += 0.01f;

            //cam.transform.LookAt(Manager.Instance.selectedCube.position);
            //cam.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(Manager.Instance.selectedCube.position), Time.deltaTime * 5);

            cam.transform.rotation = Quaternion.RotateTowards(cam.transform.rotation, Quaternion.LookRotation(lTargetDir), Time.deltaTime * 30);
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 4, Time.deltaTime * (1 + speed));

            if (cam.fieldOfView < 4.1)
            {
                speed = 0.01f;
                Manager.Instance.transitionIn = false;
                SceneManager.LoadSceneAsync("2DPartition", LoadSceneMode.Additive);
            }
        }
        else
        {
            cam.fieldOfView = 60;
        }
    }

    public void cameraTransitionOut()
    {
        Camera cam = GameObject.Find("Main Camera").GetComponent<Camera>();

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, 60, Time.deltaTime * 1);

        if (cam.fieldOfView > 59.9)
        {
            Manager.Instance.transitionIn = false;
            Manager.Instance.transitionOut = false;
        }
    }
}
