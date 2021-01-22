﻿using System.Windows.Forms;
using CairoDesktop.AppGrabber;
using CairoDesktop.Application.Interfaces;
using CairoDesktop.Configuration;
using CairoDesktop.Infrastructure.Services;
using ManagedShell.Interop;

namespace CairoDesktop.SupportingClasses
{
    public class MenuBarWindowService : AppBarWindowService
    {
        private readonly AppGrabberService _appGrabber;
        private readonly IApplicationUpdateService _updateService;

        public MenuBarWindowService(ICairoApplication cairoApplication, ShellManagerService shellManagerService, WindowManager windowManager, AppGrabberService appGrabber, IApplicationUpdateService updateService) : base(cairoApplication, shellManagerService, windowManager)
        {
            _appGrabber = appGrabber;
            _updateService = updateService;

            EnableMultiMon = Settings.Instance.EnableMenuBarMultiMon;
            EnableService = Settings.Instance.EnableMenuBar;
        }

        protected override void HandleSettingChange(string setting)
        {
            switch (setting)
            {
                case "EnableMenuBar":
                    HandleEnableServiceChanged(Settings.Instance.EnableMenuBar);
                    break;
                case "EnableMenuBarMultiMon":
                    HandleEnableMultiMonChanged(Settings.Instance.EnableMenuBarMultiMon);
                    break;
            }
        }

        protected override void OpenWindow(Screen screen)
        {
            MenuBar newMenuBar = new MenuBar(_cairoApplication, _shellManager, _windowManager, _appGrabber, _updateService, screen, NativeMethods.ABEdge.ABE_TOP);
            Windows.Add(newMenuBar);
            newMenuBar.Show();
        }
    }
}
