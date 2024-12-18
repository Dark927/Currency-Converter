﻿namespace Currency_Converter.ViewModels
{
    public abstract class MainViewBase
    {
        protected MainWindow Window { get; private set; }

        public MainViewBase(MainWindow window)
        {
            Window = window;
        }

        abstract public void Clear(object? parameter = null);
        abstract public void Confirm(object? parameter = null);
        abstract public void BindData(object data);
    }
}
