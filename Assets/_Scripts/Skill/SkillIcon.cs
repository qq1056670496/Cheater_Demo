using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class SkillIcon : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
{
    private GameObject clone;
    private GameObject canvas;
    private int skillId;
    private SkillPanel skillPanel;

    private void Start()
    {

        skillPanel = transform.parent.GetComponentInParent<SkillPanel>();
    }
    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        canvas = GameObject.Find("Canvas");
        skillId = this.transform.parent.GetComponent<SkillItem>().id;
        clone = Instantiate(this.gameObject, canvas.transform);
        clone.GetComponent<CanvasGroup>().blocksRaycasts = false;//取消阻挡射线
        skillPanel.hasPickedSkill = true;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        clone.transform.position = Input.mousePosition;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        if (eventData.pointerEnter == true)
        {
            GameObject surface = eventData.pointerEnter.gameObject;
            if (surface != null && surface.tag == "Shortcut")
            {
                surface.GetComponent<ShortcutSlot>().SetSkill(skillId);
            }
            else if (surface != null && surface.transform.parent.tag == "Shortcut")
            {
                surface.transform.parent.GetComponent<ShortcutSlot>().SetSkill(skillId);
            }
        }
        //clone.GetComponent<CanvasGroup>().blocksRaycasts = true;//阻挡射线  ??这有用吗
        Destroy(clone);

        skillPanel.hasPickedSkill = false;
    }
}
