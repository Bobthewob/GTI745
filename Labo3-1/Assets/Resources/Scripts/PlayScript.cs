using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class PlayScript : MonoBehaviour {

    private Light light;
    private GlowScript glowPlay;

    // Use this for initialization
    void Start () {
        glowPlay = this.transform.Find("Glow Play").GetComponent<GlowScript>();
    }
	
	// Update is called once per frame
	void Update () {
        /*GlowScript glow = this.transform.parent.transform.parent.transform.Find("Glow Yellow").GetComponent<GlowScript>();

        if (this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>().isPlaying)
        {
            glow.EnableLight();
        }
        else
        {
            glow.DisenableLight();
        }*/
    }

    public void OnMouseUp()
    {
        if (Manager.Instance.cursorType == cursorType.FreeView && !Manager.Instance.transitionIn)
        {
            MusicTest musicScript = GameObject.Find("MusicSource").GetComponent<MusicTest>();
            musicScript.startMusicMelodies2D(this.transform.parent.transform.parent.gameObject.GetComponent<cubeScript>());
        }
    }

    public void OnMouseEnter()
    {
        if (Manager.Instance.cursorType == cursorType.FreeView && !Manager.Instance.transitionIn)
            glowPlay.EnableLight();
    }

    public void OnMouseExit()
    {
        glowPlay.DisenableLight();
    }

    public void UpdateLight()
    {
        glowPlay.changeRange();
    }
}
