using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class VitualJoyStick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler {
	//[SerializeField]
	private Image bgImg;
	//[SerializeField]
	private Image joystickImg;

	//public Vector3 InputDirection{ get; set;}
	Vector3 inputVector;

	public static bool useJoyStick;

	void Start(){
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild(0).GetComponent<Image> ();
	}

	public virtual void OnDrag(PointerEventData ped){
		//Debug.Log ("onDrag");
		Vector2 pos;
		if (RectTransformUtility.ScreenPointToLocalPointInRectangle
			(bgImg.rectTransform,
				ped.position,
				ped.pressEventCamera,
				out pos)) {
			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVector = new Vector3 (pos.x * 2, 0, pos.y * 2);
			inputVector = (inputVector.magnitude > 1.0f) ? inputVector.normalized : inputVector;
			//float x = (bgImg.rectTransform.pivot.x == 1) ? pos.x * 2 + 1 : pos.x * 2 - 1;
			//float y = (bgImg.rectTransform.pivot.y == 1) ? pos.y * 2 + 1 : pos.y * 2 - 1;

			//InputDirection = new Vector3 (x, 0, y);
			//InputDirection = (InputDirection.magnitude > 1) ? InputDirection.normalized : InputDirection;

			joystickImg.rectTransform.anchoredPosition = 
				new Vector3 (inputVector.x * (bgImg.rectTransform.sizeDelta.x / 3),
					inputVector.z * (bgImg.rectTransform.sizeDelta.y / 3));
		}
	}

	public virtual void OnPointerDown(PointerEventData ped){
		bgImg.color = new Color (1f, 1f, 1f, 0.6f);
		joystickImg.color = new Color (1f, 1f, 1f, 0.6f);
		useJoyStick = true;
		OnDrag (ped);
	}

	public virtual void OnPointerUp(PointerEventData ped){
		bgImg.color = new Color (1f, 1f, 1f, 0.2f);
		joystickImg.color = new Color (1f, 1f, 1f, 0.2f);
		useJoyStick = false;
		joystickImg.rectTransform.anchoredPosition = Vector3.zero;
	}

	public float Horizontal(){
		if (inputVector.x != 0) {
			return inputVector.x;
		} else
			return Input.GetAxis ("Horizontal");
	}

	public float Vertical(){
		if (inputVector.z != 0) {
			return inputVector.z;
		} else
			return Input.GetAxis ("Vertical");
	}
}
