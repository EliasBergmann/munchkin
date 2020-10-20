using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munchkin.Utils
{
    public static class JavaScriptLibrary
    {
        public static async Task FocusAsync(string elementId, IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("exampleJsFunctions.focusElement", elementId);
        }

        public static async Task UpdateLayoutAsync(string elementClass, IJSRuntime jsRuntime)
        {
            await jsRuntime.InvokeVoidAsync("jsFunctions.updateLayout", elementClass);
        }
    }
}
