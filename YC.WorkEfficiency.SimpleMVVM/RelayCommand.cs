/* 项目名称： RelayCommand.cs
 * 命名空间： YC.WorkEfficiency.SimpleMVVM
 * 类 名 称: RelayCommand
 * 作   者 : 杨程
 * 概   述 : 
 * 创建时间 : 2021/2/20 18:26:51
 * 更新时间 : 2021/2/20 18:26:51
 * CLR版本 : 4.0.30319.42000
 * ******************************************************
 * Copyright@Administrator 2021 .All rights reserved.
 * ******************************************************
 */

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace YC.WorkEfficiency.SimpleMVVM
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="parameter"></param>
    public delegate void DelegateCommand<T>(T parameter);
    /// <summary>
    /// 
    /// </summary>
    public delegate void DelegateCommand();


    public class RelayCommand : ICommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="command"></param>
        public RelayCommand(DelegateCommand command)
        {
            this.delCommand = command;
        }

        

        private DelegateCommand delCommand;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler CanExecuteChanged;

        /// <summary>
        /// 执行测试
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {


            if (delCommand == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                CanExecuteChanged?.Invoke(this, null);
                delCommand.Invoke();
            }
        }
    }

    //public class RelayCommand<T> : ICommand
    //{
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="command"></param>
    //    public RelayCommand(DelegateCommand<T> command)
    //    {
    //        delCommand = command;
    //    }

    //    DelegateCommand<T> delCommand;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    public event EventHandler CanExecuteChanged;

    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="parameter"></param>
    //    /// <returns></returns>
    //    public bool CanExecute(object parameter)
    //    {

    //        if (delCommand == null)
    //        {
    //            return false;
    //        }
    //        return true;
    //    }
    //    /// <summary>
    //    /// 
    //    /// </summary>
    //    /// <param name="parameter"></param>
    //    public void Execute(object parameter)
    //    {
    //        T t = (T)parameter;
    //        if (CanExecute(parameter))
    //        {
    //            CanExecuteChanged?.Invoke(this, null);
    //            delCommand.Invoke(t);
    //        }
    //    }
    //}

    public class RelayCommand<T> : ICommand
    {
        private readonly Action<T> _execute;

        private readonly Predicate<T> _canExecute;

        public RelayCommand(Action<T> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<T> execute, Predicate<T> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            
            _execute = execute;
            _canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }

            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        [SuppressMessage("Microsoft.Performance", "CA1822:MarkMembersAsStatic",
            Justification = "The this keyword is used in the Silverlight version")]
        [SuppressMessage("Microsoft.Design", "CA1030:UseEventsWhereAppropriate",
            Justification = "This cannot be an event")]
        public void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute((T)parameter);
        }

        public void Execute(object parameter)
        {
            _execute((T)parameter);
        }
    }
}
