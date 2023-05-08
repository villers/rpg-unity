using System;
using System.IO;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Inspector
    [Header(":: Game Setup")]
    public bool OfflineOnly = false;
    public bool LocalHost = false;

    [Header(":: Rendering Setup")]
    public AudioMixerGroup BGMMixerGroup;
    public AudioMixerGroup EffectsMixerGroup;
    public Light WorldLight;

    public Camera CursorCamera;
    public Camera MainCamera;
    #endregion

    /*private MapLoader MapLoader;
    private MapRenderer MapRenderer;*/
    private AudioSource AudioSource;


    #region Components
    //private EntityManager EntityManager;
    //private NetworkClient NetworkClient;
    #endregion

    //public RemoteConfiguration RemoteConfiguration { get; private set; }
    //public LocalConfiguration LocalConfiguration { get; private set; }

    public static long Tick => new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();

    //public GameMap CurrentMap { get; private set; }

    private static GameManager Instance;

    private void Awake()
    {
        if (AudioSource == null)
        {
            AudioSource = gameObject.AddComponent<AudioSource>();
            AudioSource.outputAudioMixerGroup = BGMMixerGroup;
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
        RenderPipelineManager.endCameraRendering += OnEndCameraRendering;
    }

    private void OnDisable()
    {
        RenderPipelineManager.endCameraRendering -= OnEndCameraRendering;
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        MainCamera = Camera.main;
        var cameraObject = GameObject.FindGameObjectWithTag("CursorCamera");
        if (cameraObject != null)
        {
            CursorCamera = cameraObject.GetComponent<Camera>();
        }
    }

    private void OnEndCameraRendering(ScriptableRenderContext context, Camera camera)
    {
        OnPostRender();
    }

    void Start()
    {
        Init();
    }

    void Update()
    {
        if (MainCamera == null)
        {
            MainCamera = Camera.main;
        }
    }

    private void Init()
    {
        InitManagers();
    }

    public void OnPostRender()
    {
        
    }

    public void InitCamera()
    {
        MainCamera = Camera.main;
    }

    public void PlayBgm(string name)
    {
        var bgm = Addressables.LoadAssetAsync<AudioClip>(Path.Combine("bgm", name)).WaitForCompletion();
        AudioSource.clip = bgm;
        AudioSource.Play();
    }

    public Task<bool> LoadScene(string sceneName, LoadSceneMode mode)
    {
        var t = new TaskCompletionSource<bool>();

        SceneManager.LoadSceneAsync(sceneName, mode).completed += delegate {
            t.TrySetResult(true);
        };

        return t.Task;
    }

    public Task<bool> UnloadScene(string sceneName)
    {
        var t = new TaskCompletionSource<bool>();

        SceneManager.UnloadSceneAsync(sceneName).completed += delegate {
            t.TrySetResult(true);
        };

        return t.Task;
    }

    private void InitManagers()
    {
        //new GameObject("ThreadManager").AddComponent<ThreadManager>();
        //NetworkClient = new GameObject("NetworkClient").AddComponent<NetworkClient>();
        //EntityManager = new GameObject("EntityManager").AddComponent<EntityManager>();
        //new GameObject("CursorRenderer").AddComponent<CursorRenderer>();
        //new GameObject("GridRenderer").AddComponent<GridRenderer>();
        //new GameObject("ItemManager").AddComponent<ItemManager>();
    }
}
