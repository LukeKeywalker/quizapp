using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class QuizController : MonoBehaviour {

	private List<QuestionView> m_questionViews;
	private List<Question> m_questions;
	private int m_currentQuestionIndex;
	private Dictionary<Type, Action<Question>> m_viewDispatcher;
	private QuestionView m_currentView;

	// Use this for initialization
	void Start () {
		CreateViewDispatcher();
		InitializeQuestions();
		AskNextQuestion();
	}

	// Ta procedura symuluje wczytywanie pytań z zewnętrznej klasy
	void InitializeQuestions()
	{
		m_currentQuestionIndex = -1;
		
		m_questions = new List<Question>()
		{
			new TextQuestion(
				"Ile Pudzian bierze na klate kiedy ma dobry dzien?",
				new Dictionary<string, string>()
				{
					{ "A", "Miliard" },
					{ "B", "Milion" },
					{ "C", "240" },
					{ "D", "180" },
				}, 
				"A"
			),
			
			new TextQuestion(
				"Jak sie nazywa znak w zapisie nutowym podwyzszajacy dzwiek o pol tonu?",
				new Dictionary<string, string>()
				{
					{ "A", "Cis-mol" },
					{ "B", "Krzyzyk" },
					{ "C", "Bemol" },
					{ "D", "Kropka" },
				}, 
				"B"
			),
			
			new TextQuestion(
				"Na jaka chorobe zakazna zmarl Fryderyk Szopen?",
				new Dictionary<string, string>()
				{
					{ "A", "Ja Yeti" },
					{ "B", "Syfilis" },
					{ "C", "Gruzlica" },
					{ "D", "Ebola" },
				}, 
				"C"
			),
			
			new ImageQuestion(
				"Ktora czesc ciala obcial sobie autore tego obrazu",
				new Dictionary<string, string>()
				{
					{ "A", "Ucho" },
					{ "B", "Reke" },
					{ "C", "Noge" },
					{ "D", "c==/=3 XD" },
				}, 
				"A",
				"vanGogh1"
			),

			new ImageQuestion(
				"W jakim stylu zostal namalowany ten fragment obrazu?",
				new Dictionary<string, string>()
				{
					{ "A", "Impresjonizm" },
					{ "B", "Realizm" },
					{ "C", "Rokoko" },
					{ "D", "Surrealizm" },
				}, 
				"A",
				"impresjonizm"
			)
		};
	}
	
	// Tworzy słownik mapujący pytanie do odpowiedniego widoku.
	void CreateViewDispatcher()
	{
		m_viewDispatcher = new Dictionary<Type, Action<Question>>()
		{
			{ typeof(TextQuestion), (question) => { AskTextQuestion(question); } },
			{ typeof(ImageQuestion), (question) => { AskImageQuestion(question); } }
		};
	}

	// Pobierz wszystkie widoki ze sceny i zapamiętaj je w liście
	// Lepiej unikać wiązania widoków z kontrolerem w edytorze, gdyż
	// podczas zespołowej pracy często dochodzi do odpinania referencji
	// co generuje uciążliwe w wykrywaniu błędy
	void AssingQuestionViews()
	{
		m_questionViews = new List<QuestionView>(FindObjectsOfType<QuestionView>());
	}
	
	// Jeśli jest jeszcze jakieś pytanie w rundzie to je zadaj
	// Jeśli nie, wyświetl podsumowanie
	void AskNextQuestion()
	{
		m_currentQuestionIndex++;
		HideAllViews();
		if (m_currentQuestionIndex < m_questions.Count)
		{
			var currentQuestion = m_questions[m_currentQuestionIndex];
			DispatchQuestion(currentQuestion);
		}
		else
		{
			// ShowSummary
		}
	}

	void HideAllViews()
	{
		foreach (var view in m_questionViews)
		{
			view.gameObject.SetActive(false);
		}
	}

	void AskTextQuestion(Question question)
	{
		var textQuestion = question as TextQuestion;
		var textQuestionView = m_questionViews.Find(SelectTextQuestionView) as TextQuestionView;

		textQuestionView.QuestionText = textQuestion.Question;
		SetAnswers(textQuestionView, question);
	}

	void AskImageQuestion(Question question)
	{
		var imageQuestion = question as ImageQuestion;
		var imageQuestionView = m_questionViews.Find(SelectImageQuestionView) as ImageQuestionView;
		
		imageQuestionView.QuestionText = imageQuestion.Question;
		imageQuestionView.QuestionImage = imageQuestion.ImageId;
		SetAnswers(imageQuestionView, question);
	}

	void SetAnswers(QuestionView view, Question question)
	{
		m_currentView = view;
		view.gameObject.SetActive(true);

		view.AnswerA = question.GetAnswerOption("A");
		view.AnswerB = question.GetAnswerOption("B");
		view.AnswerC = question.GetAnswerOption("C");
		view.AnswerD = question.GetAnswerOption("D");
	}
	
	bool SelectTextQuestionView(QuestionView questionView)
	{
		return (questionView as TextQuestionView != null);
	}

	bool SelectImageQuestionView(QuestionView questionView)
	{
		return (questionView as ImageQuestionView != null);
	}

	void DispatchQuestion(Question question)
	{
		var questionType = question.GetType();

		if (m_viewDispatcher.ContainsKey(questionType))
		{
			var questionMethod = m_viewDispatcher[questionType];
			questionMethod(question);
		}
	}

	void OnEnable()
	{
		AssingQuestionViews();
		BindViews();
		GameEvents.Subscribe<Question.Verified>(HandleQuestionVerified);
	}

	void OnDisable()
	{
		GameEvents.Unsubscribe<Question.Verified>(HandleQuestionVerified);
		UnbindViews();
	}

	void BindViews()
	{
		foreach (var view in m_questionViews)
		{
			view.AnswerSelected += HandleAnswerSelected;
		}
	}
	
	void UnbindViews()
	{	
		foreach (var view in m_questionViews)
		{
			view.AnswerSelected -= HandleAnswerSelected;
		}
	}

	void HandleAnswerSelected(string answerKey)
	{
		var currentQuestion = m_questions[m_currentQuestionIndex];
		currentQuestion.Answer(answerKey);
	}

	void HandleQuestionVerified(Question.Verified verified)
	{
		StartCoroutine(ShowResult(verified));
	}

	IEnumerator ShowResult(Question.Verified verified)
	{
		m_currentView.Highlight(verified.Answer, verified.CorrectAnswer);

		yield return new WaitForSeconds(3.0f);
		AskNextQuestion();
	}
}
