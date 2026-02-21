using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizController : MonoBehaviour
{
    [Serializable]
    public enum Effect {
        None,
        TeleportToScaryMap,
        ExitGame
    }

    [Serializable]
    public class QuestionItem
    {
        public Texture2D texture;
        public Sprite textureSprite;
        public string questionName;
        public int Default_Or_Yes_Branch;
        public int No_Branch;
        public Effect effect = Effect.None;

    }
    [Serializable]
    public class Entry
    {
        public int questionId;
        public QuestionItem questionItem;
    }

    [Header("Target")]
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private Image targetUI;
    [SerializeField] private AudioSource audioSourceClick;
    [SerializeField] private FadeAndExit exitScript;
    [SerializeField] private int scaryMapIndex;

    [Header("Textures Dictionary")]
    [SerializeField] private List<Entry> questionsPreDict;

    private Material _matInstance;
    private int _currentId;
    private Dictionary<int, QuestionItem> _questions;

    private static readonly int BaseMap = Shader.PropertyToID("_BaseMap"); 
    private static readonly int EmissionMap = Shader.PropertyToID("_EmissionMap");

    private InputAction _leftClick;
    private InputAction _rightClick;

    private void Awake()
    {
        if (!exitScript)
        {
            exitScript = GetComponent<FadeAndExit>();
        }
        if (!audioSourceClick)
        {
            audioSourceClick = GetComponent<AudioSource>();
        }
        if (!targetRenderer) 
        { 
            targetRenderer = GetComponent<Renderer>(); 
        }

        _matInstance = targetRenderer.material;

        _questions = new Dictionary<int, QuestionItem>();
        foreach(Entry entry in questionsPreDict)
        {
            _questions.Add(entry.questionId, entry.questionItem);
        }

        _currentId = GameData.Instance.StartingQuestion;
        Apply(_currentId);

        _leftClick = new InputAction("LeftClick", InputActionType.Button, "<Mouse>/leftButton");
        _rightClick = new InputAction("RightClick", InputActionType.Button, "<Mouse>/rightButton");

        _leftClick.performed += _ => Yes();
        _rightClick.performed += _ => No();
    }

    private void OnDestroy()
    {
        if (_leftClick != null) _leftClick.performed -= _ => Yes();
        if (_rightClick != null) _rightClick.performed -= _ => No();
        _leftClick?.Dispose();
        _rightClick?.Dispose();
    }

    private void OnEnable()
    {
        _leftClick?.Enable();
        _rightClick?.Enable();
    }

    private void OnDisable()
    {
        _leftClick?.Disable();
        _rightClick?.Disable();
    }

    private void Next(bool answear)
    {
        audioSourceClick.Play();
        var currentQuestion = _questions[_currentId];
        if (!answear && currentQuestion.No_Branch != 0)
        {
            _currentId = currentQuestion.No_Branch;
        }
        else
        {
            _currentId = currentQuestion.Default_Or_Yes_Branch;
        }

        switch (currentQuestion.effect)
        {
            case Effect.None:
                break;
            case Effect.TeleportToScaryMap:
                SceneManager.LoadScene(scaryMapIndex);
                GameData.Instance.StartingQuestion = _currentId;
                return;

            case Effect.ExitGame:
                exitScript.ExitGame();
                break;
        }

        Apply(_currentId);
    }

    private void Yes()
    {
        Next(true);
    }

    private void No()
    {
        Next(false);
    }

    private void Apply(int i)
    {
        if (_matInstance == null || _questions == null || _questions.Count == 0) 
        { 
            return; 
        }

        QuestionItem question;
        _questions.TryGetValue(i, out question);

        if (question == null)
        {
            return;
        }

        if (question.texture != null)
        {
            _matInstance.SetTexture(BaseMap, question.texture);
            _matInstance.SetTexture(EmissionMap, question.texture);
            targetUI.sprite = question.textureSprite;
        }
    }
}