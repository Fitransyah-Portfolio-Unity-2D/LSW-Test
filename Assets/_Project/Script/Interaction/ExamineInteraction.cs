using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Examine Object", menuName = "Examine Property")]
public class ExamineInteraction :ScriptableObject
{
    [SerializeField]
    List<string> examineDecriptions;

    

    public IEnumerator DisplayExamineDescription()
    {
        for (int i = 0; i < examineDecriptions.Count; i++)
        {
            if (i >= examineDecriptions.Count)
            {
                yield return null;
            }

            Debug.Log(examineDecriptions[i]);

            yield return new WaitUntil(() => Input.GetKey(KeyCode.Space));
        }    
    }
}
