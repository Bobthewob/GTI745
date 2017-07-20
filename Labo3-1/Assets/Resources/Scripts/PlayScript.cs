using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GameObject glow = this.transform.parent.transform.parent.transform.Find("Glow Yellow").gameObject;

        if (this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().isPlaying)
        {
            //Component halo = this.transform.parent.transform.parent.gameObject.GetComponent("Halo");
            //halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
            glow.SetActive(true);
        }
        else
        {
            //Component halo = this.transform.parent.transform.parent.gameObject.GetComponent("Halo");
            //halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
            glow.SetActive(false);
        }
    }

    public void OnMouseUp()
    {
        //Manager.Instance.selectedCube = this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().cube;
        MusicTest musicScript = GameObject.Find("MusicSource").GetComponent<MusicTest>();
        //if ((int)musicScript.PlaySong == -1)
        //musicScript.PlaySong = MusicTest.MusicPlayer.Melodies2D;
        musicScript.startMusicMelodies2D(this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>());
    }

    public void OnMouseEnter()
    {
        Component halo = this.gameObject.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, true, null);
    }

    public void OnMouseExit()
    {
        Component halo = this.gameObject.GetComponent("Halo");
        halo.GetType().GetProperty("enabled").SetValue(halo, false, null);
    }
}
