using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class JsonParser {

    private string path;
    private string jsonString;
    private jsonClass parsedJson;

    public jsonClass parse(string filename)
    {
        path = Application.streamingAssetsPath + "/json/" + filename;
        jsonString = File.ReadAllText(path);
        parsedJson = JsonUtility.FromJson<jsonClass>(jsonString);
        return parsedJson;
    }
}

[System.Serializable]
public class jsonClass
{
    public agentAttr agentAttr;
    public List<node> nodes;
}

[System.Serializable]
public class agentAttr
{
    public short mood; //range -10 <-> 10 neutral -3 <=> 3
    public int goal;
    public float patience;
}

[System.Serializable]
public class node
{
    public string name;
    public int wait;
    public List<string> dialogue;
    public List<string> animation;
    public List<string> listen;
    public List<short> change;
    public List<string> toNode;
    public string noResponse;
    public short noResponseChange;
}