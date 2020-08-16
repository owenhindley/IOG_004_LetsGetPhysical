using UnityEngine;

public static class CameraExtension {
	public static bool IsWorldPointOnScreen(this Camera parent, Vector3 targetPosition)
	{
		return new Rect(0,0,Screen.width, Screen.height).Contains(parent.WorldToScreenPoint(targetPosition));
	}
	
	public static void LayerCullingShow(this Camera cam, int layerMask) {
		cam.cullingMask |= layerMask;
	}

	public static void LayerCullingShow(this Camera cam, string layer) {
		LayerCullingShow(cam, 1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingHide(this Camera cam, int layerMask) {
		cam.cullingMask &= ~layerMask;
	}

	public static void LayerCullingHide(this Camera cam, string layer) {
		LayerCullingHide(cam, 1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingToggle(this Camera cam, int layerMask) {
		cam.cullingMask ^= layerMask;
	}

	public static void LayerCullingToggle(this Camera cam, string layer) {
		LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer));
	}

	public static bool LayerCullingIncludes(this Camera cam, int layerMask) {
		return (cam.cullingMask & layerMask) > 0;
	}

	public static bool LayerCullingIncludes(this Camera cam, string layer) {
		return LayerCullingIncludes(cam, 1 << LayerMask.NameToLayer(layer));
	}

	public static void LayerCullingToggle(this Camera cam, int layerMask, bool isOn) {
		bool included = LayerCullingIncludes(cam, layerMask);
		if (isOn && !included) {
			LayerCullingShow(cam, layerMask);
		} else if (!isOn && included) {
			LayerCullingHide(cam, layerMask);
		}
	}

	public static void LayerCullingToggle(this Camera cam, string layer, bool isOn) {
		LayerCullingToggle(cam, 1 << LayerMask.NameToLayer(layer), isOn);
	}

	public static bool CheckPointInsideFrustum(this Camera cam, Vector3 position)
	{
		var horizontalFovAngle = cam.fieldOfView;
		var camH = Mathf.Tan(horizontalFovAngle  * Mathf.Deg2Rad * 0.5f) / cam.aspect;
		var verticalFovAngle = Mathf.Atan(camH) * 2 * Mathf.Rad2Deg;
		
		var destination = position;
		var direction = (destination - cam.transform.position).normalized;
		var horizontalAngle = Vector3.Angle(Vector3.ProjectOnPlane(direction, cam.transform.up),
			cam.transform.forward);
		var verticalAngle = Vector3.Angle(Vector3.ProjectOnPlane(direction, cam.transform.right),
			cam.transform.forward);

		return (Mathf.Abs(horizontalAngle) < horizontalFovAngle / 2f && Mathf.Abs(verticalAngle) < verticalFovAngle / 2f);
	}
}
