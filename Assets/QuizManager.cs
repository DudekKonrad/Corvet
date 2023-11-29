using System;
using UnityEngine;
using System.Collections.Generic;
using System.IO;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuizManager : MonoBehaviour
{
    public string csvFilePath = "Assets/QuizData.csv";
    private List<Question> _questions = new List<Question>();
    [SerializeField] private TMP_Text _questionText;
    
    
    [SerializeField] private Transform[] buttons;
    [SerializeField] private TMP_Text _button1Text;
    [SerializeField] private TMP_Text _button2Text;
    [SerializeField] private TMP_Text _button3Text;
    [SerializeField] private TMP_Text _button4Text;
    
    [SerializeField] private TMP_Text _resultAnswer;
    [SerializeField] private TMP_Text _dobre;
    [SerializeField] private TMP_Text _zle;
    [SerializeField] private TMP_Text _zostalo;
    [SerializeField] private float _delay;

    [SerializeField] private List<Question>  _wrongQuestions = new List<Question>();

    private int _questionIndex;
    private int _good;
    private int _wrong;
    private int _remaining;

    // Funkcja do tasowania przycisków
    public void Shuffle2()
    {
        // Losowe tasowanie przycisków
        for (int i = 0; i < buttons.Length; i++)
        {
            int randomIndex = Random.Range(0, buttons.Length);

            (buttons[i], buttons[randomIndex]) = (buttons[randomIndex], buttons[i]);
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].SetSiblingIndex(i);
        }
    }

    public void Shuffle<T>(IList<T> list)  
    {  
        int n = list.Count;  
        while (n > 1) {  
            n--;  
            int k = Random.Range(0, n + 1);  
            (list[k], list[n]) = (list[n], list[k]);
        }  
    }

    void Start()
    {
        buttons = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            buttons[i] = transform.GetChild(i);
        }
        Shuffle2();
        LoadQuestionsFromCsv();
        Shuffle(_questions);
        LoadQuestion(_questions[_questionIndex]);
    }

    public void AnswerGiven(int answerIndex)
    {
        if (answerIndex == _questions[_questionIndex].CorrectAnswerIndex)
        {
            _resultAnswer.text = "Brawo Natusia, super Ci idzie! :*";
            _resultAnswer.DOColor(Color.green, 0.5f);
            _good++;
            _dobre.text = _good.ToString();
        }
        else
        {
            _resultAnswer.text = "Spokojnie Natuisa, następnym razem odpowiesz dobrze! :*";
            _resultAnswer.DOColor(Color.red, 0.5f);
            _wrong++;
            _zle.text = _wrong.ToString();
            _wrongQuestions.Add(_questions[_questionIndex]);
        }

        _questionIndex++;
        _remaining--;
        _zostalo.text = _remaining.ToString();
        DOVirtual.DelayedCall(_delay, () => LoadQuestion(_questions[_questionIndex]));
    }

    private void LoadQuestion(Question question)
    {
        _resultAnswer.DOColor(Color.white, 0.5f);
        _resultAnswer.text = "Tutaj zobaczysz czy dobrze odpowiedziałaś :*";
        _questionText.text = question.QuestionText;
        _button1Text.text = question.Options[0];
        _button2Text.text = question.Options[1];
        _button3Text.text = question.Options[2];
        _button4Text.text = question.Options[3];
    }

    private void LoadQuestionsFromCsv()
    {
        var lines = File.ReadAllLines(csvFilePath);
        _remaining = lines.Length;
        _zostalo.text = _remaining.ToString();
        if (File.Exists(csvFilePath))
        {
            for (int i = 1; i < lines.Length; i++)
            {
                var columns = lines[i].Split(';');
                var question = new Question
                {
                    QuestionText = columns[0],
                    Options = new string[] { columns[1], columns[2], columns[3], columns[4] },
                    CorrectAnswerIndex = int.Parse(columns[5]) // Indeks odpowiedzi koryguj o 1
                };

                _questions.Add(question);
            }
        }
        else
        {
            Debug.LogError("Plik CSV nie istnieje: " + csvFilePath);
        }
    }
}

[Serializable]
public class Question
{
    public string QuestionText;
    public string[] Options;
    public int CorrectAnswerIndex;
}
