using UnityEngine;

public class GameService : GenericMonoSingleton<GameService>
{
    //Services:
    public SoundService SoundService {get; private set;}
    public EventService EventService { get; private set; }
    [SerializeField] private UIService uiService;
    public UIService GetUIService() => uiService;


    //Scriptable Objects:
    [Header("Scriptable Objects")]
    [SerializeField] private SoundSO soundSO;

    //Audio Sources:
    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgAudioSource;
    [SerializeField] private AudioSource sfxAudioSource;
    protected override void Awake()
    {
        base.Awake();
        EventService = new EventService();
        SoundService = new SoundService(soundSO,bgAudioSource,sfxAudioSource);
    }
}
