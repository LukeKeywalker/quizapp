using UnityEngine;
using System.Collections;

public class QuizScore  {

	public class QuizScoreChanged
	{
		public int Change { get; private set; }

		public QuizScoreChanged(int scoreDelta)
		{
			Change = scoreDelta;
		}
	}

	private int m_score = 0;

	public void IncrementScore()
	{
		m_score++;
		GameEvents.Invoke<QuizScoreChanged>(new QuizScoreChanged(1));
	}


}
