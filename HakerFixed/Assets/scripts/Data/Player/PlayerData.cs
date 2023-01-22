using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public Computer PC = new Computer(1, 1, 1, 1);
    public PlayerDataToSave Data = new PlayerDataToSave();
    public PrologData Prolog = new PrologData();
    public string VersionAplication = "";

    [Serializable]
    public class PlayerDataToSave
    {
        public int BTC = 1;
        public int LvlExpieriens = 1;
        public int LvlStatus = 1;
        public int LvlMashine = 0;
        public int LvlSolution = 1;
        public int LvlMaxHealth = 120;
        public int CountBreakMashine = 0;
        public int Health = 100;

        public int[] Abilitys = new int[4];

        public bool Telegram = false;
        public bool Ads = false;
        public string NickName = "Nigirenio";
        public string _currentMashineIp = "192.228.735";
    }

    [SerializeField] private Program[] _instalingPrograms;

    public List<string> FinishTaskFormChrome = new List<string>();

    public int CountInstallingPrograms => _instalingPrograms.Length;
    public string CurrentMashineIp => Data._currentMashineIp;

    private void Awake()
    {
        LoadData();
    }

    public void TakeDamageAttack(int damage)
    {
        Data.Health -= damage;

        if(Data.Health <= 0)
        {
            Data.Health = 0;
            Data.LvlMaxHealth -= 1;
        }
    }

    public void SaveData()
    {
        PlayerPrefs.SetString("savePlayerData", JsonUtility.ToJson(Data));
        PlayerPrefs.SetString("savePrologData", JsonUtility.ToJson(Prolog));
    }

    public void LoadData()
    {
        VersionAplication = Application.version;

        if (PlayerPrefs.HasKey("savePlayerData"))
        {
            Data = JsonUtility.FromJson<PlayerDataToSave>(PlayerPrefs.GetString("savePlayerData"));
        }
        else
        {
            PlayerPrefs.SetString("savePlayerData", JsonUtility.ToJson(Data));
            Data = JsonUtility.FromJson<PlayerDataToSave>(PlayerPrefs.GetString("savePlayerData"));
        }
    }

    public void LoadPrologData()
    {
        if (PlayerPrefs.HasKey("savePrologData"))
        {
            Prolog = JsonUtility.FromJson<PrologData>(PlayerPrefs.GetString("savePrologData"));
        }
        else
        {
            PlayerPrefs.SetString("savePrologData", JsonUtility.ToJson(Prolog));
            Prolog = JsonUtility.FromJson<PrologData>(PlayerPrefs.GetString("savePrologData"));
        }
    }

    public void UpdateDataCurrentPrograms()
    {
        for (int i = 0; i < _instalingPrograms.Length; i++)
        {
            _instalingPrograms[i].LoadDataCurrentProgram();
        }
    }

    public bool AddTakeMoney(int value)
    {
        Data.BTC = CalculateCurrectValue(Data.BTC, value);
        return true;
    } 

    public bool AddTakeLvlExpieiens(int value)
    {
        Data.LvlExpieriens = CalculateCurrectValue(Data.LvlExpieriens, value);
        return true;
    }

    public int CalculateProcentValue(float procent, int valueToConvert = -1)
    {
        int bust;

        if(valueToConvert < 0)
        {
            bust = Convert.ToInt32((Data.LvlExpieriens * procent) / 100);
        }
        else
        {
            bust = Convert.ToInt32((valueToConvert * procent) / 100);
        }

        bust = bust + 1;

        return bust;
    }

    public bool AddTakeHealth(int value)
    {
        if(value + Data.Health > Data.LvlMaxHealth)
        {
            Data.Health = Data.LvlMaxHealth;
            return true;
        }

        Data.Health = CalculateCurrectValue(Data.Health, value);
        return true;
    }

    public bool AddTakeStatus(int value)
    {
        Data.LvlStatus = CalculateCurrectValue(Data.LvlStatus, value);
        return true;
    }

    public bool AddBreakMashine(int value = 1)
    {
        if(value <= 0)
        {
            value = 1;
        }

        Data.CountBreakMashine = Data.CountBreakMashine + value;
        return true;
    }

    public string GetCountNeedPlayerParametrsForBouing(int indexProgramToBuy)
    {
        return $"You need: <color=red>{_instalingPrograms[indexProgramToBuy].Cost}</color>|BTC and <color=red>{_instalingPrograms[indexProgramToBuy].ExpieriensToBuy}</color>|EXP";
    }

    public bool BuyCurrectProgramm(int indexProgramToBuy)
    {
        if(Data.BTC >= _instalingPrograms[indexProgramToBuy].Cost && Data.LvlExpieriens >= _instalingPrograms[indexProgramToBuy].ExpieriensToBuy)
        {
            Data.BTC -= _instalingPrograms[indexProgramToBuy].Cost;
            _instalingPrograms[indexProgramToBuy].Save.IsBuyed = true;
            _instalingPrograms[indexProgramToBuy].SaveDataCurrentProgram();
            
            return true;
        }
        else
        {
            return false;
        }
    }

    public Program GetFirstOrDefaultPlayerProgram()
    {
        return _instalingPrograms[0];
    }

    public Program GetProgramUnderIndex(int index)
    {
        _instalingPrograms[index].LoadDataCurrentProgram();

        return _instalingPrograms[index];
    }

    public Program GetInstallProgram(int index)
    {
        _instalingPrograms[index].LoadDataCurrentProgram();

        return _instalingPrograms[index];
    }

    private int CalculateCurrectValue(int startValue, int addingValue)
    {
        int count = startValue + addingValue;

        if (count <= 0)
            count = 0;

        return count;
    }
}
