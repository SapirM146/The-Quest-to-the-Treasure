using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;


public static class SaveSystem
{
    static string path = Application.persistentDataPath + "/player.sav";
    //  C:\Users\ {User} \AppData\LocalLow\DefaultCompany\World of Mysteries
    //  C:\Users\Sapir\AppData\LocalLow\DefaultCompany\World of Mysteries

    public static void SavePlayer(PlayerData player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, player);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            PlayerData player = formatter.Deserialize(stream) as PlayerData;
            stream.Close();
            return player;
        }

        return null;
    }
}
