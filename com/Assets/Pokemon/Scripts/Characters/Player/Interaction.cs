using UnityEngine;
using System.Collections;

public class Interaction : BaseController 
{
	public RPGText rpgText;
	public string Text = string.Empty;
	public SoundInfo sfx;

	public bool isActive = false;

	public string DetermineText()
	{
		string _text = Text;
		return _text;
	}

	public void Activate()
	{	
		if (isActive)
			return;

		if(Vector3.Distance(WorldManager.Instance.Player.transform.position, this.transform.position) < 2)
		{
			rpgText.ActivateText(DetermineText());

			if(sfx.clip)
				SoundManager.Instance.PlaySFX(sfx);
		}
	}

	public void Deactivate()
	{
		rpgText.DeactivateText();
	}

	void Update()
	{
		isActive = rpgText.visable;

		if (isActive)
			return;

	}
}
