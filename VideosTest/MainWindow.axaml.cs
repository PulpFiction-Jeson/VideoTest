using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform;
using Avalonia.Rendering;
using Silk.NET.SDL;
using System;
using System.Collections.Generic;

using System.Threading;


namespace VideosTest
{
    public unsafe partial class MainWindow : Avalonia.Controls.Window
    {
        public MainWindow()
        {

            InitializeComponent();


            Sdl sdl = Sdl.GetApi();
            sdl.Init(Sdl.InitVideo);
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            Sdl sdl = Sdl.GetApi();
            sdl.Init(Sdl.InitEvents);
            sdl.CaptureMouse(SdlBool.True);
            var window = sdl.CreateWindowFrom((void*)test.Handle);
            var renderer = sdl.CreateRenderer(window, -1, (uint)RendererFlags.Accelerated);
            sdl.GetWindowMouseGrab(window);
            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                sdl.SetWindowInputFocus(window);
                while (true)
                {
             
                    Console.WriteLine("MouseGrab:" + sdl.GetWindowMouseGrab(window));
                    //Console.WriteLine("MouseFocus:" + (long)sdl.GetMouseFocus());
                    //int x = 0, y = 0;
                    //uint z = sdl.GetGlobalMouseState(ref x, ref y);
                    //Console.WriteLine(x + "-" + y + "-" + z);
                     

                    //int x1 = 0, y1 = 0;
                    //uint z1 = sdl.GetMouseState(ref x1, ref y1);
                    //Console.WriteLine(x1 + "-" + y1 + "-" + z1);
                    Event e = new Event();
                    while (sdl.PollEvent(&e) != 0)
                    {
                       
                        Console.WriteLine((EventType)e.Type);

                    }



                    System.Threading.Thread.Sleep(100);

                }


            });
            t.Start();
        }
    }
    public class NativeEmbeddingControl : NativeControlHost
    {
        public IntPtr Handle { get; private set; }

        protected override IPlatformHandle CreateNativeControlCore(IPlatformHandle parent)
        {

            var handle = base.CreateNativeControlCore(parent);
            Handle = handle.Handle;
            Console.WriteLine($"Handle : {Handle}");
            return handle;
        }



    }
}