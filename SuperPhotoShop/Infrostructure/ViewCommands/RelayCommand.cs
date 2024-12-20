﻿using SuperPhotoShop.Infrostructure.ViewCommands.Base;
using System;
using System.Runtime.InteropServices;


namespace SuperPhotoShop.Infrostructure.ViewCommands
{
    internal class RelayCommand : ViewCommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;
        public RelayCommand(Action<object> Execute, Func<object,bool> CanExecute = null) 
        {
            _execute = Execute ?? throw new ArgumentNullException(nameof(Execute));
            _canExecute = CanExecute;
        }

        public override bool CanExecute(object parameter) => _canExecute?.Invoke(parameter) ?? true;

        public override void Execute(object obj) => _execute?.Invoke(obj);
    }
}
