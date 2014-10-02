using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class ModGUI : MonoBehaviour {

	public Vector2 selection;
	public Rect windowRect;

	void Start()
	{
		windowRect = new Rect(Screen.width/2-200, Screen.height/2-200, 400, 400);
	}

	void OnGUI() {
		windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Poop");
	}

	void DoMyWindow(int windowID) 
	{
		selection = GUILayout.BeginScrollView(selection, GUILayout.Width(400), GUILayout.Height(400));

		//get dat game masta
		GameObject gm = GameObject.FindGameObjectWithTag ("GM");

		//get dat GAME_VALUES type
		Type type = gm.GetComponent<GameMaster>()._M.GetType();

		//get dem properties
		FieldInfo[] properties = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		Debug.Log (properties.Length);
		foreach(FieldInfo m in properties)
		{
			//this is the first column with names and values
			GUILayout.BeginHorizontal();
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)))
			{
				//so basically just make a box with info if it's a float
				GUILayout.Box(m.Name+": "+m.GetValue(gm.GetComponent<GameMaster>()._M), GUILayout.Width(200));
			}
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(bool)))
			{
				//make a box with just the name if it's a boolean
				GUILayout.Box(m.Name+": ", GUILayout.Width(200));
			}

			//this column has the '+' signs and toggle buttons
			GUILayout.BeginVertical();
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)))
			{
				if(GUILayout.Button("-", GUILayout.Width(20)))
				{
					//box in that shit and modify
					object p = (object)gm.GetComponent<GameMaster>()._M;
					m.SetValue(p, (float)m.GetValue(gm.GetComponent<GameMaster>()._M)-1f);
					gm.GetComponent<GameMaster>()._M = (GameMaster.GAME_VALUES)p;
				}
			}
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(bool)))
			{
				//box in that shit and modify
				object p = (object)gm.GetComponent<GameMaster>()._M;
				m.SetValue(p, (bool)GUILayout.Toggle((bool)m.GetValue(gm.GetComponent<GameMaster>()._M), ""));
				gm.GetComponent<GameMaster>()._M = (GameMaster.GAME_VALUES)p;
			}
			GUILayout.EndVertical();

			//this is the column that contains the '-' signs
			GUILayout.BeginVertical();
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)))
			{
				if(GUILayout.Button("+", GUILayout.Width(20)))
				{
					//box in that shit and modify
					object p = (object)gm.GetComponent<GameMaster>()._M;
					m.SetValue(p, (float)m.GetValue(gm.GetComponent<GameMaster>()._M)+1f);
					gm.GetComponent<GameMaster>()._M = (GameMaster.GAME_VALUES)p; 
				}
			}
			GUILayout.EndVertical();

			GUILayout.Space(125F);

			GUILayout.EndHorizontal();
		}
		GUILayout.EndScrollView();
	}
}
