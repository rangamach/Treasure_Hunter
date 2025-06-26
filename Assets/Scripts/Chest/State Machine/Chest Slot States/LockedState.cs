using UnityEngine.UI;
using UnityEngine;

public class LockedState<T> : IState where T : ChestSlotController 
{
    public ChestSlotController Owner { get; set; }
    private GenericStateMachine<T> stateMachine; 
    
    public LockedState(GenericStateMachine<T> machine) => this.stateMachine = machine;


    public void OnStateEnter()
    {
        //Image slotImage = Owner.GetChestSlotModel().SlotButtonsSO.SlotButtons[Owner.index].gameObject.GetComponent<Image>();
        Image slotImage = Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].slotButton.gameObject.GetComponent<Image>();
        SetButtonImageSprite(slotImage);
        SetImageNonTransparent(slotImage);
    }
    private void SetImageNonTransparent(Image slotImage)
    {
        Color slotImageColor = slotImage.color;
        slotImageColor.a = 1f;
        slotImage.color = slotImageColor;
    }

    private void SetButtonImageSprite(Image slotImage) => slotImage.sprite = Owner.GetChest().Closed;

    public void OnStateExit()
    {
        SetButtonInteraction(false);
    }
    private void SetButtonInteraction(bool interaction) => Owner.GetChestSlotModel().SlotButtonsSO.SlotUIList[Owner.index].slotButton.interactable = interaction;
    //private void SetButtonInteraction(bool interaction) => Owner.GetChestSlotModel().SlotButtonsSO.SlotButtons[Owner.index].interactable = interaction;

    public void Update()
    {
        
    }
}
