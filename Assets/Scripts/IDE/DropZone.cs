using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Draggable dragged = eventData.pointerDrag.GetComponent<Draggable>();

        if (dragged != null)
        {
            GameObject draggedCopy = dragged.GetDraggedCopy();

            if (draggedCopy != null)
            {
                // Instancia um novo bloco dentro da área de montagem
                GameObject newBlock = Instantiate(dragged.gameObject, transform);
                newBlock.name = dragged.gameObject.name + "_Instance";

                // Garante que o novo bloco seja funcional (pode ser arrastado depois)
                CanvasGroup cg = newBlock.GetComponent<CanvasGroup>();
                if (cg == null) cg = newBlock.AddComponent<CanvasGroup>();
                cg.blocksRaycasts = true;
                cg.alpha = 1f;

                // Remove qualquer cópia temporária
                Destroy(draggedCopy);
            }
        }
    }
}
