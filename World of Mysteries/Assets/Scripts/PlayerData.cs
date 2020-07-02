using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public int Level { get; set; }
    public int Stage { get; set; }

    public PlayerData(int level, int stage=0)
    {
        Level = level;
        Stage = stage;
    }
}
