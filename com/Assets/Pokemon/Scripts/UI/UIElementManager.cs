using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIElementManager : MonoBehaviour {

	public Image fade;

	// Use this for initialization
	void Awake () {
	UIElementStatic.FadeEffect=fade;
	}
}
