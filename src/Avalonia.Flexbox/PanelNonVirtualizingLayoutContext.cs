using System.Collections.Generic;

using Avalonia.Controls;
using Avalonia.Layout;

namespace Avalonia.Flexbox
{
    internal sealed class PanelNonVirtualizingLayoutContext : NonVirtualizingLayoutContext
    {
        private readonly Panel _panel;

        public PanelNonVirtualizingLayoutContext(Panel panel)
        {
            _panel = panel;
        }

        protected override IReadOnlyList<ILayoutable> ChildrenCore => _panel.Children;
    }
}
