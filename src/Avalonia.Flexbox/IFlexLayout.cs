namespace Avalonia.Flexbox
{
    internal interface IFlexLayout : IAvaloniaObject
    {
        public FlexDirection Direction { get; }

        public JustifyContent JustifyContent { get; }

        public AlignItems AlignItems { get; }

        public AlignContent AlignContent { get; }

        public FlexWrap Wrap { get; }

        public double ColumnSpacing { get; }

        public double RowSpacing { get; }
    }
}
