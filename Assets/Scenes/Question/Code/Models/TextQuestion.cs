using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextQuestion : Question {

	public string Question
	{
		get;
		private set;
	}
	
	public TextQuestion(string question, Dictionary<string, string> answers, string correctKey)
		: base(answers, correctKey)
	{
		Question = question;
	}


}
