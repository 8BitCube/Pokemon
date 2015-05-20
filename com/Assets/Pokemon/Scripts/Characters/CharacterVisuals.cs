using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class CharacterVisuals : CharacterBase 
{
	public enum DirectionState { Down, Left, Right, Up }
	private DirectionState CurDirState = DirectionState.Down;

	public Texture2D defaultTexture;

	public Renderer myRenderer;
	public MeshFilter myMeshFilter;

	public bool IsAnimating;
	private bool m_ForceAnimate;
	
	private Vector3 m_PrevEular;
	private float m_PrevIndex;

	public Transform spriteTransform;

	public int m_UVTileX = 4;
	public int m_UVTileY = 4;
	public int m_FPS = 10;
		
	private Vector3 m_LeftEulerAngle =  new Vector3(0.0f, 90.0f, 0.0f);
	private Vector3 m_BackEulerAngle =  new Vector3(0.0f, 180.0f, 0.0f);
	private Vector3 m_RightEulerAngle = new Vector3(0.0f, 270.0f, 0.0f);
	private Vector3 m_FaceEulerAngle =  new Vector3(0.0f, 0.0f, 0.0f);

	private SpriteMesh sMesh;

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		myRenderer.sharedMaterial.mainTexture=defaultTexture;

		if(sMesh==null)
			sMesh = new SpriteMesh(defaultTexture);

		ApplyImageRotationAndSpriteTexture ();
		UpdateIdolImage ();
	}

	/// <summary>
	/// Lates the update.
	/// </summary>
	private void LateUpdate () 
	{
		// Apply Handling the image rotation
		ApplyImageRotationAndSpriteTexture ();
		UpdateImage ();
	}

	private bool m_PrevAnimateWasTrue;
	/// <summary>
	/// Updates the image.
	/// </summary>
	public void UpdateImage()
	{
		if(sMesh.texture!=defaultTexture)
			sMesh.texture=defaultTexture;

		// Calculate index based on FPS and our timeScale
		int _index = (int)(Time.timeSinceLevelLoad * m_FPS) % (m_UVTileX * m_UVTileY);

		// split into horizontal and vertical index
		int uIndex = 0;

		//This will create an instant character image switch when the player rotates the player around the player.  
		// There used to be a noticable pause in changing the image
		if(m_ForceAnimate == false)
			if(m_PrevIndex == _index)	
				return;

		//Are we animating?  If so then update the uIndex
		if(IsAnimating == true) 
		{
			uIndex = _index % m_UVTileX;
			m_ForceAnimate = IsAnimating;
		}

		//If at anypoint our forceAnimate is false, do NOT update the mesh
		if(m_ForceAnimate == false)
			return;

		m_ForceAnimate = false;
		m_PrevIndex = _index;

		myRenderer.sharedMaterial.mainTexture=defaultTexture;
		
		myMeshFilter.mesh=null;
		myMeshFilter.mesh=sMesh.depthMesh(new Vector2((uIndex)*defaultTexture.width/4,(3-(int)CurDirState)*defaultTexture.height/4), new Vector2(defaultTexture.width/4,defaultTexture.height/4));	
	}

	public void UpdateIdolImage()
	{
		myRenderer.sharedMaterial.mainTexture=defaultTexture;

		myMeshFilter.mesh=null;
		myMeshFilter.mesh =	sMesh.depthMesh(new Vector2((0)*defaultTexture.width/4,(3-(int)CurDirState)*defaultTexture.height/4), new Vector2(defaultTexture.width/4,defaultTexture.height/4));	
	}

	/// <summary>
	/// Applies the image rotation depending on the rotation of the main Camera
	/// </summary>
	private void ApplyImageRotationAndSpriteTexture()
	{
		if(Camera.main == null)
			return;

		float _forwardPosition  = 	Vector3.Distance(Camera.main.transform.position, transform.position + transform.forward);
		float _leftPosition     = 	Vector3.Distance(Camera.main.transform.position, transform.position + -transform.right);
		float _rightPosition    = 	Vector3.Distance(Camera.main.transform.position, transform.position + transform.right);
		float _backwardPosition =   Vector3.Distance(Camera.main.transform.position, transform.position + -transform.forward);
		
		var _result = Mathf.Min(Mathf.Min (_forwardPosition, _leftPosition), Mathf.Min(_rightPosition, _backwardPosition));
		
		if(_result == _leftPosition)
		{
			this.CurDirState = DirectionState.Left;
			if(m_PrevEular == m_LeftEulerAngle) return;

			m_PrevEular = spriteTransform.localEulerAngles = m_LeftEulerAngle;
			m_ForceAnimate = true;

			return;
		}
		else if(_result == _forwardPosition)
		{
			this.CurDirState = DirectionState.Down;
			if(m_PrevEular == m_BackEulerAngle) return;

			m_PrevEular = spriteTransform.localEulerAngles = m_BackEulerAngle;
			m_ForceAnimate = true;

			return;
		}
		else if(_result == _rightPosition)
		{
			
			this.CurDirState = DirectionState.Right;
			if(m_PrevEular == m_RightEulerAngle) return;

			m_PrevEular = spriteTransform.localEulerAngles = m_RightEulerAngle;
			m_ForceAnimate = true;

			return;
		}
		else if(_result == _backwardPosition)
		{
			
			this.CurDirState = DirectionState.Up;
			if(m_PrevEular == m_FaceEulerAngle) return;

			m_PrevEular = spriteTransform.localEulerAngles = m_FaceEulerAngle;
			m_ForceAnimate = true;

			return;
		}
	}
}
