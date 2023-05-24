using UnityEngine;

[System.Serializable]
public class Wallet
{
    private static Wallet instance = null;
    public static Wallet Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Wallet();
                instance.Load();
            }
            return instance;
        }
    }

    public float coins;

    private Wallet()
    {
        coins = 0;
    }

    public void AddCoins(float amount)
    {
        coins += amount;
        Save();
    }

    public void RemoveCoins(float amount)
    {
        coins -= amount;
        Save();
    }
    public void ResetCoins()
    {
        coins = 0;
        Save();
    }

    public void Save()
    {
        string json = JsonUtility.ToJson(this);
        PlayerPrefs.SetString("wallet", json);
    }

    public void Load()
    {
        if (PlayerPrefs.HasKey("wallet"))
        {
            string json = PlayerPrefs.GetString("wallet");
            Wallet savedWallet = JsonUtility.FromJson<Wallet>(json);
            coins = savedWallet.coins;
        }
    }
}
