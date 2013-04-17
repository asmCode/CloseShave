using UnityEngine;
using System.Collections;

public class RazorBehaviour : MonoBehaviour
{
	private GameObject m_beard;
	private GameObject m_beardModel;
	private MeshCollider m_beardMeshCollider;
	private MeshRenderer m_breadMeshRenderer;
	private Texture2D m_beardTexture;
	
	private Vector3 m_razorShift = new Vector3();
	private float m_razorRoll;
	
	private Color[] m_eraseShape;
	private GameObject[] m_edges;
	
	private Point2D m_edgeDir;
	
	private Point2D m_lastTexPoint;
	
	private static readonly Vector3 PutAsideDestinationPosition = new Vector3(0.0f, -0.06f, 0.12f);
	private bool m_isPuttingAside;
	private float m_putAsideProgress;
	private Vector3 m_putAsidePosition;
	private Quaternion m_putAsideRotation;
	
	/// <summary>
	/// Rotation based on beard normal but without roll angle.
	/// </summary>
	private Quaternion m_baseRotation;
	
	void Awake()
	{		
		m_beard = (GameObject)GameObject.Find("Beard");
		m_beardModel = (GameObject)GameObject.Find("BeardModel");
		m_beardMeshCollider = m_beardModel.GetComponent<MeshCollider>();
		m_breadMeshRenderer = m_beardModel.GetComponent<MeshRenderer>();
		m_beardTexture = (Texture2D)m_breadMeshRenderer.material.mainTexture;
		
		m_eraseShape = new Color[4 * 4];
		
		m_edges = GameObject.FindGameObjectsWithTag("Edge");
	}
	
	void Start()
	{
	
	}
	
	void Update()
	{
		if (m_isPuttingAside)
		{			
			Vector3 velocity = Vector3.zero;
			
			transform.localPosition =
				Vector3.SmoothDamp(transform.localPosition, m_putAsidePosition, ref velocity, 0.05f);
			
			velocity = Vector3.zero;
			
//			transform.localRotation = Quaternion.LookRotation(
//				Vector3.SmoothDamp(
//					transform.forward,
//					//Vector3.forward,
//					Vector3.forward,
//					ref velocity,
//					10.5f));
			
			transform.localRotation =
				Quaternion.Slerp(
					transform.localRotation,
					m_putAsideRotation,
					10.0f * Time.deltaTime);
		}
		
		return;
		
		if (Input.GetKey(KeyCode.Q))
			m_razorRoll += 100.0f * Time.deltaTime;
		
		if (Input.GetKey(KeyCode.W))
			m_razorRoll -= 100.0f * Time.deltaTime;
		
		
		m_razorShift.x += Input.GetAxis("Mouse X") * 0.01f;
		m_razorShift.y += Input.GetAxis("Mouse Y") * 0.01f;
		//m_razorShift.z = -0.14f;
		
		Vector3 rayPoint =
			transform.position +
			(m_baseRotation * Vector3.up) * Input.GetAxis("Mouse Y") * 0.01f +
			(m_baseRotation * Vector3.right) * Input.GetAxis("Mouse X") * 0.01f -
			transform.forward * 1.0f;
		
		{
			Ray razorRay = new Ray(rayPoint, transform.forward);
			RaycastHit hit;
			if (m_beardMeshCollider.Raycast(razorRay, out hit, float.MaxValue))
			{
				transform.position = hit.point;
				transform.LookAt(-hit.normal, Quaternion.AngleAxis(m_razorRoll, transform.forward) * Vector3.up);
				m_baseRotation.SetLookRotation(-hit.normal, Vector3.up);
			}
			else
			{
				//transform.position = rayPoint;
				return;
			}
		}
		
		bool isTexModified = false;
		
//		for (int i = 0; i < m_edges.Length; i++)
//		{
//			Ray razorRay = new Ray(m_edges[i].transform.position - transform.forward * 0.01f, transform.forward);
//			RaycastHit hit;
//			if (m_beardMeshCollider.Raycast(razorRay, out hit, float.MaxValue))
//			{
//				isTexModified = true;
//				
//				int texXCoord = (int)((float)m_breadMeshRenderer.material.mainTexture.width * hit.textureCoord.x);
//				int texYCoord = (int)((float)m_breadMeshRenderer.material.mainTexture.height * hit.textureCoord.y);
//				
//				((Texture2D)m_breadMeshRenderer.material.mainTexture).SetPixels(texXCoord, texYCoord, 4, 4, m_eraseShape);
//			}
//		}
		
		
		
		Ray razorRay1 = new Ray(m_edges[0].transform.position - transform.forward * 0.01f, transform.forward);
		Ray razorRay2 = new Ray(m_edges[m_edges.Length - 1].transform.position - transform.forward * 0.01f, transform.forward);
		RaycastHit hit1;
		RaycastHit hit2;
	
		if (m_beardMeshCollider.Raycast(razorRay1, out hit1, float.MaxValue) &&
		    m_beardMeshCollider.Raycast(razorRay2, out hit2, float.MaxValue))
		{
			isTexModified = true;
			
			Point2D p1 = new Point2D(
				(int)((float)m_beardTexture.width * hit1.textureCoord.x),
				(int)((float)m_beardTexture.height * hit1.textureCoord.y));
			
			Point2D p2 = new Point2D(
				(int)((float)m_beardTexture.width * hit2.textureCoord.x),
				(int)((float)m_beardTexture.height * hit2.textureCoord.y));
			
			m_edgeDir = p2 - p1;
			
			//ShapesRenderer.DrawLine(m_beardTexture, x1, y1, x1, y1 + 5, new Color(0, 0, 0, 0), MoveAlongLine);
			ShapesRenderer.DrawLine(m_beardTexture, p1, m_lastTexPoint, new Color(0, 0, 0, 0), MoveAlongLine);
			//((Texture2D)m_breadMeshRenderer.material.mainTexture).SetPixels(texXCoord, texYCoord, 4, 4, m_eraseShape);
			
			m_lastTexPoint = p1;
		}
		
		if (isTexModified)
			((Texture2D)m_breadMeshRenderer.material.mainTexture).Apply(true);
	}
	
	public void PutAside()
	{
		m_isPuttingAside = true;
		m_putAsidePosition = PutAsideDestinationPosition;
		m_putAsideRotation =
			Quaternion.AngleAxis(30, Vector3.forward) *
			Quaternion.AngleAxis(-90, Vector3.right);
	}
	
	public void PutToFace(Vector3 position, Vector3 forward)
	{
		m_isPuttingAside = false;
		
		Ray razorRay = new Ray(position, forward);
		RaycastHit hit;
		if (m_beardMeshCollider.Raycast(razorRay, out hit, float.MaxValue))
		{
			//transform.position = hit.point;
			//transform.LookAt(-hit.normal, Vector3.up);
			//m_baseRotation.SetLookRotation(-hit.normal, Vector3.up);
			
			m_isPuttingAside = true;
			m_putAsidePosition = transform.parent.worldToLocalMatrix.MultiplyPoint(hit.point);
			m_putAsideRotation = Quaternion.LookRotation(transform.parent.worldToLocalMatrix.MultiplyVector(-hit.normal));
		}
	}
	
	public void MoveHorizontal(float val)
	{
		
	}
	
	private void SetPixel(Point2D point, Color color)
	{
		m_beardTexture.SetPixel(point.X, point.Y, color);
	}
	
	private void MoveAlongLine(Point2D point, Color color)
	{
		ShapesRenderer.DrawLine(m_beardTexture, point, point + m_edgeDir, new Color(0, 0, 0, 0), SetPixel);
	}
}
