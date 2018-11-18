# WizardWrx BitMath Library

The following sections summarize the classes. To use them, add a reference to
the `WizardWrx.BitMath.dll`, and add a using directive that specifies the
`WizardWrx` namespace.

Use the links in the table of contents along the left side of this page to view
the documentation.

## Why the Root Namespace Is Called WizardWrx

The short version is that I needed a short, descriptive root namespace name, and
I was doing business as WizardWrx, a name suggested by my then-new bride, to
convey her belief that I am a wizard, and because we happened to be living in a
village called Wizard Wells.

## Class Overview

*	__BCLIntegerTypeInfo__: Instances of this type are built into the library as
a static read-only array, which is incorporated into the static BitHelpers class
for use by its static methods, all of which are internal to this assembly.

*	__BitArray32__: This object exposes properties and methods to simplify using
masks of up to 32 bits in day to day programming.

*	__BitHelpers__: This static class defines utility methods to support
formatting of integral data types as arrays of individual bits, along with
embedded read-only data structures to support their work.

*	__BitMaskAndRuler__: Instances of this class keep a bit mask and a correctly
oriented ruler together.

*	__BitMaskFormat__: A default instance of this class is fed into an overload
of string.Format to render the bit array.

*	__FormattingParameters__: Static method ParseFormatString on class BitHelpers
reports its findings by constructing and returning an instance of this class.