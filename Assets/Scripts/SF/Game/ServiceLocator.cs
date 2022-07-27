﻿using SF.Common.Camera;
using SF.Common.Factories;
using SF.Common.Logger;
using SF.Common.Ticks;
using SF.UI.Controller;

namespace SF.Game
{
    public class ServiceLocator : IServiceLocator
    {
        public IDebugLogger Logger { get; }
        public ITickProcessor TickProcessor { get; }
        public IFactoryHolder FactoryHolder { get; }
        public ISmartCameraRegistrar CameraHolder { get; }
        public IWindowController WindowController { get; }
        
        public ServiceLocator(IWindowController windowController)
        {
            Logger = new UnityDebugLogger();
            TickProcessor = new TickProcessor();
            FactoryHolder = new FactoryHolder();
            CameraHolder = new SmartCameraRegistrar(Logger);

            WindowController = windowController;
        }
    }
}