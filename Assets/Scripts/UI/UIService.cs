using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private RectTransform initialUI;
    [SerializeField] private Button addChestButton;
    private void Start()
    {
        subscribeToEvents();
        invokeEvents();

        initialUI.gameObject.SetActive(true);
    }
    private void Update()
    {
        if(GameService.Instance?.ChestSlotService != null)
        {
            foreach (ChestSlotController slotController in GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().chestSlotControllers)
            {
                if (slotController?.GetChestSlotState() == ChestStates.Unlocking)
                {
                    slotController.Update();
                }
            }
        }
    }
    private void subscribeToEvents()
    {
        GameService.Instance.EventService.OnAddChest.AddListener(onAddChest);
        for (int i = 0; i < GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().TotalSlots; i++)
        {
            int index = i;
            //List<SlotUI> slotList = GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().SlotButtonsSO.SlotUIList;
            GameService.Instance.EventService.OnSlotButtonClickedEvents[index].AddListener(() => onSlotButtonClicked(index));
        }
    }
    private void invokeEvents()
    {
        addChestButton.onClick.AddListener(() => GameService.Instance.EventService.OnAddChest.InvokeEvent());
        for(int i = 0; i<GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().TotalSlots;i++)
        {
            int index = i;
            GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().SlotButtonsSO.SlotUIList[index].slotButton.onClick.AddListener(
                () => GameService.Instance.EventService.OnSlotButtonClickedEvents[index].InvokeEvent() );
        }
    }

    private void unSubscribeToEvents() => GameService.Instance.EventService.OnAddChest.RemoveListener(onAddChest);
    private void onAddChest()
    {
        GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
        GameService.Instance.ChestSlotService.AddRandomChestInFirstEmptySlotController();
    }
    private void onSlotButtonClicked(int index)
    {
        GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
        GameService.Instance.ChestSlotService.OnSlotButtonClicked(index);
    }
    private void OnDestroy() => unSubscribeToEvents();
    
}
