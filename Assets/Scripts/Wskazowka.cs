
[System.Serializable]
public class Wskazowka
{
    public int id;
    public string nazwa;
    public string opis;
    public Wskazowka(int id, string nazwa, string opis)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.opis = opis;
    }
}
