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
		m_camera.transform.position =
			-Vector3.up * 0.02f +
			 -Vector3.forward * 0.3f;
		m_camera.transform.LookAt(Vector3.zero, Vector3.up);
		m_razor.PutToFace(m_camera.transform.position, m_camera.transform.forward);
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
		
		if (Input.GetMouseButtonDown(0))
		{
			Screen.lockCursor = true;
			m_razor.StartShaving();
		}
		else if (Input.GetMouseButtonUp(0))
		{
			Screen.lockCursor = false;
			m_razor.StopShaving();
		}
		
		if (!m_razor.IsRotating)
		{
			m_razor.MoveHorizontal(Input.GetAxis("Mouse X") * 5.0f);
			m_razor.MoveVertical(Input.GetAxis("Mouse Y") * -5.0f);
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 30, 400, 20), "Ruch myszką - Pozycjonowanie brzytwy");
		GUI.Label(new Rect(10, 50, 400, 20), "Lewy przycisk myszki + ruch myszką - Golenie");
		GUI.Label(new Rect(10, 70, 400, 20), "Prawy przycisk myszki + ruch myszką - Obracanie mordy");
	}
}
