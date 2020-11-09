using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public int level;
    public bool dialogueFinished = false;
    private LoadLevel loadLevel;
    private GameObject upbar, downbar, character, enemy, nextSentenceButton;
    private Text levelText, nameText, messageText;
    private Dialogue dialogue;
    private Chapter selectedLevel;
    private Queue<string> phraseList = new Queue<string>();
    private string[] splitted;
    void Awake()
    {
        loadLevel = GameObject.Find("LevelManager").GetComponent<LoadLevel>();
        upbar = GameObject.Find("DialogUpBar");
        downbar = GameObject.Find("DialogDownBar");
        character = GameObject.Find("Character");
        enemy = GameObject.Find("Enemy");
        levelText = GameObject.Find("TextLevel").GetComponent<Text>();
        nameText = GameObject.Find("TextName").GetComponent<Text>();
        messageText = GameObject.Find("TextMessage").GetComponent<Text>();
        nextSentenceButton = GameObject.Find("NextSentenceButton");
        nextSentenceButton.SetActive(false);
    }

    public void StartDialog()
    {
        LeanTween.moveY(upbar, 0.389f, 0.5f);
        LeanTween.moveY(downbar, -9.491f, 0.5f);
        StartCoroutine(CallDialogue());
    }

    IEnumerator CallDialogue()
    {
        LoadDialogueFile();
        phraseList.Clear();
           
        foreach(Chapter chap in dialogue.chapters)
        {
            if (chap.level == level)
                selectedLevel = chap;
        };
        foreach(string sentence in selectedLevel.phrases)
        {
            phraseList.Enqueue(sentence);
        };


        yield return new WaitForSeconds(0.5f);
        LeanTween.alpha(character, 1f, 0.2f);
        LeanTween.moveY(character, -5.8f, 0.2f);
        LeanTween.alpha(enemy, 1f, 0.2f);
        LeanTween.moveY(enemy, -5.8f, 0.2f);
        yield return new WaitForSeconds(0.2f);

        levelText.text = level + " - 1";
        LeanTween.alphaCanvas(levelText.gameObject.GetComponent<CanvasGroup>(), 1f, 0.2f);
        yield return new WaitForSeconds(0.3f);

        nextSentenceButton.SetActive(true);

        NextSentence();

    }

    public void NextSentence()
    {
        if (phraseList.Count > 0)
        {
            StopAllCoroutines();
            StartCoroutine(ShowText());
            splitted = phraseList.Dequeue().Split(':');
        }
        else
        {
            StartCoroutine(StartGame());
        }
    }

    IEnumerator ShowText()
    {
        LeanTween.alphaCanvas(messageText.gameObject.GetComponent<CanvasGroup>(), 0f, 0.2f);
        LeanTween.alphaCanvas(nameText.gameObject.GetComponent<CanvasGroup>(), 0f, 0.2f);
        yield return new WaitForSeconds(0.3f);
        nameText.text = splitted[0];
        messageText.text = splitted[1];
        LeanTween.alphaCanvas(messageText.gameObject.GetComponent<CanvasGroup>(), 1f, 0.2f);
        LeanTween.alphaCanvas(nameText.gameObject.GetComponent<CanvasGroup>(), 1f, 0.2f);
    }

    IEnumerator StartGame()
    {
        nextSentenceButton.SetActive(false);
        LeanTween.alpha(character, 0f, 0.2f);
        LeanTween.moveY(character, -7.63f, 0.2f);
        LeanTween.alpha(enemy, 0f, 0.2f);
        LeanTween.moveY(enemy, -7.63f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        character.SetActive(false);
        LeanTween.alphaCanvas(levelText.gameObject.GetComponent<CanvasGroup>(), 0f, 0.3f);
        LeanTween.alphaCanvas(messageText.gameObject.GetComponent<CanvasGroup>(), 0f, 0.3f);
        LeanTween.alphaCanvas(nameText.gameObject.GetComponent<CanvasGroup>(), 0f, 0.3f);
        yield return new WaitForSeconds(0.3f);
        levelText.gameObject.SetActive(false);
        messageText.gameObject.SetActive(false);
        nameText.gameObject.SetActive(false);
        LeanTween.moveY(upbar, 8.97f, 0.2f);
        LeanTween.moveY(downbar, -19.77f, 0.2f);
        yield return new WaitForSeconds(0.2f);
        loadLevel.StartGame();
    }

    private void LoadDialogueFile() 
    {

        string path = Application.streamingAssetsPath + "/Dialogue.json";
        string json = File.ReadAllText(path);
        dialogue = JsonUtility.FromJson<Dialogue>(json);
        
    }
}
