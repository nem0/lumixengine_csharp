using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Lumix
{
	public static class Renderer	{
		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static void makeScreenshot(string filename);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static bool isOpenGL();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayersCount();


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static int getLayer(string name);


		[MethodImplAttribute(MethodImplOptions.InternalCall)]
		extern static string getLayerName(int idx);


		public static void MakeScreenshot(string filename)
		{
			makeScreenshot(filename);
		}

		public static bool IsOpenGL()
		{
			return isOpenGL();
		}

		public static int GetLayersCount()
		{
			return getLayersCount();
		}

		public static int GetLayer(string name)
		{
			return getLayer(name);
		}

		public static string GetLayerName(int idx)
		{
			return getLayerName(idx);
		}

	}

}
