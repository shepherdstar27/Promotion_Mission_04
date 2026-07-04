using System;

[Serializable]
public class ItemData
{
    public string Id;
    public string Name;
    public string Description;
    public string IconPath;
    public string UseType;  // 효과 종류 ("Heal", "None", "Unusable")
    public int UseValue;    // 효과 수치
}