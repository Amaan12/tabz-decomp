using UnityEngine;

public class LookAtVelocity : MonoBehaviour
{
	private Rigidbody rig;

	private void Start()
	{
		rig = GetComponent<Rigidbody>();
	}

	private void Update()
	{
		if (rig.linearVelocity.magnitude != 0f)
		{
			base.transform.rotation = Quaternion.LookRotation(rig.linearVelocity);
		}
	}
}
