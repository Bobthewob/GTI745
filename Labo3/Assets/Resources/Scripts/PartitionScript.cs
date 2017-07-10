using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartitionScript : MonoBehaviour {

    public GameObject Tooltip;
	public Dropdown melodies;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        var position = Input.mousePosition;
        var partition = GameObject.Find("Partition").GetComponent<RectTransform>().rect;
        partition.position = GameObject.Find("Partition").GetComponent<RectTransform>().position;

        var partition1 = GameObject.Find("Partition1").GetComponent<RectTransform>().rect;
        partition1.position = GameObject.Find("Partition1").GetComponent<RectTransform>().position;

        var nbLine = 21;
        var stepY = 10;
		var nbcolumn = 40;
		var stepX = 37.5f;

		string[] notes = {"Do - C", "Ré - D", "Mi - E", "Fa - F", "Sol - G", "La - A", "Si - B" };

        
        if (partition.Contains(Input.mousePosition) || partition1.Contains(Input.mousePosition))
        {
            Tooltip.SetActive(true);
			int magnetYPosition = 0;
			int partitionIn = -1;

            for (int i = 0; i < nbLine; i++)
            {
                if (betweenY(position, (int)partition.position.y + i*stepY + 5, (int)partition.position.y + (i+1)*stepY + 5) ||
					betweenY(position, (int)partition1.position.y + i*stepY + 5, (int)partition1.position.y + (i+1)*stepY + 5))
                {
					Tooltip.GetComponentInChildren<Text>().text = notes[i % 7];
                    Tooltip.GetComponent<Transform>().position = new Vector3(position.x + 50, position.y + 50, 0);	

					if (betweenY (position, (int)partition.position.y + i * stepY + 5, (int)partition.position.y + (i + 1) * stepY + 5)) {
						magnetYPosition = (int)partition.position.y + i * stepY - 1;
						partitionIn = 1;
						break;
					} else if (betweenY (position, (int)partition1.position.y + i * stepY + 5, (int)partition1.position.y + (i + 1) * stepY + 5)) {
						magnetYPosition = (int)partition1.position.y + i * stepY - 1;
						partitionIn = 2;
						break;
					}
                }
            }

			bool demiTone = false;

			if (Input.GetMouseButton (2)) {
				demiTone = true;
			}
			
			if (Input.GetMouseButton (0)) {
				if (partitionIn != -1) {	//just to be sure we don't have unwanted behaviour
					if (partitionIn == 1) {
						for (int i = 0; i < nbcolumn; i++) { //partition1
							if (betweenX (position, partition.position.x + (i * stepX) + 18.75f, partition.position.x + ((i + 1) * stepX) + 18.75f)) {
							
								var oldNote = GameObject.Find ("Note_" + i + "_" + partitionIn);
								if (oldNote != null) {
									GameObject.Destroy (oldNote);
								}

								int indexNote = 0;

								indexNote = (int)((magnetYPosition - 654) / stepY);

								int offset = 0;
								bool canHalfTone = true;
								indexNote = indexNote * 2;

								if ((int)(indexNote / 42) > 0)
									offset = 6;
								else if ((int)(indexNote / 34) > 0)
									offset = 5;
								else if ((int)(indexNote / 28) > 0)
									offset = 4;
								else if ((int)(indexNote / 20) > 0)
									offset = 3;
								else if ((int)(indexNote / 14) > 0)
									offset = 2;
								else if ((int)(indexNote / 6) > 0)
									offset = 1;

								Debug.Log ("note : " + indexNote + ", offset : " + offset);

								if (indexNote == 4 || indexNote == 12 || indexNote == 18 || indexNote == 26 || indexNote == 32 || indexNote == 40) //mi et si peuvent pas etre dieser
								canHalfTone = false;

								indexNote -= offset;



                                if (demiTone && canHalfTone)
                                { //tabarnak de criss de marde pk les input sont pas detect

                                    Debug.Log("DemiTone");
                                    ++indexNote;

                                    var newNote = (GameObject)Instantiate(Resources.Load("noteSpriteDemiToneObject"));
                                    newNote.transform.position = new Vector3((int)partition.position.x + (i * stepX) + 18.75f, magnetYPosition, 0);
                                    newNote.transform.SetParent(GameObject.Find("Canvas2DPartition").transform, false);
                                    newNote.transform.SetAsLastSibling();
                                    newNote.name = "Note_" + i + "_" + partitionIn;

                                }
                                else {
                                    var newNote = (GameObject)Instantiate(Resources.Load("NoteSpriteObject"));
                                    newNote.transform.position = new Vector3((int)partition.position.x + (i * stepX) + 18.75f, magnetYPosition, 0);
                                    newNote.transform.SetParent(GameObject.Find("Canvas2DPartition").transform, false);
                                    newNote.transform.SetAsLastSibling();
                                    newNote.name = "Note_" + i + "_" + partitionIn;
                                }

								//Debug.Log(indexNote);

								Manager.Instance.selectedCube.children [melodies.value].partition [i] = indexNote;
								//Debug.Log ("Note_" + i + "_" + partitionIn + ", value : " + indexNote + ", magnetpos : " + magnetYPosition);

							}	
						}
					} else {
						for (int i = nbcolumn; i < nbcolumn * 2; i++) { //partition2
							//Debug.Log ("pos : " + position.ToString() + " , " + (partition.position.x + ((i - nbcolumn) * stepX) + 18.75f) + " , " + (partition.position.x + (((i - nbcolumn) + 1) * stepX) + 18.75f));
							if (betweenX (position, partition.position.x + ((i - nbcolumn) * stepX) + 18.75f, partition.position.x + (((i - nbcolumn) + 1) * stepX) + 18.75f)) {

								var oldNote = GameObject.Find ("Note_" + (i - nbcolumn) + "_" + partitionIn);
								if (oldNote != null) {
									GameObject.Destroy (oldNote);
								}

								int indexNote = 0;

								indexNote = (int)((magnetYPosition - 354) / stepY);

								int offset = 0;
								bool canHalfTone = true;
								indexNote = indexNote * 2;

								if ((int)(indexNote / 42) > 0) //do function
									offset = 6;
								else if ((int)(indexNote / 34) > 0)
									offset = 5;
								else if ((int)(indexNote / 28) > 0)
									offset = 4;
								else if ((int)(indexNote / 20) > 0)
									offset = 3;
								else if ((int)(indexNote / 14) > 0)
									offset = 2;
								else if ((int)(indexNote / 6) > 0)
									offset = 1;

								Debug.Log ("note : " + indexNote + ", offset : " + offset);

								if (indexNote == 4 || indexNote == 12 || indexNote == 18 || indexNote == 26 || indexNote == 32 || indexNote == 40) //mi et si peuvent pas etre dieser
								canHalfTone = false;

								indexNote -= offset;

                                if (demiTone && canHalfTone)
                                { //tabarnak de criss de marde pk les input sont pas detect

                                    Debug.Log("DemiTone");
                                    ++indexNote;

                                    var newNote = (GameObject)Instantiate(Resources.Load("NoteSpriteDemiToneObject"));
                                    newNote.transform.position = new Vector3((int)partition.position.x + ((i - nbcolumn) * stepX) + 18.75f, magnetYPosition, 0);
                                    newNote.transform.SetParent(GameObject.Find("Canvas2DPartition").transform, false);
                                    newNote.transform.SetAsLastSibling();
                                    newNote.name = "Note_" + (i - nbcolumn) + "_" + partitionIn;

                                }
                                else
                                {
                                    var newNote = (GameObject)Instantiate(Resources.Load("NoteSpriteObject"));
                                    newNote.transform.position = new Vector3((int)partition.position.x + ((i - nbcolumn) * stepX) + 18.75f, magnetYPosition, 0);
                                    newNote.transform.SetParent(GameObject.Find("Canvas2DPartition").transform, false);
                                    newNote.transform.SetAsLastSibling();
                                    newNote.name = "Note_" + (i - nbcolumn) + "_" + partitionIn;
                                }

                                //Debug.Log(indexNote);

                                Manager.Instance.selectedCube.children [melodies.value].partition [i] = indexNote;
								//Debug.Log ("Note_" + i + "_" + partitionIn + ", value : " + indexNote + ", magnetpos : " + magnetYPosition);
							}
						}
					}

					Tooltip.transform.SetAsLastSibling ();
				}
			}
		}
        else {
            Tooltip.SetActive(false);
        }
	}

    bool betweenY (Vector3 position, int y1, int y2)
    {
        return (position.y >= y1) && (position.y < y2);
    }

	bool betweenX (Vector3 position, float x1, float x2)
	{
		return (position.x >= x1) && (position.x < x2);
	}
}
