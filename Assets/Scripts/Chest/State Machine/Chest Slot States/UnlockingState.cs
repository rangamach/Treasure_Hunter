public class UnlockingState<T> : IState where T : ChestSlotController
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;

    public UnlockingState(GenericStateMachine<T> machine) => this.stateMachine = machine;

    public void OnStateEnter()
    {
    }

    public void OnStateExit()
    {
    }

    public void Update()
    {
    }
}
