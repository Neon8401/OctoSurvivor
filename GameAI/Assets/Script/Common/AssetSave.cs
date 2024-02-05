using UnityEditor;
using UnityEngine;
public class AssetSave : MonoBehaviour
{
    [SerializeField]
    ScriptableObject Table;
    void Start()
    {
#if UNITY_EDITOR
        EditorUtility.SetDirty(Table);
        AssetDatabase.SaveAssets();
#endif
    }
}