using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseUp()
    {
        //Manager.Instance.selectedCube = this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube;
        MusicTest musicScript = GameObject.Find("MusicSource").GetComponent<MusicTest>();
        //if ((int)musicScript.PlaySong == -1)
        //musicScript.PlaySong = MusicTest.MusicPlayer.Melodies2D;
        musicScript.startMusicMelodies2D(this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube);
    }
}
