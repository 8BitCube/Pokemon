using UnityEngine;
using System.Collections;

public class LogoManager : MonoBehaviour 
{
	public bool SkipLogo = false;

	// Use this for initialization
	void Start () 
	{
		if(SkipLogo == false)
			StartCoroutine (YieldLogoTransition ());
		else
			Application.LoadLevel ("Menu");
	}

	IEnumerator YieldLogoTransition()
	{
		IEnumerator fadeIn = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeIn);
		IEnumerator fadeOut = FadeManager.Instance.FadeScreen(FadeManager.FadeType.FadeOut);

		while (fadeIn.MoveNext())
		{ yield return null; }

		yield return new WaitForSeconds (3);
		
		while (fadeOut.MoveNext())
		{ yield return null; }
		
		yield return null;

		Application.LoadLevel ("Menu");
	}
}
