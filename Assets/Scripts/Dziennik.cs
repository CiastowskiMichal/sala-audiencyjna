
[System.Serializable]
public class Dziennik
{
    public int id;
    public string nazwa;
    public string opis;
    public Dziennik(int id, string nazwa, string opis)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.opis = opis;
    }
}
