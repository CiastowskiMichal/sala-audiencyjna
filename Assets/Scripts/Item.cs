
[System.Serializable]
public class Item
{
    public int id;
    public string nazwa;
    public string opis;
    public Item(int id, string nazwa, string opis)
    {
        this.id = id;
        this.nazwa = nazwa;
        this.opis = opis;
    }
}
