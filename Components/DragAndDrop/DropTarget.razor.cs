using Microsoft.AspNetCore.Components;
using Munchkin.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace Munchkin.Components
{
    public partial class DropTarget<T>: ComponentBase
    {
        #region Inject

        [Inject] protected DragAndDropService DragAndDropService { get; set; }

        #endregion

        #region Parameters

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Zone { get; set; }

        [Parameter] public Action<T> Drop { get; set; }

        #endregion

        #region Private Methods

        private void OnDrop()
        {
            if (Drop != null && DragAndDropService.Accepts(Zone))
            {
                Drop((T)DragAndDropService.Data);
            }
        }

        #endregion
    }
}
