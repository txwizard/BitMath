/*
    ============================================================================

    Namespace:          TestStand

    Class Name:         BitArray32TestStand

    File Name:          BitArray32TestStand.cs

    Synopsis:           This class puts the BitArray32 class through its paces.

    Remarks:            When it moved to the BitMath namespace and library,
                        Bitmask32 became BitArray32, to prevent the inevitable
                        name collision when a class imports both namespaces.

	License:            Copyright (C) 2009-2016, David A. Gray.
						All rights reserved.

                        Redistribution and use in source and binary forms, with
                        or without modification, are permitted provided that the
                        following conditions are met:

                        *   Redistributions of source code must retain the above
                            copyright notice, this list of conditions and the
                            following disclaimer.

                        *   Redistributions in binary form must reproduce the
                            above copyright notice, this list of conditions and
                            the following disclaimer in the documentation and/or
                            other materials provided with the distribution.

                        *   Neither the name of David A. Gray, nor the names of
                            his contributors may be used to endorse or promote
                            products derived from this software without specific
                            prior written permission.

                        THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND
                        CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
                        WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
                        WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A
                        PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
                        David A. Gray BE LIABLE FOR ANY DIRECT, INDIRECT,
                        INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
                        (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
                        SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
                        PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON
                        ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
                        LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
                        ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN
                        IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Synopsis
    ---------- ------- --- -----------------------------------------------------
    2009/12/01 1.0     DAG Initial version.

    2009/12/15 2.4.15  DAG Merge the Bitmask32TestHarness class into this
                           program, and incorporate the build increment and
                           logging module. The class was at version 1.0.0.2.

    2010/01/18 2.43    DAG Cosmetic cleanup of these comments. The code, itself,
                           is unchanged.

    2014/05/22 3.0     DAG Relocated to BitMath namespace and library, and
                           renamed BitArray32, so that the old and new class
                           won't collide if both namespaces happen to get
                           imported into the same module.

	2016/04/29 4.0     DAG Incorporate tests for the new bit array formatters.

	2016/05/03 4.2     DAG Incorporate a complete test of the ruler generators.   
    ============================================================================
*/

using System;
using System.Globalization;
using System.IO;
using WizardWrx;


namespace TestStand
{
    static class BitArray32TestStand
    {
		const string FORMAT_FIRST_ITEM_AS_BIT_ARRAY = "{0:H}";
		const string FORMAT_FIRST_ITEM_AS_REVERSED_BIT_ARRAY = "{0:L}";
		const string FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY = "{0:H4}";
		
		enum OutputOptions : byte
        {
            /// <summary>
            /// Show Method Name if TRUE.
            ///
            /// If the EventLog flag is also set, the method name is always
            /// written there.
            /// </summary>
            Method = 0x01 ,

            /// <summary>
            /// Show Source (Assembly) Name if TRUE.
            ///
            /// If the EventLog flag is also set, the originating assembly name
            /// is always written there.
            /// </summary>
            Source = 0x02 ,

            /// <summary>
            /// Show Stack Trace if TRUE.
            ///
            /// If the EventLog flag is also set, the stack trace is always
            /// written there.
            /// </summary>
            Stack = 0x04 ,

            /// <summary>
            /// Post to associated event log if TRUE.
            ///
            /// The value of the event source associated with the current
            /// ExceptionLogger instance determines which event log gets the
            /// message.
            ///
            /// WARNING: Unless your event source string is already registered,
            /// the application MUST run, one time only, with full administrator
            /// privileges AND use the event source to write a message into the
            /// Windows event log in order for it to be registered with Windows,
            /// which maps it to an event log.
            ///
            /// Once the event source string is registered, the application can
            /// use it to post messages to the event log in any Windows security
            /// context.
            /// </summary>
            EventLog = 0x08 ,

            /// <summary>
            /// Write message on STDOUT if TRUE and if the application has a
            /// working console.
            ///
            /// CAUTION: Although it is technically legal to set both
            /// StandardOutput and StandardError to TRUE, this can have the
            /// unwanted consequence of generating TWO copies of the message,
            /// unless STDOUT and/or STDERR is directed to a file or if both are
            /// redirected to the SAME file.
            /// </summary>
            StandardOutput = 0x10 ,

            /// <summary>
            /// Write message on STDERR if TRUE and if the application has a
            /// working console.
            ///
            /// CAUTION: Although it is technically legal to set both
            /// StandardOutput and StandardError to TRUE, this can have the
            /// unwanted consequence of generating TWO copies of the message,
            /// unless STDOUT and/or STDERR is directed to a file or if both are
            /// redirected to the SAME file.
            /// </summary>
            StandardError = 0x20 ,

            /// <summary>
            /// Undefined - reserved for future use
            /// </summary>
            Reserved1 = 0x40 ,

            /// <summary>
            /// Undefined - reserved for future use
            /// </summary>
            Reserved2 = 0x80 ,
        }  // OutputOptions

        const byte OUTPUT_OPTIONS_ALL_OFF = 0;

		static readonly OutputOptions [ ] s_aenmStackTraceDisp =
        {
            OutputOptions.Stack ,
            OutputOptions.Stack & OUTPUT_OPTIONS_ALL_OFF
        };	// s_aenmStackTraceDisp

		static readonly OutputOptions [ ] s_aenmAppSubsystem =
        {
            OutputOptions.StandardError & OUTPUT_OPTIONS_ALL_OFF ,
            OutputOptions.StandardError ,
            OutputOptions.StandardError ,
            OutputOptions.StandardError & OUTPUT_OPTIONS_ALL_OFF ,
            OutputOptions.StandardError & OUTPUT_OPTIONS_ALL_OFF
        };	// s_aenmAppSubsystem

