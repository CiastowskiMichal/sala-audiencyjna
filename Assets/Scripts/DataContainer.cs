using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataContainer : MonoBehaviour
{
    private Item item = new Item(1, "", "");

    public GameObject panel;

    public void SetItem(Item item)
    {
        this.item.id = item.id;
        this.item.nazwa = item.nazwa;
        this.item.opis = item.opis;
    }
    public Item GetItem()
    {
        return item;
    }
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(pasteOnPanel);
        Debug.Log("start");
    }
    void pasteOnPanel()
    {
        
        foreach (Transform child in panel.transform)
        {
            Debug.Log(child.name);
            if (child.tag == "Szczegoly")
            {
                Debug.Log("paste");
                foreach (Transform grandchild in child.transform)
                {
                    Debug.Log(grandchild.name);
                    if (grandchild.tag == "Tytul")
                    {
                        grandchild.GetComponent<Text>().text = item.nazwa;
                    }
                    if (grandchild.tag == "Opis")
                    {
                        grandchild.GetComponent<Text>().text = item.opis;
                    }
                }
            }
        }
    }
}
