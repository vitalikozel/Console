using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileToLooks
{
    public string Name;
    public string Description;
    public bool IsFolder;
    public List<FileToLooks> Files;

    public FileToLooks(string name, List<FileToLooks> folder, string description = "", bool isFolder = false)
    {
        Name = name;
        Files = folder;
        Description = description;
        IsFolder = isFolder;
    }
}
