using System.Security.Cryptography;
// Download: https://www.patreon.com/posts/3301589
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Prevent change
/// </summary>
public class SafePlayerPrefs {

    private string key;
    private List<string> properties = new List<string>();

    public SafePlayerPrefs (string key, params string [] properties)
    {
        this.key = key;
        foreach (string property in properties)
            this.properties.Add(property);
        Save();
    }

    // Generates the checksum
    private string GenerateChecksum ()
    {
        string hash = "";
        foreach (string property in properties)
        {
            hash += property + ":";
            if (PlayerPrefs.HasKey(property))
                hash += PlayerPrefs.GetString(property);
        }
        return MD5.Create(hash + key).ToString();
    }

    // Saves the checksum
    public void Save()
    {
        string checksum = GenerateChecksum();
        PlayerPrefs.SetString("CHECKSUM" + key, checksum);
        PlayerPrefs.Save();
    }

    // Checks if there has been an edit
    public bool HasBeenEdited ()
    {
        if (! PlayerPrefs.HasKey("CHECKSUM" + key))
            return true;

        string checksumSaved = PlayerPrefs.GetString("CHECKSUM" + key);
        string checksumReal = GenerateChecksum();

        return checksumSaved.Equals(checksumReal);
    }
    // ...
}