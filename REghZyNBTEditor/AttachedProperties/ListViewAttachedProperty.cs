using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace REghZyNBTEditor.AttachedProperties {
    public static class ListViewAttachedProperty {
        private static readonly Dictionary<ListView, bool> CAN_EXECUTE_MAP = new Dictionary<ListView, bool>();

        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(ListViewAttachedProperty),
                new FrameworkPropertyMetadata(null, (d, e) => OnDoubleClickCommandChanged((ListView) d, (ICommand) e.OldValue, (ICommand) e.NewValue)));

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached(
                "CommandTarget",
                typeof(IInputElement),
                typeof(ListViewAttachedProperty),
                new FrameworkPropertyMetadata((object) null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(ListViewAttachedProperty),
                new FrameworkPropertyMetadata((object) null));

        private static void OnDoubleClickCommandChanged(ListView element, ICommand oldCommand, ICommand newCommand) {
            if (oldCommand != null) {
                UnhookCommand(element, oldCommand);
            }

            if (newCommand == null) {
                element.MouseDoubleClick -= ElementOnMouseDoubleClick;
                CAN_EXECUTE_MAP.Remove(element);
            }
            else {
                HookCommand(element, newCommand);
                element.MouseDoubleClick -= ElementOnMouseDoubleClick;
                element.MouseDoubleClick += ElementOnMouseDoubleClick;
            }
        }

        private static void ElementOnMouseDoubleClick(object sender, MouseButtonEventArgs e) {
            if (sender is ListView view && view.SelectedItem != null) {
                ICommand command = GetCommand(view);
                if (CAN_EXECUTE_MAP.TryGetValue(view, out bool canExecute) && canExecute) {
                    command.Execute(GetCommandParameter(view));
                }
            }
        }

        public static ICommand GetCommand(ListView item) {
            return (ICommand) item.GetValue(CommandProperty);
        }

        public static void SetCommand(ListView item, ICommand command) {
            item.SetValue(CommandProperty, command);
        }

        public static IInputElement GetCommandTarget(ListView item) {
            return (IInputElement) item.GetValue(CommandTargetProperty);
        }

        public static void SetCommandTarget(ListView item, IInputElement command) {
            item.SetValue(CommandTargetProperty, command);
        }

        public static object GetCommandParameter(ListView item) {
            return item.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(ListView item, object command) {
            item.SetValue(CommandParameterProperty, command);
        }

        private static void HookCommand(ListView item, ICommand command) {
            CanExecuteChangedEventManager.AddHandler(command, OnDoubleClickCanExecuteChanged);
            UpdateDoubleClickCanExecute(item, command);
        }

        private static void UnhookCommand(ListView item, ICommand command) {
            CanExecuteChangedEventManager.RemoveHandler(command, OnDoubleClickCanExecuteChanged);
            UpdateDoubleClickCanExecute(item, command);
        }

        private static void UpdateDoubleClickCanExecute(ListView element, ICommand command) {
            if (command != null) {
                CAN_EXECUTE_MAP[element] = command.CanExecute(GetCommandParameter(element));
            }
            else {
                CAN_EXECUTE_MAP[element] = true;
            }
        }

        private static void OnDoubleClickCanExecuteChanged(object sender, EventArgs e) {
            if (sender is ListView element) {
                UpdateDoubleClickCanExecute(element, GetCommand(element));
            }
        }
    }
}
