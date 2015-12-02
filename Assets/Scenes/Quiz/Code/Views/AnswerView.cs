using UnityEngine;
using System.Collections;

public class AnswerView : MonoBehaviour {

	public Color m_correctColor;
	public Color m_wrongColor;
	public UILabel m_answerLabel;
	
	private UIButton m_button;
	private UISprite m_buttonSprite;

	public string Answer
	{
		set
		{
			m_answerLabel.text = value;
		}
	}

	void Start()
	{
		m_button = GetComponent<UIButton>();
		m_buttonSprite = GetComponent<UISprite>();
	}

	public void Unselect()
	{
		m_buttonSprite.color = m_button.defaultColor;
	}

	public void Lock()
	{
		m_button.GetComponent<Collider>().enabled = false;
		m_button.SetState(UIButtonColor.State.Disabled, true);

	}

	public void Unlock()
	{
		m_button.GetComponent<Collider>().enabled = true;
		m_button.SetState(UIButtonColor.State.Normal, true);
	}

	public void Select(bool correct)
	{
		var selectionColor = correct? m_correctColor: m_wrongColor;
		m_button.SetState(UIButtonColor.State.Disabled, true);
		m_buttonSprite.color = selectionColor;
	}
}
