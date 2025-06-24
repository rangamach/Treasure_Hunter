using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestSlotService
{
    private ChestSlotController chestSlotController;

    public ChestSlotService(ChestTypesSO chestTypesSO, int totalSlots, List<Button> slotButtons)
    {
        //this controller is to initialize the chest slot model.
        this.chestSlotController = new ChestSlotController(chestTypesSO, totalSlots,slotButtons);
    }
    public void AddRandomChestInFirstEmptySlotController()
    {
        if (chestSlotController.GetChestSlotModel().chestSlotControllers[0] == null)
            chestSlotController.GetChestSlotModel().InitializeEachSlotController();
        ChestSlotModel chestModel = chestSlotController.GetChestSlotModel();
        for (int i = 0; i < chestModel.TotalSlots; i++)
        {
            if (chestModel.chestSlotControllers[i].chest == null)
            {
                ChestSO randomChest = GetRandomChest();
                chestModel.chestSlotControllers[i].chest = randomChest;
                chestModel.chestSlotControllers[i].GetStateMachine().ChangeState(ChestStates.Locked);
                break;
            }
        }
    }
    private ChestSO GetRandomChest()
    {
        float roll = Random.value * 100;
        float cumulative = 0f;
        ChestTypesSO chestTypes = chestSlotController.GetChestSlotModel().ChestTypesSO;

        foreach (var chest in chestTypes.ChestTypes)
        {
            cumulative += chest.PercentChance;
            if (roll < cumulative)
            {
                return chest.ChestSO;
            }
        }

        return chestTypes.ChestTypes[0].ChestSO;
    }
}
