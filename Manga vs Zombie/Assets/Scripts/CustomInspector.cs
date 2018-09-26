using UnityEngine;
using UnityEditor;


public class CustomInspector : EditorWindow
{
    float myFloat = 5;
    
    int tab;
    public string stringToEdit = "1", stringToEdit1 = "";

    [MenuItem("Window/Custom Isnpector")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CustomInspector));
    }
    public static void Init()
    {
        GetWindow<CustomInspector>("FocusedWindow");
    }

    void OnGUI()
    {
        tab = GUILayout.Toolbar(tab, new string[] { "Player", "Enemy","Map","Shop","sence"});
        switch (tab)
        {
            case 0:
                GUILayout.Label("Player Settings", EditorStyles.boldLabel);
                GUILayout.Label("Attack", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Attack delay :", EditorStyles.boldLabel);
                
                stringToEdit = GUILayout.TextField(stringToEdit, 2);
                double abc = 0;
                System.Double.TryParse(stringToEdit, out abc);
                Player1Controller.UpdateABC((float)abc);

                EditorGUILayout.EndHorizontal();
                //EditorGUILayout.BeginHorizontal();
                //GUILayout.Label("Dmg :", EditorStyles.boldLabel);

                //stringToEdit = GUILayout.TextField(stringToEdit1, 2);

                //EditorGUILayout.EndHorizontal();

                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                break;
            case 1:
                GUILayout.Label("Enemy Settings", EditorStyles.boldLabel);
                break;
            case 2:
                GUILayout.Label("Map Settings", EditorStyles.boldLabel);
                break;
            case 3:
                GUILayout.Label("Shop Settings", EditorStyles.boldLabel);
                break;
            case 4:
                GUILayout.Label("Sence Settings", EditorStyles.boldLabel);
                break;

        }
        //GUILayout.Label(EditorWindow.focusedWindow.ToString());
        //GUILayout.Label("Sound Settings", EditorStyles.boldLabel);
        //mybool = EditorGUILayout.Toggle("Mute All Sounds", mybool);


        //myFloat = EditorGUILayout.Slider("Sounds", myFloat, 0, 10);

        //GUILayout.Label("Player Settings", EditorStyles.boldLabel);



    }
}





