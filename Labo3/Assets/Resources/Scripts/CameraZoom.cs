using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var menuRectangle = GameObject.Find("Panel").GetComponent<RectTransform>().rect;

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
                        if (hitInfo.collider != null)
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
                        else {
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
                        if (hitInfo.collider != null)
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
                            if (hitInfo.collider != null)
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
			case cursorType.Play:
				if (Input.GetMouseButtonDown (0)) {
					playSelect = true; //now for each frame that we collide with a sun we add it in the playlist until we release the click
				}else if (Input.GetMouseButtonUp(0)){ //left click up
					//TODO : play the list	
					playSelect = false;
				}else{ //we are dragging the mouse over the elements we want to add in the playlist
					//TODO
					if(playSelect){
						
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
}
