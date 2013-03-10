using UnityEngine; 
using System.Collections;  

public class SmoothFollow1 : MonoBehaviour {     

	public GameObject target; 
	public float distance = 10f;
	public float height = 5f; 
	public float heightDamping = 2.0f; 
	public float rotationDamping = 3.0f;	

    // Update is called once per frame 
	void LateUpdate ()
	{		
		if(!target)
			target = GameObject.FindWithTag("Player");
		
		// Calculate the current rotation angles
        float wantedRotationAngle = target.transform.eulerAngles.y;
        float wantedHeight = target.transform.position.y + height; 

        float currentRotationAngle = transform.eulerAngles.y;
        float currentHeight = transform.position.y; 

        // Damp the rotation around the y-axis
        float dt = Time.deltaTime;
        currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * dt);//Time.GetMyDeltaTime()); 

        // Damp the height
        currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * dt);//Time.GetMyDeltaTime());

        // Convert the angle into a rotation
        Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);

        // Set the position of the camera on the x-z plane to:
        // distance meters behind the target
        //transform.position = target.position;

        Vector3 pos = target.transform.position - currentRotation * Vector3.forward * distance; 

        pos.y = currentHeight;

        // Set the height of the camera
        transform.position = pos; 

        // Always look at the target
        transform.LookAt(target.transform);
	}
}