        static readonly OutputOptions [ ] s_aenmLoggingState =
        {
            OutputOptions.EventLog ,
            OutputOptions.EventLog & OUTPUT_OPTIONS_ALL_OFF
        };	// s_aenmLoggingState

		static readonly BitArray32.BitCount [ ] s_aenmBitCount =
		{
			BitArray32.BitCount.Unspecified ,
			BitArray32.BitCount.Count08 ,
			BitArray32.BitCount.Count16 ,
			BitArray32.BitCount.Count32 ,
			BitArray32.BitCount.Count64
		};	// s_aenmBitCount

		static readonly object [ ] s_aobjRulerSample =
		{
			0 ,												// BitArray32.BitCount.Unspecified (This is an unused placeholder.)
			( System.Byte ) 7 ,								// BitArray32.BitCount.Count08
			( System.UInt16 ) 101 ,							// BitArray32.BitCount.Count16
			( System.UInt32 ) 65536 ,						// BitArray32.BitCount.Count32
			( System.UInt64 ) 9223372036854770000			// BitArray32.BitCount.Count64
		};	// s_aobjRulerSample

		static readonly BitArray32.BitDisplayOrder [ ] s_aenmBitDisplayOrder =
		{
			BitArray32.BitDisplayOrder.Unspecified ,
			BitArray32.BitDisplayOrder.HighBitToLowBit ,
			BitArray32.BitDisplayOrder.LowBitToHighBit
		};	// BitDisplayOrder

		static readonly System.Byte [ ] s_abytExamples =
		{
			0 ,
			1 ,
			2 ,
			3 ,
			4 ,
			5 ,
			6 ,
			7 ,
			8 ,
			9 ,
			10 ,
			11 ,
			12 ,
			13 ,
			14 ,
			15 ,
			16 ,
			17 ,
			18 ,
			19 ,
			20 ,
			21 ,
			22 ,
			23 ,
			24 ,
			25 ,
			26 ,
			27 ,
			28 ,
			29 ,
			30 ,
			31 ,
			32 ,
			33 ,
			34 ,
			35 ,
			36 ,
			37 ,
			38 ,
			39 ,
			40 ,
			41 ,
			42 ,
			88 ,
			89 ,
			90 ,
			91 ,
			92 ,
			93 ,
			94 ,
			95 ,
			96 ,
			97 ,
			98 ,
			99 ,
			100 ,
			101 ,
			102 ,
			103 ,
			120 ,
			121 ,
			122 ,
			123 ,
			124 ,
			125 ,
			126 ,
			127 ,
			128 ,
			129 ,
			130 ,
			131 ,
			132 ,
			133 ,
			134 ,
			135 ,
			136 ,
			137 ,
			138 ,
			139 ,
			140 ,
			141 ,
			142 ,
			143 ,
			144 ,
			145 ,
			150 ,
			151 ,
			199 ,
			200 ,
			201 ,
			222 ,
			224 ,
			225 ,
			248 ,
			249 ,
			250 ,
			251 ,
			252 ,
			253 ,
			254 ,
			255
		};

		//	--------------------------------------------------------------------
		//	Save those thousands separator commas for when you really need them.
		//	Putting them in literals causes said literals to be split into
		//	separate array elements.
		//	--------------------------------------------------------------------

		static readonly UInt16 [ ] s_aint16Examples =
		{
			0 ,
			1 ,
			2 ,
			3 ,
			4 ,
			5 ,
			6 ,
			7 ,
			8 ,
			9 ,
			10 ,
			11 ,
			12 ,
			13 ,
			14 ,
			15 ,
			16 ,
			17 ,
			18 ,
			19 ,
			20 ,
			21 ,
			22 ,
			23 ,
			24 ,
			25 ,
			26 ,
			27 ,
			28 ,
			29 ,
			30 ,
			31 ,
			32 ,
			33 ,
			34 ,
			35 ,
			36 ,
			37 ,
			38 ,
			39 ,
			40 ,
			41 ,
			42 ,
			88 ,
			89 ,
			90 ,
			91 ,
			92 ,
			93 ,
			94 ,
			95 ,
			96 ,
			97 ,
			98 ,
			99 ,
			100 ,
			101 ,
			102 ,
			103 ,
			120 ,
			121 ,
			122 ,
			123 ,
			124 ,
			125 ,
			126 ,
			127 ,
			128 ,
			129 ,
			130 ,
			131 ,
			132 ,
			133 ,
			134 ,
			135 ,
			136 ,
			137 ,
			138 ,
			139 ,
			140 ,
			141 ,
			142 ,
			143 ,
			144 ,
			145 ,
			150 ,
			151 ,
			199 ,
			200 ,
			201 ,
			222 ,
			224 ,
			225 ,
			248 ,
			249 ,
			250 ,
			251 ,
			252 ,
			253 ,
			254 ,
			255 ,
			256 ,
			257 ,
			258 ,
			259 ,
			260 ,
			261 ,
			262 ,
			263 ,
			264 ,
			265 ,
			32763 ,
			32764 ,
			32765 ,
			32766 ,
			32767 ,
			65524 ,
			65525 ,
			65526 ,
			65527 ,
			65528 ,
			65529 ,
			65530 ,
			65531 ,
			65532 ,
			65533 ,
			65534 ,
			65535
		};	// s_aint16Examples

		//	--------------------------------------------------------------------
		//	Save those thousands separator commas for when you really need them.
		//	Putting them in literals causes said literals to be split into
		//	separate array elements.
		//	--------------------------------------------------------------------

