using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InteractObject : MonoBehaviour
{
    [SerializeField] ExamineInteraction examineInteraction;
    [SerializeField] TMP_Text infoPanelText;

    string startingInfoPanelText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            infoPanelText = GameObject.FindGameObjectWithTag("InfoPanel").GetComponent<TMP_Text>();
            startingInfoPanelText = infoPanelText.text;
            StartCoroutine(DisplayExamineDescription(examineInteraction.GetDescriptionList()));
        }
    } 
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(DisplayExamineDescription(examineInteraction.GetDescriptionList()));
            infoPanelText.text = startingInfoPanelText;
        }
    }
    public IEnumerator DisplayExamineDescription(List<string> examineDescriptions)
    {
        for (int i = 0; i < examineDescriptions.Count; i++)
        {
            infoPanelText.text = examineDescriptions[i];

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
        }
    }

}
