using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
public class SettingsDataPersistenceManager : MonoBehaviour
{
    [Header("Debugging")]
    [SerializeField] private bool disableSettingsDataPersistence = false;
    [SerializeField] private bool initializeDataIfNull = false;
    [SerializeField] private bool overideSelectedProfileId = false;
    [SerializeField] private string debugSelectedProfileId = "Debug";

    [Header("File Storage Config")]

    [SerializeField] private string fileName;
    [SerializeField] private bool useEncryption;

    private SettingsData SettingsData;
    private List<SettingsDataPersistence> dataPersistencesObjects;
    private FileSettingsDataHandler settingsHandler;

    private string selectedProfileId = "";
    public static SettingsDataPersistenceManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Found one or more SettingsDataPersistenceManagers, deleting object...");
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        if (disableSettingsDataPersistence)
        {
            Debug.LogWarning("Data Persistence is currently disabled!");
        }
        DontDestroyOnLoad(this.gameObject);

        this.settingsHandler = new FileSettingsDataHandler(Application.persistentDataPath, fileName, useEncryption);

        this.selectedProfileId = settingsHandler.GetMostRecentlyUpdatedProfileId();
        if (overideSelectedProfileId)
        {
            this.selectedProfileId = debugSelectedProfileId;
            Debug.LogWarning("Override selected profile id with debugging id, " + debugSelectedProfileId);
        }
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistencesObjects = FindAllDataPersistenceObjects();
        LoadGame();
    }

    public void ChangeSelectedProfileId(string newProfileId)
    {
        this.selectedProfileId = newProfileId;

        LoadGame();
    }
    public void NewGame()
    {
        this.SettingsData = new SettingsData();
    }

    public void LoadGame()
    {
        if (disableSettingsDataPersistence)
        {
            return;
        }
        this.SettingsData = settingsHandler.Load(selectedProfileId);

        if (this.SettingsData == null && initializeDataIfNull)
        {
            NewGame();
        }
        //if no data is found, we will initialize to a new game
        if (this.SettingsData == null)
        {
            Debug.LogWarning("No data was found. A new game needs to be started");
            return;
        }

        //looking through the list
        foreach (SettingsDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.LoadData(SettingsData);
        }

    }

    public void SaveGame()
    {
        if (disableSettingsDataPersistence)
        {
            return;
        }

        if (this.SettingsData == null)
        {
            Debug.LogWarning("No data was found. A New Game needs to be created");
            return;
        }

        foreach (SettingsDataPersistence dataPersistenceObj in dataPersistencesObjects)
        {
            dataPersistenceObj.SaveData(SettingsData);
        }
        SettingsData.lastUpdated = System.DateTime.Now.ToBinary();
        settingsHandler.Save(SettingsData, selectedProfileId);
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }

    //returns a list of type SettingsDataPersistence
    private List<SettingsDataPersistence> FindAllDataPersistenceObjects()
    {
        //find all scripts that use the IDataPersistence Interface
        IEnumerable<SettingsDataPersistence> settingsDataPersistenceObjects = FindObjectsOfType<MonoBehaviour>(true)
            .OfType<SettingsDataPersistence>();

        return new List<SettingsDataPersistence>(settingsDataPersistenceObjects);
    }

    public bool HasGameData()
    {
        return SettingsData != null;
    }

    public Dictionary<string, SettingsData> GetAllProfilesGameData()
    {
        return settingsHandler.LoadAllProfiles();
    }
}

