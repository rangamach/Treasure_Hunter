public class ChestSLotStateMachine : GenericStateMachine<ChestSlotController>
{
    public ChestSLotStateMachine(ChestSlotController owner) : base(owner)
    {
        this.Owner = owner;
        CreateStates();
        SetOwner();
    }
    private void CreateStates()
    {
        States.Add(ChestStates.Empty, new EmptyState<ChestSlotController>(this));
        States.Add(ChestStates.Locked, new LockedState<ChestSlotController>(this));
        States.Add(ChestStates.Unlocking, new UnlockingState<ChestSlotController>(this));
        States.Add(ChestStates.Unlocked, new UnlockedState<ChestSlotController>(this));
    }
}