		static readonly UInt32 [ ] s_aint32Examples =
		{
			0 ,
			1 ,
			2 ,
			3 ,
			4 ,
			5 ,
			6 ,
			7 ,
			8 ,
			9 ,
			10 ,
			11 ,
			12 ,
			13 ,
			14 ,
			15 ,
			16 ,
			17 ,
			18 ,
			19 ,
			20 ,
			21 ,
			22 ,
			23 ,
			24 ,
			25 ,
			26 ,
			27 ,
			28 ,
			29 ,
			30 ,
			31 ,
			32 ,
			33 ,
			34 ,
			35 ,
			36 ,
			37 ,
			38 ,
			39 ,
			40 ,
			41 ,
			42 ,
			88 ,
			89 ,
			90 ,
			91 ,
			92 ,
			93 ,
			94 ,
			95 ,
			96 ,
			97 ,
			98 ,
			99 ,
			100 ,
			101 ,
			102 ,
			103 ,
			120 ,
			121 ,
			122 ,
			123 ,
			124 ,
			125 ,
			126 ,
			127 ,
			128 ,
			129 ,
			130 ,
			131 ,
			132 ,
			133 ,
			134 ,
			135 ,
			136 ,
			137 ,
			138 ,
			139 ,
			140 ,
			141 ,
			142 ,
			143 ,
			144 ,
			145 ,
			150 ,
			151 ,
			199 ,
			200 ,
			201 ,
			222 ,
			224 ,
			225 ,
			248 ,
			249 ,
			250 ,
			251 ,
			252 ,
			253 ,
			254 ,
			255 ,
			256 ,
			257 ,
			258 ,
			259 ,
			260 ,
			261 ,
			262 ,
			263 ,
			264 ,
			265 ,
			32763 ,
			32764 ,
			32765 ,
			32766 ,
			32767 ,
			65524 ,
			65525 ,
			65526 ,
			65527 ,
			65528 ,
			65529 ,
			65530 ,
			65531 ,
			65532 ,
			65533 ,
			65534 ,
			65535 ,
			65536 ,
			2147483639 ,
			2147483640 ,
			2147483641 ,
			2147483642 ,
			2147483643 ,
			2147483644 ,
			2147483645 ,
			2147483646 ,
			2147483647 ,
			2147483648 ,
			2147483649 ,
			2147483650 ,
			2147483651 ,
			2147483652 ,
			4294967290 ,
			4294967291 ,
			4294967292 ,
			4294967293 ,
			4294967294 ,
			4294967295
		};	// s_aint32Examples

		//	--------------------------------------------------------------------
		//	Save those thousands separator commas for when you really need them.
		//	Putting them in literals causes said literals to be split into
		//	separate array elements.
		//	--------------------------------------------------------------------

		static readonly UInt64 [ ] s_aint64Examples =
		{
			0 ,
			1 ,
			2 ,
			3 ,
			4 ,
			5 ,
			6 ,
			7 ,
			8 ,
			9 ,
			10 ,
			11 ,
			12 ,
			13 ,
			14 ,
			15 ,
			16 ,
			17 ,
			18 ,
			19 ,
			20 ,
			21 ,
			22 ,
			23 ,
			24 ,
			25 ,
			26 ,
			27 ,
			28 ,
			29 ,
			30 ,
			31 ,
			32 ,
			33 ,
			34 ,
			35 ,
			36 ,
			37 ,
			38 ,
			39 ,
			40 ,
			41 ,
			42 ,
			88 ,
			89 ,
			90 ,
			91 ,
			92 ,
			93 ,
			94 ,
			95 ,
			96 ,
			97 ,
			98 ,
			99 ,
			100 ,
			101 ,
			102 ,
			103 ,
			120 ,
			121 ,
			122 ,
			123 ,
			124 ,
			125 ,
			126 ,
			127 ,
			128 ,
			129 ,
			130 ,
			131 ,
			132 ,
			133 ,
			134 ,
			135 ,
			136 ,
			137 ,
			138 ,
			139 ,
			140 ,
			141 ,
			142 ,
			143 ,
			144 ,
			145 ,
			150 ,
			151 ,
			199 ,
			200 ,
			201 ,
			222 ,
			224 ,
			225 ,
			248 ,
			249 ,
			250 ,
			251 ,
			252 ,
			253 ,
			254 ,
			255 ,
			256 ,
			257 ,
			258 ,
			259 ,
			260 ,
			261 ,
			262 ,
			263 ,
			264 ,
			265 ,
			32763 ,
			32764 ,
			32765 ,
			32766 ,
			32767 ,
			65524 ,
			65525 ,
			65526 ,
			65527 ,
			65528 ,
			65529 ,
			65530 ,
			65531 ,
			65532 ,
			65533 ,
			65534 ,
			65535 ,
			2147483639 ,
			2147483640 ,
			2147483641 ,
			2147483642 ,
			2147483643 ,
			2147483644 ,
			2147483645 ,
			2147483646 ,
			2147483647 ,
			2147483648 ,
			2147483649 ,
			2147483650 ,
			2147483651 ,
			2147483652 ,
			4294967290 ,
			4294967291 ,
			4294967292 ,
			4294967293 ,
			4294967294 ,
			4294967295 ,
			9223372036854775800 ,
			9223372036854775801 ,
			9223372036854775802 ,
			9223372036854775803 ,
			9223372036854775804 ,
			9223372036854775805 ,
			9223372036854775806 ,
			9223372036854775807 ,
			9223372036854775808 ,
			18446744073709551598 ,
			18446744073709551599 ,
			18446744073709551600 ,
			18446744073709551601 ,
			18446744073709551602 ,
			18446744073709551603 ,
			18446744073709551604 ,
			18446744073709551605 ,
			18446744073709551606 ,
			18446744073709551607 ,
			18446744073709551608 ,
			18446744073709551609 ,
			18446744073709551610 ,
			18446744073709551611 ,
			18446744073709551612 ,
			18446744073709551613 ,
			18446744073709551614 ,
			18446744073709551615
		};	// s_aint64Examples

