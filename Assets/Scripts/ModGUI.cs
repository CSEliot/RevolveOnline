using UnityEngine;
using System.Collections;
using System.Reflection;
using System;

public class ModGUI : MonoBehaviour {

	public Vector2 selection;
	public Rect windowRect;

	private GameMaster.GAME_VALUES original; 
	private float[] changes_made = new float[256];

	public bool changed = false;

	public int MAX_CHANGES;

	void Start()
	{
		windowRect = new Rect(Screen.width/2-((Screen.width*.4f)/2), Screen.height/2-((Screen.height*.4f)/2), Screen.width*.40f, Screen.height*.40f);
		original = GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ()._M;
	}

	void OnGUI() {
		if(GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ().isGameOver())
			windowRect = GUILayout.Window(0, windowRect, DoMyWindow, "Game Modifiers");
	}

	void DoMyWindow(int windowID) 
	{
		selection = GUILayout.BeginScrollView(selection, GUILayout.Width(Screen.width*.40f), GUILayout.Height(Screen.height*.40f));

		//get dat game masta
		GameObject gm = GameObject.FindGameObjectWithTag ("GM");

		//get dat GAME_VALUES type
		Type type = gm.GetComponent<GameMaster>()._M.GetType();

		//get dem properties
		FieldInfo[] properties = type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);

		int count = 0;

		Color default__ = GUI.color;

		foreach(FieldInfo m in properties)
		{
			if(oneChangeMade(original) && Mathf.Abs(changes_made[count]) > 0)
			{
				GUI.color = Color.green;
			}
			else if(Mathf.Abs(changes_made[count]) > 0)
				GUI.color = Color.red;
			else
				GUI.color = default__;

			//this is the first column with names and values
			GUILayout.BeginHorizontal();
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)))
			{
				//so basically just make a box with info if it's a float
				if(changes_made[count] > 0) GUILayout.Box(m.Name+": "+m.GetValue(original)+"(+"+changes_made[count]+")", GUILayout.Width(300));
				else if(changes_made[count] < 0) GUILayout.Box(m.Name+": "+"("+m.GetValue(original)+changes_made[count]+")", GUILayout.Width(300));
				else GUILayout.Box(m.Name+": "+m.GetValue(original), GUILayout.Width(300));
			}
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(bool)))
			{
				//make a box with just the name if it's a boolean
				GUILayout.Box(m.Name+": ", GUILayout.Width(300));
			}

			GUI.color = default__;

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

			count++;
		}
		GUILayout.EndScrollView();

		GUILayout.Space(20F);

		oneChangeMade (original);

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
                    if (GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>()._M.ColumnArena)
                    {
    					Application.LoadLevel(2);
                    }
                    else
                    {
                        Application.LoadLevel(1); 
                    }
				}
				if(GUILayout.Button("Revert"))
				{
					GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ()._M = original;
				}
			}
			else
			{
				GUILayout.Box("Only "+MAX_CHANGES+" Changes May Be Made");
				if(GUILayout.Button("Revert"))
				{
					GameObject.FindGameObjectWithTag ("GM").GetComponent<GameMaster> ()._M = original;
				}
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

			changes_made[i] = 0;

			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(float)) && m2.GetValue(mm).GetType().Equals(typeof(float)))
			{
				changes += (int)Mathf.Abs((float)m.GetValue(gm.GetComponent<GameMaster>()._M)-(float)m2.GetValue(mm));
				if((int)Mathf.Abs((float)m.GetValue(gm.GetComponent<GameMaster>()._M)-(float)m2.GetValue(mm)) > 0)
				{
					changes_made[i] = ((float)m.GetValue(gm.GetComponent<GameMaster>()._M)-(float)m2.GetValue(mm));
				}
			}
			if(m.GetValue(gm.GetComponent<GameMaster>()._M).GetType().Equals(typeof(bool)) && m2.GetValue(mm).GetType().Equals(typeof(bool)))
			{
				if(!m.GetValue(gm.GetComponent<GameMaster>()._M).Equals(m2.GetValue(mm)))
				{
					changes += 1;
					if((bool)m.GetValue(gm.GetComponent<GameMaster>()._M) == true) changes_made[i] = 1.0F; else changes_made[i] = -1.0f;
				}
			}
		}

		return (changes == MAX_CHANGES) ? true : false;
	}
}
