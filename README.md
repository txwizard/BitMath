# WizardWrx BitMath ReadMe

The purpose of this class library is to simplify working with arrays of bits
stored in 32 bit unsigned integers. The original class, BitArray32, supports
testing and turning bits on and off by number. Recent additions support display
of such bit arrays in ways that give instant access to their individual bits.

To maximize compatibility with client code, the library targets version 2.0 of
the Microsoft .NET Framework, enabling it to support projects that target that
version, or any later version, of the framework. Since its implementation needs
only core features of the Base Class Library, I have yet to discover an issue in
using it with any of the newer frameworks.

The class belongs to the WizardWrx namespace, which I created to organize the
helper libraries that I use in virtually every production assembly, regardless
of what framework version is its target, and whether its surface is a Windows
console, the Windows desktop, or the ASP.NET Web server. To date, I have used
classes and methods in these libraries in all three environments.


## Using These Libraries

Since there are no name collisions, you may safely set references to all 6
namespaces in the same source module.

Detailed API documentation is at <https://txwizard.github.io/BitMath/>.

For those who just want to use them, debug and release builds of the libraries
and the unit test program are available as archives off the project root
directory.

*	`WizardWrx_BitMath_Binaries_Debug.7z` is the debug build of the binaries.

*	`WizardWrx_BitMath_Binaries_Release.7z` is the release build of the binaries.

There is a DLL, PDB, and XML file for each library. To derive maximum benefit,
including support for the Visual Studio managed code debugger and IntelliSense
in the text editor, take all three.

# Revision History

2016-06-07 12:07:00 Initial publication, with built-in IntelliSense help

2018/09/18 23:23:55 DocFX documentation added

2018/11/18 01:46:46 Generate new DocFX documentation based on my improved style sheets.