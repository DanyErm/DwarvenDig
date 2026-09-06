using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{

    [SerializeField] private GameSettings _gameSettings;
    [SerializeField] private DiggingProcess _diggingProcess;
    [SerializeField] private Inventory _inventory;

    //[Header("Audio")]
    //[SerializeField] private MusicManager _musicManager;
    //[SerializeField] private sfxManager _sfxManager;

    //[Header("UI")]
    //[SerializeField] private PanelsManager _panelsManager;


    public override void InstallBindings()
    {
        BindGameDependencies();
        BindUIDependencies();
        BindAudioDependencies();
    }

    private void BindGameDependencies()
    {
        Container
            .Bind<GameSettings>()
            .FromInstance(_gameSettings)
            .AsSingle();

        Container
            .Bind<DiggingProcess>()
            .FromInstance(_diggingProcess)
            .AsSingle();

        Container
            .Bind<Inventory>()
            .FromInstance(_inventory)
            .AsSingle();
    }



    private void BindUIDependencies()
    {
        //Container
        //    .Bind<PanelsManager>()
        //    .FromInstance(_panelsManager)
        //    .AsSingle();
    }

    private void BindAudioDependencies()
    {
        //Container
        //    .Bind<MusicManager>()
        //    .FromInstance(_musicManager)
        //    .AsSingle();

        //Container
        //    .Bind<sfxManager>()
        //    .FromInstance(_sfxManager)
        //    .AsSingle();
    }
}