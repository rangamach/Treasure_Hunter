using UnityEngine;

public class EventService
{
    public EventController OnButtonClicked { get; private set; }
    public EventController OnAddChest { get; private set; }
    public EventService()
    {
        OnButtonClicked = new EventController();
        OnAddChest = new EventController();
    }
}
