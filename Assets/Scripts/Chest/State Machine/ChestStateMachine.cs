//public class ChestStateMachine : GenericStateMachine<ChestController>
//{
//    public ChestStateMachine(ChestController owner) : base(owner)
//    {
//        this.Owner = owner;
//        CreateStates();
//        SetOwner();
//    }
//    private void CreateStates()
//    {
//        States.Add(ChestStates.Locked, new LockedState<ChestSlotController>(this));
//        States.Add(ChestStates.Unlocking, new UnlockingState<ChestSlotController>(this));
//        States.Add(ChestStates.Unlocked, new UnlockedState<ChestController>(this));
//    }
//}
