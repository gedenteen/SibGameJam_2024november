using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceForDragableBlock : MonoBehaviour
{
    [SerializeField] private DragableBlocksManager _dragableBlocksManager;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        // if (other.transform.parent == null)
        // {
        //     Debug.Log($"PlaceForDragableBlock: OnTriggerEnter2D: other.transform.parent == null");
        //     return;
        // }

        DragableBlock dragableBlock;
        bool success = other/*.transform.parent*/.TryGetComponent<DragableBlock>(out dragableBlock);
        Debug.Log($"PlaceForDragableBlock: OnTriggerEnter2D: success={success} name={other.name}");

        if (success)
        {
            _dragableBlocksManager.MarkDragableBlockAsCompleted(dragableBlock);
            dragableBlock.MarkAsUninteractable();
        }
    }
}
