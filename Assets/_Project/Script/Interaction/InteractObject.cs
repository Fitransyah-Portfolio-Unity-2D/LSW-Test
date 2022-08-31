using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

namespace LSWTest.Core
{
    public class InteractObject : MonoBehaviour
    {
        [SerializeField] ExamineInteraction examineInteraction;
        [SerializeField] TMP_Text infoPanelText;

        string startingInfoPanelText;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (collision.gameObject == player)
            {
                infoPanelText = GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TMP_Text>();
                startingInfoPanelText = infoPanelText.text;
                StartCoroutine(ExamineRoutine(examineInteraction.GetDescriptionList()));
            }
        }
        private void OnTriggerExit2D(Collider2D collision)
        {

            var player = GameObject.FindGameObjectWithTag("Player");
            if (collision.gameObject == player)
            {
                StartCoroutine(StopExamineRoutine());
            }
        }
        IEnumerator ExamineRoutine(List<string> examineDescriptions)
        {
            
            for (int i = 0; i < examineDescriptions.Count; i++)
            {
                infoPanelText.text = examineDescriptions[i];

                Debug.Log(examineDescriptions[i]);

                yield return new WaitUntil(() => Input.GetKeyUp(KeyCode.Space));
                yield return new WaitForSeconds(0.75f);
            }
        }
        IEnumerator StopExamineRoutine()
        {
            yield return new WaitForSeconds(1f);
            infoPanelText.text = startingInfoPanelText;
            StopCoroutine(ExamineRoutine(examineInteraction.GetDescriptionList()));
        }
    }
}


