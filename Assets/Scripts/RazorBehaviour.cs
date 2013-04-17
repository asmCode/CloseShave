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
	
	private void SetPixel(Point2D point, Color color)
	{
		m_beardTexture.SetPixel(point.X, point.Y, color);
	}
	
	private void MoveAlongLine(Point2D point, Color color)
	{
		ShapesRenderer.DrawLine(m_beardTexture, point, point + m_edgeDir, new Color(0, 0, 0, 0), SetPixel);
	}
}
