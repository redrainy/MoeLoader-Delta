﻿using System;
using System.Windows;
using System.Windows.Threading;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace MoeLoaderDelta.Control.Toast
{
    public class ToastBoxWork
    {
        private Notifier notifier;

        /// <summary>
        /// Toast实现 调用此类进行工作
        /// </summary>
        public ToastBoxWork()
        {
            notifier = new Notifier(cfg =>
            {
                cfg.DisplayOptions.Width = 360;
                cfg.DisplayOptions.TopMost = false;
                cfg.PositionProvider = new WindowPositionProvider(Application.Current.MainWindow, Corner.BottomCenter, 0, 80);
                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(TimeSpan.FromSeconds(5), MaximumNotificationCount.FromCount(5));
                cfg.Dispatcher = Dispatcher.CurrentDispatcher;
            });
        }

        /// <summary>
        /// 释放Toast
        /// </summary>
        public void Dispose() { try { notifier?.Dispose(); } catch { } }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message">消息</param>
        /// <param name="msgType">类型</param>
        public void Show(string message, ToastBoxNotification.MsgType msgType = ToastBoxNotification.MsgType.Info)
        {
            if (string.IsNullOrWhiteSpace(message)) { return; }
            var options = new MessageOptions
            {
                FreezeOnMouseEnter = true,
                UnfreezeOnMouseLeave = true,
                NotificationClickAction = n => { n.Close(); }
            };

            notifier.ShowToastAsync(message, msgType, options);
        }

    }
}
