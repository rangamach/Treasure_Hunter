using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIService : MonoBehaviour
{
    [SerializeField] private Button test;

    private void Start()
    {
        test.onClick.AddListener(() => GameService.Instance.EventService.OnButtonClicked.InvokeEvent());
        SubscribeToEvents();
    }
    private void SubscribeToEvents()
    {
        GameService.Instance.EventService.OnButtonClicked.AddListener(OnButtonClicked);
    }
    private void UnSubscribeToEvents()
    {
        GameService.Instance.EventService.OnButtonClicked.RemoveListener(OnButtonClicked);
    }
    private void OnButtonClicked() => GameService.Instance.SoundService.PlaySoundEffect(SoundType.Button_Click);
    private void OnDestroy()
    {
        UnSubscribeToEvents();
    }
}
