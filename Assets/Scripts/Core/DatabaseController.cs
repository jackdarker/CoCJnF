using UnityEngine;
using System;
using System.IO;
using SQLite4Unity3d;

// https://docs.unity3d.com/ScriptReference/Application-streamingAssetsPath.html

public class DatabaseController
{
    #region Fields & Properties
    public readonly string databaseName;
    public string tempFilePath { get; private set; }
    public string saveFilePath { get; private set; }
    public string assetFilePath { get; private set; }
    public SQLiteConnection connection { get; private set; }
    #endregion

    #region Constructor
    public DatabaseController(string databaseName)
    {
        this.databaseName = databaseName;
        tempFilePath = Path.Combine(Application.persistentDataPath, string.Format("Temp/{0}", databaseName));
        saveFilePath = Path.Combine(Application.persistentDataPath, databaseName);
        assetFilePath = Path.Combine(Application.streamingAssetsPath, databaseName);
    }
    #endregion

    #region Public
    public void Load(SQLiteOpenFlags openFlags = SQLiteOpenFlags.ReadOnly, Action<DatabaseController> onComplete = null)
    {
        Unload();
        if ((openFlags & SQLiteOpenFlags.ReadWrite) == SQLiteOpenFlags.ReadWrite)
        {
            LoadReadWrite(openFlags, onComplete);
        }
        else
        {
            LoadReadOnly(openFlags, onComplete);
        }
    }

    public void Save()
    {
        if (File.Exists(tempFilePath))
        {
            Copy(tempFilePath, saveFilePath, null);
        }
        Debug.Log("Saved to: " + saveFilePath);
    }
    #endregion

    #region Private
    void LoadReadOnly(SQLiteOpenFlags openFlags, Action<DatabaseController> onComplete)
    {
        if (File.Exists(saveFilePath))
        {
            DidLoad(new SQLiteConnection(saveFilePath, openFlags, true), onComplete);
        }
        else if (!assetFilePath.Contains("://"))
        {
            DidLoad(new SQLiteConnection(assetFilePath, openFlags, true), onComplete);
        }
        else
        {
            Copy(assetFilePath, saveFilePath, delegate {
                DidLoad(new SQLiteConnection(saveFilePath, openFlags, true), onComplete);
            });
        }
    }

    void LoadReadWrite(SQLiteOpenFlags openFlags, Action<DatabaseController> onComplete)
    {
        string pathToCopy = File.Exists(saveFilePath) ? saveFilePath : (File.Exists(assetFilePath) ? assetFilePath : null);
        if (!string.IsNullOrEmpty(pathToCopy))
        {
            Copy(pathToCopy, tempFilePath, delegate {
                DidLoad(new SQLiteConnection(tempFilePath, openFlags, true), onComplete);
            });
        }
        else
        {
            var tempPath = Path.Combine(Application.persistentDataPath, "Temp");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);
            DidLoad(new SQLiteConnection(tempFilePath, openFlags, true), onComplete);
        }
    }

    void DidLoad(SQLiteConnection connection, Action<DatabaseController> onComplete)
    {
        this.connection = connection;
        if (onComplete != null)
            onComplete(this);
    }

    void Copy(string fromPath, string toPath, Action onComplete)
    {
        Directory.CreateDirectory(Path.GetDirectoryName(toPath));
        if (fromPath.Contains("://"))
        {
            StreamingAssetCopier.CopyToPersistentDataPath(fromPath, toPath, delegate {
                if (onComplete != null)
                    onComplete();
            });
        }
        else
        {
            File.Copy(fromPath, toPath, true);
            if (onComplete != null)
                onComplete();
        }
    }

    void Unload()
    {
        if (connection != null)
        {
            connection.Close();
            connection = null;
        }
    }
    #endregion
}

/*
public class SQLiteUtility {
	#region Public
	public static void Load (string databaseName, SQLiteOpenFlags openFlags, Action<SQLiteConnection> onComplete) {
		var tempFilePath = TempFilePath(databaseName);
		var pathToCopy = SaveFilePath(databaseName);

		if (!File.Exists(pathToCopy)) {
			pathToCopy = AssetFilePath(databaseName);
			if (!File.Exists(pathToCopy)) {
				Connect(databaseName, onComplete);
				return;
			}
		}

		if (pathToCopy.Contains("://")) {
			StreamingAssetCopier.CopyToPersistentDataPath(pathToCopy, tempFilePath, delegate {
				Connect(databaseName, onComplete);
			});
		} else {
			Copy(pathToCopy, tempFilePath);
			Connect(databaseName, onComplete);
		}
	}

	public static void Save (string databaseName) {
		var tempFilePath = TempFilePath(databaseName);
		var saveFilePath = SaveFilePath(databaseName);

		if (!File.Exists(tempFilePath)) {
			return;
		}

		Copy(tempFilePath, saveFilePath);
		Debug.Log("Saved to: " + saveFilePath);
	}
	#endregion

	#region Private
	static string TempFilePath (string databaseName) {
		return Path.Combine(Application.persistentDataPath, string.Format("Temp/{0}", databaseName));
	}

	static string SaveFilePath (string databaseName) {
		return Path.Combine(Application.persistentDataPath, databaseName);
	}

	static string AssetFilePath (string databaseName) {
		return Path.Combine(Application.streamingAssetsPath, databaseName);
	}

	static void Connect (string databaseName, Action<SQLiteConnection> onComplete) {
		var tempFilePath = TempFilePath(databaseName);
		var connection = new SQLiteConnection(tempFilePath, SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create);
		Debug.Log("Loaded: " + tempFilePath);
		if (onComplete != null)
			onComplete(connection);
	}

	static void Copy (string fromPath, string toPath) {
		Directory.CreateDirectory(Path.GetDirectoryName(toPath));
		File.Copy(fromPath, toPath, true);
	}
	#endregion
}
*/
