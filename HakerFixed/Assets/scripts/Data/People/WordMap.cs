using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordMap : MonoBehaviour
{
    public string[] Citys =
    {
        "Vodohlobus",
        "Alpha",
        "Brodno",
        "Yahei",
        "Zaembija",
        "Night"
    };

    public string Country = "Freedom";
    public string AvalibeCity;

    public WordMap(int index = -1)
    {
        if(index == -1)
        {
            AvalibeCity = Citys[Random.Range(0, Citys.Length)];
        }

        AvalibeCity = Citys[index];
    }
}
