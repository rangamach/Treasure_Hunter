using System.Collections.Generic;
using UnityEngine;

public class ChestSlotModel
{
    public int TotalSlots { get; private set; }
    public ChestTypesSO ChestTypesSO { get; private set; }
    public ChestSlotController[] chestSlotControllers { get; private set; }
    public string[] TimerTexts { get; private set; }
    public SlotButtonsSO SlotButtonsSO { get; private set; }

    public ChestSlotModel(ChestTypesSO chestTypes, int totalSlots, List<SlotUI> slotButtons)
    {
        this.TotalSlots = totalSlots;
        this.ChestTypesSO = chestTypes;
        this.SlotButtonsSO = ScriptableObject.CreateInstance<SlotButtonsSO>();

        InitializeChestSlotControllersArray();
        InitializeSlotButtons(slotButtons);
    }
    private void InitializeChestSlotControllersArray() => chestSlotControllers = new ChestSlotController[TotalSlots];
    private void InitializeSlotButtons(List<SlotUI> slotButtons)
    {
        EventController[] slotButtonEvents = GameService.Instance.EventService.OnSlotButtonClickedEvents;
        for (int i = 0;i<slotButtons.Count;i++ )
            SlotButtonsSO.SlotUIList.Add(slotButtons[i]);
    }
    public void InitializeEachSlotController()
    {
        for (int i = 0; i < TotalSlots; i++)
        {
            chestSlotControllers[i] = new ChestSlotController();
            chestSlotControllers[i].index = i;
        }
    }
}
