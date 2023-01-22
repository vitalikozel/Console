using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Bot Mashine Attack", menuName = "Attack", order = 51)]
public class AttackTemplateForProtection : ScriptableObject
{
    public string Title;
    public string MailMessage;
    public int CountAttacks;
    public int StatusDamage;
    public int MoneyDamage;
    public float MinClickLifeTime;
    public float MaxClickLifeTime;

    public ClickEnemy[] Enemys;
}
