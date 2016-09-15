#pragma strict

function Start () {

}

function Update () {

		var PlayerPlane = new Plane (Vector3.up, transform.position);
		var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		var hitdisk = 0f;

		if(PlayerPlane.Raycast (ray,hitdisk))
		{
			var targetPoint = ray.GetPoint(hitdisk);

			var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

			transform.rotation = targetRotation;
		}

}