using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SwitchSceneScript : MonoBehaviour {

    private bool isAnimate = false;
    private bool isDone = false;

    public void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
    }

    public void Update()
    {
        /*Debug.Log(Manager.Instance.transitionOut);

        if (Manager.Instance.transitionIn)
        {
            cameraZoomIn();
        }
        else if (Manager.Instance.transitionOut)
        {
            Debug.Log("Transition Out");
            cameraZoomOut();
        }*/
    }

    public void OnClick() {
        Manager.Instance.transitionIn = true;
    }
}
