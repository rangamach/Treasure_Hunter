using System.Collections.Generic;
using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    //Services:
    public SoundService SoundService {get; private set;}
    public ChestSlotService ChestSlotService { get; private set; }
    public EventService EventService { get; private set; }
    [SerializeField] private UIService uiService;
    public UIService GetUIService() => uiService;


    //Scriptable Objects:
    [Header("Scriptable Objects")]
    [SerializeField] private SoundSO soundSO;
    [SerializeField] private ChestTypesSO allChestTypes;

    //Audio Sources:
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;

    //Variables:
    [Header("Variables")]
    [SerializeField] private int totalSlots;
    [SerializeField] private List<SlotUI> slotButtons;
    protected override void Awake()
    {
        base.Awake();
        CreateServices();
    }
    private void CreateServices()
    {
        EventService = new EventService(totalSlots);
        ChestSlotService = new ChestSlotService(allChestTypes, totalSlots, slotButtons);
        SoundService = new SoundService(soundSO, bgAudioSource, sfxAudioSource);
    }
}
