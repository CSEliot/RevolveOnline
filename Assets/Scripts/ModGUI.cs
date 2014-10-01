using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class ModGUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI()
	{
		//GUILayout.BeginArea (new Rect (0, 0, 200, Screen.height));

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
				if(GUILayout.Button("+"))
				{
					//box in that shit and modify
					object p = (object)gm.GetComponent<GameMaster>()._M;
					m.SetValue(p, (float)m.GetValue(gm.GetComponent<GameMaster>()._M)+1f);
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
				if(GUILayout.Button("-"))
				{
					//box in that shit and modify
					object p = (object)gm.GetComponent<GameMaster>()._M;
					m.SetValue(p, (float)m.GetValue(gm.GetComponent<GameMaster>()._M)-1f);
					gm.GetComponent<GameMaster>()._M = (GameMaster.GAME_VALUES)p; 
				}
			}
			GUILayout.EndVertical();
			GUILayout.EndHorizontal();
		}
		//GUILayout.EndArea ();
	}
}
