using UnityEngine;

public class TxtReader 
{
    public static string GetTxtFile(string name)
    {
        var path = "TxtFile/" + name;
        TextAsset asset = ResManager.Instance.Load<TextAsset>(path);
        return asset.text;
    }
}
