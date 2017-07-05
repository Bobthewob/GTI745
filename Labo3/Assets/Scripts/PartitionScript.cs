using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartitionScript : MonoBehaviour {

    public GameObject Tooltip;

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

            for (int i = 0; i < nbLine; i++)
            {
                if (betweenY(position, (int)partition.position.y + i*stepY + 5, (int)partition.position.y + (i+1)*stepY + 5) ||
					betweenY(position, (int)partition1.position.y + i*stepY + 5, (int)partition1.position.y + (i+1)*stepY + 5))
                {
					Tooltip.GetComponentInChildren<Text>().text = notes[i % 7];
                    Tooltip.GetComponent<Transform>().position = new Vector3(position.x + 50, position.y + 50, 0);

					if (betweenY (position, (int)partition.position.y + i * stepY + 5, (int)partition.position.y + (i + 1) * stepY + 5)) {
						magnetYPosition = (int)partition.position.y + i * stepY - 1;
						break;
					} else if (betweenY (position, (int)partition1.position.y + i * stepY + 5, (int)partition1.position.y + (i + 1) * stepY + 5)) {
						magnetYPosition = (int)partition1.position.y + i * stepY - 1;
						break;
					}
                }
            }
			//Debug.Log("1::: mouse y pos : " + position.ToString() + " y1: " + (int)partition.position.y + " y2: "+ (int)partition.position.y + (21 * step));
			//Debug.Log("2::: mouse y pos : " + position.ToString() + " y1: " + (int)partition1.position.y + " y2: "+ (int)partition1.position.y + (21 * step));
        	
			if (Input.GetMouseButtonDown (0)) {
				for (int i = 0; i < nbcolumn; i++) {
					if (betweenX (position, (int)partition.position.x + i * stepX + 18.75f, (int)partition.position.x + (i + 1) * stepX + 18.75f) ||
					    betweenX (position, (int)partition1.position.x + i * stepX + 18.75f, (int)partition1.position.x + (i + 1) * stepX + 18.75f)) {
						var newNote = (GameObject)Instantiate(Resources.Load("Note"));

						newNote.transform.position = new Vector3 ((int)partition1.position.x + i * stepX + 18.75f, magnetYPosition, 0);
						newNote.transform.SetParent (GameObject.Find ("Canvas").transform, false);
						newNote.transform.SetAsLastSibling ();
					}
				}
			}
		}
        else {
            Tooltip.SetActive(false);
        }
	}

    bool betweenY (Vector3 position, int y1, int y2)
    {
        return (position.y > y1) && (position.y < y2);
    }

	bool betweenX (Vector3 position, float x1, float x2)
	{
		return (position.x > x1) && (position.x < x2);
	}
}
