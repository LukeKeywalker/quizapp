using UnityEngine;
using System.Collections;
using System;

public class QuestionView : MonoBehaviour {

	// Referencje do widokow labelek ustawiane z edytora
	// pomimo widocznosci public konwencja jest nazywanie ich
	// jak zmiennych private, gdyz ze wzgledu na enkapsulacje
	// nie powinny byc one modyfikowane spoza komponentu
	// do ktorego sa doczepione
	public AnswerView m_answerViewA;
	public AnswerView m_answerViewB;
	public AnswerView m_answerViewC;
	public AnswerView m_answerViewD;


	// Zdarzenie wystawione dla kontrolerow
	public event Action<string> AnswerSelected;

	// Metody dopiete w edytorze do zdarzenia On Click komponentu UIButton
	public void AnswerASelected()
	{
		OnAnswerSelected("A");
	}

	public void AnswerBSelected()
	{
		OnAnswerSelected("B");
	}

	public void AnswerCSelected()
	{
		OnAnswerSelected("C");
	}

	public void AnswerDSelected()
	{
		OnAnswerSelected("D");
	}
	
	// Interfejs widoku to ustawiania tekstu odpowiedzi
	public string AnswerA
	{
		set
		{
			m_answerViewA.Answer = value;
			m_answerViewA.Unlock();
		}
	}

	public string AnswerB
	{
		set
		{
			m_answerViewB.Answer = value;
			m_answerViewB.Unlock();
		}
	}

	public string AnswerC
	{
		set
		{
			m_answerViewC.Answer = value;
			m_answerViewC.Unlock();
		}
	}
	
	public string AnswerD
	{
		set
		{
			m_answerViewD.Answer = value;
			m_answerViewD.Unlock();
		}
	}

	public void Highlight(string answerKey, string correctKey)
	{
		LockAnswerViews();

		bool correct = answerKey == correctKey;
		if (answerKey == "A")
		{
			m_answerViewA.Select(correct);

		}
		else if (answerKey == "B")
		{
			m_answerViewB.Select(correct);

		}
		else if (answerKey == "C")
		{
			m_answerViewC.Select(correct);

		}
		else if (answerKey == "D")
		{
			m_answerViewD.Select(correct);
		}
	}

	protected void LockAnswerViews()
	{
		m_answerViewA.Lock();
		m_answerViewB.Lock();
		m_answerViewC.Lock();
		m_answerViewD.Lock();
	}

	protected void UnlockAnswerViews()
	{
		m_answerViewA.Unlock();
		m_answerViewB.Unlock();
		m_answerViewC.Unlock();
		m_answerViewD.Unlock();
	}
	
	protected virtual void OnAnswerSelected(string answerKey)
	{
		Debug.Log("OnAnswerSelected: " + answerKey);
		if (AnswerSelected != null)
			AnswerSelected(answerKey);
	}
}
