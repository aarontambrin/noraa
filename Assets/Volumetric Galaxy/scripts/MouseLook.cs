using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
	Vector2 rotation = Vector2.zero;
	float pos = 0;
	public float RotationSpeed = 0.7f;
	public float MoveSpeed = 0.1f;

	void Update()
	{
		if (Input.GetKey(KeyCode.UpArrow))

        {
			rotation.x -= RotationSpeed;
		}
		if (Input.GetKey(KeyCode.DownArrow))

		{
			rotation.x += RotationSpeed;
		}

		if (Input.GetKey(KeyCode.LeftArrow))

		{
			rotation.y -= RotationSpeed;
		}
		if (Input.GetKey(KeyCode.RightArrow))

		{
			rotation.y += RotationSpeed;
		}

		if (Input.GetKey("w"))
        {
			transform.position += transform.forward * MoveSpeed;
        }
		if (Input.GetKey("s"))
		{
			transform.position -= transform.forward * MoveSpeed;

		}

		//rotation.x += -Input.GetAxis("Mouse Y");
		transform.eulerAngles = (Vector2)rotation * RotationSpeed;
		
	}
}
