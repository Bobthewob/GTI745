using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class List : MonoBehaviour {

    public RectTransform myPanel;
    public GameObject myTextPrefab;
    private GameObject newText;
    // Use this for initialization
    void Start()
    {
		
    }
    // Update is called once per frame
    void Update()
    {
        var singleton = Manager.Instance;

        foreach (var item in GameObject.FindGameObjectsWithTag("ListItem"))
        {
            Destroy(item);
        }

        if (singleton.selectedCube == null)
        {
            foreach (var cube in singleton.rootCubes)
            {
                GameObject newText = (GameObject)Instantiate(myTextPrefab);
                newText.transform.SetParent(myPanel);
                newText.GetComponent<Text>().text = cube.name;
            }
        }
        else {
            foreach (var partition in singleton.selectedCube)
            {
                GameObject newText = (GameObject)Instantiate(myTextPrefab);
                newText.transform.SetParent(myPanel);
                newText.GetComponent<Text>().text = partition.name;
            }
        }
    }
}
