using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform originalParent;
    private GameObject dragCopy;
    private Canvas canvas;

    private void Awake()
    {
        canvas = FindFirstObjectByType<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Cria uma cópia visual do bloco original
        dragCopy = Instantiate(gameObject, canvas.transform);
        dragCopy.GetComponent<CanvasGroup>().blocksRaycasts = false;
        dragCopy.GetComponent<RectTransform>().sizeDelta = GetComponent<RectTransform>().sizeDelta;
        dragCopy.name = gameObject.name + "_Copy";

        originalParent = transform.parent;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (dragCopy != null)
            dragCopy.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Verifica se soltou em uma DropZone
        if (eventData.pointerEnter != null && eventData.pointerEnter.GetComponent<DropZone>() != null)
        {
            // A DropZone cuida da instância definitiva
            // destruímos apenas a cópia visual temporária
        }
        else
        {
            // Se não foi solto em área válida, destrói a cópia
            if (dragCopy != null)
                Destroy(dragCopy);
        }
    }

    // Método auxiliar usado pelo DropZone
    public GameObject GetDraggedCopy()
    {
        return dragCopy;
    }
}
