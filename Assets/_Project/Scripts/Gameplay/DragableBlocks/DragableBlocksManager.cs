using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DragableBlocksManager : MonoBehaviour
{
    [SerializeField] private List<DragableBlock> _dragableBlocks;
    [SerializeField] private List<UnityEvent> _eventsForInvokeAfterComplete;

    // Будем хранить состояние блоков. true означает, что блок помещен в PlaceForDragableBlock
    private Dictionary<DragableBlock, bool> _statusesOfDragableBlocks = new Dictionary<DragableBlock, bool>();

    private void Awake()
    {
        Debug.Log($"DragableBlocksManager: Awake: !!!!! _dragableBlocks.Count={_dragableBlocks.Count}");
        
        for (int i = 0; i < _dragableBlocks.Count; i++)
        {
            _statusesOfDragableBlocks.Add(_dragableBlocks[i], false);
        }
    }

    public void MarkDragableBlockAsCompleted(DragableBlock dragableBlock)
    {
        if (!_statusesOfDragableBlocks.ContainsKey(dragableBlock))
        {
            Debug.LogError($"DragableBlocksManager: MarkDragableBlockAsCompleted: i have no entry for this block");
            return;
        }

        _statusesOfDragableBlocks[dragableBlock] = true;
        CheckForComplete();
    }

    private void CheckForComplete()
    {
        for (int i = 0; i < _dragableBlocks.Count; i++)
        {
            if (_statusesOfDragableBlocks[_dragableBlocks[i]] == false)
            {
                Debug.Log($"DragableBlocksManager: CheckForComplete: not completed");
                return;
            }
        }

        Debug.Log($"DragableBlocksManager: CheckForComplete: completed!");

        for (int i = 0; i < _eventsForInvokeAfterComplete.Count; i++)
        {
            _eventsForInvokeAfterComplete[i].Invoke();
        }
    }
}
