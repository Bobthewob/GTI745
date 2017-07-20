using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadialMenuSpawner : MonoBehaviour {
    
    public static RadialMenuSpawner instance;
    public RadialMenu menuPrefab;

    private void Awake()
    {
        instance = this;
    }

    public void SpawnMenu(Interactable obj)
    {
        RadialMenu newMenu = Instantiate(menuPrefab) as RadialMenu;
        newMenu.transform.SetParent(transform, false);
        newMenu.transform.position = Input.mousePosition;
        newMenu.SpawnButtons(obj);
        newMenu.label.text = obj.title.ToUpper();
    }
}
