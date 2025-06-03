using UnityEngine;

public class EventService
{
    public EventController OnButtonClicked { get; private set; }
    public EventService()
    {
        OnButtonClicked = new EventController();
    }
}
