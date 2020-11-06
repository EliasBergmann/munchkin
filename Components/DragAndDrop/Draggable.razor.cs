using Microsoft.AspNetCore.Components;
using Munchkin.Service;


namespace Munchkin.Components
{
    public partial class Draggable<T> : ComponentBase
    {
        #region Constructor

        public Draggable()
        {

        }

        #endregion

        #region Inject

        [Inject] protected DragAndDropService DragAndDropService { get; set; }

        #endregion

        #region Parameters

        [Parameter] public RenderFragment ChildContent { get; set; }

        [Parameter] public string Zone { get; set; }

        [Parameter] public T Data { get; set; }

        #endregion

        #region Private Methods

        private void OnDragStart()
        {
            DragAndDropService.StartDrag(Data, Zone);
        }

        #endregion
    }
}