        public static void Exercise ( string pstrReportFileName )
        {
            const string HEXADECIMAL_8 = @"X8";
			const string BITMASK_FORMAT_STRING = "G";

			int intTestNbr = TestBit32ArrayToStringOverload ( );
			TestBitArrayRulers ( ref intTestNbr );
			TestBitMaskAndRuler ( ref intTestNbr );

            Console.WriteLine ( @"Begin BitArray32TestStand Exercises" );
            Console.WriteLine ( @"{1}    Test # {0} - Display public constants.{1}" , ++intTestNbr , Environment.NewLine );

			Console.WriteLine ( 
				"        Bit  1      = {0} (Individual bits: {1})" ,			// Format control string
				BitArray32.BIT_01.ToString ( HEXADECIMAL_8 ) ,					// Format Item 0 = Bit as hexadecimal integer
				BitArray32.FormatIntegerAsBitArray (							// Format Item 1 = Bit as array of bits
					BitArray32.BIT_01 ,											// Specify the same bit, raw.
					BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );					// This should be passing a value of 1.

			Console.WriteLine ( @"        Bit  2      = {0} (Individual bits: {1})" , BitArray32.BIT_02.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_02 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  3      = {0} (Individual bits: {1})" , BitArray32.BIT_03.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_03 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  4      = {0} (Individual bits: {1})" , BitArray32.BIT_04.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_04 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  5      = {0} (Individual bits: {1})" , BitArray32.BIT_05.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_05 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  6      = {0} (Individual bits: {1})" , BitArray32.BIT_06.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_06 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  7      = {0} (Individual bits: {1})" , BitArray32.BIT_07.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_07 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  8      = {0} (Individual bits: {1})" , BitArray32.BIT_08.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_08 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit  9      = {0} (Individual bits: {1})" , BitArray32.BIT_09.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_09 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit 10      = {0} (Individual bits: {1})" , BitArray32.BIT_10.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_10 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );

			Console.WriteLine ( @"        Bit 11      = {0} (Individual bits: {1})" , BitArray32.BIT_11.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_11 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit 12      = {0} (Individual bits: {1})" , BitArray32.BIT_12.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_12 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit 13      = {0} (Individual bits: {1})" , BitArray32.BIT_13.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_13 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit 14      = {0} (Individual bits: {1})" , BitArray32.BIT_14.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_14 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 15      = {0} (Individual bits: {1})" , BitArray32.BIT_15.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_15 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 16      = {0} (Individual bits: {1})" , BitArray32.BIT_16.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_16 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 17      = {0} (Individual bits: {1})" , BitArray32.BIT_17.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_17 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 18      = {0} (Individual bits: {1})" , BitArray32.BIT_18.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_18 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 19      = {0} (Individual bits: {1})" , BitArray32.BIT_19.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_19 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 20      = {0} (Individual bits: {1})" , BitArray32.BIT_20.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_20 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );

			Console.WriteLine ( @"        Bit 21      = {0} (Individual bits: {1})" , BitArray32.BIT_21.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_21 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        Bit 22      = {0} (Individual bits: {1})" , BitArray32.BIT_22.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_22 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 23      = {0} (Individual bits: {1})" , BitArray32.BIT_23.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_23 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 24      = {0} (Individual bits: {1})" , BitArray32.BIT_24.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_24 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 25      = {0} (Individual bits: {1})" , BitArray32.BIT_25.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_25 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 26      = {0} (Individual bits: {1})" , BitArray32.BIT_26.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_26 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 27      = {0} (Individual bits: {1})" , BitArray32.BIT_27.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_27 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 28      = {0} (Individual bits: {1})" , BitArray32.BIT_28.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_28 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 29      = {0} (Individual bits: {1})" , BitArray32.BIT_29.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_29 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 30      = {0} (Individual bits: {1})" , BitArray32.BIT_30.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_30 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );

            Console.WriteLine ( @"        Bit 31      = {0} (Individual bits: {1})" , BitArray32.BIT_31.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_31 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
            Console.WriteLine ( @"        Bit 32      = {0} (Individual bits: {1})" , BitArray32.BIT_32.ToString ( HEXADECIMAL_8 ) , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_32 , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );

			Console.WriteLine ( @"        BIT_NBR_MIN = {0} (Individual bits: {1})" , BitArray32.BIT_NBR_MIN , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_NBR_MIN , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );
			Console.WriteLine ( @"        BIT_NBR_MAX = {0} (Individual bits: {1})" , BitArray32.BIT_NBR_MAX , BitArray32.FormatIntegerAsBitArray ( BitArray32.BIT_NBR_MAX , BitArray32.MINIMUM_BIT_GROUP_IN_FORMAT ) );

			Console.WriteLine ( @"        OFF         = {0}" , BitArray32.OFF );
			Console.WriteLine ( @"        ON          = {0}" , BitArray32.ON );

            Console.WriteLine ( @"{1}    Test # {0} - Default Constructor.{1}" , ++intTestNbr , Environment.NewLine );

            BitArray32 Mask1 = new BitArray32 ( );
			Console.WriteLine ( @"        Initial value of default BitArray32 = {0} (Individual bits: {1})" , Mask1.ToString ( BITMASK_FORMAT_STRING ) , Mask1.ToString ( "B4" ) );

            Mask1.BitOn ( 10 );
			Console.WriteLine ( @"        Bit 10 was turned ON. Mask1 = {0} (Individual bits: {1})" , Mask1.ToString ( BITMASK_FORMAT_STRING ) , Mask1.ToString ( "B4" ) );

            Console.WriteLine ( @"{1}    Test # {0} - First overloaded Constructor.{1}" , ++intTestNbr , Environment.NewLine );

            BitArray32 Mask2 = new BitArray32 ( BitArray32.BIT_07 );

			//	----------------------------------------------------------------
			//	The following statement writes a two-line ruler above the area
			//	where the next three statements will display bit masks. Since
			//	this group prints beneath the ruler, it eschews the spacing in
			//	favor of the compact format that aligns correctly with the
			//	ruler.
			//	----------------------------------------------------------------

			Console.WriteLine (
				"{2}                                                                            {0}{2}                                                                            {1}" ,
				BitArray32.GetRulerTens (
					BitArray32.BitCount.Count32 ,
					BitArray32.BitDisplayOrder.HighBitToLowBit ) ,
				BitArray32.GetRulerUnits (
					BitArray32.BitCount.Count32 ,
					BitArray32.BitDisplayOrder.HighBitToLowBit ) ,
				Environment.NewLine );

			Console.WriteLine ( @"        Initial value of Overloaded BitArray32 = {0} (Individual bits: {1})" , Mask2.ToString ( BITMASK_FORMAT_STRING ) , Mask2.ToString ( "H" ) );

            Mask2.BitOn ( 10 );
			Console.WriteLine ( @"        Bit 10 was turned ON.       Mask2      = {0} (Individual bits: {1})" , Mask2.ToString ( BITMASK_FORMAT_STRING ) , Mask2.ToString ( "H" ) );

            Mask2.BitOff ( 7 );
			Console.WriteLine ( @"        Bit 7 was turned OFF.       Mask2      = {0} (Individual bits: {1})" , Mask2.ToString ( BITMASK_FORMAT_STRING ) , Mask2.ToString ( "H" ) );

            if ( Mask2.IsBitOn ( 10 ) )
                Console.WriteLine ( @"        Bit 10 is ON" );
            else
                Console.WriteLine ( @"        Bit 10 is OFF" );

            if ( Mask2.IsBitOn ( 7 ) )
                Console.WriteLine ( @"        Bit 7 is ON" );
            else
                Console.WriteLine ( @"        Bit 7 is OFF" );

            if ( Mask2.IsBitOff ( 10 ) )
                Console.WriteLine ( @"        Bit 10 is OFF" );
            else
                Console.WriteLine ( @"        Bit 10 is NOT OFF" );

            if ( Mask2.IsBitOff ( 7 ) )
                Console.WriteLine ( @"        Bit 7 is OFF" );
            else
                Console.WriteLine ( @"        Bit 7 is NOT OFF" );

            StreamWriter swReport = null;

            try
            {
                swReport = new StreamWriter (
                    pstrReportFileName ,
                    StandardConstants.FILE_OUT_CREATE ,
                    System.Text.Encoding.ASCII );
                RealWorldTests ( swReport );
            }
            catch ( Exception ex )
            {
                Console.WriteLine ( ex.Message );
            }
            finally
            {   // Clean up the StreamWriter.
                if ( swReport != null )
                {
                    swReport.Close ( );
                    swReport.Dispose ( );
                    swReport = null;
                }   // if ( swReport != null )
            }   // One way or another, the StreamWriter got cleaned up.

            Console.WriteLine (
                "{0}End BitArray32TestStand Exercises{0}" ,
                Environment.NewLine );
        }   // static Exercise method


        private static void RealWorldTests ( StreamWriter pswReport )
        {
            const string MSG_BEGIN = @"{0}Begin RealWorldTests{0}";
			const string MSG_DONE = @"{0}RealWorldTests Done{0}";
            const string MSG_FLAG_STACK = @"    New s_aenmStackTraceDisp value             = {0} ({1,3})";
            const string MSG_FLAG_SUBSYSTEM = @"        New s_aenmAppSubsystem value           = {0} ({1,3})";
            const string MSG_FLAG_EVENTLOGGING = @"            New s_aenmLoggingState value       = {0} ({1,3})";

            const string MSG_OPTION_FLAGS_DISP = @"                Test Case {0,3} OptionFlags {1,3} = {2,3}";
            const string MSG_OPTIONS_ANTE = @"Ante";
            const string MSG_OPTIONS_POST = @"Post";

            const string REPORT_HEADINGS = "StackTraceDisposition\tOutputDestination\tEventLogging\tInitialFlags\tFinalFlags";
            const string REPORT_DETAIL_LINE = "{0,3}\t{1,3}\t{2,3}\t{3,3}\t{4,3}";

            Console.WriteLine (
                MSG_BEGIN ,
                Environment.NewLine );

            pswReport.WriteLine ( REPORT_HEADINGS );

            OutputOptions enmFlags = OutputOptions.Method | OutputOptions.Source;
            uint uintCase = StandardConstants.ZERO;

            foreach ( OutputOptions enmStackDisp in s_aenmStackTraceDisp )
            {   // Stack tracing is either ON or OFF.
                Console.WriteLine (
                    MSG_FLAG_STACK ,
                    enmStackDisp ,
                    ( byte ) enmStackDisp );

                foreach ( OutputOptions enmSubsystem in s_aenmAppSubsystem )
                {   // ExceptionLogger.Subsystem collapses to one of only three values.
                    Console.WriteLine (
                        MSG_FLAG_SUBSYSTEM ,
                        enmSubsystem ,
                        ( byte ) enmSubsystem );

                    foreach ( OutputOptions enmEventLogUsage in s_aenmLoggingState )
                    {   // Event logging is either ON or OFF.
                        Console.WriteLine (
                            MSG_FLAG_EVENTLOGGING ,
                            enmEventLogUsage ,
                            ( byte ) enmEventLogUsage );

                        //  ----------------------------------------------------
                        //  From this point forward, the code is identical with
                        //  the test code that brought this matter to my
                        //  attention.
                        //  ----------------------------------------------------

                        Console.WriteLine (
                            MSG_OPTION_FLAGS_DISP ,
                            ++uintCase ,
                            MSG_OPTIONS_ANTE ,
                            enmFlags );

                        OutputOptions enmInitialFlags = enmFlags;
                        BitArray32 b32Flags = new BitArray32 ( ( UInt32 ) enmFlags );

                        if ( ( enmSubsystem & OutputOptions.StandardError ) == OutputOptions.StandardError )
                            b32Flags.BitOn ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.StandardError ) );
                        else
                            b32Flags.BitOff ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.StandardError ) );

                        if ( ( enmStackDisp & OutputOptions.Stack ) == OutputOptions.Stack )
                            b32Flags.BitOn ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.Stack ) );
                        else
                            b32Flags.BitOff ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.Stack ) );

