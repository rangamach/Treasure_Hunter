using System.Collections.Generic;
using UnityEngine;

public class ChestSlotService
{
    public ChestSlotController chestSlotController { get; private set; }
    public ChestSlotService(ChestTypesSO chestTypesSO, int totalSlots, List<SlotUI> slotButtons)
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
            if (chestModel.chestSlotControllers[i].GetChest() == null)
            {
                ChestSlotController slotController = chestModel.chestSlotControllers[i];
                slotController.SetChest(GetRandomChest());
                slotController.GetStateMachine().ChangeState(ChestStates.Locked);
                slotController.SetChestSlotState(ChestStates.Locked);
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
    public void OnSlotButtonClicked(int index)
    {
        ChestSlotController slotController = chestSlotController.GetChestSlotModel().chestSlotControllers[index];
        if (slotController.GetChestSlotState() == ChestStates.Locked)
        {
            if (!CheckIfAnyChestUnlocking())
            {
                GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
                slotController.SetChestSlotState(ChestStates.Unlocking);
                slotController.GetStateMachine().ChangeState(ChestStates.Unlocking);
            }
            else
                GameService.Instance.SoundService.PlaySoundEffect(SoundType.Not_Enough_Gems);
        }
        else if(slotController.GetChestSlotState() == ChestStates.Unlocking)
        {
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
            slotController.isTimerPaused = true;
            GameService.Instance.GetUIService().AreYouSureUI(slotController);
        }
        else if(slotController.GetChestSlotState() == ChestStates.Unlocked)
        {
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
            Vector2Int rewards = slotController.GetRandomCoinsAndGems();
            GameService.Instance.GetUIService().RewardsUI(rewards.x, rewards.y);
            slotController.GetStateMachine().ChangeState(ChestStates.Empty);
            slotController.SetChestSlotState(ChestStates.Empty);
        }
    }
    private bool CheckIfAnyChestUnlocking()
    {
        foreach (ChestSlotController controller in chestSlotController.GetChestSlotModel().chestSlotControllers)
        {
            if (controller.GetChestSlotState() == ChestStates.Unlocking)
                return true;
        }
        return false;
    }
    public void OnXButtonClicked()
    {
        ChestSlotController controller = GetSelectedChestController();
        if (controller != null)
            controller.isTimerPaused = false;

        GameService.Instance.GetUIService().DisableAreYouSureUI();
    }
    public ChestSlotController GetSelectedChestController()
    {
        ChestSlotController[] controllers = chestSlotController.GetChestSlotModel().chestSlotControllers;
        foreach (ChestSlotController controller in controllers)
        {
            if (controller.isTimerPaused = true && controller.GetTimeRemaining() > 0)
                return controller;
        }
        return null;
    }
    public bool AreAllSlotsFilled()
    {
        if (chestSlotController.GetChestSlotModel().chestSlotControllers[0] == null)
            return false;
        else
        {
            foreach (ChestSlotController controller in chestSlotController.GetChestSlotModel().chestSlotControllers)
            {
                if (controller.GetChest() == null)
                    return false;
            }
        }
        return true;
    }
}