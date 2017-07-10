using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicTest : MonoBehaviour {

    public int PlaySong = -1; // -1 -> do not play ; 0 -> play selected ; 1 -> play all
    private List<AudioSource> sources = new List<AudioSource>();
    private int currentNoteIndex = 0;
    private float time = 0.25f;

    private bool firstRun = true;
	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

        if (PlaySong == 0) //2DSelected
        {

            time += Time.deltaTime;

            if (time > 0.25f)
            {

                sources.Add(gameObject.AddComponent<AudioSource>());
                var currentSource = sources[sources.Count - 1];
                int note = Manager.Instance.selectedCube.children[GameObject.Find("Dropdown").GetComponent<Dropdown>().value].partition[currentNoteIndex];

                time = 0;

                if (note != 255)
                {
                    Debug.Log("note played : " + note);

                    currentSource.clip = Resources.Load(note.ToString(), typeof(AudioClip)) as AudioClip;
                    currentSource.Play();

                    if (sources.Count >= 2)
                    {
                        var oldSource = sources[sources.Count - 2];
                        oldSource.Stop();
                    }
                }

                ++currentNoteIndex;

                if (currentNoteIndex == 80)
                {
                    PlaySong = -1;
                    currentNoteIndex = 0;
                    time = 0.25f;
                    sources.Clear();
                }
            }
        }
        else if (PlaySong == 1) //2DAll
        {

            time += Time.deltaTime;

            if (time > 0.25f)
            {
                time = 0;

                foreach (var melody in Manager.Instance.selectedCube.children)
                {
                    if (currentNoteIndex == 80)
                    {
                        PlaySong = -1;
                        currentNoteIndex = 0;
                        time = 0.25f;
                        sources = new List<AudioSource>();
                        break; // so we do not play the note at the index 0 of the other melodies
                    }
                    else
                    {
                        sources.Add(gameObject.AddComponent<AudioSource>());
                        var currentSource = sources[sources.Count - 1];
                        int note = melody.partition[currentNoteIndex];

                        if (note != 255)
                        {
                            Debug.Log("note played : " + note + " time : " + currentNoteIndex);

                            currentSource.clip = Resources.Load(note.ToString(), typeof(AudioClip)) as AudioClip;
                            currentSource.Play();

                            if (sources.Count > Manager.Instance.selectedCube.children.Count)
                            {
                                var oldSource = sources[sources.Count - (Manager.Instance.selectedCube.children.Count + 1)];
                                oldSource.Stop();
                            }
                        }
                    }
                }

                ++currentNoteIndex;
            }
        }
        else if (PlaySong == 2) { // allselected3D

            time += Time.deltaTime;

            if (time > 0.25f)
            {
                time = 0;
                foreach (var melody in Manager.Instance.selectedCube.children)
                {
                    if (currentNoteIndex == 80)
                    {
                        PlaySong = -1;
                        currentNoteIndex = 0;
                        time = 0.25f;
                        sources = new List<AudioSource>();
                        break; // so we do not play the note at the index 0 of the other melodies
                    }
                    else
                    {
                        sources.Add(gameObject.AddComponent<AudioSource>());
                        var currentSource = sources[sources.Count - 1];
                        int note = melody.partition[currentNoteIndex];

                        currentSource.transform.position = Manager.Instance.selectedCube.position;
                        Debug.Log(currentSource.transform.position.ToString());

                        if (note != 255)
                        {
                            Debug.Log("note played : " + note + " time : " + currentNoteIndex);

                            currentSource.clip = Resources.Load(note.ToString(), typeof(AudioClip)) as AudioClip;
                            currentSource.spatialBlend = 0.6f;
                            currentSource.dopplerLevel = 0;
                            currentSource.Play();

                            if (sources.Count > Manager.Instance.selectedCube.children.Count)
                            {
                                var oldSource = sources[sources.Count - (Manager.Instance.selectedCube.children.Count + 1)];
                                oldSource.Stop();
                            }
                        }
                    }
                }

                ++currentNoteIndex;
            }
        }
       
    }
}
