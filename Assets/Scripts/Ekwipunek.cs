
[System.Serializable]
public class Ekwipunek
{
    public int id;
    public string nazwa;
    public string opis;
    public Ekwipunek(int id, string nazwa, string opis)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.opis = opis;
    }
}
