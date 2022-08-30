using LSWTest.Inventory;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LSWTest.Core
{
    [CreateAssetMenu(fileName = "Examine Interaction", menuName = "LSW Test/Interaction/Examine")]
    public class ExamineInteraction : ScriptableObject
    {
        [SerializeField]
        [TextArea]
        List<string> examineDescriptions;

        public List<string> GetDescriptionList()
        {
            return examineDescriptions;
        }
    }
}


