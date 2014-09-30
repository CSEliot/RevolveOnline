#pragma strict

var style : GUIStyle;
var style2 : GUIStyle;

function Start () {

}

function Update () {

}

function OnGUI()
{
	if(GUI.Button(Rect(Screen.width/2-50, Screen.height/2+100, 100, 100), "Play", style2))
	{
		Application.LoadLevel(Application.loadedLevel+1);
	}
	
	style.normal.textColor.a = 1;
	GUI.Label(Rect(Screen.width/2-50, Screen.height/2-50, 100, 100), "Revolve Online", style);
	style.normal.textColor.a = .3;
}