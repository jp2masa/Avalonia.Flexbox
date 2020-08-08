using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using ReactiveUI;

namespace Avalonia.Flexbox.Demo.ViewModels
{
    internal sealed class MainWindowViewModel : ReactiveObject
    {
        private bool _isItemsControl = true;
        private bool _isItemsRepeater;

        private FlexDirection _direction = FlexDirection.Row;
        private JustifyContent _justifyContent = JustifyContent.FlexStart;
        private AlignItems _alignItems = AlignItems.FlexStart;
        private AlignContent _alignContent = AlignContent.FlexStart;
        private FlexWrap _wrap = FlexWrap.Wrap;

        private int _columnSpacing = 8;
        private int _rowSpacing = 32;

        public IEnumerable DirectionValues { get; } = Enum.GetValues(typeof(FlexDirection));

        public IEnumerable JustifyContentValues { get; } = Enum.GetValues(typeof(JustifyContent));

        public IEnumerable AlignItemsValues { get; } = Enum.GetValues(typeof(AlignItems));

        public IEnumerable AlignContentValues { get; } = Enum.GetValues(typeof(AlignContent));

        public IEnumerable WrapValues { get; } = Enum.GetValues(typeof(FlexWrap));

        public bool IsItemsControl
        {
            get => _isItemsControl;
            set => this.RaiseAndSetIfChanged(ref _isItemsControl, value);
        }

        public bool IsItemsRepeater
        {
            get => _isItemsRepeater;
            set => this.RaiseAndSetIfChanged(ref _isItemsRepeater, value);
        }

        public FlexDirection Direction
        {
            get => _direction;
            set => this.RaiseAndSetIfChanged(ref _direction, value);
        }

        public JustifyContent JustifyContent
        {
            get => _justifyContent;
            set => this.RaiseAndSetIfChanged(ref _justifyContent, value);
        }

        public AlignItems AlignItems
        {
            get => _alignItems;
            set => this.RaiseAndSetIfChanged(ref _alignItems, value);
        }

        public AlignContent AlignContent
        {
            get => _alignContent;
            set => this.RaiseAndSetIfChanged(ref _alignContent, value);
        }

        public FlexWrap Wrap
        {
            get => _wrap;
            set => this.RaiseAndSetIfChanged(ref _wrap, value);
        }

        public int ColumnSpacing
        {
            get => _columnSpacing;
            set => this.RaiseAndSetIfChanged(ref _columnSpacing, value);
        }

        public int RowSpacing
        {
            get => _rowSpacing;
            set => this.RaiseAndSetIfChanged(ref _rowSpacing, value);
        }

        public IEnumerable<int> Numbers { get; } = Enumerable.Range(1, 40);
    }
}
