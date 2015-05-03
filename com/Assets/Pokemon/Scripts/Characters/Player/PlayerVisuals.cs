﻿using UnityEngine;
using System.Collections;
[ExecuteInEditMode]
public class PlayerVisuals : BaseController 
{
	public enum DirectionState { Down, Left, Right, Up }
	private DirectionState CurDirState = DirectionState.Down;

	public enum SpriteState { Walking, Running, Biking, Fishing, Swimming, SwimmingFishing, Diving }
	public SpriteState CurSpriteState = SpriteState.Walking;

	public Texture2D defaultTexture;

	public Renderer myRenderer;
	public GameObject UIObject;

	public bool IsAnimating;

	private Transform spriteTransform;

	public int m_UVTileX = 4;
	public int m_UVTileY = 28;
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
		spriteTransform = UIObject.transform;

		
		myRenderer.sharedMaterial.mainTexture=defaultTexture;
		myRenderer.GetComponent<MeshFilter>().mesh=null;
		sMesh = new SpriteMesh (defaultTexture);
		myRenderer.GetComponent<MeshFilter> ().mesh = sMesh.depthMesh(new Vector2((3)*64,(3-3)*64), new Vector2(64,64));
	}

	/// <summary>
	/// Lates the update.
	/// </summary>
	private void Update () 
	{
		// Apply Handling the image rotation
		ApplyImageRotationAndSpriteTexture ();
		UpdateImage ();
	}

	/// <summary>
	/// Updates the image.
	/// </summary>
	private void UpdateImage()
	{
		// Calculate index
		int _index = (int)(Time.timeSinceLevelLoad * m_FPS) % (m_UVTileX * m_UVTileY);
		
		// split into horizontal and vertical index
		int uIndex = 0;
		
		if(IsAnimating) //Make sure a key is pressed so we can animate
			uIndex = _index % m_UVTileX;
		
		int vIndex = (int)CurDirState;
		int hIndex = 0; //(int)CurSpriteState;
		
		int yIndex = (vIndex + ( 4 * (hIndex)));

		myRenderer.sharedMaterial.mainTexture=defaultTexture;
		myRenderer.GetComponent<MeshFilter>().mesh=null;
		myRenderer.GetComponent<MeshFilter>().mesh=sMesh.depthMesh(new Vector2((uIndex)*64,(3-yIndex)*64), new Vector2(64,64));	
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
			spriteTransform.localEulerAngles = m_LeftEulerAngle;
			this.CurDirState = DirectionState.Left;
			return;
		}
		else if(_result == _forwardPosition)
		{
			spriteTransform.localEulerAngles = m_BackEulerAngle;
			this.CurDirState = DirectionState.Down;
			return;
		}
		else if(_result == _rightPosition)
		{
			spriteTransform.localEulerAngles = m_RightEulerAngle;
			this.CurDirState = DirectionState.Right;
			return;
		}
		else if(_result == _backwardPosition)
		{
			spriteTransform.localEulerAngles = m_FaceEulerAngle;
			this.CurDirState = DirectionState.Up;
			return;
		}
	}
}
