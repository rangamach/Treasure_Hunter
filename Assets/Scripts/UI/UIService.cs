using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIService : MonoBehaviour
{
    private int cost;

    [SerializeField] private RectTransform initialUI;
    [SerializeField] private RectTransform rewardsUI;
    [SerializeField] private RectTransform areYouSureUI;
    [SerializeField] private RectTransform popUpBG;
    [SerializeField] private Button addChestButton;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private TextMeshProUGUI gemsText;
    [SerializeField] private TextMeshProUGUI coinsRewardedText;
    [SerializeField] private TextMeshProUGUI gemsRewardedText;
    [SerializeField] private TextMeshProUGUI areYouSureText;
    [SerializeField] private Button xButton;
    [SerializeField] private Button tickButton;

    public int Coins { get; private set; }
    public int Gems { get; private set; }

    private void Start()
    {
        subscribeToEvents();
        invokeEvents();

        InitializeCoinsGems();

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
            GameService.Instance.EventService.OnSlotButtonClickedEvents[index].AddListener(() => onSlotButtonClicked(index));
        }
        GameService.Instance.EventService.OnXButtonClicked.AddListener(onXButtonClicked);
        GameService.Instance.EventService.OnTickButtonClicked.AddListener(onTickButtonClicked);
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
        xButton.onClick.AddListener(() => GameService.Instance.EventService.OnXButtonClicked.InvokeEvent());
        tickButton.onClick.AddListener(() => GameService.Instance.EventService.OnTickButtonClicked.InvokeEvent());
    }
    private void unSubscribeToEvents()
    {
        GameService.Instance.EventService.OnAddChest.RemoveListener(onAddChest);
        for (int i = 0; i < GameService.Instance.ChestSlotService.chestSlotController.GetChestSlotModel().TotalSlots; i++)
        {
            int index = i;
            GameService.Instance.EventService.OnSlotButtonClickedEvents[index].RemoveListener(() => onSlotButtonClicked(index));
        }
        GameService.Instance.EventService.OnXButtonClicked.RemoveListener(onXButtonClicked);
        GameService.Instance.EventService.OnTickButtonClicked.RemoveListener(onTickButtonClicked);
    }
    private void InitializeCoinsGems()
    {
        Coins = 0;
        Gems = 0;

        SetCoinsGemsText(Coins, Gems);
    }
    private void onAddChest()
    {
        if (!GameService.Instance.ChestSlotService.AreAllSlotsFilled())
        {
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Adding_Chest);
            GameService.Instance.ChestSlotService.AddRandomChestInFirstEmptySlotController();
        }
        else
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Not_Enough_Gems);

        DeselectSelectedButton();
    }
    private void onSlotButtonClicked(int index)
    {
        if (!IsPopUpActive())
            GameService.Instance.ChestSlotService.OnSlotButtonClicked(index);
        else
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Not_Enough_Gems);
        DeselectSelectedButton();
    }

    private static void DeselectSelectedButton() => UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
    public void RewardsUI(int coinsRewarded, int gemsRewarded)
    {
        GameService.Instance.SoundService.PlaySoundEffect(SoundType.Bought);
        coinsRewardedText.text = "You have earned " + coinsRewarded.ToString() +" coins!";
        gemsRewardedText.text = "You have earned " + gemsRewarded.ToString() +" gems!";

        SetCoinsGemsText(Coins + coinsRewarded, Gems + gemsRewarded);

        rewardsUI.gameObject.SetActive(true);
        areYouSureUI.gameObject.SetActive(false);
        EnablePopUpBG();
        StartCoroutine(AutoClosePopUp());
    }
    public void AreYouSureUI(ChestSlotController controller)
    {
        if (!IsPopUpActive())
        {
            cost = GetChestCost(controller);
            areYouSureText.text = $"Do you want to open this chest for {cost} gems?";

            rewardsUI.gameObject.SetActive(false);
            areYouSureUI.gameObject.SetActive(true);
            EnablePopUpBG();
        }
    }
    private int GetChestCost(ChestSlotController controller)
    {
        float minutes = controller.GetTimeRemaining() / 60;
        int cst = Mathf.CeilToInt(minutes / 10); 
        return cst;
    }
    private void EnablePopUpBG()
    {
        if (!popUpBG.gameObject.activeInHierarchy)
        {
            popUpBG.gameObject.SetActive(true);
        }
    }
    System.Collections.IEnumerator AutoClosePopUp()
    {
        yield return new WaitForSeconds(5f);
        rewardsUI.gameObject.SetActive(false);
        popUpBG.gameObject.SetActive(false);
    }
    private void SetCoinsGemsText(int coins, int gems)
    {
        Coins = coins;
        Gems = gems;

        coinsText.text = Coins.ToString();
        gemsText.text = Gems.ToString();
    }
    private void onXButtonClicked()
    {
        GameService.Instance.SoundService.PlaySoundEffect(SoundType.Not_Enough_Gems);
        GameService.Instance.ChestSlotService.OnXButtonClicked();
    }
    private void onTickButtonClicked()
    {
        ChestSlotController controller = GameService.Instance.ChestSlotService.GetSelectedChestController();
        if (Gems < cost)
        {
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Not_Enough_Gems);
            DisableAreYouSureUI();
        }
        else if( Gems >= cost)
        {
            GameService.Instance.SoundService.PlaySoundEffect(SoundType.Sold);
            Gems -= cost;
            SetCoinsGemsText(Coins, Gems);
            controller.GetStateMachine().ChangeState(ChestStates.Unlocked);
            controller.SetChestSlotState(ChestStates.Unlocked);
            DisableAreYouSureUI();
        }
        controller.isTimerPaused = false;
    }
    public void DisableAreYouSureUI()
    {
        areYouSureUI.gameObject.SetActive(false);
        popUpBG.gameObject.SetActive(false);
    }
    private bool IsPopUpActive() => popUpBG.gameObject.activeInHierarchy;
    private void OnDestroy() => unSubscribeToEvents();
    
}
