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

    }

    public void OnClick() {
        Manager.Instance.transitionIn = true;
        MusicTest musicScript = GameObject.Find("MusicSource").GetComponent<MusicTest>();
        musicScript.StopMusic();
    }
}
