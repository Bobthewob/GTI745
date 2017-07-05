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
        var step = -20;
        string[] notes = { "Si - B", "La - A", "Sol - G", "Fa - F", "Mi - E", "Ré - D", "Do - C" };

        Debug.Log("mouse : " + position.ToString());
        if (partition.Contains(Input.mousePosition) || partition1.Contains(Input.mousePosition))
        {
            Tooltip.SetActive(true);
            Debug.Log("TESTESTEST");
            Debug.Log(partition.position.ToString());
            for (int i = 0; i < nbLine; i++)
            {
                if (between(position, (int)partition.position.y + i*step, (int)partition.position.y + (i+1)*step))
                {
                    Tooltip.GetComponent<Text>().text = notes[i%7];
                    Tooltip.GetComponent<Transform>().position = new Vector3(position.x + 50, position.y + 50, 0);
                }
            }
        }
        else {
            Tooltip.SetActive(false);
        }
	}

    bool between (Vector3 position, int y1, int y2)
    {
        return (position.y > y1) && (position.y < y2);
    }
}
