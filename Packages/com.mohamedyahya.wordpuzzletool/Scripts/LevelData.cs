using System.Collections.Generic;
using UnityEngine;

public enum MechanicType
{
    Click,
    Drag
}
[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/Create New Level")]
public class LevelData : ScriptableObject
{
    public string levelName;
    public int levelNumber;  //store the level number
    public Sprite scenarioImage; // The scenario image for the level
    public List<string> words;   // The word options for the level
    public int correctWordIndex; // The index of the correct word in the words array
    public string dialogueText;
    public Sprite goodEndingImage;  // Image for when the user selects the correct answer
    public Sprite badEndingImage;   // Image for when the user selects the wrong answer

    public MechanicType mechanicType; // Use this to choose the mechanic type
}



//using System.Collections.Generic;
//using UnityEngine;

//[CreateAssetMenu(fileName = "NewLevel", menuName = "Level/Create New Level")]
//public class LevelData : ScriptableObject
//{
//    public string levelName;
//    public int levelNumber;  //store the level number
//    public Sprite scenarioImage; // The scenario image for the level
//    public List<string> words;       // The word options for the level
//    public int correctWordIndex; // The index of the correct word in the words array
//    public string dialogueText;

//    public Sprite goodEndingImage;  // Image for when the user selects the correct answer
//    public Sprite badEndingImage;   // Image for when the user selects the wrong answer

//    public bool isClickMechanic;
//}