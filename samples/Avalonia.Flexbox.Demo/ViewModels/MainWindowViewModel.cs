using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Windows.Input;

using ReactiveUI;

namespace Avalonia.Flexbox.Demo.ViewModels
{
    public sealed class MainWindowViewModel : ReactiveObject
    {
        private readonly ObservableCollection<ItemViewModel> _numbers;

        private bool _isItemsControl = true;
        private bool _isItemsRepeater;

        private FlexDirection _direction = FlexDirection.Row;
        private JustifyContent _justifyContent = JustifyContent.FlexStart;
        private AlignItems _alignItems = AlignItems.FlexStart;
        private AlignContent _alignContent = AlignContent.FlexStart;
        private FlexWrap _wrap = FlexWrap.Wrap;

        private int _columnSpacing = 8;
        private int _rowSpacing = 32;

        private int _currentNumber = 41;

        private ItemViewModel? _selectedItem;

        public MainWindowViewModel()
        {
            _numbers = new ObservableCollection<ItemViewModel>(Enumerable.Range(1, 40).Select(x => new ItemViewModel(x)));

            Numbers = new ReadOnlyObservableCollection<ItemViewModel>(_numbers);

            AddItemCommand = ReactiveCommand.Create(AddItem);
            RemoveItemCommand = ReactiveCommand.Create(RemoveItem, this.WhenAnyValue(vm => vm.SelectedItem).Select(x => x != null));
        }

        public IEnumerable DirectionValues { get; } = Enum.GetValues(typeof(FlexDirection));

        public IEnumerable JustifyContentValues { get; } = Enum.GetValues(typeof(JustifyContent));

        public IEnumerable AlignItemsValues { get; } = Enum.GetValues(typeof(AlignItems));

        public IEnumerable AlignContentValues { get; } = Enum.GetValues(typeof(AlignContent));

        public IEnumerable WrapValues { get; } = Enum.GetValues(typeof(FlexWrap));

        public IEnumerable AlignSelfValues { get; } = Enum.GetValues(typeof(AlignItems)).Cast<AlignItems>().Prepend(ItemViewModel.AlignSelfAuto);

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

        public ReadOnlyObservableCollection<ItemViewModel> Numbers { get; }

        public ItemViewModel? SelectedItem
        {
            get => _selectedItem;
            set => this.RaiseAndSetIfChanged(ref _selectedItem, value);
        }

        public ICommand AddItemCommand { get; }

        public ICommand RemoveItemCommand { get; }

        private void AddItem() => _numbers.Add(new ItemViewModel(_currentNumber++));

        private void RemoveItem()
        {
            if (SelectedItem is null)
            {
                throw new InvalidOperationException();
            }

            _numbers.Remove(SelectedItem);

            SelectedItem.IsSelected = false;
            SelectedItem = null;
        }
    }
}
