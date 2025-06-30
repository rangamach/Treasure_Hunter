public class EventService
{
    public EventController OnAddChest { get; private set; }
    public EventController[] OnSlotButtonClickedEvents;
    public EventController OnXButtonClicked;
    public EventController OnTickButtonClicked;
    public EventService(int totalSlots)
    {
        OnAddChest = new EventController();
        OnSlotButtonClickedEvents = new EventController[totalSlots];
        for (int i = 0; i < totalSlots; i++)
            OnSlotButtonClickedEvents[i] = new EventController();
        OnXButtonClicked = new EventController();
        OnTickButtonClicked = new EventController();
    }
}
