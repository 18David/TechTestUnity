using System;
using UnityEngine;

[Serializable]
public class ObjectToDonwload
{
    [SerializeField]
    private string _name;
    [SerializeField]
    private string _url;

    public string Name { get => _name; set => _name = value; }
    public string Url { get => _url; set => _url = value; }

    public ObjectToDonwload(string name, string url)
    {
        _name = name;
        _url = url;
    }
}

