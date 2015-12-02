using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImageQuestionView : QuestionView {

	public UISprite m_questionImageSprite;
	public UILabel m_questionTextLabel;

	public string QuestionImage
	{
		set
		{
			m_questionImageSprite.spriteName = value;
		}
	}

	public string QuestionText
	{
		set
		{
			m_questionTextLabel.text = value;
		}
	}
}
