﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Question {

	public class Verified
	{
		public string Answer { get; private set; }
		public string CorrectAnswer { get; private set; }
		public bool Correctly { get { return CorrectAnswer == Answer; } }

		public Verified(string answerKey, string correctAnswer)
		{
			Answer = answerKey;
			CorrectAnswer = correctAnswer;
		}
	}

	protected string m_correctKey;
	protected Dictionary<string, string> m_answers;

	public Question(Dictionary<string, string> answers, string correctKey)
	{
		m_answers = answers;
		m_correctKey = correctKey;
	}

	public virtual void Answer(string answerKey)
	{
		GameEvents.Invoke<Verified>(
			new Verified(answerKey, m_correctKey)
		);
	}

	
	public virtual string GetAnswerOption(string answerKey)
	{
		if (m_answers.ContainsKey(answerKey))
		{
			return m_answers[answerKey];
		}
		else
		{
			Debug.LogError(string.Format("No answer {0} for {1}", answerKey, this));
			return string.Format("No such answer: {0}", answerKey);
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Question]");
	}
}
