using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class ModGUI : MonoBehaviour {

	public Vector2 selection;
	public Rect windowRect;

	private GameMaster.GAME_VALUES original; 

	public bool changed = false;

	void Start()
	{
		windowRect = new Rect(Screen.width/2-200, Screen.height/2-300, 400, Screen.height-500);
		original = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ()._M;
	}

	void OnGUI() {
		if(GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ().isGameOver())
			windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Poop");
	}

	void DoMyWindow(int windowID) 
	{
		selection = GUILayout.BeginScrollView(selection, GUILayout.Width(400), GUILayout.Height(Screen.height-500));

		//get dat game masta
		GameObject gm = GameObject.FindGameObjectWithTag ("GM");

		//get dat GAME_VALUES type
		Type type = gm.GetComponent<GameMaster>()._M.GetType();

		//get dem properties
		FieldInfo[] properties = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

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

		GUILayout.Space(20F);

		if(original.Equals(gm.GetComponent<GameMaster>()._M))
		{
			GUILayout.Box("Make A Change");
		}
		else
		{
			if(oneChangeMade(original))
			{
				if(GUILayout.Button("Confirm"))
				{
					GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ().Save_Values();
					Application.LoadLevel(Application.loadedLevel);
				}
				if(GUILayout.Button("Revert"))
				{
					GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ()._M = original;
				}
			}
			else
			{
				GUILayout.Box("Only One Change Per Win");
			}
		}
	}

	private bool oneChangeMade(GameMaster.GAME_VALUES mm)
	{
		GameObject gm = GameObject.FindGameObjectWithTag ("GM");
		
		//get dat GAME_VALUES type
		Type type_M = gm.GetComponent<GameMaster>()._M.GetType();
		Type type = mm.GetType ();
		
		//get dem properties
		FieldInfo[] properties_M = type_M.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
		FieldInfo[] properties = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		int changes = 0;

		for(int i = 0; i < properties_M.Length; i++)
		{
			FieldInfo m = properties_M[i];
			FieldInfo m2 = properties[i];

			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)) && m2.GetValue(mm).GetType().Equals(typeof(float)))
			{
				changes += (int)Mathf.Abs((float)m.GetValue(gm.GetComponent<GameMaster>()._M)-(float)m2.GetValue(mm));
			}
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(bool)) && m2.GetValue(mm).GetType().Equals(typeof(bool)))
			{
				if(!m.GetValue(gm.GetComponent<GameMaster>()._M).Equals(m2.GetValue(mm)))
				{
					changes += 1;
				}
			}
		}

		return (changes == 1) ? true : false;
	}
}
