using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using XNode;

public class NodeReader : MonoBehaviour
{
    public TMP_Text dialog;
    public Sprite backgroundImage;
    public GameObject ImageGO;
    public NodeGraph graph;
    public GameObject characterSheet;
    public BaseNode currentNode;

    public TMPro.TMP_Text buttonAText;
    public TMPro.TMP_Text buttonBText;
    public GameObject buttonA;
    public GameObject buttonB;
    public GameObject nextButtonGo;

    // 🎭 Actor Image UI
    public GameObject actorImageGO;
    public Image actorImageComponent;

void Start()
{
    if (actorImageGO != null)
    {
        actorImageComponent = actorImageGO.GetComponent<Image>();
        if (actorImageComponent == null)
        {
            Debug.LogError("No Image component found on actorImageGO!");
        }
    }
    else
    {
        Debug.LogError("actorImageGO is not assigned in the Inspector!");
    }

    currentNode = GetStartNode();
    AdvanceDialog();
}

    public BaseNode GetStartNode()
    {
        return graph.nodes.Find(node => node is BaseNode && node.name == "Start") as BaseNode;
    }

    public void DisplayNode(BaseNode node)
{
    if (node == null)
    {
        Debug.LogError("DisplayNode called with a null node!");
        return;
    }

    dialog.text = node.getDialogText() ?? "Missing Dialog Text"; // Avoid null reference

    backgroundImage = node.getSprite();
    if (ImageGO != null && ImageGO.GetComponent<Image>() != null)
    {
        ImageGO.GetComponent<Image>().sprite = backgroundImage;
    }
    else
    {
        Debug.LogError("ImageGO or its Image component is missing!");
    }

    // 🎭 Display Actor Image & Animate If Needed
    if (node is SimpleDialog simpleDialog)
    {
        Debug.Log("Changing music to: " + simpleDialog.backgroundMusicChoice);
        simpleDialog.PlaySelectedMusic();

        if (actorImageGO != null && actorImageComponent != null)
        {
            if (simpleDialog.ActorImage != null)
            {
                actorImageComponent.sprite = simpleDialog.ActorImage;
                actorImageGO.SetActive(true);

                if (simpleDialog.animateActor)
                {
                    StartCoroutine(SlideInActor());
                }
                else
                {
                    actorImageGO.transform.localPosition = new Vector3(0, actorImageGO.transform.localPosition.y, 0);
                }
            }
            else
            {
                actorImageGO.SetActive(false);
            }
        }
        else
        {
            Debug.LogError("actorImageGO or actorImageComponent is null!");
        }
    }

    if (node is MultipleChoiceDialog multipleChoiceNode)
    {
        buttonAText.text = multipleChoiceNode.a ?? "Option A";
        buttonBText.text = multipleChoiceNode.b ?? "Option B";

        buttonA.SetActive(true);
        buttonB.SetActive(true);
        nextButtonGo.SetActive(false);
    }
    else
    {
        buttonA.SetActive(false);
        buttonB.SetActive(false);
        nextButtonGo.SetActive(true);
    }
}


    public void AdvanceDialog()
    {
        var nextNode = GetNextNode(currentNode);
        if (nextNode != null)
        {
            currentNode = nextNode;
            DisplayNode(currentNode);
        }
        else
        {
            Debug.Log("Nothing found!");
        }
    }

    private BaseNode GetNextNode(BaseNode node)
    {
        if (node is MultipleChoiceDialog)
        {
            GameObject clickedButton = EventSystem.current.currentSelectedGameObject;
            TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();

            if (buttonText.text == ((MultipleChoiceDialog)node).a)
            {   
                return currentNode.GetOutputPort("a")?.Connection.node as BaseNode;
            }

            if (buttonText.text == ((MultipleChoiceDialog)node).b)
            {
                return currentNode.GetOutputPort("b")?.Connection.node as BaseNode;
            }

            return currentNode.GetOutputPort("exit")?.Connection.node as BaseNode;
        }
        else if (node is AbilityCheckNode)
        {
            int d20 = Random.Range(0, 21);
            if ((d20 + characterSheet.gameObject.GetComponent<CharacterStats>().awareness) >= ((AbilityCheckNode)node).getDifficultyCheck())
            {
                return currentNode.GetOutputPort("success")?.Connection.node as BaseNode;
            }
            else
            {
                return currentNode.GetOutputPort("failed")?.Connection.node as BaseNode;
            }
        }
        else
        {
            return currentNode.GetOutputPort("exit")?.Connection.node as BaseNode;
        }
    }

    // 🎬 Slide-in Animation Coroutine for Actor
    IEnumerator SlideInActor()
    {
        float duration = 0.5f;
        Vector3 startPosition = new Vector3(-500, actorImageGO.transform.localPosition.y, 0);
        Vector3 endPosition = new Vector3(0, actorImageGO.transform.localPosition.y, 0);
        
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            actorImageGO.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        actorImageGO.transform.localPosition = endPosition;
    }
}
