using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmoothMouseLook : MonoBehaviour
{
	Vector2 rotation = Vector2.zero;
	public float speed = 3;

	private bool _rotateOn = true;

    private void Start()
    {
		TurnOn();
    }

    void Update()
	{
		if (_rotateOn)
        {
			rotation.y += Input.GetAxis("Mouse X");
			rotation.x += -Input.GetAxis("Mouse Y");
			transform.eulerAngles = (Vector2)rotation * speed;
		}

		if (Input.GetKeyDown(KeyCode.Escape))
        {
			if (_rotateOn)
				TurnOff();
			else
				TurnOn();
        }
	}

	public void TurnOn()
	{
		_rotateOn = true;
		Cursor.lockState = CursorLockMode.Locked;
	}

	public void TurnOff()
	{
		_rotateOn = false;
		Cursor.lockState = CursorLockMode.None;
	}
}