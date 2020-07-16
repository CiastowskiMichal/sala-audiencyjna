
[System.Serializable]
public class Ciekawostka
{
    public int id;
    public string nazwa;
    public string opis;
    public Ciekawostka(int id, string nazwa, string opis)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.opis = opis;
    }
}
