﻿# Introduction to the WizardWrx BitMath Library

I created the __WizardWrx BitMath__ to simplify the C# and VB.NET programming
that I do for myself and my clients by encapsulating tricky bit manipulations in
a set of methods that support treating 32 bit unsigned integers as bit maps.

For maximum compatibility, the library was built against version 2.0 of the
Microsoft .NET Framework, and it is compatible with versions from 2.0 up. The
classes in this library define numerous constants and utility methods, all
assigned to the base `WizardWrx` namespace.

Since the base `WizardWrx` namespace is free of name collisions, you may safely
add any `WizardWrx` assembly to any project, and add a `using WizardWrx`
directive to any source module to gain concurrent aaccess to all of them.

Use the __API Documentation__ link in the navigation bar at the top of this page
to display a summary of the classes, along with links to complete documentation
of the public constants, enumerations, and methods exposed by them.

# Road Map

This library is very stable, and I anticipate few changes beyond maybe extending
the bit maps to support more bits (e. g., 48, 64, 96, 128). For now, a 32 bit
map is more than enough to meet my needs, so that's as far as I went, except for
the bit map formetter, which I extended to 64 bits when I created it, so that it
covers the bits of an unsigned long integer.

The current version of this library was built on 04 May 2016, and many of my
other libraries, all of which are in active use and development, depend upon its
classes for *critical* functions. The `TestStand` project is a comprehensive unit
test set that covers every property and method in the library, and does double
duty as a demonstration of its use. `BitMath_TestStand_20181118_134455.LOG` in
the root directory of the repository is the output from executing it in a
command prompt window. Other examples exist here and there in the
[WizardWrx .NET API](https://github.com/txwizard/WizardWrx_NET_API).

1.	__NuGet Packages__ are a certainty, once I find time to create them.

# Contributing

Though I created this library to meet my individual development needs, I have
put a good bit of thought and care into its design. Moreover, since I will not
live forever, and I hope these libraries can outlive me, I would be honored to
add contributions from others to it, and eventually designate a new owner. My
expectations are relatively few, simple, easy to meet, and intended to preserve
the consistency of the code base and its API.

1.	__Naming Conventions__: I use Hungarian notation. Some claim that it has
outlived its usefulness. I think it remains useful because it encodes data
about the objects to which the names are applied that follows them wherever they
go, and convey it without help from IntelliSense.

2.	__Coding Style__: I have my editor set to leave spaces around every token.
This spacing has helped me quicly spot misplaced puncuation, such as the right
bracket that closes an array initializer that is in the wrong place, and it
makes mathematical expressions easier to read and mentally parse.

3.	__Comments__: I comment liberally and very deliberately. Of particular
importance are the comments that I append to the bracket that closes a block. It
does either or both of two things: link it to the opening statement, and
document which of two paths an __if__ statement is expected to follow most of
the time. When blocks get nested two, three, or four deep, they earn their keep.

4.	__Negative Conditions__: I do my best to avoid them, because they almost
always cause confusion. Astute observers will notice that they occur from time
to time, because there are _a few cases_ where they are _less_ confusing.

5.	__Array Initializers__: Arrays that have more than a very few initializers,
or that are initialized to long strings, are laid out as lists, with line
comments, if necessary, that describe the intent of each item.

6.	__Format Item Lists__: Lists of items that are paired with format items in
calls to `string.Format`, `Console.WriteLine`, and their relatives, are laid out
as arrays, even if there are too few to warrant one, and the comments show the
corresponding format item in context. This helps ensure that the items are
listed in the correct order, and that all format items are covered. This
practice has exposed countless bugs very early, when they are easiest to correct.

7.	__Symbolic Constants__: I use symbolic constants to document what a literal
value means in the context in which it is used, and to disambiguate tokens that
are easy to confuse, suzh as `1` and `l` (lower case L), `0` and `o` (lower case O),
literal spaces (1 and 2 spaces are common), underscores, the number `-1`, and so
forth. Literals that are widely applicable are defined in a set of classes that
comprise the majority of the root `WizardWrx` namespace.

8.	__Argument Lists__: I treat argument lists as arrays, and often comment each
argument with the name of the paramter that it represents. These lists help
guarantee that a long list of positional arguments is specified in the correct
order, especially when several are of the same type (e. g., two or more integer
arguments). This practice exposes bugs early, some subtle, when they are easy to
correct.

9.	__Triple-slash Comments__: These go on _everything_, even private members and
methods, so that everything supports IntelliSense, and it's easy to apply a tool
(I use DocFX.) to generate reference documentation.

With respect to the above items, you can expect me to be a nazi, though I shall
endeavor to give contributors a fair hearing for a good case. Otherwise, please
exercise your imagination, and submit your pull requests. When I get the NuGet
package implemented, I'll take care of rolling the contributions into the
package, and contributors will get prominent credit on the package page in the
official public repository. If you skim the headnotes of the code, you will
notice that I take great pains to give others credit when I icorporate their
code into my projects, even to the point of disclaiming copyright or leaving
their copyright notice intact. Along the same lines, the comments are liberally
sprinkled with references to articles and Stack Overflow discussions that
contributed to the work.