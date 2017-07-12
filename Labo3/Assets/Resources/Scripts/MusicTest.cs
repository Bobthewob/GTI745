using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MusicTest : MonoBehaviour {

	public MusicPlayer PlaySong = MusicPlayer.NotPlaying;
    private List<AudioSource> sources = new List<AudioSource>();
    private int currentNoteIndex = 0;
    private float time = 0.25f;
    private bool firstRun = true;

	public enum MusicPlayer{
		NotPlaying = -1,
		Melody2D = 0,
		Melodies2D = 1,
		All3D = 2
	}

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		switch (PlaySong) //2DSelected
        {
			case MusicPlayer.Melody2D:
				time += Time.deltaTime;

				if (time > 0.25f) {

					sources.Add (gameObject.AddComponent<AudioSource> ());
					var currentSource = sources [sources.Count - 1];
					int note = Manager.Instance.selectedCube.children [GameObject.Find ("Dropdown").GetComponent<Dropdown> ().value].partition [currentNoteIndex];

					time = 0;

					if (note != 255) {
						Debug.Log ("note played : " + note);

						currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
						currentSource.Play ();

						if (sources.Count >= 2) {
							var oldSource = sources [sources.Count - 2];
							oldSource.Stop ();
						}
					}

					++currentNoteIndex;

					if (currentNoteIndex == 80) {
						PlaySong = MusicPlayer.NotPlaying;
						currentNoteIndex = 0;
						time = 0.25f;
						sources.Clear ();
					}
				}
			break;
		case MusicPlayer.Melodies2D:

			time += Time.deltaTime;

			if (time > 0.25f) {
				time = 0;

				foreach (var melody in Manager.Instance.selectedCube.children) {
					if (currentNoteIndex == 80) {
						PlaySong = MusicPlayer.NotPlaying;
						currentNoteIndex = 0;
						time = 0.25f;
						sources = new List<AudioSource> ();
						break; // so we do not play the note at the index 0 of the other melodies
					} else {
						sources.Add (gameObject.AddComponent<AudioSource> ());
						var currentSource = sources [sources.Count - 1];
						int note = melody.partition [currentNoteIndex];

						if (note != 255) {
							Debug.Log ("note played : " + note + " time : " + currentNoteIndex);

							currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
							currentSource.Play ();

							if (sources.Count > Manager.Instance.selectedCube.children.Count) {
								var oldSource = sources [sources.Count - (Manager.Instance.selectedCube.children.Count + 1)];
								oldSource.Stop ();
							}
						}
					}
				}

				++currentNoteIndex;
			}
			break;
		case MusicPlayer.All3D:	
			
			time += Time.deltaTime; //delta time is the time in second between 2 frames
			int totalMelodiesCount = Manager.Instance.rootCubes.Sum(x => x.children.Count());

			if (time > 0.25f) {
				time = 0;

				foreach (var cubes in Manager.Instance.rootCubes) {					
					foreach (var melody in cubes.children) {
							if (currentNoteIndex == 80) {
								PlaySong = MusicPlayer.NotPlaying;
								currentNoteIndex = 0;
								time = 0.25f;
								sources = new List<AudioSource> ();
								break; // so we do not play the note at the index 0 of the other melodies
							} else {
								sources.Add (gameObject.AddComponent<AudioSource> ());
								var currentSource = sources [sources.Count - 1];
								int note = melody.partition [currentNoteIndex];

								currentSource.transform.position = cubes.position;
								Debug.Log (currentSource.transform.position.ToString ());

								if (note != 255) {
									Debug.Log ("note played : " + note + " time : " + currentNoteIndex);

									currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
									currentSource.spatialBlend = 1f;
									currentSource.dopplerLevel = 0;
									currentSource.Play ();

									if (sources.Count > totalMelodiesCount) {
										var oldSource = sources [sources.Count - (totalMelodiesCount + 1)];
										oldSource.Stop ();
									}
								}
							}
						}

						++currentNoteIndex;
					}
				}
			break;
        }    
    }
}