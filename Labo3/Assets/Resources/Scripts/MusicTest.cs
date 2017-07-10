using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTest : MonoBehaviour {

	private AudioClip[] allPitchs;
	private int currentNoteIndex = 0;
	private float time = 0.25f;
	private bool firstRun = true;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Manager.Instance.playSong) {
			var sources = new List<AudioSource> ();
			int oldNote = -1;
			time += Time.deltaTime;

			if (time > 0.25f) { 
				
				sources.Add (gameObject.AddComponent<AudioSource> ());
				var currentSource = sources [sources.Count - 1];
				int note = Manager.Instance.selectedCube.children [GameObject.Find("Dropdown").GetComponent<Dropdown>().value].partition [currentNoteIndex];

				time = 0;

				if (note != 255) {
					Debug.Log ("note played : " + note);

					currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
					currentSource.Play ();
				}
	
				if (sources.Count >= 2) {
					var oldSource = sources [sources.Count - 2];
					oldSource.Stop ();
				}
				++currentNoteIndex;
				//oldNote = note;

				if (currentNoteIndex == 80) {
					Manager.Instance.playSong = false;
					currentNoteIndex = 0;
				}
			} /*else if (time > 0.20f) { //make fade out to remove tiking sound
				int note = Manager.Instance.selectedCube.children [0].partition [currentNoteIndex];
				if (note != 255 && sources != null) {
					var currentSource = sources [sources.Count - 1];
					currentSource.volume = 0.5f;
				}
			}*/
		}
    }
}
