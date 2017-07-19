using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class MusicTest : MonoBehaviour {

	public MusicPlayer PlaySong = MusicPlayer.NotPlaying;
    private List<AudioSource> sources = new List<AudioSource>();
    private List<GameObject> sources3D = new List<GameObject>();
    private int currentNoteIndex = 0;
    private float time = 0.25f;
    private bool firstRun = true;
    private CubeParent star;

	public enum MusicPlayer{
		NotPlaying = -1,
		Melody2D = 0,
		Melodies2D = 1,
		All3D = 2,
        Single3D = 3
	}

	// Use this for initialization
	void Start () {
		
	}

    public void startMusicMelodies2D(CubeParent star)
    {
        this.star = star;
        startMusic(MusicPlayer.Single3D);
    }

    public void startMusicAll3D()
    {  
        startMusic(MusicPlayer.All3D);
    }

    public void startMusic(MusicPlayer type)
    {
        StopMusic();
        this.PlaySong = type;
    }

    //stopMusicFromPlaying
    public void StopMusic() {
        PlaySong = MusicPlayer.NotPlaying;
        sources.Clear();
        firstRun = true;
        currentNoteIndex = 0;
        time = 0.25f;
        foreach (var source3D in sources3D)
        {
            GameObject.Destroy(source3D);
        }
        sources3D.Clear();
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
                        StopMusic();
                    }
				}
			break;
		case MusicPlayer.Melodies2D:

			time += Time.deltaTime;

			if (time > 0.25f) {
				time = 0;

				foreach (var melody in star) {
					if (currentNoteIndex == 80) {
                        StopMusic();
                        break; // so we do not play the note at the index 0 of the other melodies
					} else {
						sources.Add (gameObject.AddComponent<AudioSource> ());
						var currentSource = sources [sources.Count - 1];
						int note = melody.partition [currentNoteIndex];
                        
						if (note != 255) {
							Debug.Log ("note played : " + note + " time : " + currentNoteIndex);

							currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
							currentSource.Play ();

							if (sources.Count > star.children.Count) {
								var oldSource = sources [sources.Count - (star.children.Count + 1)];
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
                                    //int totalMelodiesCount = Manager.Instance.rootCubes.Sum(x => x.children.Count());

            if (time > 0.25f) {
				time = 0;
                List<MelodyWithPosition> allmelodies = new List<MelodyWithPosition>();
                
                foreach (var cubes in Manager.Instance.rootCubes)
                {
                        foreach (var melody in cubes.children)
                        {
                            var newMelo = new MelodyWithPosition(melody, cubes.star);
                            allmelodies.Add(newMelo);
                        }
                }
  		
				foreach (var melodyWithPosition in allmelodies) {
						if (currentNoteIndex == 80) {
                            StopMusic();
                            break; // so we do not play the note at the index 0 of the other melodies
						} else {
                            
                            int note = melodyWithPosition.melody.partition [currentNoteIndex];

							if (note != 255) {
                                //Debug.Log ("note played : " + melodyWithPosition.melody.partition[currentNoteIndex] + " position : " + melodyWithPosition.position);

                                var currentNote3D = Instantiate(Resources.Load("note3D", typeof(GameObject))) as GameObject;
                                var currentSource = currentNote3D.GetComponent<AudioSource>();
                                sources3D.Add(currentNote3D);
                                currentSource.transform.position = melodyWithPosition.position.transform.position;

                                currentSource.clip = Resources.Load (note.ToString (), typeof(AudioClip)) as AudioClip;
								currentSource.spatialBlend = 1f;
								currentSource.dopplerLevel = 0;
								currentSource.Play ();

								if (sources3D.Count > allmelodies.Count()) {
									var oldSource = sources3D[sources3D.Count - (allmelodies.Count() + 1)];
									oldSource.GetComponent<AudioSource>().Stop ();
								}
							}
						}
					}

					++currentNoteIndex;
				}
			    break;
            case MusicPlayer.Single3D:

                time += Time.deltaTime; //delta time is the time in second between 2 frames
                                        //int totalMelodiesCount = Manager.Instance.rootCubes.Sum(x => x.children.Count());

                if (time > 0.25f)
                {
                    time = 0;
                    List<MelodyWithPosition> allmelodies = new List<MelodyWithPosition>();

                    foreach (var melody in star.children)
                    {
                        var newMelo = new MelodyWithPosition(melody, star.star);
                        allmelodies.Add(newMelo);
                    }

                    foreach (var melodyWithPosition in allmelodies)
                    {
                        if (currentNoteIndex == 80)
                        {
                            StopMusic();
                            break; // so we do not play the note at the index 0 of the other melodies
                        }
                        else
                        {

                            int note = melodyWithPosition.melody.partition[currentNoteIndex];

                            if (note != 255)
                            {
                                //Debug.Log ("note played : " + melodyWithPosition.melody.partition[currentNoteIndex] + " position : " + melodyWithPosition.position);

                                var currentNote3D = Instantiate(Resources.Load("note3D", typeof(GameObject))) as GameObject;
                                var currentSource = currentNote3D.GetComponent<AudioSource>();
                                sources3D.Add(currentNote3D);
                                currentSource.transform.position = melodyWithPosition.position.transform.position;

                                currentSource.clip = Resources.Load(note.ToString(), typeof(AudioClip)) as AudioClip;
                                currentSource.spatialBlend = 1f;
                                currentSource.dopplerLevel = 0;
                                currentSource.Play();

                                if (sources3D.Count > allmelodies.Count())
                                {
                                    var oldSource = sources3D[sources3D.Count - (allmelodies.Count() + 1)];
                                    oldSource.GetComponent<AudioSource>().Stop();
                                }
                            }
                        }
                    }

                    ++currentNoteIndex;
                }
                break;
        }    
    }
}

struct MelodyWithPosition {
    public CubeChildren melody;
    public GameObject position;

    public MelodyWithPosition(CubeChildren melody, GameObject star) {
        this.melody = melody;
        this.position = star;
    }
}