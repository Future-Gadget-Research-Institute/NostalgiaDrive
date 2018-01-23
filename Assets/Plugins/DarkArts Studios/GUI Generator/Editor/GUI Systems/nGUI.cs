using UnityEngine;
using UnityEditor;
using System.Collections;

namespace DarkArtsStudios.GUIGenerator
{
    [InitializeOnLoad]
    public class nGUI : GUISystem
    {
        private static System.Type nGUIAtlasType = null;
        private static System.Type nGUIUIAtlasMakerType = null;
        private static System.Type nGUIUIAtlasMakerSpriteEntryType = null;

        static nGUI ()
        {
            GUISystem ngui = new nGUI ("nGUI") as GUISystem;
            ngui.name = "nGUI Sprite & Atlas";
            RegisterGUISystem (ngui);

            nGUIAtlasType = GetType("UIAtlas");
            nGUIUIAtlasMakerType = GetType("UIAtlasMaker");
            nGUIUIAtlasMakerSpriteEntryType = GetType("UIAtlasMaker+SpriteEntry");
        }

        public nGUI (string _name)
        {
            _name = name;
        }

        public override bool Exists ()
        {
            return nGUIAtlasType != null && nGUIUIAtlasMakerType != null;
        }

        public UnityEngine.Object atlas;

        public override bool OnGUI ()
        {
            EditorGUILayout.LabelField("Drag an existing atlas you would like to update");
            EditorGUILayout.LabelField("or leave it blank to create a new atlas.");
            atlas = EditorGUILayout.ObjectField (atlas, nGUIAtlasType, false);
            return true;
        }

        public override void OnTexture (global::DarkArtsStudios.GUIGenerator.Sprite sprite)
        {
            string spriteAssetPath = AssetDatabase.GetAssetPath( sprite.texture );
            string baseFolder = System.IO.Path.GetDirectoryName( spriteAssetPath );
            string baseName = System.IO.Path.GetFileNameWithoutExtension( spriteAssetPath );

            if (atlas == null)
            {
                string atlasPath = string.Format("{0}/nGUI Atlas {1}.prefab", baseFolder, baseName);
                GameObject nguiAtlasGameObject = new GameObject();
                nguiAtlasGameObject.name = "GUI Generator nGUI Atlas";
                nguiAtlasGameObject.AddComponent( nGUIAtlasType );
                GameObject nguiAtlasPrefab = PrefabUtility.CreatePrefab( atlasPath, nguiAtlasGameObject );
                GameObject.DestroyImmediate( nguiAtlasGameObject );
                atlas = nguiAtlasPrefab.GetComponent( nGUIAtlasType );
                Material material = new Material(Shader.Find("Unlit/Transparent"));
                AssetDatabase.AddObjectToAsset( material, atlas );
                nGUIAtlasType.GetProperty("spriteMaterial").SetValue( atlas, material ,null);
            }

            object spriteEntry = System.Activator.CreateInstance( nGUIUIAtlasMakerSpriteEntryType );
            nGUIUIAtlasMakerSpriteEntryType.GetField("tex").SetValue(spriteEntry,sprite.texture);
            nGUIUIAtlasMakerSpriteEntryType.GetField("name").SetValue(spriteEntry,baseName);
            nGUIUIAtlasMakerSpriteEntryType.GetField("borderBottom").SetValue(spriteEntry, Mathf.RoundToInt(sprite.borders.y));
            nGUIUIAtlasMakerSpriteEntryType.GetField("borderTop").SetValue(spriteEntry, Mathf.RoundToInt(sprite.borders.w));
            nGUIUIAtlasMakerSpriteEntryType.GetField("borderLeft").SetValue(spriteEntry, Mathf.RoundToInt(sprite.borders.x));
            nGUIUIAtlasMakerSpriteEntryType.GetField("borderRight").SetValue(spriteEntry, Mathf.RoundToInt(sprite.borders.z));
            nGUIUIAtlasMakerSpriteEntryType.GetField("width").SetValue(spriteEntry,sprite.texture.width);
            nGUIUIAtlasMakerSpriteEntryType.GetField("height").SetValue(spriteEntry,sprite.texture.height);

            System.Reflection.MethodInfo addOrUpdate = nGUIUIAtlasMakerType.GetMethod("AddOrUpdate",new System.Type [] {nGUIAtlasType,nGUIUIAtlasMakerSpriteEntryType});
            addOrUpdate.Invoke(null,new object[] { atlas, spriteEntry });

            AssetDatabase.ImportAsset (AssetDatabase.GetAssetPath(atlas), ImportAssetOptions.ForceUpdate);
        }
    }

}
