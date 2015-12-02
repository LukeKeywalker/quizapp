using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ImageQuestion : TextQuestion {

	public string ImageId
	{
		get;
		private set;
	}
	
	public ImageQuestion(string question, Dictionary<string, string> answers, string correctKey, string imageId)
		: base(question, answers, correctKey)
	{
		ImageId = imageId;
	}
}
