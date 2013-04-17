using UnityEngine;
using System.Collections;

public class HUDBehaviour : MonoBehaviour
{
	private bool m_rmbDown;
	private bool m_lmbDown;
	
	private CameraBehaviour m_camera;
	
	private readonly static string CameraName = "Main Camera";
	
	void Awake()
	{
		m_camera = GameObject.Find(CameraName).GetComponent<CameraBehaviour>();
	}

	void Start()
	{
	
	}
	
	void Update()
	{
		if (Input.GetMouseButton(1) && !m_rmbDown)
		{
			m_rmbDown = true;
			Screen.lockCursor = true;
		}
		else if (!Input.GetMouseButton(1) && m_rmbDown)
		{
			m_rmbDown = false;
			Screen.lockCursor = false;
		}
		
		if (m_rmbDown)
		{
			m_camera.OrbitHorizontal(Input.GetAxis("Mouse X") * 5.0f);
			m_camera.OrbitVertical(Input.GetAxis("Mouse Y") * 5.0f);
		}
	}
}
