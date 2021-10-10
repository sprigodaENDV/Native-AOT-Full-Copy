// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Runtime.InteropServices;

[StructLayout(LayoutKind.Auto)]
public class GenBase<T>
{
	T t;
	T Dummy(T t) { this.t = t; return t;}
}

[StructLayout(LayoutKind.Sequential)]
public class GenInt : GenBase<int>
{
	
}

public class GenTest
{
	private GenInt InternalTest()
	{
		return new GenInt();
	}

	private void IndirectTest()
	{
		InternalTest();
	}
	public bool Test()
	{
		try
		{
			IndirectTest();
			Console.WriteLine("Test did not throw expected TypeLoadException");
			return false;
		}
		catch(TypeLoadException)
		{
			return true;
		}
		catch(Exception E)
		{
			Console.WriteLine("Test caught unexpected Exception " + E);
			return false;
		}
	}
}

public class Test
{
	public static int counter = 0;
	public static bool result = true;
	public static void Eval(bool exp)
	{
		counter++;
		if (!exp)
		{
			result = exp;
			Console.WriteLine("Test Failed at location: " + counter);
		}
	
	}
	
	public static int Main()
	{

		Eval(new GenTest().Test());
		
		
		if (result)
		{
			Console.WriteLine("Test Passed");
			return 100;
		}
		else
		{
			Console.WriteLine("Test Failed");
			return 1;
		}
	}
}











