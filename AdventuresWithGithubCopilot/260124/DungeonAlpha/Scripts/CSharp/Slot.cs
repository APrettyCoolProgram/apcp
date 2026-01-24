using Godot;

public class Slot
{
    public Card OccupiedCard { get; private set; }
    public bool IsEmpty => OccupiedCard == null;
    public void Place(Card card) { OccupiedCard = card; }
    public void Clear() { OccupiedCard = null; }
}
