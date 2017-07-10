using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    private static object _lock = new object();

    public static T Instance
    {
        get
        {
            if (applicationIsQuitting)
            {
                Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                    "' already destroyed on application quit." +
                    " Won't create again - returning null.");
                return null;
            }

            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = (T)FindObjectOfType(typeof(T));

                    if (FindObjectsOfType(typeof(T)).Length > 1)
                    {
                        Debug.LogError("[Singleton] Something went really wrong " +
                            " - there should never be more than 1 singleton!" +
                            " Reopening the scene might fix it.");
                        return _instance;
                    }

                    if (_instance == null)
                    {
                        GameObject singleton = new GameObject();
                        _instance = singleton.AddComponent<T>();
                        singleton.name = "(singleton) " + typeof(T).ToString();

                        DontDestroyOnLoad(singleton);

                        Debug.Log("[Singleton] An instance of " + typeof(T) +
                            " is needed in the scene, so '" + singleton +
                            "' was created with DontDestroyOnLoad.");
                    }
                    else
                    {
                        Debug.Log("[Singleton] Using instance already created: " +
                            _instance.gameObject.name);
                    }
                }

                return _instance;
            }
        }
    }

    private static bool applicationIsQuitting = false;
    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed, 
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    public void OnDestroy()
    {
        applicationIsQuitting = true;
    }
}

public enum cursorType
{
    FreeView,
    CreateCube,
    MergeCube,
	EditMelody,
	Play
}

public class Manager : Singleton<Manager>
{
    protected Manager() { } // guarantee this will be always a singleton only - can't use the constructor!

    public cursorType cursorType = cursorType.FreeView;
    public List<CubeParent> rootCubes = new List<CubeParent>();   
    public CubeParent selectedCube;                       //currently selected cube
    public int cubeUID = 0;
	public bool playSong = false;

    public string getUniqueCubeName() {
        return "Cube #" + ++cubeUID;
    }

	public void clearUINotes(){
		var UINotes = GameObject.FindGameObjectsWithTag ("Note");

		foreach (var UINote in UINotes) {
			GameObject.Destroy (UINote);
		}
	}

	public void loadUINotes(int index){
		var partition = selectedCube.children [index].partition;
		var stepY = 10;
		var stepX = 37.5f;
		var nbcolumn = 40;
		var partitionUI = GameObject.Find ("Partition").GetComponent<RectTransform> ().rect;
		partitionUI.position = GameObject.Find ("Partition").GetComponent<RectTransform> ().position;

		for (int i = 0; i < nbcolumn; i++) { //for each notes in partition 1
			if (partition [i] != 255) {
				float yPos = ((int)(partition [i] / 2) * stepY) + 654;
				float xPos = (int)partitionUI.position.x + (i * stepX) + 18.75f;

				var newNote = (GameObject)Instantiate (Resources.Load ("NoteSpriteObject"));
				newNote.transform.position = new Vector3 (xPos, yPos, 0);
				newNote.transform.SetParent (GameObject.Find ("Canvas2DPartition").transform, false);
				newNote.transform.SetAsLastSibling ();	
				newNote.name = "Note_" + i + "_1";
			}
		}
		for (int i = nbcolumn; i < nbcolumn * 2; i++) { //for each notes in partition 2
			if (partition [i] != 255) {
				Debug.Log ("UI partition 2 : " + partition [i]);

				int offset = 0;
				bool canHalfTone = true;
				int indexNote = partition [i] * 2;

				if ((int)(indexNote / 36) > 0)
					offset = 6;
				else if ((int)(indexNote / 29) > 0)
					offset = 5;
				else if ((int)(indexNote / 24) > 0)
					offset = 4;
				else if ((int)(indexNote / 17) > 0)
					offset = 3;
				else if ((int)(indexNote / 12) > 0)
					offset = 2;
				else if ((int)(indexNote / 5) > 0)
					offset = 1;

				indexNote = partition [i] + offset;
				indexNote = indexNote / 2;

				float yPos = ((int)(partition [i] / 2) * stepY) + 354;
				float xPos = (int)partitionUI.position.x + ((i - nbcolumn) * stepX) + 18.75f;

				var newNote = (GameObject)Instantiate (Resources.Load ("NoteSpriteObject"));
				newNote.transform.position = new Vector3 (xPos, yPos, 0);
				newNote.transform.SetParent (GameObject.Find ("Canvas2DPartition").transform, false);
				newNote.transform.SetAsLastSibling ();	
				newNote.name = "Note_" + (i - nbcolumn) + "_2";
			}
		}
	}
}

public class Cube
{

}

public class CubeParent : Cube, IEnumerable<CubeChildren> {
    public List<CubeChildren> children;
    public string name;

    public CubeParent(string name) {
        children = new List<CubeChildren>();
        this.name = name;
        children.Add(new CubeChildren(name));
    }

    public IEnumerator<CubeChildren> GetEnumerator()
    {
        return children.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}

public class CubeChildren : Cube {
    public int[] partition;
    public string name;

    public CubeChildren(string name)
    {
        partition = new int[80];
		for (int i = 0; i < 80; i++) {
			partition [i] = 255;
		}

        this.name = name;
    }
}

