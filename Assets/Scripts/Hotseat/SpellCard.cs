using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class SpellCard : MonoBehaviour {

    public Image Image;

    public Vector3 TargetPosition;

	// Use this for initialization
	public void Init ()
    {
        Image = GetComponent<Image>();
        TargetPosition = MapMiddle.Instance.transform.position;
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = Vector3.Lerp(transform.position, TargetPosition, 0.1f);

        if((transform.position - TargetPosition).sqrMagnitude <= 0.1f) {
            foreach(var spellCard in GameObject.FindObjectsOfType<SpellCard>()) {
                if(spellCard != HotseatManager.Instance.CurrentTopCard) {
                    GameObject.Destroy(spellCard.gameObject);
                }
            }
        }
	}
}
