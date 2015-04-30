using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HealthBar : MonoBehaviour 
{
	public Transform healthFillTransform;
	public bool isUI = false;

	private Color m_CurrentColor;
	private Vector3 m_TempFill;
	private float m_MaxWidth;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () 
	{		
		//Assign the default max Width
		ApplyNewMaxHealth ();
	}
	
	void LateUpdate () 
	{				
		m_TempFill = healthFillTransform.localScale;
				
		healthFillTransform.localScale = m_TempFill;
	}

	/// <summary>
	/// Applies the new health.
	/// </summary>
	/// <param name="aValue">A value.</param>
	public void ApplyNewHealth(float aValue)
	{
		//Keep min and max Parameters before visual Change
		m_TempFill.x = m_TempFill.x + aValue;
		
		if(m_TempFill.x < 0)
			m_TempFill.x = 0;
		
		if(m_TempFill.x > m_MaxWidth)
			m_TempFill.x = m_MaxWidth;
		
		ApplyVisualChange ();
	}

	/// <summary>
	/// Applies the new max health.
	/// </summary>
	public void ApplyNewMaxHealth()
	{
		m_MaxWidth = healthFillTransform.localScale.x;
	}

	private void ApplyVisualChange()
	{
		//Apply new health visual changes based on their percentage
		if(healthFillTransform.localScale.x > m_MaxWidth * 0.5f)
			m_CurrentColor = Color.green;
		else if(healthFillTransform.localScale.x < m_MaxWidth * 0.5f && healthFillTransform.localScale.x > m_MaxWidth * 0.25f)
			m_CurrentColor = Color.yellow;
		else if(healthFillTransform.localScale.x < m_MaxWidth * 0.25f)
			m_CurrentColor = Color.red;

		//We have 2 different situations where the visual change can be, make sure
		if(isUI)
			healthFillTransform.GetComponent<Image>().color = m_CurrentColor;
		else
			healthFillTransform.GetComponent<SpriteRenderer>().color = m_CurrentColor;
	}
}
