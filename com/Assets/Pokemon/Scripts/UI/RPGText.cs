using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RPGText : MonoBehaviour
{
	public static RPGText Instance;
	public ScrollRect scrollRect;
	public Image image;
	public Text textUI;
	
	public string newText;
	public int splitIndex = 25;
	public float textSpeed = 0.05f;
	public bool UseConstructBlocks = true;
	
	//Private Variables
	private string m_TextToScreen = "";
	private string[] m_TextBlocks;
	private int m_CurrentTextBlockIndex = 0;
	private string m_CurrentTextBlock;
	private float m_PauseTimer = 0.0f;

	public bool visable = false;
	
	void Start()
	{
		if(Instance == null)
			Instance = this;
		
		image.gameObject.SetActive(false);
		scrollRect.verticalScrollbar.value=0f;

		DontDestroyOnLoad (this);
	}
	
	private void Update() 
	{		
		if(!visable)
			return;
		
		//if(Input.GetKeyDown(KeyCode.X) || Input.GetMouseButtonDown(0))
			//FillText();
		
		textUI.text = m_TextToScreen;
		
		// This is optional, this prevents scrolling so it may not be wanted.
		// ///////////////////////////////////////////////////////////////////
		Canvas.ForceUpdateCanvases();
		scrollRect.verticalScrollbar.value=0f;
		Canvas.ForceUpdateCanvases();
		// ///////////////////////////////////////////////////////////////////
		
		if(PauseCharacterIndex())
		{
			if(m_TextToScreen.Length < m_CurrentTextBlock.Length)
				m_TextToScreen = m_CurrentTextBlock.Substring(0, m_TextToScreen.Length+1);
		}
	}
	
	public void ActivateText(string aText)
	{
		newText = aText;
		visable = Activate ();
		image.gameObject.SetActive(visable);
	}
	
	public void DeactivateText()
	{
		visable = Deactivate ();
		image.gameObject.SetActive(visable);
	}
	
	private bool Activate()
	{
		if(UseConstructBlocks)
			BuildTextBlocks();
		
		if(m_TextBlocks.Length == 0)
			return false;
		
		Reset ();
		
		return true;
	}
	
	/// <summary>
	/// Deactivate this instance.
	/// </summary>
	private bool Deactivate()
	{
		Reset ();
		return false;
	}
	
	/// <summary>
	/// Builds the text blocks.
	/// </summary>
	private void BuildTextBlocks()
	{
		float remainder = newText.Length % splitIndex;
		m_TextBlocks = new string[(int)(Mathf.Floor(newText.Length / (float)splitIndex)) + ((remainder==0) ? 0 : 1)];
		
		for(int x = 0; x < m_TextBlocks.Length; x++)
		{
			m_TextBlocks[x] = newText.Substring(x*splitIndex, (int)((x==m_TextBlocks.Length-1) ? ((remainder==0) ? splitIndex : remainder) : splitIndex));
		}
		
		for(int x = 0; x < m_TextBlocks.Length; x++)
		{
			if(m_TextBlocks[x].Contains(" "))
			{
				while(m_TextBlocks[x].Substring(m_TextBlocks[x].Length - 1) != " ")
				{
					if(x+1 != m_TextBlocks.Length)
					{
						m_TextBlocks[x+1] = m_TextBlocks[x+1].Insert(0, m_TextBlocks[x].Substring(m_TextBlocks[x].Length - 1));
						m_TextBlocks[x] = m_TextBlocks[x].Remove(m_TextBlocks[x].Length - 1);
					}
					else
						break;
				}
			}
		}
	}
	
	/// <summary>
	/// Reset this instance.
	/// </summary>
	private void Reset()
	{
		m_TextToScreen = ""; // clear the text
		m_CurrentTextBlockIndex = 0;
		m_CurrentTextBlock = m_TextBlocks[m_CurrentTextBlockIndex];
	}
	
	/// <summary>
	/// Pauses the index of the character.
	/// </summary>
	/// <returns><c>true</c>, if character index was paused, <c>false</c> otherwise.</returns>
	private bool PauseCharacterIndex()
	{
		//Increment the pauseTimer to match the deltaTime
		m_PauseTimer += Time.deltaTime;
		
		//When we cap the PauseTimer, reset the value, and change the VelocityState
		if(m_PauseTimer > textSpeed)
		{
			m_PauseTimer = 0.0f;
			return true;
		}
		return false;
	}
	
	/// <summary>
	/// Fills the text linked to the character .
	/// </summary>
	/// <returns><c>true</c>, if text was filled, <c>false</c> otherwise.</returns>
	public bool FillText()
	{
		if(m_TextToScreen == m_CurrentTextBlock)
			NextTextBlock();
		else
			m_TextToScreen = m_CurrentTextBlock;
		
		return visable;
	}
	
	/// <summary>
	/// Activate the next text to show in on the screen
	/// </summary>
	private void NextTextBlock() 
	{
		m_TextToScreen = ""; // clear the text
		
		if(m_CurrentTextBlockIndex < m_TextBlocks.Length-1)
		{
			m_CurrentTextBlockIndex++; 
			
			if(CheckBlockValidation())
				m_CurrentTextBlock = m_TextBlocks[m_CurrentTextBlockIndex];
			else
				NextTextBlock();
		}
		else
			DeactivateText();
	}
	
	/// <summary>
	/// Checks to make sure our block is valid.
	/// </summary>
	/// <returns><c>true</c>, if block acceptance was checked, <c>false</c> otherwise.</returns>
	private bool CheckBlockValidation()
	{
		if(m_TextBlocks[m_CurrentTextBlockIndex] == null || m_TextBlocks[m_CurrentTextBlockIndex].Length == 0
		   || m_TextBlocks[m_CurrentTextBlockIndex] == " " && m_TextBlocks[m_CurrentTextBlockIndex].Length == 1)
			return false;
		return true;
	}
}
