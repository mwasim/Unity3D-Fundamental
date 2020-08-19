using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerControl_Complete : MonoBehaviour
{
	public float mouseSensitivity = 120f;
	public bool invertMouseY = false;
	public float moveSpeed = 10f;
	public float shotDistance = 50f;
	public float shotForce = 10f;

	Transform camBody;
	float pitchRotation; //head rotation
	Rigidbody playerRB;
 
	void Start()
	{
		//Camera.main //Using Camera.main is slow and expensive, better approach is to get Camera component that we know already exists

		Camera cam = GetComponentInChildren<Camera>();
		if (cam == null)
			Debug.LogError("Player missing cam");
		else
			camBody = cam.transform;

		pitchRotation = camBody.rotation.eulerAngles.x;

		playerRB = GetComponent<Rigidbody>();

		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		Look();
		Move();
		Shoot();
	}

	void Look()
	{
		/*
		 * NOTE:
			If on rotation, the player falls down, simply freeze the rotation on the Rigidbody (all X, Y, and Z axis)
		 */

		float mX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime; //yaw
		float mY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime; //pitch

		//if (invertMouseY)
		//	yawRotation -= mY;
		//else
		//	yawRotation += mY;

		pitchRotation += invertMouseY ? -mY : mY;
		pitchRotation = Mathf.Clamp(pitchRotation, -90f, 90f);

		transform.Rotate(0f, mX, 0f);
		camBody.localRotation = Quaternion.Euler(pitchRotation, 0f, 0f);
	}

	void Move()
	{
		float moveFB = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
		float moveLR = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

		Vector3 offset = new Vector3(moveLR, 0f, moveFB);
		//transform.Translate(offset);
		Vector3 newPos = playerRB.position + transform.TransformDirection(offset);
		playerRB.MovePosition(newPos);
	}
	
	void Shoot()
	{
		if (!Input.GetKeyDown(KeyCode.LeftControl))
			return;

		RaycastHit hit;
		Ray ray = new Ray(camBody.position, camBody.forward);

		if (Physics.Raycast(ray, out hit, shotDistance))
		{
			if (hit.rigidbody == null)
				return;

			Vector3 force = -hit.normal * shotForce;
			hit.rigidbody.AddForceAtPosition(force, hit.point, ForceMode.Impulse);
		}
	}
}
