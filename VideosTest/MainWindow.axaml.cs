using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform;
using Avalonia.Rendering;
using SDL3;
using System;
using System.Collections.Generic;

using System.Threading;
using static SDL3.SDL;


namespace VideosTest
{
    public unsafe partial class MainWindow : Avalonia.Controls.Window
    {
        public MainWindow()
        {

            InitializeComponent();

            SDL.Init(SDL.InitFlags.Video);
          
            this.Loaded += MainWindow_Loaded;

        }

        private void MainWindow_Loaded(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
          
            SDL.Init(SDL.InitFlags.Events);
            nint window = nint.Zero;
            if (Environment.OSVersion.Platform == PlatformID.Win32NT)
            {
                uint prop = SDL.CreateProperties();
                SDL.SetPointerProperty(prop, SDL.Props.WindowCreateWin32HWNDPointer, test.Handle);
               
                window = SDL.CreateWindowWithProperties(prop);
            }
            else
            {
                uint prop = SDL.CreateProperties();
                SDL.SetNumberProperty(prop, SDL.Props.WindowCreateX11WindowNumber, test.Handle);
                window = SDL.CreateWindowWithProperties(prop);
            }
            nint renderer =   SDL.CreateRenderer(window, null);

           var targetTexture = SDL.CreateTexture(renderer, SDL.PixelFormat.BGRA8888, TextureAccess.Target, (int)test.Bounds.Width, (int)test.Bounds.Height);
            SDL.SetRenderTarget(renderer, targetTexture);
            //borderColor
            SDL.SetRenderDrawColor(renderer, 0, 255, 0, 255);
            SDL.RenderClear(renderer);
            SDL.SetRenderTarget(renderer, nint.Zero);
            SDL.RenderTexture(renderer, targetTexture, nint.Zero, nint.Zero);
            SDL.RenderPresent(renderer);
            SDL.SetRenderTarget(renderer, targetTexture);

            System.Threading.Thread t = new System.Threading.Thread(() =>
            {
                
                while (true)
                {
             
                  
                    //Console.WriteLine("MouseFocus:" + (long)sdl.GetMouseFocus());
                    //int x = 0, y = 0;
                    //uint z = sdl.GetGlobalMouseState(ref x, ref y);
                    //Console.WriteLine(x + "-" + y + "-" + z);
                     

                    //int x1 = 0, y1 = 0;
                    //uint z1 = sdl.GetMouseState(ref x1, ref y1);
                    //Console.WriteLine(x1 + "-" + y1 + "-" + z1);
                    SDL.Event e = new SDL.Event();
                    while (SDL.PollEvent(out e))
                    {
                       
                        Console.WriteLine((SDL.EventType)e.Type);

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