using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAbilites : MonoBehaviour
{
    [SerializeField] private GameObject _specialAttackParent;
    [SerializeField] private PlayerData _playerDataManipulator;
    [SerializeField] private SpecialAttackTemplate[] _abilitesToCheckWithPlayerData;

    private List<string> PlayerAbilitysListToView = new List<string>();

    public void SpawnAvalibleAbilitis(EnemyTemplate _enemyToSpawn)
    {
        List<SpecialAttackTemplate> abilitysToSpawn = new List<SpecialAttackTemplate>();
        int[] playerAbilitys = _playerDataManipulator.Data.Abilitys;

        for (int i = 0; i < playerAbilitys.Length; i++)
        {
            for (int j = 0; j < _abilitesToCheckWithPlayerData.Length; j++)
            {
                if (playerAbilitys[i] == _abilitesToCheckWithPlayerData[j].Id)
                {
                    abilitysToSpawn.Add(_abilitesToCheckWithPlayerData[j]);

                    string currectTextToAdd = $"" +
                        $"<u>{_abilitesToCheckWithPlayerData[j].Title}</u>\n" +
                        $"you will take damge: <color=#00EAFF><i>{_abilitesToCheckWithPlayerData[j].PlayerHealthDamage}</i></color>\n" +
                        $"damage to enemy: <color=#FF3C00><i>{_abilitesToCheckWithPlayerData[j].GlobalDamage}</i></color>\n" +
                        $"time to restart: <i>{_abilitesToCheckWithPlayerData[j].GetComponent<SupportSpecialAttackView>().RestartEndAttackTime}</i>s\n" +
                        $"----\n";

                    PlayerAbilitysListToView.Add(currectTextToAdd);
                }
            }
        }

        for (int i = 0; i < abilitysToSpawn.Count; i++)
        {
            var ability = Instantiate(abilitysToSpawn[i], _specialAttackParent.transform);
            ability.Init(_enemyToSpawn, _playerDataManipulator);
        }
    }

    public string[] GetListAbilitysToView()
    {
        return PlayerAbilitysListToView.ToArray();
    }

    public void SpawnAbilitesTemplate(SpecialAttackTemplate[] abilitesToSpawn, EnemyTemplate _enemyToSpawn)
    {
        for (int i = 0; i < abilitesToSpawn.Length; i++)
        {
            var ability = Instantiate(abilitesToSpawn[i], _specialAttackParent.transform);
            ability.Init(_enemyToSpawn, _playerDataManipulator);
        }
    }

    public void Clear()
    {
        int nbChildren = _specialAttackParent.transform.childCount;

        for (int i = 0; i < +nbChildren; i++)
        {
            Destroy(_specialAttackParent.transform.GetChild(i).gameObject);
        }
    }
}