                        if ( ( enmEventLogUsage & OutputOptions.EventLog ) == OutputOptions.EventLog )
                            b32Flags.BitOn ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.EventLog ) );
                        else
                            b32Flags.BitOff ( ( int ) BitArray32.BitNumber ( ( UInt32 ) OutputOptions.EventLog ) );

                        enmFlags = ( OutputOptions ) ( ( UInt32 ) b32Flags );

                        Console.WriteLine (
                            MSG_OPTION_FLAGS_DISP ,
                            uintCase ,
                            MSG_OPTIONS_POST ,
                            enmFlags );

                        pswReport.WriteLine (
                            REPORT_DETAIL_LINE ,
                            new object [ ]
                            {
                                ( byte ) enmStackDisp ,         // Token 0 = StackTraceDisposition
                                ( byte ) enmSubsystem ,         // Token 1 = OutputDestination
                                ( byte ) enmEventLogUsage ,     // Token 2 = EventLogging
                                ( byte ) enmInitialFlags ,      // Token 3 = InitialFlags
                                ( byte ) enmFlags               // Token 4 = FinalFlags
                            } );
                    }   // foreach ( OutputOptions enmStackDisp in s_aenmStackTraceDisp )
                }   // foreach ( OutputOptions enmSubsystem in s_aenmAppSubsystem )
            }   // foreach ( OutputOptions enmEventLogUsage in s_aenmLoggingState )

            Console.WriteLine (
                MSG_DONE ,
                Environment.NewLine );
        }   // private static void RealWorldTests


		private static void TestBitArrayRulers ( ref int pintTestNbr )
		{
			Console.WriteLine (
				"Test {0}, TestBitArrayRulers Begin:" ,
				++pintTestNbr );

			int intSampleIndex = Util.ARRAY_FIRST_ELEMENT;

			foreach ( BitArray32.BitCount enmBitCount in s_aenmBitCount )
			{
				Console.WriteLine (
					"{2}    Bit Count = {0} (integer value = {1}):" ,
					enmBitCount ,
					( int ) enmBitCount ,
					Environment.NewLine );

				foreach ( BitArray32.BitDisplayOrder enmBitDisplayOrder in s_aenmBitDisplayOrder )
				{
					Console.WriteLine (
						"{2}        Bit Order = {0} (integer value = {1}):{2}" ,
						enmBitDisplayOrder ,
						( int ) enmBitDisplayOrder ,
					Environment.NewLine );

					try
					{
						Console.WriteLine (
							"        {0}{6}        {1}{6}        {2} ({3} value as integer: hexadecimal = {4} = {5:N0}  decimal){6}" ,		// Format control string
							new object [ ]
							{
								BitArray32.GetRulerTens (
									enmBitCount ,
									enmBitDisplayOrder ) ,																					// Format Item 0 = Tens row of ruler
								BitArray32.GetRulerUnits (
									enmBitCount ,
									enmBitDisplayOrder ) ,																					// Format Item 1 = Units row of ruler
								string.Format (
									new WizardWrx.BitMaskFormat ( ) ,
									enmBitDisplayOrder == BitArray32.BitDisplayOrder.LowBitToHighBit
										? FORMAT_FIRST_ITEM_AS_REVERSED_BIT_ARRAY
										: FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
									s_aobjRulerSample [ intSampleIndex ] ) ,																// Format Item 2 = Bit mask of Sample
								BitHelpers.InfoForIntegralType ( s_aobjRulerSample [ intSampleIndex ].GetType ( ) ) ,						// Format Item 3 = BCLIntegerTypeInfo type information (ToString reports all of its properties.)
								BitArray32.FormatIntegerAsHex ( s_aobjRulerSample [ intSampleIndex ] ) ,									// Format Item 4 = Formatted Hexadecimal representation of Sample.
								s_aobjRulerSample [ intSampleIndex ] ,																		// Format Item 5 = Formatted Decimal representation of Sample.
								Environment.NewLine																							// Format Item 6 = Embedded Newline
							} );
					}
					catch ( Exception exAllKinds )
					{
						Console.WriteLine (
							"An {0} exception occurred:{4}{4}    Message:        {1}{4}    Method:         {2}{4}    Stack Trace: {3}{4}" ,
							new object [ ]
							{
								exAllKinds.GetType ( ).FullName ,				// Format Item 0 = Exception Type
								Util.BeautifyExceptionMessage (
									exAllKinds.Message , 4 ) ,					// Format Item 1 = Exception Message
								exAllKinds.TargetSite ,							// Format Item 2 = Exception Method
								Util.BeautifyStackTrace (
									exAllKinds.StackTrace , 17 ) ,				// Format Item 3 = Exception Stack Trace
								Environment.NewLine								// Format Item 4 = Embedded Newline
							} );

						if ( exAllKinds.InnerException == null )
						{
							Console.WriteLine (
								"There are further details.{0}" ,
								Environment.NewLine );
						}	// TRUE (There is at least one inner exception.) block, if ( exAllKinds.InnerException == null )
						else
						{
							ReportInnerException ( exAllKinds.InnerException );
						}	// FALSE (There are no nested exceptions.) block, if ( exAllKinds.InnerException == null )
					}	// catch ( Exception exAllKinds ) Try/Catch block
				}	// foreach ( BitArray32.BitDisplayOrder enmRulelerDirection in s_aenmBitDisplayOrder )

				intSampleIndex++;		// Increment the subscript AFTER we use it!
			}	// foreach ( BitArray32.BitCount enmBitCount in s_aenmBitCount )

			Console.WriteLine (
				"Test {0} Done{1}" ,	// Format control string
				pintTestNbr ,			// Format Item 0 = Test Number
				Environment.NewLine );	// Format Item 1 = Embedded newline
		}	// private static void TestBitArrayRulers


		private static void TestBitMaskAndRuler ( ref int pintTestNbr )
		{
			const int LEADING_SPACE_COUNT = 10;

			Console.WriteLine (
				"Test {0}, TestBitMaskAndRuler Begin:{1}" ,	// Format control string
				++pintTestNbr ,								// Format Item 0 = Test Number
				Environment.NewLine );						// Format Item 1 = Embedded newline

			for ( int intOrientation = Util.ARRAY_SECOND_ELEMENT ;
				      intOrientation < s_aenmBitDisplayOrder.Length ;
					  intOrientation++ )
			{
				for ( int intSampleIndex = Util.ARRAY_SECOND_ELEMENT ;
					      intSampleIndex < s_aobjRulerSample.Length ;
						  intSampleIndex++ )
				{
					Console.WriteLine (
						"Creating a BitMaskAndRuler object with the following properties:{2}    BitDisplayOrder = {0}{2}    Integer Value   = 0x{1:X8} (decimal value = {1}){2}" ,
						s_aenmBitDisplayOrder [ intOrientation ] ,
						s_aobjRulerSample [ intSampleIndex ] ,
						Environment.NewLine );
					BitMaskAndRuler bmrBitMaskAndRuler = null;
					Type typOfSample = s_aobjRulerSample [ intSampleIndex ].GetType ( );

					if ( typOfSample == typeof ( byte ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( byte ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( UInt16 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( UInt16 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( UInt32 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( UInt32 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( UInt64 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( UInt64 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( Int16 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( Int16 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( Int32 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( Int32 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else if ( typOfSample == typeof ( Int64 ) )
					{
						bmrBitMaskAndRuler = new BitMaskAndRuler (
							( Int64 ) s_aobjRulerSample [ intSampleIndex ] ,
							s_aenmBitDisplayOrder [ intOrientation ] );
					}
					else
					{
						Console.WriteLine (
							"ERROR: The type of the sample at index {0}, {1} is unsupported. For reference, the value is {2}." ,
							intSampleIndex ,
							typOfSample ,
							s_aobjRulerSample [ intSampleIndex ] );
					}

					Console.WriteLine (
						"{1}Output of DisplayBitMaskFromMultilineString method (1 of 2):{1}{0}{1}" ,
						bmrBitMaskAndRuler.DisplayBitMaskFromMultilineString ( ) ,
						Environment.NewLine );
					Console.WriteLine (
						"{2}Output of DisplayBitMaskFromMultilineString method (2 of 2), with {0} leading spaces:{2}{1}{2}" ,	// Format Control String
						LEADING_SPACE_COUNT ,																					// Format Item 0 = Leading space count
						bmrBitMaskAndRuler.DisplayBitMaskFromMultilineString ( LEADING_SPACE_COUNT ) ,							// Format Item 1 = The whole enchilada
						Environment.NewLine );																					// Format Item 2 = Embedded newline

					string [ ] astrAllTheBits = bmrBitMaskAndRuler.DisplayBitMaskFromArray ( );
					Console.WriteLine (
						"Output of DisplayBitMaskFromArray method:{3}    Tens Ruler:  {0}{3}    Units Ruler: {1}{3}    Bit Mask:    {2}{3}" ,
						new string [ ]
						{
							astrAllTheBits [ BitMaskAndRuler.DISPLAY_TENS_ROW ] ,
							astrAllTheBits [ BitMaskAndRuler.DISPLAY_UNITS_ROW ] ,
							astrAllTheBits [ BitMaskAndRuler.DISPLAY_BITS_ROW ] ,
							Environment.NewLine
						} );
				}	// for ( int intSampleIndex = Util.ARRAY_SECOND_ELEMENT ; intSampleIndex < s_aobjRulerSample.Length ; intSampleIndex++ )
			}	// for ( int intOrientation = Util.ARRAY_SECOND_ELEMENT ; intOrientation < s_aenmBitDisplayOrder.Length ; intOrientation++ )

			Console.WriteLine(
				"Test {0} Done{1}" ,	// Format control string
				pintTestNbr ,			// Format Item 0 = Test Number
				Environment.NewLine );	// Format Item 1 = Embedded newline
		}	// private static void TestBitMaskAndRuler


		private static void ReportInnerException ( Exception pexInner )
		{
			Console.WriteLine (
				"    Inner Exception:{4}{4}    An {0} exception occurred:{4}{4}          Message:        {1}{4}          Method:         {2}{4}          Stack Trace: {3}{4}" ,
				new object [ ]
							{
								pexInner.GetType ( ).FullName ,					// Format Item 0 = Exception Type
								Util.BeautifyExceptionMessage (
									pexInner.Message , 14 ) ,					// Format Item 1 = Exception Message
								pexInner.TargetSite ,							// Format Item 2 = Exception Method
								Util.BeautifyStackTrace (
									pexInner.StackTrace , 27 ) ,				// Format Item 3 = Exception Stack Trace
								Environment.NewLine								// Format Item 4 = Embedded Newline
							} );

			if ( pexInner.InnerException == null )
			{
				Console.WriteLine (
					"    There are no further exceptions to report.{0}" ,
					Environment.NewLine );
				return;					// Document that this case ends the recursive calls.
			}	// TRUE (There are no further nested exceptions to report.) block, if ( pexInner.InnerException == null )
			else
			{	// ReportInnerException is called recursively until 
				ReportInnerException ( pexInner.InnerException );
			}	// FALSE (There is at least one more nested exceptions to report.) block, if ( pexInner.InnerException == null )
		}	// private static void ReportInnerException


		private static int TestBit32ArrayToStringOverload ( )
		{
			int rintTestCounter = Util.ARRAY_SECOND_ELEMENT;

			Console.WriteLine ( 
				"Test {0}TestBit32ArrayToStringOverload Begin:" ,
				rintTestCounter );

			//	----------------------------------------------------------------
			//	Test 8 bit integers, a. k. a., bytes.
			//	----------------------------------------------------------------

			Console.WriteLine ( "{0}    Exercising the formatter with 8 bit integers:{0}" , Environment.NewLine );

			foreach ( byte bytToTest in s_abytExamples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,3:N0}, Hexadecimal = {0,2:x2}, Individual Bits = {1}" ,
					bytToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
						bytToTest ) );
			}	// foreach ( byte bytToTest in s_abytExamples )

			Console.WriteLine (
				"{0}    Repeat with formatted array:{0}" ,
				Environment.NewLine );

			foreach ( byte bytToTest in s_abytExamples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,3:N0}, Hexadecimal = {0,2:x2}, Individual Bits = {1}" ,
					bytToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY ,
						bytToTest ) );
			}	// foreach ( byte bytToTest in s_abytExamples )

			Console.WriteLine ( "{0}    Done with 8 bit integers" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Test 16 bit integers, a. k. a., UInt16s or shorts.
			//	----------------------------------------------------------------

			Console.WriteLine ( "{0}    Exercising the formatter with 16 bit integers:{0}" , Environment.NewLine );

			foreach ( UInt16 int16BitsToTest in s_aint16Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,6:N0}, Hexadecimal = {0,4:x4}, Individual Bits = {1}" ,
					int16BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
						int16BitsToTest ) );
			}	// foreach ( UInt16 int16BitsToTest in s_aint16Examples )

			Console.WriteLine (
				"{0}    Repeat with formatted array:{0}" ,
				Environment.NewLine );

			foreach ( UInt16 int16BitsToTest in s_aint16Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,6:N0}, Hexadecimal = {0,4:x4}, Individual Bits = {1}" ,
					int16BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY ,
						int16BitsToTest ) );
			}	// foreach ( UInt16 int16BitsToTest in s_aint16Examples )

			Console.WriteLine ( "{0}    Done with 16 bit integers" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Test 32 bit integers, a. k. a., UInt32s or Integers.
			//	----------------------------------------------------------------

			Console.WriteLine ( "{0}    Exercising the formatter with 32 bit integers:{0}" , Environment.NewLine );

			foreach ( UInt32 int32BitsToTest in s_aint32Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,13:N0}, Hexadecimal = {0,8:x8}, Individual Bits = {1}" ,
					int32BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
						int32BitsToTest ) );
			}	// foreach ( UInt32 int32BitsToTest in s_aint32Examples )

			Console.WriteLine (
				"{0}    Repeat with formatted array:{0}" ,
				Environment.NewLine );

			foreach ( UInt32 int32BitsToTest in s_aint32Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,13:N0}, Hexadecimal = {0,8:x8}, Individual Bits = {1}" ,
					int32BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY ,
						int32BitsToTest ) );
			}	// foreach ( UInt32 int32BitsToTest in s_aint32Examples )

			Console.WriteLine ( "{0}    Done with 32 bit integers" , Environment.NewLine );

			//	----------------------------------------------------------------
			//	Test 64 bit integers, a. k. a., UInt64s or Long Integers.
			//	----------------------------------------------------------------

			Console.WriteLine ( "{0}    Exercising the formatter with 64 bit integers:{0}" , Environment.NewLine );

			foreach ( UInt64 int64BitsToTest in s_aint64Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,26:N0}, Hexadecimal = {0,16:x16}, Individual Bits = {1}" ,
					int64BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
						int64BitsToTest ) );
			}	// foreach ( UInt64 int64BitsToTest in s_aint64Examples )

			Console.WriteLine (
				"{0}    Repeat with formatted array:{0}" ,
				Environment.NewLine );

			foreach ( UInt64 int64BitsToTest in s_aint64Examples )
			{
				Console.WriteLine (
					"        Value: Decimal = {0,26:N0}, Hexadecimal = {0,16:x16}, Individual Bits = {1}" ,
					int64BitsToTest ,
					string.Format (
						new WizardWrx.BitMaskFormat ( ) ,
						FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY ,
						int64BitsToTest ) );
			}	// foreach ( UInt64 int64BitsToTest in s_aint64Examples )

			Console.WriteLine (
				"{0}    Done with 64 bit integers" ,
				Environment.NewLine );

			Console.WriteLine (
				"{1}Test {0} Done{1}" ,
				rintTestCounter ,
				Environment.NewLine );

			return rintTestCounter;
		}	// TestBit32ArrayToStringOverload
    }   // internal static BitArray32TestStand class
}   // partial namespace TestStand