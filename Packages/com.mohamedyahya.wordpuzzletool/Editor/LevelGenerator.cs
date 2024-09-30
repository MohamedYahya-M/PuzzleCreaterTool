#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using com.mohamedyahya.wordpuzzletool;

namespace com.mohamedyahya.wordpuzzletool
{
    public class LevelGenerator : EditorWindow
    {

        int numberOfLevels = 1;
        string[] wordChoices = new string[5];
        int correctAnswerIndex = 0;
        Sprite scenarioImage;
        Sprite goodEndingImage;
        Sprite badEndingImage;

        [MenuItem("Tools/Level Generator Tool")]
        public static void ShowWindow()
        {
            GetWindow<LevelGenerator>("Level Generator Tool");
        }

        void OnGUI()
        {
            GUILayout.Label("Level Generator", EditorStyles.boldLabel);

            numberOfLevels = EditorGUILayout.IntField("Number of Levels", numberOfLevels);

            for (int i = 0; i < numberOfLevels; i++)
            {
                GUILayout.Space(10);
                GUILayout.Label($"Level {i + 1} Configuration", EditorStyles.boldLabel);

                scenarioImage = (Sprite)EditorGUILayout.ObjectField("Scenario Image", scenarioImage, typeof(Sprite), false);
                goodEndingImage = (Sprite)EditorGUILayout.ObjectField("Good Ending Image", goodEndingImage, typeof(Sprite), false);
                badEndingImage = (Sprite)EditorGUILayout.ObjectField("Bad Ending Image", badEndingImage, typeof(Sprite), false);

                for (int j = 0; j < wordChoices.Length; j++)
                {
                    wordChoices[j] = EditorGUILayout.TextField($"Word Option {j + 1}", wordChoices[j]);
                }

                correctAnswerIndex = EditorGUILayout.IntSlider("Correct Answer Index", correctAnswerIndex, 0, wordChoices.Length - 1);

                if (GUILayout.Button("Generate Level"))
                {
                    GenerateLevel(i);
                }
            }
        }

        void GenerateLevel(int levelIndex)
        {
            LevelData newLevel = LevelDataFactory.CreateLevelData(
                $"Level {levelIndex + 1}",
                scenarioImage,
                wordChoices,
                correctAnswerIndex,
                goodEndingImage,
                badEndingImage
            );

            AssetDatabase.CreateAsset(newLevel, $"Assets/Levels/Level_{levelIndex + 1}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
#endif

//#if UNITY_EDITOR
//using UnityEditor;
//using UnityEngine;

//public class LevelGenerator : EditorWindow
//{
//    int numberOfLevels = 1;
//    string[] wordChoices = new string[5]; // Set the maximum number of words
//    int correctAnswerIndex = 0;
//    Sprite scenarioImage;
//    Sprite goodEndingImage;
//    Sprite badEndingImage;

//    [MenuItem("Tools/Level Generator Tool")]
//    public static void ShowWindow()
//    {
//        GetWindow<LevelGenerator>("Level Generator Tool");
//    }

//    void OnGUI()
//    {
//        GUILayout.Label("Level Generator", EditorStyles.boldLabel);

//        numberOfLevels = EditorGUILayout.IntField("Number of Levels", numberOfLevels);

//        for (int i = 0; i < numberOfLevels; i++)
//        {
//            GUILayout.Space(10);
//            GUILayout.Label($"Level {i + 1} Configuration", EditorStyles.boldLabel);

//            scenarioImage = (Sprite)EditorGUILayout.ObjectField("Scenario Image", scenarioImage, typeof(Sprite), false);
//            goodEndingImage = (Sprite)EditorGUILayout.ObjectField("Good Ending Image", goodEndingImage, typeof(Sprite), false);
//            badEndingImage = (Sprite)EditorGUILayout.ObjectField("Bad Ending Image", badEndingImage, typeof(Sprite), false);

//            for (int j = 0; j < wordChoices.Length; j++)
//            {
//                wordChoices[j] = EditorGUILayout.TextField($"Word Option {j + 1}", wordChoices[j]);
//            }

//            correctAnswerIndex = EditorGUILayout.IntSlider("Correct Answer Index", correctAnswerIndex, 0, wordChoices.Length - 1);

//            if (GUILayout.Button("Generate Level"))
//            {
//                GenerateLevel(i);
//            }
//        }
//    }

//    void GenerateLevel(int levelIndex)
//    {
//        // Create and save the level as a ScriptableObject
//        LevelData newLevel = ScriptableObject.CreateInstance<LevelData>();
//        newLevel.levelName = $"Level {levelIndex + 1}";
//        newLevel.scenarioImage = scenarioImage;
//        newLevel.words = wordChoices;
//        newLevel.correctWordIndex = correctAnswerIndex;
//        newLevel.goodEndingImage = goodEndingImage;  // Assign the good ending image
//        newLevel.badEndingImage = badEndingImage;    // Assign the bad ending image

//        // Save the generated ScriptableObject in the Assets folder
//        AssetDatabase.CreateAsset(newLevel, $"Assets/Levels/Level_{levelIndex + 1}.asset");
//        AssetDatabase.SaveAssets();
//    }
//}
//#endif