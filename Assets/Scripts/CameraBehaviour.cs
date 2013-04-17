using UnityEngine;
using System.Collections;

public class CameraBehaviour : MonoBehaviour
{
	public Transform m_target;
	
	private float m_horiAngle;
	private float m_vertAngle;
	
	void Start()
	{
		transform.position = Vector3.forward;
	}
	
	void Update()
	{
//		if (m_target == null)
//			return;
//		
//		Vector3 backVector = new Vector3(m_target.position.x, 0, m_target.position.z);
//		backVector.Normalize();
//		
//		transform.position = m_target.position + backVector * 0.2f;
//		transform.LookAt(Vector3.zero, Vector3.up);
		
		transform.position =
			(Quaternion.AngleAxis(m_horiAngle, Vector3.up) *
			 Quaternion.AngleAxis(m_vertAngle, Vector3.right)) *
			 -Vector3.forward * 0.3f;
		transform.LookAt(Vector3.zero, Vector3.up);
	}
	
	public void OrbitHorizontal(float angle)
	{
		m_horiAngle += angle;
		m_horiAngle = Mathf.Clamp(m_horiAngle, -90.0f, 90.0f);
	}
	
	public void OrbitVertical(float angle)
	{
		m_vertAngle += angle;
		m_vertAngle = Mathf.Clamp(m_vertAngle, -30.0f, 30.0f);
	}
}
