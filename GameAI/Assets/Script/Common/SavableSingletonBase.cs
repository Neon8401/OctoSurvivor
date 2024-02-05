using UnityEngine;
using System;
using System.IO;
using System.Security.Cryptography;

/// <summary>
/// PlayerPrefまたはファイルにデータを保存できるクラス
/// このクラスを継承すると、Save()メソッドを呼ぶことでpublicまたは[SerializeField]にしたフィールドをJSONに保存できる
/// </summary>
public abstract class SavableSingletonBase<T> where T : SavableSingletonBase<T>, new()
{
    private static T _instance;
    private bool _isLoaded;

    /// <summary>
    /// シリアライズしたJSONをPlayerPrefに保存するか、ファイルに保存するかの設定。継承したクラスでtrue/falseを指定可能にしている
    /// </summary>
    protected virtual bool IsSaveToPlayerPref => true;

    public static T Instance
    {
        get
        {
            if (null != _instance) return _instance;

            string json;
            _instance = new T();
            // インスタンス生成時にデータを自動ロードする
            if (_instance.IsSaveToPlayerPref)
            {
                json = PlayerPrefs.GetString(SaveKey);
            }
            else
            {
                json = File.Exists(SavePath) ? File.ReadAllText(SavePath) : "";
            }

            if (string.IsNullOrEmpty(json) || !LoadFromJson(json))
            {
                _instance._isLoaded = true;
            }

            return _instance;
        }
    }

    /// <summary>
    /// データをJSONにシリアライズ
    /// </summary>
    protected virtual string SerializedData => JsonUtility.ToJson(this);

    private static string SavePath => $"{Application.persistentDataPath}/{SaveKey}";

    private static string SaveKey
    {
        get
        {
            // クラス名のハッシュ値を生成している
            var provider = new SHA1CryptoServiceProvider();
            var hash = provider.ComputeHash(
                System.Text.Encoding.ASCII.GetBytes(typeof(T).FullName ?? throw new InvalidOperationException()));
            return BitConverter.ToString(hash);
        }
    }

    /// <summary>
    /// JSONデータからデータを復元します。
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public static bool LoadFromJson(string json)
    {
        try
        {
            _instance = JsonUtility.FromJson<T>(json);
            _instance._isLoaded = true;
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning(e.ToString());
            return false;
        }
    }

    /// <summary>
    /// データを保存します。
    /// </summary>
    public void Save()
    {
        if (!_isLoaded) return;

        if (IsSaveToPlayerPref)
        {
            PlayerPrefs.SetString(SaveKey, SerializedData);
            PlayerPrefs.Save();
        }
        else
        {
            var path = SavePath;
            File.WriteAllText(path, SerializedData);
#if UNITY_IOS
            // iOSでデータをiCloudにバックアップさせない設定
            UnityEngine.iOS.Device.SetNoBackupFlag(path);
#endif
        }
    }

    /// <summary>
    /// データをリセットします。
    /// </summary>
    public void Reset()
    {
        _instance = null;
    }

    /// <summary>
    /// データを削除します。
    /// </summary>
    public void Delete()
    {
        if (IsSaveToPlayerPref)
        {
            PlayerPrefs.DeleteKey(SaveKey);
            PlayerPrefs.Save();
        }
        else
        {
            if (File.Exists(SavePath))
            {
                File.Delete(SavePath);
            }
        }

        _instance = null;
    }
}