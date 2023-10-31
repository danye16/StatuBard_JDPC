using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Plugin.CurrentActivity;
using StatuBard_JDPC.Droid.statusbar;
using StatuBard_JDPC.VistaModelo;
using Xamarin.Forms;
[assembly:Dependency(typeof(StatusBar))]
namespace StatuBard_JDPC.Droid.statusbar
{
    internal class StatusBar : VMstatusbar
    {
        //Con esto volveemos cualquie cambio a su estado original
        WindowManagerFlags _originalFlags;
        Window GetCurrentwindos() 
        {
            var windows = CrossCurrentActivity.Current.Activity.Window;
            windows.ClearFlags(WindowManagerFlags.TranslucentStatus);
            windows.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            //Con esto le decimos que limpie lo que trae por default y nos permite
            //dibujar nuestra propia ventana
            return windows;
        }
        public void CambiarColor()
        {
            MostrarStatus();
            if (Build.VERSION.SdkInt>= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindows = GetCurrentwindos();
                    currentWindows.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutStable;
                    currentWindows.SetStatusBarColor(Android.Graphics.Color.Rgb(18, 18, 18));
                });
            }
        }

        public void MostrarStatus()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            attrs.Flags = _originalFlags;
            activity.Window.Attributes = attrs;
        }

        public void OcultarStatusBar()
        {
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.Fullscreen;
            activity.Window.Attributes = attrs;
        }

        public void Transparente()
        {
            MostrarStatus();
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindows = GetCurrentwindos();
                    currentWindows.DecorView.SystemUiVisibility = (StatusBarVisibility)SystemUiFlags.LayoutFullscreen;
                    currentWindows.SetStatusBarColor(Android.Graphics.Color.Transparent);
                });
            }
        }

        public void Traslucido()
        {
            MostrarStatus();//Es por si nuestro status bar estaba oculto, primero se muestre y luego aplique el tema
            var activity = (Activity)Forms.Context;
            var attrs = activity.Window.Attributes;
            _originalFlags = attrs.Flags;
            attrs.Flags |= WindowManagerFlags.TranslucentStatus;
            activity.Window.Attributes = attrs;
        }
    }
}