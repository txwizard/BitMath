                                 BitMath ReadMe

================================================================================
2016-06-07 12:07:00
================================================================================

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

Since this library is small and self-contained, it is the first of several that
I plan to publish as public GitHub repositories.
================================================================================
