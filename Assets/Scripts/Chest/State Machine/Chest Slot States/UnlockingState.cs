using UnityEngine;

public class UnlockingState<T> : IState where T : ChestSlotController
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;


    public UnlockingState(GenericStateMachine<T> machine) => this.stateMachine = machine;

    public void OnStateEnter()
    {
        Owner.SetTimeRemaining(Owner.GetChest().TimerInMinutes * 60);
    }

    public void OnStateExit()
    {
    }

    public void Update()
    {
        if (Owner.GetTimeRemaining() > 0)
        {
            Owner.SetTimeRemaining(Owner.GetTimeRemaining() - Time.deltaTime);
        }
    }
}
