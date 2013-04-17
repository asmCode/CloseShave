using UnityEngine;
using System.Collections;

public class HUDBehaviour : MonoBehaviour
{
	private bool m_rmbDown;
	private bool m_lmbDown;
	
	private CameraBehaviour m_camera;
	private RazorBehaviour m_razor;
	
	private readonly static string CameraName = "Main Camera";
	private readonly static string RazorName = "Razor";
	
	void Awake()
	{
		m_camera = GameObject.Find(CameraName).GetComponent<CameraBehaviour>();
		m_razor = GameObject.Find(RazorName).GetComponent<RazorBehaviour>();
	}

	void Start()
	{
	
	}
	
	void Update()
	{
		if (Input.GetMouseButtonDown(1))
		{
			m_razor.PutAside();
		}
			
		if (Input.GetMouseButton(1))
		{
			Screen.lockCursor = true;
			
			m_camera.OrbitHorizontal(Input.GetAxis("Mouse X") * 5.0f);
			m_camera.OrbitVertical(Input.GetAxis("Mouse Y") * -5.0f);
			
			
		}
		else if (Input.GetMouseButtonUp(1))
		{
			Screen.lockCursor = false;
			
			m_razor.PutToFace(m_camera.transform.position, m_camera.transform.forward);
		}
		
		//if (m_rmbDown)
		{
			
		}
	}
}
