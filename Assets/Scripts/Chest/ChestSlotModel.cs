using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlotModel
{
    public int TotalSlots { get; private set; }
    public ChestTypesSO ChestTypesSO { get; private set; }
    public ChestSlotController[] chestSlotControllers { get; private set; }
    public SlotButtonsSO SlotButtonsSO { get; private set; }

    public ChestSlotModel(ChestTypesSO chestTypes, int totalSlots, List<Button> slotButtons)
    {
        this.TotalSlots = totalSlots;
        this.ChestTypesSO = chestTypes;
        this.SlotButtonsSO = ScriptableObject.CreateInstance<SlotButtonsSO>();

        InitializeChestSlotControllersArray();
        InitializeSlotButtons(slotButtons);
        //InitializeEachSlotController();
    }
    private void InitializeChestSlotControllersArray() => chestSlotControllers = new ChestSlotController[TotalSlots];
    private void InitializeSlotButtons(List<Button> slotButtons)
    {
        foreach (Button button in slotButtons)
            SlotButtonsSO.SlotButtons.Add(button);
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
