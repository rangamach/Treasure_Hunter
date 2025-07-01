using System.Collections.Generic;

public class GenericStateMachine<T> where T : ChestSlotController
{
    protected T Owner;
    protected IState currentState;
    protected Dictionary<ChestStates, IState> States = new Dictionary<ChestStates, IState>();

    public GenericStateMachine(T owner) => this.Owner = owner;

    public void Update() => currentState?.Update();

    protected void ChangeState(IState newState)
    {
        currentState?.OnStateExit();
        currentState = newState;
        currentState?.OnStateEnter();
    }

    public void ChangeState(ChestStates newState) => ChangeState(States[newState]);

    protected void SetOwner()
    {
        foreach (IState state in States.Values)
            state.Owner = Owner;
    }
}
