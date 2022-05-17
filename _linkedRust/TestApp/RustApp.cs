using System.Runtime.InteropServices;

public class RustApp
{
	[DllImport(@"libs/rust_dll.dll")]
	private static extern void rust_function();

	public void RustFunc()
	{
		rust_function();
	}
}
