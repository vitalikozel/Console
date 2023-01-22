using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer
{
    public Dictionary<string, string> Parameters = new Dictionary<string, string>()
    {
        { "Processor", "NULL"},
        { "VideoCard", "NULL"},
        { "Motherboard", "NULL"},
        { "RAM", "NULL"},
        { "SSDM2", "NULL"},
        { "SSDDrive", "NULL"},
        { "HDD", "NULL"},
        { "PowerSupply", "NULL"},
        { "CoolingProcessor", "NULL"},
        { "Case", "NULL"},
        { "SOFT", "NULL"},
    };

    public int Health = 100;
    public bool IsBreak;
    public bool IsBreakProcessor;

    private GenerateRandomHaracteristicsLevel generate = new GenerateRandomHaracteristicsLevel();

    public Computer()
    {
        Parameters["Processor"] = generate.GetRandomProcessor();
        Parameters["Motherboard"] = generate.GetRandomMotherBoard();
        Parameters["VideoCard"] = generate.GetRandomVideoCard();
        Parameters["SOFT"] = generate.GetRandomOS();
    }

    public Computer(int processor, int videoCard, int motherBoard, int soft)
    {
        Parameters["Processor"] = generate.GetRandomProcessor(processor);
        Parameters["Motherboard"] = generate.GetRandomMotherBoard(videoCard);
        Parameters["VideoCard"] = generate.GetRandomVideoCard(motherBoard);
        Parameters["SOFT"] = generate.GetRandomOS(soft);
    }
}

public class GenerateRandomHaracteristicsLevel
{
    public string GetRandomProcessor(int index = -1)
    {
        string[] processors =
        {
            "Xeon_W 1390P 8/16",
            "Core i9-11900K 8/16",
            "Core i9-11900KF 8/16",
            "Xeon_W 1370P 8/16",
            "Core i7-11700K 8/16",
            "Core i7-11700KF 8/16",
            "Core i9-10900K 10/20"
        };

        if(index != -1)
        {
            return processors[index];
        }

        return processors[Random.Range(0, processors.Length - 1)];
    }

    public string GetRandomVideoCard(int index = -1)
    {
        string[] VideoCard =
        {
            "GeForce® GTX TITAN. GeForce® GTX TITAN Z",
            "GeForce RTX™ 40 Series. GeForce RTX™ 4090",
            "GeForce RTX™ 30 Series. GeForce RTX™ 3090 Ti",
            "GeForce RTX™ 20 Series. GeForce RTX™ 2080 Ti",
            "GeForce® GTX 1660 Ti. GeForce® GTX 1660 SUPER™",
            "GeForce® GTX 10 Series. GeForce® GTX 1080 Ti",
            "AORUS GeForce RTX™ 3080 XTREME WATERFORCE 10G"
        };

        if (index != -1)
        {
            return VideoCard[index];
        }

        return VideoCard[Random.Range(0, VideoCard.Length - 1)];
    }

    public string GetRandomMotherBoard(int index = -1)
    {
        string[] matherBoard =
        {
            "ASUS TUF Gaming Z590-Plus",
            "ASUS ROG Strix B560-A Gaming",
            "ASRock B560M Steel Legend",
            "ASRock Z490 Phantom Gaming-ITX / TB3",
            "MSI MEG Z590 Godlike"
        };

        if (index != -1)
        {
            return matherBoard[index];
        }

        return matherBoard[Random.Range(0, matherBoard.Length - 1)];
    }

    public string GetRandomOS(int index = -1)
    {
        string[] os =
        {
            "Linux",
            "Windows"
        };

        if (index != -1)
        {
            return os[index];
        }

        return os[Random.Range(0, os.Length - 1)];
    }
}