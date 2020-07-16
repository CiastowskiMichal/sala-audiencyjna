using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectingData : MonoBehaviour
{
    [SerializeField]
    GameObject data1;
    private bool czyInterakcja = false;
    public float interactionDistance = 3f;
    public DziennikList DziennikList = new DziennikList();
    public EkwipunekList EkwipunekList = new EkwipunekList();
    public WskazowkaList WskazowkaList = new WskazowkaList();
    public CiekawostkaList CiekawostkaList = new CiekawostkaList();
    [SerializeField]
    GameObject cam;
    [SerializeField]
    Camera cam2;
    [SerializeField]
    Camera cam3;
    [SerializeField]
    GameObject LuteCameraController;
    [SerializeField]
    GameObject LuteSystem;
    [SerializeField]
    GameObject safeCollider;
    public bool playingLute;
    public bool safePuzzle;
    [SerializeField]
    GameObject GuitarCollider;
    [SerializeField]
    Text interactText;
    [SerializeField]
    Text interactText2;
    private float counterForText = 0f;
    [SerializeField]
    Image cursor;
    [SerializeField]
    GameObject DialogPanel;
    private bool dialogSwitch = false;

    public List<Item> Przedmioty = new List<Item>();
    // public List<Item> Akcje = new List<Item>();
    // public List<Item> Ciekawostki = new List<Item>();
    // public List<Item> Wskazowki = new List<Item>();
    // private List<Item> tempList;
    GameObject luteController;
    void Start()
    {
        luteController = GameObject.Find("SoundControl");
        luteController.GetComponent<GameController>().DisableStrings();
        playingLute = false;
        safePuzzle = false;
        cam.GetComponent<Camera>().enabled = true;
        cam2.enabled = false;
        cam3.enabled = false;
        cursor.enabled = false;
        LuteSystem.transform.localPosition = new Vector3(LuteSystem.transform.localPosition.x, LuteSystem.transform.localPosition.y - 2f, LuteSystem.transform.localPosition.z);
        //LuteSystem.transform.position = new Vector3(-4.5f, -2f, 14.5f);

        // Item testItem;
        // testItem = GetData(6, "Ekwipunek");
        // InstatiateButtonOnList(testItem, "Ekwipunek");
        // Przedmioty.Add(testItem);

        // for (int i = 1; i <= 14; i++)
        // {
        //     testItem = GetData(i, "Dziennik");
        //     InstatiateButtonOnList(testItem, "Dziennik");
        // }

        // for (int i = 1; i <= 7; i++)
        // {
        //     testItem = GetData(i, "Ekwipunek");
        //     InstatiateButtonOnList(testItem, "Ekwipunek");
        // }
        // for (int i = 1; i <= 14; i++)
        // {
        //     testItem = GetData(i, "Wskazowka");
        //     InstatiateButtonOnList(testItem, "Wskazowka");
        // }
        // for (int i = 11; i <= 14; i++)
        // {
        //     testItem = GetData(i, "Ciekawostka");
        //     InstatiateButtonOnList(testItem, "Ciekawostka");
        // }
        // for (int i = 21; i <= 23; i++)
        // {
        //     testItem = GetData(i, "Ciekawostka");
        //     InstatiateButtonOnList(testItem, "Ciekawostka");
        // }
    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {

        if (playingLute || safePuzzle)
        {
            cursor.enabled = true;
        }
        else
        {
            cursor.enabled = false;
        }
        //Debug.Log(counterForText.ToString("f3"));
        if (counterForText >= 0)
        {
            counterForText -= Time.deltaTime;
            interactText2.color = new Vector4(1, 1, 1, counterForText);
        }
        RaycastHit hit;

        Debug.DrawRay(cam.transform.position, cam.transform.forward * interactionDistance);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionDistance))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Interakcja" || hit.transform.tag == "GuitarCollider" || hit.transform.tag == "SejfCollider")
            {
                interactText.color = new Vector4(1, 1, 1, Mathf.Lerp(interactText.color.a, 1, 0.4f));
            }
            else interactText.color = new Vector4(1, 1, 1, Mathf.Lerp(interactText.color.a, 0, 0.4f));
        }
        else interactText.color = new Vector4(1, 1, 1, Mathf.Lerp(interactText.color.a, 0, 0.4f));



        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionDistance) && Input.GetButtonDown("Interact") && !GetComponent<Walking>().showMenu)
        {
            if (hit.transform.tag == "Interakcja")
            {
                //string ktoraLista;
                // switch (hit.transform.GetComponent<Interaction>().getCategory())
                // {
                //     case "Dziennik":
                //         tempList = Akcje;
                //         break;

                //     case "Ekwipunek":
                //         tempList = Przedmioty;
                //         break;

                //     case "Wskazowka":
                //         tempList = Wskazowki;
                //         break;

                //     case "Ciekawostka":
                //         tempList = Ciekawostki;
                //         break;

                //     default:
                //         break;
                // }
                //Debug.Log(tempList.Count);
                hit.transform.GetComponent<Interaction>().Interact(Przedmioty);
                Item item = GetData(hit.transform.GetComponent<Interaction>().getId(), hit.transform.GetComponent<Interaction>().getCategory());
                // if (!checkPrzedmioty(tempList, hit.transform.GetComponent<Interaction>().getId()))
                // {
                InstatiateButtonOnList(item, hit.transform.GetComponent<Interaction>().getCategory());
                if (hit.transform.GetComponent<Interaction>().getCategory() == "Ekwipunek")
                {

                    Przedmioty.Add(item);
                    if (item.id == 2)
                    {
                        dialogSwitch = true;
                    }
                }

                // if (hit.transform.GetComponent<Interaction>().getCategory() != "Ekwipunek")
                // {    
                //     tempList.Add(item);
                // }
                // }
                if (hit.transform.GetComponent<Interaction>().getCategory() == "Wskazowka")
                {
                    interactText2.text = item.opis;
                    counterForText = 3;
                    //interactText2.color = new Vector4(1,1,1,counterForText);
                }
                if (hit.transform.GetComponent<Interaction>().getCategory() == "Ekwipunek")
                {
                    interactText2.text = "Zdobyto: " + item.nazwa;
                    counterForText = 3;
                }
                hit.transform.GetComponent<Interaction>().CheckDestroy();
                //Debug.Log(tempList.Count);
            }
            if (hit.transform.name == "Dworzanin_Kolizja")
            {
                if (!dialogSwitch)
                {
                    GameObject instance = Instantiate(DialogPanel);
                    instance.GetComponent<DialogController>().setSubtitle("Zaginęła jedna z ksiąg ksiązecej biblioteki. Jeśli uda Ci sie ją znaleźć, otrzymasz ode mnie klucz do sali audiencyjnej");
                    instance.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
                }
                else
                {
                    GameObject instance = Instantiate(DialogPanel);
                    instance.GetComponent<DialogController>().setSubtitle("Świetnie, udało Ci sie go zdobyć. Proszę, oto klucz. Gdy uda Ci się otworzyć biblioteczkę, umieść księgę na miejsce.");
                    instance.transform.SetParent(GameObject.FindGameObjectWithTag("MainCanvas").transform, false);
                    GameObject.Find("Dworzanin_Model").GetComponent<Animator>().SetBool("completed",true);
                }
            }
        }

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, interactionDistance) && Input.GetButtonDown("Interact") && !GetComponent<Walking>().showMenu)
        {
            if (hit.transform.tag == "GuitarCollider")
            {
                //Debug.Log("Lutnia");
                cam.GetComponent<Camera>().enabled = false;
                luteController.GetComponent<GameController>().EnableStrings();
                LuteSystem.transform.localPosition = new Vector3(LuteSystem.transform.localPosition.x, LuteSystem.transform.localPosition.y + 2f, LuteSystem.transform.localPosition.z);
                cam2.enabled = true;
                playingLute = true;
                GuitarCollider.GetComponent<BoxCollider>().enabled = false;
                LuteCameraController.transform.localRotation = Quaternion.Euler(0, 5, 0);
                cam2.transform.localRotation = Quaternion.Euler(16, 0, 0);
            }
            if (hit.transform.tag == "SejfCollider")
            {
                cam.GetComponent<Camera>().enabled = false;
                safeCollider.transform.localPosition = new Vector3(safeCollider.transform.localPosition.x, safeCollider.transform.localPosition.y + 2, safeCollider.transform.localPosition.z);
                cam3.enabled = true;
                safePuzzle = true;
            }
        }
        if (playingLute && Input.GetButtonDown("Back"))
        {
            playingLute = false;
            luteController.GetComponent<GameController>().DisableStrings();
            cam.GetComponent<Camera>().enabled = true;
            cam2.enabled = false;
            LuteSystem.transform.localPosition = new Vector3(LuteSystem.transform.localPosition.x, LuteSystem.transform.localPosition.y - 2f, LuteSystem.transform.localPosition.z);
            GuitarCollider.GetComponent<BoxCollider>().enabled = true;
        }
        if (safePuzzle && Input.GetButtonDown("Back"))
        {
            safePuzzle = false;
            cam.GetComponent<Camera>().enabled = true;
            safeCollider.transform.localPosition = new Vector3(safeCollider.transform.localPosition.x, safeCollider.transform.localPosition.y - 2, safeCollider.transform.localPosition.z);
            cam3.enabled = false;

        }
    }
    public void InstatiateButtonOnList(Item item, string category)
    {
        GameObject newdata = Instantiate(data1);
        //Debug.Log("Lista: " + GameObject.FindGameObjectWithTag("DziennikLista"));
        switch (category)
        {
            case "Dziennik":
                newdata.transform.SetParent(GameObject.FindGameObjectWithTag("DziennikLista").transform, false);
                newdata.GetComponent<DataContainer>().SetItem(item);
                newdata.GetComponent<DataContainer>().panel = GameObject.FindGameObjectWithTag("Dziennik");
                newdata.GetComponent<Text>().text = item.nazwa;
                break;

            case "Ekwipunek":
                newdata.transform.SetParent(GameObject.FindGameObjectWithTag("EkwipunekLista").transform, false);
                newdata.GetComponent<DataContainer>().SetItem(item);
                newdata.GetComponent<DataContainer>().panel = GameObject.FindGameObjectWithTag("Ekwipunek");
                newdata.GetComponent<Text>().text = item.nazwa;
                break;

            case "Wskazowka":
                newdata.transform.SetParent(GameObject.FindGameObjectWithTag("WskazowkiLista").transform, false);
                newdata.GetComponent<DataContainer>().SetItem(item);
                newdata.GetComponent<DataContainer>().panel = GameObject.FindGameObjectWithTag("Wskazowki");
                newdata.GetComponent<Text>().text = item.nazwa;
                break;

            case "Ciekawostka":
                newdata.transform.SetParent(GameObject.FindGameObjectWithTag("CiekawostkiLista").transform, false);
                newdata.GetComponent<DataContainer>().SetItem(item);
                newdata.GetComponent<DataContainer>().panel = GameObject.FindGameObjectWithTag("Ciekawostki");
                newdata.GetComponent<Text>().text = item.nazwa;
                break;

            default:
                break;
        }
    }
    public Item GetData(int id, string category)
    {

        Item item = new Item(1, "", "");
        TextAsset asset = Resources.Load("Dane") as TextAsset;
        switch (category)
        {
            case "Dziennik":

                if (asset != null)
                {
                    DziennikList = JsonUtility.FromJson<DziennikList>(asset.text);
                    foreach (Dziennik dziennik in DziennikList.Dziennik)
                    {
                        //Debug.Log(dziennik.id + " - " + dziennik.nazwa + " - " + dziennik.opis);
                        if (dziennik.id == id)
                            item = new Item(dziennik.id, dziennik.nazwa, dziennik.opis);
                    }
                }
                else
                {
                    Debug.Log("asset is null");
                }
                break;

            case "Ekwipunek":
                //TextAsset asset = Resources.Load("Dane") as TextAsset;
                if (asset != null)
                {
                    EkwipunekList = JsonUtility.FromJson<EkwipunekList>(asset.text);
                    foreach (Ekwipunek ekwipunek in EkwipunekList.Ekwipunek)
                    {
                        //Debug.Log(dziennik.id + " - " + dziennik.nazwa + " - " + dziennik.opis);
                        if (ekwipunek.id == id)
                            item = new Item(ekwipunek.id, ekwipunek.nazwa, ekwipunek.opis);
                    }
                }
                else
                {
                    Debug.Log("asset is null");
                }
                break;

            case "Wskazowka":
                //TextAsset asset = Resources.Load("Dane") as TextAsset;
                if (asset != null)
                {
                    WskazowkaList = JsonUtility.FromJson<WskazowkaList>(asset.text);
                    foreach (Wskazowka wskazowka in WskazowkaList.Wskazowka)
                    {
                        //Debug.Log(dziennik.id + " - " + dziennik.nazwa + " - " + dziennik.opis);
                        if (wskazowka.id == id)
                            item = new Item(wskazowka.id, wskazowka.nazwa, wskazowka.opis);
                    }
                }
                else
                {
                    Debug.Log("asset is null");
                }
                break;

            case "Ciekawostka":
                //TextAsset asset = Resources.Load("Dane") as TextAsset;
                if (asset != null)
                {
                    CiekawostkaList = JsonUtility.FromJson<CiekawostkaList>(asset.text);
                    foreach (Ciekawostka ciekawostka in CiekawostkaList.Ciekawostka)
                    {
                        //Debug.Log(dziennik.id + " - " + dziennik.nazwa + " - " + dziennik.opis);
                        if (ciekawostka.id == id)
                            item = new Item(ciekawostka.id, ciekawostka.nazwa, ciekawostka.opis);
                    }
                }
                else
                {
                    Debug.Log("asset is null");
                }
                break;

            default:
                break;
        }

        return item;
    }
    private bool checkPrzedmioty(List<Item> list, int idSzukane)
    {
        bool czyJest = false;
        foreach (Item item in list)
        {
            if (item.id == idSzukane)
            {
                czyJest = true;
                break;
            }
            else
                czyJest = false;
        }
        return czyJest;
    }
}
