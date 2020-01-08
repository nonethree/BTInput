using UnityEditor;
using UnityEngine;

namespace BT.Input.Touch.Editor
{
    public class MakeMobileTouchSettingsMenu
    {
        [MenuItem("Assets/Create/BT/MobileTouchSettings")]
        public static void CreateMyAsset()
        {
            var asset = ScriptableObject.CreateInstance<MobileTouchSettings>();
            var path = string.Format("Assets/Resources/{0}.asset", Constants.SETTINGS_FILE_NAME);
            AssetDatabase.CreateAsset(asset, path);
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = asset;
        }
    }
}