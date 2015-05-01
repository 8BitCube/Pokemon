using UnityEngine;
using System.Collections;

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
	
	private Vector2 m_size;
	private Vector2 m_offset;
	
	private Vector3 m_LeftEulerAngle =  new Vector3(0.0f, 90.0f, 0.0f);
	private Vector3 m_BackEulerAngle =  new Vector3(0.0f, 180.0f, 0.0f);
	private Vector3 m_RightEulerAngle = new Vector3(0.0f, 270.0f, 0.0f);
	private Vector3 m_FaceEulerAngle =  new Vector3(0.0f, 0.0f, 0.0f);

	/// <summary>
	/// Start this instance.
	/// </summary>
	private void Start()
	{
		m_size = new Vector2 (1.0f / m_UVTileX, 1.0f / m_UVTileY);
		spriteTransform = UIObject.transform;
		myRenderer.GetComponent<MeshFilter>().mesh=new SpriteMesh(defaultTexture).depthMesh(new Vector2(0,0), new Vector2(64,64));

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

	/// <summary>
	/// Updates the image.
	/// </summary>
	private void UpdateImage()
	{
		/*// Calculate index
		int _index = (int)(Time.timeSinceLevelLoad * m_FPS) % (m_UVTileX * m_UVTileY);

		// split into horizontal and vertical index
		int uIndex = 0;
		
		if(IsAnimating) //Make sure a key is pressed so we can animate
			uIndex = _index % m_UVTileX;

		int vIndex = (int)CurDirState;
		int hIndex = 0; //(int)CurSpriteState;

		int yIndex = (vIndex + ( 4 * (hIndex)));

		// build offset -- v coordinate is the bottom of the image in opengl so we need to invert.
		m_offset = new Vector2 (uIndex * m_size.x, 1.0f - m_size.y - yIndex * m_size.y);

		myRenderer.material.mainTexture = defaultTexture;
		myRenderer.material.SetTextureOffset ("_MainTex", m_offset);
		myRenderer.material.SetTextureScale ("_MainTex", m_size);*/
		
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
