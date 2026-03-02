using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New_dialogue", menuName = "Dialogue")]

public class SO_Dialogue : ScriptableObject
{
    public class Info
    {
        [TextArea(4, 8)]
        public string dialogue;
    }

    public Info[] dialogueInfo;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
