using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillInfo
{
    public int id;
    public string name;
    public int mp;
    public int coldTime;
    public int applyTime;
    public int distance;
    public int level;
    public SkillType skillType;
    public string description;
    public float value;
    public string sprite;
    public string effectPath;
}

public enum SkillType
{
    Buff,
    Normal,
    Direction,
    Position
}
