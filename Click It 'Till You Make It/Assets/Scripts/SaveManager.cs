using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    [Serializable]
    public struct SaveData
    {
        public float money;
        public float moneyPerSecond;

        public List<int> upgradePurchasedAmount;
    }

    int saveNumber;

    SaveData saveData;

    GameManager gameManager;
    UpgradeManager upgradeManager;

    public static event Action<SaveData> onGameplayLoaded;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += CheckLoadedScene;
        UIManager_Gameplay.onGameplayExit += SaveFile;
        UIManager_MainMenu.onGameLoad += LoadFile;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= CheckLoadedScene;
        UIManager_Gameplay.onGameplayExit -= SaveFile;
        UIManager_MainMenu.onGameLoad -= LoadFile;
    }

    void CheckLoadedScene(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Gameplay" && onGameplayLoaded != null)
            onGameplayLoaded(saveData);
    }

    void CreateFile(string filePath)
    {
        FileStream file = File.Create(filePath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        saveData.money = 0;
        saveData.moneyPerSecond = 0;
        saveData.upgradePurchasedAmount = new List<int>();

        binaryFormatter.Serialize(file, saveData);

        file.Close();
    }

    void UpdateFileValues()
    {
        saveData.money = gameManager.money;
        saveData.moneyPerSecond = gameManager.moneyPerSecond;
        saveData.upgradePurchasedAmount = new List<int>();

        int upgradeAmount = upgradeManager.upgradeAmount;
        List<UpgradeModel> upgrades = upgradeManager.upgrades;
        for (int i = 0; i < upgradeAmount; i++)
        {
            saveData.upgradePurchasedAmount.Add(upgrades[i].Amount);
        }
    }

    public void SaveFile()
    {
        string filePath = Application.persistentDataPath + "\\saves\\save" + saveNumber + ".dat";
        FileStream file = File.OpenWrite(filePath);
        BinaryFormatter binaryFormatter = new BinaryFormatter();

        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        upgradeManager = GameObject.Find("Upgrade Manager").GetComponent<UpgradeManager>();

        UpdateFileValues();

        binaryFormatter.Serialize(file, saveData);

        file.Close();
    }

    public void LoadFile(int number)
    {
        string filePath = Application.persistentDataPath + "\\saves\\save" + number + ".dat";

        saveNumber = number;

        if (!File.Exists(filePath))
        {
            CreateFile(filePath);
        }
        else
        {
            FileStream file = File.OpenRead(filePath);
            BinaryFormatter binaryFormatter = new BinaryFormatter();

            saveData = (SaveData)binaryFormatter.Deserialize(file);

            file.Close();
        }
    }
}