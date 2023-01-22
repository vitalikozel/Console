using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public abstract class Program : MonoBehaviour
{
    [SerializeField] private string _codeName;
    [SerializeField] private int _cost;
    [SerializeField] private int _expieriensToBuy;
    [SerializeField] protected bool _IsAlreadyInstall;

    private Command[] _commands;

    public SaveDataProgram Save;

    public string CodeName => _codeName;
    public int Cost => _cost;
    public int ExpieriensToBuy => _expieriensToBuy;

    public abstract void TransformOnThisAplication();

    private void Start()
    {
        Save.IsBuyed = _IsAlreadyInstall;
    }

    public virtual int SendEnterCommand(string EnteringCommand, conclusionViewCommnd view, GlobalAplicationParametrs workTimeSleep)
    {
        if(EnteringCommand.Contains(".commands"))
        {
            ShowAllCommandsToCurrentProgram(view);

            return 0;
        }

        foreach (Command command in _commands)
        {
            if (EnteringCommand.Contains(command.CurrectCommand.ToLower()))
            {
                string currentFlag = "";
                string currentArgument = "";

                string argumentWithFlag = EnteringCommand.Replace(command.CurrectCommand, "");
                currentArgument = argumentWithFlag;

                int flagPosition = argumentWithFlag.LastIndexOf(" -");
                if(flagPosition > 0)
                {
                    currentFlag = argumentWithFlag.Substring(flagPosition);
                    currentArgument = argumentWithFlag.Substring(0, flagPosition);
                }

                return command.Doing(view, currentArgument, currentFlag, workTimeSleep);
            }
        }

        return 1;
    }

    public void ShowAllCommandsToCurrentProgram(conclusionViewCommnd view)
    {
        view.ConclusionText("---Commands---");
        for (int i = 0; i < _commands.Length; i++)
        {
            if (!_commands[i].Debug)
            {
                view.ConclusionText($"{_commands[i].CurrectCommand}{_commands[i].Discription}");
            }
        }
        view.ConclusionText("------");
    }

    public virtual void LoadDataCurrentProgram()
    {
        if (PlayerPrefs.HasKey(CodeName))
        {
            Save = JsonUtility.FromJson<SaveDataProgram>(PlayerPrefs.GetString(CodeName));
        }
        else
        {
            Save = new SaveDataProgram();
        }
    }

    public virtual void SaveDataCurrentProgram()
    {
        PlayerPrefs.SetString(CodeName, JsonUtility.ToJson(Save));
    }

    public class SaveDataProgram
    {
        public bool IsBuyed;
    }

    public virtual void SetCurrectCommandsAplication(Command[] commands)
    {
        _commands = commands;
    }

    public List<string> commandsToString()
    {
        List<string> commandsToSend = new List<string>();

        for (int i = 0; i < _commands.Length; i++)
        {
            if (!_commands[i].Debug)
            {
                commandsToSend.Add(_commands[i].CurrectCommand);
            }
        }

        return commandsToSend;
    }
}
