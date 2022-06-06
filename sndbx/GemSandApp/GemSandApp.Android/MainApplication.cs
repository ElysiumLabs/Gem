using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Gem;
using Shiny;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GemSandApp.Droid
{
    [Application]
    public class MainApplication : Application
    {
        public MainApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer) 
        {
            GemShinyApp = new ShinyApp();
        }

        public static ShinyApp GemShinyApp { get; set; }

        public override void OnCreate()
        {
            base.OnCreate();
            this.ShinyOnCreate(GemShinyApp);
        }
    }
}