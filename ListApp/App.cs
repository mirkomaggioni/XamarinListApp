using Android.App;
using Android.Runtime;
using ListApp.Services;
using System;

namespace ListApp
{
	[Application]
	public class App : Application
	{
		protected App(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}
		
		public override void OnCreate()
		{
			base.OnCreate();

			ContainerFactory.Init();
		}
	}
}