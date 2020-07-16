using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class Interaction : MonoBehaviour
{
    private bool sprawdzone = false;
    public enum RodzajInterakcji { Zamek, Kontener, Przedmiot }
    public enum ListaKategorii { Dziennik, Ekwipunek, Wskazowka, Ciekawostka };
    public enum PytanieTakNie { Tak, Nie, Nic };

    [Header("1. Rodzaj interakcji:")]
    public RodzajInterakcji rodzajInterakcji = RodzajInterakcji.Zamek;


    [Header("A. Interakcja to przedmiot: ")]
    public ListaKategorii kategoria1 = ListaKategorii.Ciekawostka;
    public int id1 = 0;

    [Header("Czy przedmiot ma zniknąć:")]
    public PytanieTakNie pytanie1 = PytanieTakNie.Tak;

    [Header("B. Interakcja to kontener: ")]
    public GameObject RuszanyObiekt;
    public Vector3 LokalnyRuch;
    public Vector3 LokalnaRotacja;

    [Header("Czy wymagany jest klucz: ")]
    public PytanieTakNie pytanie2 = PytanieTakNie.Nie;

    [Header("Wymagany Predmiot do interakcji: ")]
    public ListaKategorii WymaganyKategoria = ListaKategorii.Ekwipunek;
    public int WymaganyId = 0;

    [Header("Jeśli brak przedmiotu: ")]
    public ListaKategorii Brak2Kategoria = ListaKategorii.Wskazowka;
    public int Brak2Id = 0;

    [Header("C. Interakcja to zamek: ")]
    //[Header("Potrzebny przedmiot:")]
    public ListaKategorii PotrzebnyKategoria = ListaKategorii.Ekwipunek;
    public int PotrzebnyId = 0;

    [Header("Nagroda za przedmiot który znajduje sie w EKW:")]
    public ListaKategorii NagrodaKategoria = ListaKategorii.Ekwipunek;
    public int NagrodaId = 0;

    [Header("Jeśli przedmiotu nie ma w EKW:")]
    public ListaKategorii BrakKategoria = ListaKategorii.Wskazowka;
    public int BrakId = 0;

    [Header("D. Powiązany wpis w dzienniku: ")]
    public ListaKategorii PowiazanyKategoria = ListaKategorii.Dziennik;
    public int PowiązanyId = 0;

    //[Header("5. Dla Ciekawostki / Wskazówki")]
    //public int id3;
    //public ListaKategorii kategoria3;

    private bool czyOtworzyc = false;

    private ListaKategorii OstKategoria;
    private int OstId;
    public void Interact(List<Item> list)
    {
        if (rodzajInterakcji == RodzajInterakcji.Przedmiot)
        {
            OstId = id1;
            OstKategoria = kategoria1;
        }
        if (rodzajInterakcji == RodzajInterakcji.Zamek || rodzajInterakcji == RodzajInterakcji.Kontener)
        {
            pytanie1 = PytanieTakNie.Nie;
            if (rodzajInterakcji == RodzajInterakcji.Kontener)
            {
                if (pytanie2 == PytanieTakNie.Tak)
                {
                    if (checkPrzedmioty(list, WymaganyId))
                    {
                        Debug.Log(RuszanyObiekt.transform.localRotation.eulerAngles);
                        RuszanyObiekt.transform.localPosition = new Vector3(RuszanyObiekt.transform.localPosition.x + LokalnyRuch.x, RuszanyObiekt.transform.localPosition.y + LokalnyRuch.y, RuszanyObiekt.transform.localPosition.z + LokalnyRuch.z);
                        RuszanyObiekt.transform.localRotation = Quaternion.Euler(RuszanyObiekt.transform.localRotation.eulerAngles.x + LokalnaRotacja.x, RuszanyObiekt.transform.localRotation.eulerAngles.y + LokalnaRotacja.y, RuszanyObiekt.transform.localRotation.eulerAngles.z + LokalnaRotacja.z);
                        pytanie1 = PytanieTakNie.Tak;
                        Debug.Log(RuszanyObiekt.transform.localRotation.eulerAngles);
                    }
                    else
                    {
                        OstKategoria = Brak2Kategoria;
                        OstId = Brak2Id;
                    }
                }
                else
                {
                    RuszanyObiekt.transform.localPosition = new Vector3(RuszanyObiekt.transform.localPosition.x + LokalnyRuch.x, RuszanyObiekt.transform.localPosition.y + LokalnyRuch.y, RuszanyObiekt.transform.localPosition.z + LokalnyRuch.z);
                    RuszanyObiekt.transform.localRotation = Quaternion.Euler(RuszanyObiekt.transform.localRotation.eulerAngles.x + LokalnaRotacja.x, RuszanyObiekt.transform.localRotation.eulerAngles.y + LokalnaRotacja.y, RuszanyObiekt.transform.localRotation.eulerAngles.z + LokalnaRotacja.z);
                    pytanie1 = PytanieTakNie.Tak;
                }
            }
            if (rodzajInterakcji == RodzajInterakcji.Zamek)
            {
                if (checkPrzedmioty(list, PotrzebnyId))
                {
                    OstKategoria = NagrodaKategoria;
                    OstId = NagrodaId;
                }
                else
                {
                    OstKategoria = BrakKategoria;
                    OstId = BrakId;
                }
            }
        }
    }
    public void CheckDestroy()
    {
        if (pytanie1 == PytanieTakNie.Tak)
            Destroy(this.gameObject);
    }
    public int getId()
    {
        return OstId;
    }
    public string getCategory()
    {
        return OstKategoria.ToString();
    }
    public int getDziennikId()
    {
        return PowiązanyId;
    }
    public string getDziennikCategory()
    {
        return PowiazanyKategoria.ToString();
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
