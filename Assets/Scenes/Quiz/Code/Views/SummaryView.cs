using UnityEngine;
using System.Collections;

public class SummaryView : MonoBehaviour {

	public UILabel m_scoreLabel;
	public UIButton m_retryButton;
	public TweenAlpha m_buttonAlphaTween;

	private int m_score;
	private int m_total;

	public event System.Action RetryClicked;
	public event System.Action FadeFinished;

	public int Score
	{
		set
		{
			m_score = value;
			UpdateScoreLabel();
		}
	}

	void Awake()
	{
	}

	public int Total
	{
		set
		{
			m_total = value;
			UpdateScoreLabel();
		}
	}

	public void HandleRetryClicked()
	{
		OnRetryClicked();
	}

	public void HandleAlphaTweenFinished()
	{
		OnFadeFinished();
	}

	public void FadeOut()
	{
		m_buttonAlphaTween.ResetToBeginning();
		m_buttonAlphaTween.PlayForward();
	}


	protected virtual void OnRetryClicked()
	{
		if (RetryClicked != null)
			RetryClicked();
	}

	protected virtual void OnFadeFinished()
	{
		if (FadeFinished != null)
			FadeFinished();
	}

	public void Show()
	{
		gameObject.SetActive(true);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}

	private void UpdateScoreLabel()
	{
		m_scoreLabel.text = string.Format("Wynik {0}/{1}", m_score.ToString(), m_total.ToString());
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
