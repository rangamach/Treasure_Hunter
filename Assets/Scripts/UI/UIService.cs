using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    //[SerializeField] private ChestTypesSO chestTypesSO;
    [SerializeField] private RectTransform initialRectTransform;
    [SerializeField] private Button addChestButton;
    [SerializeField] private Transform slotsParent;
    [SerializeField] private Sprite emptySprite;
    [SerializeField] private TextMeshProUGUI chestText;
    private void Start()
    {
        initialRectTransform.gameObject.SetActive(true);
        addChestButton.onClick.AddListener(() => GameService.Instance.EventService.OnAddChest.InvokeEvent());
        subscribeToEvents();
    }
    private void subscribeToEvents() => GameService.Instance.EventService.OnAddChest.AddListener(onAddChest);
    private void unSubscribeToEvents() => GameService.Instance.EventService.OnAddChest.RemoveListener(onAddChest);
    private void onAddChest()
    {
        GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
        GameService.Instance.ChestSlotService.AddRandomChestInFirstEmptySlotController();
        //GameService.Instance.GameplayService.AddChest(chestTypesSO);
    }
    private void OnDestroy() => unSubscribeToEvents();
    
    //public void UpdateChestSlotUI(ChestSO chestSO)
    //{
    //    if (chestSO == null)
    //        return;
    //    foreach (Transform slot in slotsParent)
    //    {
    //        Image chestImage = slot.GetChild(0).GetComponent<Image>();
    //        if (chestImage != null && chestImage.sprite == emptySprite)
    //        {
    //            chestImage.sprite = chestSO.Closed;
    //            break;
    //        }
    //    }
    //}
}
