using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace LSWTest.Core
{
    [CreateAssetMenu(fileName = "New Examine Object", menuName = "Examine Property")]
    public class ExamineInteraction : ScriptableObject
    {
        [SerializeField]
        List<string> examineDescriptions;

        public List<string> GetDescriptionList()
        {
            return examineDescriptions;
        }
    }
}

