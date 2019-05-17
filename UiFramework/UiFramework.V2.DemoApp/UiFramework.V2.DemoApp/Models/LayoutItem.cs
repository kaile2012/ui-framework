using System;
using MvvmHelpers;
using UiFramework.V2.Enums;
using UiFramework.V2.Interfaces;

namespace UiFramework.V2.DemoApp.Models
{
    public class LayoutItem : ObservableObject, ILayoutItem
    {
        private Guid        _id;
        private Guid        _layoutId;
        private Guid        _snippetId;
        private string      _parameterModel;
        private Parameter   _parameterType;
        private string      _parameter;
        private string      _onTappedMethodName;
        private int         _column;
        private int         _row;
        private int         _columnSpan;
        private int         _rowSpan;

        public Guid Id
		{
			get => _id;
			set => SetProperty(ref _id, value);
		}

        public Guid LayoutId
		{
			get => _layoutId;
			set => SetProperty(ref _layoutId, value);
		}

        public Guid SnippetId
		{
			get => _snippetId;
			set => SetProperty(ref _snippetId, value);
		}

        public string ParameterModel
		{
			get => _parameterModel;
			set => SetProperty(ref _parameterModel, value);
		}

        public Parameter ParameterType
		{
			get => _parameterType;
			set => SetProperty(ref _parameterType, value);
		}

        public string Parameter
		{
			get => _parameter;
			set => SetProperty(ref _parameter, value);
		}

        public string OnTappedMethodName
		{
			get => _onTappedMethodName;
			set => SetProperty(ref _onTappedMethodName, value);
		}

        public int Column
		{
			get => _column;
			set => SetProperty(ref _column, value);
		}

        public int Row
		{
			get => _row;
			set => SetProperty(ref _row, value);
		}

        public int ColumnSpan
		{
			get => _columnSpan;
			set => SetProperty(ref _columnSpan, value);
		}

        public int RowSpan
		{
			get => _rowSpan;
			set => SetProperty(ref _rowSpan, value);
		}
    }
}
