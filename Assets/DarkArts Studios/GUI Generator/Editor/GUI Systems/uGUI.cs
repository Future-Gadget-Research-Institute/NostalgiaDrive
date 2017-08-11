using UnityEngine;
using UnityEditor;
using System.Collections;

namespace DarkArtsStudios.GUIGenerator
{
	[InitializeOnLoad]
	public class uGUI : GUISystem
	{
		static uGUI ()
		{
			GUISystem ugui = new uGUI ("uGUI") as GUISystem;
			ugui.name = "uGUI Sprite";
			RegisterGUISystem (ugui);
		}
		
		public uGUI (string _name)
		{
			_name = name;
		}
		
		public override bool Exists ()
		{
			#if (UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 )
				return false;
			#else
				return true;
			#endif
		}
		
		public override bool OnGUI ()
		{
			EditorGUILayout.LabelField( "uGUI sprite modifiers will be added to this sprite");
			return true;
		}
		
		public override void OnTexture (global::DarkArtsStudios.GUIGenerator.Sprite sprite)
		{
			#if (UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5 )
			#else
				string spriteAssetPath = AssetDatabase.GetAssetPath( sprite.texture );
				TextureImporter spriteImporter = TextureImporter.GetAtPath( spriteAssetPath ) as TextureImporter;
				spriteImporter.spriteBorder = sprite.borders;
				AssetDatabase.ImportAsset (spriteAssetPath, ImportAssetOptions.ForceUpdate);
			#endif
		}
	}
	
}
