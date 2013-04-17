using UnityEngine;
using System.Collections;

public class FPSCounterBehaviour : MonoBehaviour
{
	public float m_refreshFrequency;
	
	private int m_fps;
	private int m_frames;
	private float m_time;
	
	void Start()
	{
	
	}

	void Update()
	{
		m_frames++;
		
		m_time += Time.deltaTime;
		if (m_time >= m_refreshFrequency)
		{
			m_time = 0.0f;
			m_fps = (int)((float)m_frames / m_refreshFrequency);
			m_frames = 0;
		}
	}
	
	void OnGUI()
	{
		GUI.Label(new Rect(10, 10, 100, 100), "FPS: " + ((int)m_fps).ToString());
	}
}
