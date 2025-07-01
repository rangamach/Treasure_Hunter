using UnityEngine;
using UnityEngine.UI;

public class EmptyState<T> : IState where T : ChestSlotController
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine;

    public EmptyState(GenericStateMachine<T> machine) => this.stateMachine = machine;

    public void OnStateEnter()
    {
        SetButtonInteraction(false);
        MakeButtonImageTransparent();
        SetChestAsNull();
    }
    public void OnStateExit()
    {
        SetButtonInteraction(true);
    }

    private void MakeButtonImageTransparent()
    {
        Image slotButtonImage = Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].slotButton.gameObject.GetComponent<Image>();
        Color slotImageColor = slotButtonImage.color;
        slotImageColor.a = 0f;
        slotButtonImage.color = slotImageColor;
    }
    private void SetButtonInteraction(bool interaction) => Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].slotButton.interactable = interaction;
    private void SetChestAsNull() => Owner.SetChest(null);

    public void Update()
    {
    }
}
