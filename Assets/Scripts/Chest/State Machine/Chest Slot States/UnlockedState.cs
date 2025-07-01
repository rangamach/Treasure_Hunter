using UnityEngine.UI;

public class UnlockedState<T> : IState where T : ChestSlotController
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;

    public UnlockedState(GenericStateMachine<T> machine) => this.stateMachine = machine;

    public void OnStateEnter()
    {
        SetOpenChestSprite();
    }

    public void OnStateExit()
    {
    }

    public void Update()
    {
    }
    private void SetOpenChestSprite() => Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].slotButton.gameObject.GetComponent<Image>().sprite = Owner.GetChest().Opened;
}
