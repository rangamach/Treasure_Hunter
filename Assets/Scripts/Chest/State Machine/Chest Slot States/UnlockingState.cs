using UnityEngine;

public class UnlockingState<T> : IState where T : ChestSlotController
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;


    public UnlockingState(GenericStateMachine<T> machine) => this.stateMachine = machine;

    public void OnStateEnter()
    {
        SetTimer();
    }

    public void OnStateExit()
    {
        ResetTimer();
    }

    public void Update()
    {
        UpdateTimer();
    }
    private void SetTimer()
    {
        Owner.isTimerPaused = false;
        Owner.SetTimeRemaining(Owner.GetChest().TimerInMinutes * 60);
    }
    private void ResetTimer()
    {
        Owner.SetTimeRemaining(0);
        Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].timerText.text = "";
        Owner.isTimerPaused = false;
    }
    private void UpdateTimer()
    {
        if (Owner.GetTimeRemaining() > 0)
        {
            Owner.SetTimeRemaining(Owner.GetTimeRemaining() - Time.deltaTime);
        }
    }
}
