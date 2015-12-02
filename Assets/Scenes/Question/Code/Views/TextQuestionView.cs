using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextQuestionView : QuestionView {
	public UILabel m_questionTextLabel;

	public string QuestionText
	{
		set
		{
			m_questionTextLabel.text = value;
		}
	}
}
