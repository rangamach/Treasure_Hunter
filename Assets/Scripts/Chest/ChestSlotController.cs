using System;
using System.Collections.Generic;
public class ChestSlotController
{
    private static ChestSlotModel chestSlotModel;

    private ChestSO chest;
    public int index;
    private ChestSLotStateMachine stateMachine;
    private ChestStates currentChestSlotState;
    private float timeRemaining;
    public bool isTimerPaused;

    public ChestSlotController(ChestTypesSO chestTypes, int totalSlots, List<SlotUI> slotButtons)
    {
        if(chestSlotModel == null)
            chestSlotModel = new ChestSlotModel(chestTypes, totalSlots,slotButtons);
    }
    public ChestSlotController()
    {
        CreateStateMachine();
        stateMachine.ChangeState(ChestStates.Empty);
        SetChestSlotState(ChestStates.Empty);
    }
    private void CreateStateMachine() => stateMachine = new ChestSLotStateMachine(this);
    public void Update()
    {
        if (!isTimerPaused)
        {
            stateMachine?.Update();
            UpdateTimerCountdown();
        }
    }

    private void UpdateTimerCountdown()
    {
        if (timeRemaining <= 0)
        {
            stateMachine.ChangeState(ChestStates.Unlocked);
            SetChestSlotState(ChestStates.Unlocked);
        }
        else
        {
            TimeSpan timespan = TimeSpan.FromSeconds(timeRemaining);
            string timeFormatted;
            if(timespan.Hours > 0)
                timeFormatted = string.Format("{0:00}:{1:00}:{2:00}", timespan.Hours, timespan.Minutes, timespan.Seconds);
            else
                timeFormatted = string.Format("{0:00}:{1:00}", timespan.Minutes, timespan.Seconds);
            GetChestSlotModel().SlotButtonsSO.SlotUIList[index].timerText.text = timeFormatted;
        }
    }
    public UnityEngine.Vector2Int GetRandomCoinsAndGems()
    {
        UnityEngine.Vector2Int rewards = new UnityEngine.Vector2Int();
        int coinsRewarded = GetRandomNumberInInterval(GetChest().MinCoinsReward,GetChest().MaxCoinsReward);
        int gemsRewarded = GetRandomNumberInInterval(GetChest().MinGemsReward, GetChest().MaxGemsReward);
        rewards.x = coinsRewarded;
        rewards.y = gemsRewarded;
        return rewards;
    }
    private int GetRandomNumberInInterval(int min,int max) => UnityEngine.Random.Range(min, max + 1);
    public ChestSlotModel GetChestSlotModel() => chestSlotModel;
    public ChestSLotStateMachine GetStateMachine() => stateMachine;
    public void SetChestSlotState(ChestStates newState) => this.currentChestSlotState = newState;
    public ChestStates GetChestSlotState() => this.currentChestSlotState;
    public void SetChest(ChestSO newChest) => chest = newChest;
    public ChestSO GetChest() => chest;
    public void SetTimeRemaining(float newTime) => timeRemaining = newTime;
    public float GetTimeRemaining() => timeRemaining;
}
