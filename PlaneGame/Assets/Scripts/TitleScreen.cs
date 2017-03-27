using UnityEngine;
using System.Collections;

[AddComponentMenu("MyGame/TitleScreen")]
public class TitleScreen : MonoBehaviour {
	void OnGUI(){
		GUI.skin.label.fontSize = 50;
		GUI.skin.label.alignment = TextAnchor.LowerCenter;

		GUI.Label (new Rect (0, 30, Screen.width, 100), "飞机大战");

		if (GUI.Button (new Rect (Screen.width * 0.5f - 100, Screen.height * 0.7f, 200, 30), "开始游戏")) {
			Application.LoadLevel ( "level1" );
		}
	}
}
