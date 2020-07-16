using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabController : MonoBehaviour
{
    [SerializeField]
    Button[] btns = new Button[5];
    [SerializeField]
    GameObject[] cnvs = new GameObject[5];
    [SerializeField]
    GameObject panelCanvas;
    [SerializeField]
    GameObject buttonCanvas;

    // Start is called before the first frame update
    void Start()
    {
        btns[0].onClick.AddListener(delegate {LoadPanel(0);});
        btns[1].onClick.AddListener(delegate {LoadPanel(1);});
        btns[2].onClick.AddListener(delegate {LoadPanel(2);});
        btns[3].onClick.AddListener(delegate {LoadPanel(3);});
        btns[4].onClick.AddListener(delegate {LoadPanel(4);});

        LoadPanel(0);

    }

    // Update is called once per frame
    void Update()
    {

    }
    void LoadPanel(int i)
    {
        foreach (Transform childPanels in panelCanvas.transform)
        {
            childPanels.localPosition = new Vector2(0,720);
        }
        btns[i].GetComponent<Button>().Select();   
        //Debug.Log(i);
        cnvs[i].transform.localPosition = new Vector2(0,0);
    }
}
