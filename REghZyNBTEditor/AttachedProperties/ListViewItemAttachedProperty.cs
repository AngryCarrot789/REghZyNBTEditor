using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace REghZyNBTEditor.AttachedProperties {
    public static class ListViewItemAttachedProperty {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached(
                "Command",
                typeof(ICommand),
                typeof(ListViewItemAttachedProperty),
                new FrameworkPropertyMetadata(
                    null,
                    (d, e) => {
                        OnDoubleClickCommandChanged((ListViewItem) d, (ICommand) e.OldValue, (ICommand) e.NewValue);
                    }));

        public static readonly DependencyProperty CommandTargetProperty =
            DependencyProperty.RegisterAttached(
                "CommandTarget",
                typeof(IInputElement),
                typeof(ListViewItemAttachedProperty),
                new FrameworkPropertyMetadata((object) null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached(
                "CommandParameter",
                typeof(object),
                typeof(ListViewItemAttachedProperty),
                new FrameworkPropertyMetadata((object) null));

        private static readonly Dictionary<ListViewItem, bool> CAN_EXECUTE_MAP = new Dictionary<ListViewItem, bool>();

        private static void OnDoubleClickCommandChanged(ListViewItem element, ICommand oldCommand, ICommand newCommand) {
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
            if (sender is ListViewItem element) {
                if (CAN_EXECUTE_MAP.TryGetValue(element, out bool canExecute) && canExecute) {
                    ICommand command = GetCommand(element);
                    if (command != null) {
                        command.Execute(GetCommandParameter(element));
                    }
                }
            }
        }

        public static ICommand GetCommand(ListViewItem item) {
            return (ICommand) item.GetValue(CommandProperty);
        }

        public static void SetCommand(ListViewItem item, ICommand command) {
            item.SetValue(CommandProperty, command);
        }

        public static IInputElement GetCommandTarget(ListViewItem item) {
            return (IInputElement) item.GetValue(CommandTargetProperty);
        }

        public static void SetCommandTarget(ListViewItem item, IInputElement command) {
            item.SetValue(CommandTargetProperty, command);
        }

        public static object GetCommandParameter(ListViewItem item) {
            return item.GetValue(CommandParameterProperty);
        }

        public static void SetCommandParameter(ListViewItem item, object command) {
            item.SetValue(CommandParameterProperty, command);
        }

        private static void HookCommand(ListViewItem item, ICommand command) {
            CanExecuteChangedEventManager.AddHandler(command, OnDoubleClickCanExecuteChanged);
            UpdateDoubleClickCanExecute(item, command);
        }

        private static void UnhookCommand(ListViewItem item, ICommand command) {
            CanExecuteChangedEventManager.RemoveHandler(command, OnDoubleClickCanExecuteChanged);
            UpdateDoubleClickCanExecute(item, command);
        }

        private static void UpdateDoubleClickCanExecute(ListViewItem element, ICommand command) {
            if (command != null) {
                CAN_EXECUTE_MAP[element] = command.CanExecute(GetCommandParameter(element));
            }
            else {
                CAN_EXECUTE_MAP[element] = true;
            }
        }

        private static void OnDoubleClickCanExecuteChanged(object sender, EventArgs e) {
            if (sender is ListViewItem element) {
                UpdateDoubleClickCanExecute(element, GetCommand(element));
            }
        }
    }
}
