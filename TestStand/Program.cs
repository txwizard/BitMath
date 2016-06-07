/*
    ============================================================================

    Namespace:          TestStand

    Class Name:         Program

    File Name:          Program.cs

    Synopsis:           Besides defining the entry point, this class implements
                        testing of the first two classes to join the BitMath
                        class library, BitArray32 and BitArray32.

    Remarks:            This class was created and tested in my "playpen" test

    Author:             David A. Gray

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

    Created:            Wednesday, 21 May 2014 and Thursday, 21 May 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version By  Description
    ---------- ------- --- -----------------------------------------------------
    2014/05/22 3.0     DAG Initial implementation. This is a bare bones test
                           stand. It is missing some of the usual features of my
                           test stand programs. I'll add then after I use the
                           associated class library to finish testing the new
                           versions of the required libraries.

	2016/04/29 4.0     DAG Incorporate tests for the new bit array formatters.

	2016/05/04 4.3     DAG Incorporate ShowKeyAssemblyProperties, copied from my
                           WizardWrx.DllServices2 class library.
    ============================================================================
*/


using System;
using System.Collections.Generic;

using WizardWrx;

namespace TestStand
{
    class Program
    {
		static readonly char [ ] s_achrHexDigitChars =
		{
			'0' ,
			'1' ,
			'2' ,
			'3' ,
			'4' ,
			'5' ,
			'6' ,
			'7' ,
			'8' ,
			'9' ,
			'a' ,
			'b' ,
			'c' ,
			'd' ,
			'e' ,
			'f'
		};	// s_achrHexDigitChars

		static readonly Type [ ] s_atypIntegerTypes =
		{
			typeof ( sbyte ) ,			//  1 of 21: Maps to System.Byte	Signed Byte = 1 byte, treated as Signed 8 bit integer
			typeof ( byte ) ,			//  2 of 21: Maps to System.Byte	Unsigned Byte = 1 byte, treated as Unsigned 8 bit integer
			typeof ( Int16 ) ,			//  3 of 21: Maps to System.Int16	Signed Short = 2 bytes, treated as Signed 16 bit integer
			typeof ( UInt16 ) ,			//  4 of 21: Maps to System.Int16	Unsigned Short = 2 bytes, treated as Unsigned 16 bit integer
			typeof ( short ) ,			//  5 of 21: Maps to System.Int16	Signed Short Alias = 2 bytes, treated as Signed 16 bit integer
			typeof ( ushort ) ,			//  6 of 21: Maps to System.Int16	Unsigned Short Alias = 2 bytes, treated as Unsigned 16 bit integer
			typeof ( Int32 ) ,			//  7 of 21: Maps to System.Int32	Signed Int = 4 bytes, treated as Signed 32 bit integer
			typeof ( UInt32 ) ,			//  8 of 21: Maps to System.Int32	Unsigned Int = 4 bytes, treated as Unsigned 32 bit integer
			typeof ( int ) ,			//  9 of 21: Maps to System.Int32	Signed Int Alias = 4 bytes, treated as Signed 32 bit integer
			typeof ( uint ) ,			// 10 of 21: Maps to System.Int32	Unsigned Int Alias = 4 bytes, treated as Unsigned 32 bit integer
			typeof ( Int64 ) ,			// 11 of 21: Maps to System.Int64	Signed Int = 8 bytes, treated as Signed 64 bit integer
			typeof ( UInt64 ) ,			// 12 of 21: Maps to System.Int64	Unsigned Int = 8 bytes, treated as Unsigned 64 bit integer
			typeof ( long ) ,			// 13 of 21: Maps to System.Int64	Signed Int Alias = 8 bytes, treated as Signed 64 bit integer
			typeof ( ulong ) ,			// 14 of 21: Maps to System.Int64	Unsigned Int Alias = 8 bytes, treated as Unsigned 64 bit integer
			typeof ( System.Byte ) ,	// 15 of 21: System primitive - 8 bit integer, undifferentiated with respect to sign
			typeof ( System.Int16 ) ,	// 16 of 21: System primitive - 16 bit signed integer
			typeof ( System.UInt16 ) ,	// 17 of 21: System primitive - 16 bit unsigned integer
			typeof ( System.Int32 ) ,	// 18 of 21: System primitive - 32 bit signed integer
			typeof ( System.UInt32 ) ,	// 19 of 21: System primitive - 32 bit unsigned integer
			typeof ( System.Int64 ) ,	// 20 of 21: System primitive - 64 bit signed integer
			typeof ( System.UInt64 )	// 21 of 21: System primitive - 64 bit unsigned integer
		}; // s_atypIntegerTypes

		static readonly object [ ] s_aobjIntegerMaxima =
		{
			System.Byte.MaxValue ,
			System.Int16.MaxValue ,
			System.UInt16.MaxValue ,
			System.Int32.MaxValue ,
			System.UInt32.MaxValue ,
			System.Int64.MaxValue ,
			System.UInt64.MaxValue 
		};	// s_aobjIntegerMaxima

		static string s_strProgramName = System.IO.Path.GetFileNameWithoutExtension ( System.Reflection.Assembly.GetExecutingAssembly ( ).Location );
		static DateTime s_dtmStartupTime = System.Diagnostics.Process.GetCurrentProcess ( ).StartTime.ToUniversalTime ( );

        static void Main ( string [ ] args )
        {
			Console.WriteLine (
				"BOJ {0}{2}{1}" ,
				System.Reflection.Assembly.GetExecutingAssembly ( ).FullName ,
				s_dtmStartupTime ,
				Environment.NewLine );

            Console.WriteLine ( "{1}Working Directory = {0}" , Environment.CurrentDirectory , Environment.NewLine );

            string strBitArray32Report = "BitArray32TestStand.CSV";

			Util.ShowKeyAssemblyProperties ( System.Reflection.Assembly.GetAssembly ( typeof ( BitArray32 ) ) );

            if ( args.Length > 0 )
                strBitArray32Report = args [ 0 ];

			EnumerateIntegerTypes ( );
            BitArray32TestStand.Exercise ( strBitArray32Report );
            TestSpecificBits ( );

            Console.WriteLine (
				"{0} EOJ{2}{1}{2}" ,
				s_strProgramName ,
				new TimeSpan (
					System.DateTime.UtcNow.Ticks
					- s_dtmStartupTime.Ticks ) ,
                Environment.NewLine );
            Environment.Exit ( 0 );
        }   // static void Main


		private static void EnumerateIntegerTypes ( )
		{
			Console.WriteLine (
				"{0}Enumerating GUIDs for all Integer types known to C# and the BCL:{0}" ,
				Environment.NewLine );
			int intOrdinal = Util.ARRAY_FIRST_ELEMENT;

			foreach ( Type typOfInteger in s_atypIntegerTypes )
			{
				Console.WriteLine (
					"    {0,2}: Type {1,-8} GUID = {2}" ,
					++intOrdinal ,
					typOfInteger.Name ,
					Util.ByteArrayToHexDigitString ( typOfInteger.GUID.ToByteArray ( ) ) );
			}	// foreach ( Type typOfInteger in s_atypIntegerTypes )

			Console.WriteLine (
				"{0}Enumerating maximum legal values for integral types known to the BCL:{0}" ,
				Environment.NewLine );
			intOrdinal = Util.ARRAY_FIRST_ELEMENT;

			//	----------------------------------------------------------------
			//	A generic List offers three advantages over an array.
			//
			//	1)	Items can be added without the need to keep track of a
			//		subscript.
			//
			//	2)	The loop that adds them can always get the one it just added
			//		because its subscript is known and easily computed.
			//
			//	3)	The generic List type implements an instance Sort method.
			//	----------------------------------------------------------------

			List<string> alstCompactGUID = new List<string> ( s_aobjIntegerMaxima.Length );

			foreach ( object objInegerMaxValue in s_aobjIntegerMaxima )
			{
				object intBiggestIntegerType = 0;
				Type typActualIntegrType = objInegerMaxValue.GetType ( );
				alstCompactGUID.Add ( Util.ByteArrayToHexDigitString ( typActualIntegrType.GUID.ToByteArray ( ) ) );

				if ( typActualIntegrType == typeof ( System.Byte ) )
				{
					System.Byte bytEightBitInteger = ( System.Byte ) objInegerMaxValue;
					intBiggestIntegerType = bytEightBitInteger;
				}	// TRUE (Byte is a special case.) block, if ( typActualIntegrType == typeof ( System.Byte ) )
				else
				{
					intBiggestIntegerType = objInegerMaxValue;
				}	// FALSE (All other integers should upcast successfully.

				Console.WriteLine (
					"    {0}: Type {1,-18} GUID = {2}, Maximum value = {3,20:N0}, digit count = {4,2}" ,
					new object [ ] {
						++intOrdinal ,																// Format Item 0 = Counter
						typActualIntegrType.FullName ,												// Format Item 1 = Full Type Name
						alstCompactGUID [ intOrdinal - Util.ARRAY_SUBSCRIPT_FROM_ORDINAL ] ,		// Format Item 2 = Compact GUID of type
						intBiggestIntegerType.ToString ( ) ,										// Format Item 3 = Maximum value, decimal
						intBiggestIntegerType.ToString ( ).Length } );								// Format Item 4 = Maximum value digit count
			}	// foreach ( object objInegerMaxValue in s_aobjIntegerMaxima )

			Console.WriteLine (
				"{0}That's it for the integers.{0}" ,
				Environment.NewLine );

			ListGUIDsInOrder (
				"Unsorted" ,
				alstCompactGUID );
			alstCompactGUID.Sort ( );
			ListGUIDsInOrder (
				"Sorted" ,
				alstCompactGUID );

#if BCLINTEGERTYPEINFO_PUBLIC
			Console.WriteLine ( 
				"Listing the contents of the BCLIntegerTypeInfo array, s_abclintegertypeinfo:{0}" ,
				Environment.NewLine );

			for ( intOrdinal = Util.ARRAY_FIRST_ELEMENT ;
				  intOrdinal < BitHelpers.s_abclintegertypeinfo.Length ;
				  intOrdinal++ )
			{
				Console.WriteLine (
					"    Element {0}: {1}" ,
					intOrdinal ,
					BitHelpers.s_abclintegertypeinfo [ intOrdinal ] );
			}	// for ( intOrdinal = Util.ARRAY_FIRST_ELEMENT ; intOrdinal < BitHelpers.s_abclintegertypeinfo.Length ; intOrdinal++ )

			Console.WriteLine (
				"{0}End of BCLIntegerTypeInfo array{0}" ,
				Environment.NewLine );

			Console.WriteLine (
				"Listing the BitHelpers.BCL_INTEGRAL_TYPEINFO constants:{0}" ,
				Environment.NewLine );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_INT16  = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_INT16  );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT32 = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT32 );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_BYTE   = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_BYTE   );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT64 = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT64 );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT16 = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT16 );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_INT32  = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_INT32  );
			Console.WriteLine ( "    BitHelpers.BCL_INTEGRAL_TYPEINFO_INT64  = {0}" , BitHelpers.BCL_INTEGRAL_TYPEINFO_INT64  );

			Console.WriteLine (
				"{0}End of BitHelpers.BCL_INTEGRAL_TYPEINFO constants{0}" ,
				Environment.NewLine );

			Console.WriteLine (
				"Use the BitHelpers.BCL_INTEGRAL_TYPEINFO constants to list selected elements of the s_abclintegertypeinfo array:{0}" ,
				Environment.NewLine );

			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT16  ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT16  ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT32 ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT32 ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_BYTE   ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_BYTE   ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT64 ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT64 ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT16 ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT16 ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT32  ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT32  ] );
			Console.WriteLine ( "    BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT64  ] = {0}" , BitHelpers.s_abclintegertypeinfo [ BitHelpers.BCL_INTEGRAL_TYPEINFO_INT64  ] );

			Console.WriteLine (
				"{0}End of s_abclintegertypeinfo listing via BitHelpers.BCL_INTEGRAL_TYPEINFO constants{0}" ,
				Environment.NewLine );

			Console.WriteLine (
				"Enumerate the sorted BCLIntegerTypeIndex array, s_abclintegertypeinfo array:{0}" ,
				Environment.NewLine );

			for ( intOrdinal = Util.ARRAY_FIRST_ELEMENT ;
				  intOrdinal < BitHelpers.s_abclintegertypeinfo.Length ;
				  intOrdinal++ )
			{
				Console.WriteLine (
					"    Element {0}: {1}" ,
					intOrdinal ,
					BitHelpers.s_abclintegertypeinfo [ intOrdinal ].ToString ( ) );
			}	// for ( intOrdinal = Util.ARRAY_FIRST_ELEMENT ; intOrdinal < BitHelpers.s_abclintegertypeinfo.Length ; intOrdinal++ )

			Console.WriteLine (
				"{0}End of s_abclintegertypeinfo listing{0}" ,
				Environment.NewLine );

			Console.WriteLine (
				"Thoroughly exercise the new type information query routine.{0}" ,
				Environment.NewLine );

			foreach ( Type typOfInteger in s_atypIntegerTypes )
			{
				Console.WriteLine (
					"    C# Type {0}: {1}" ,
					typOfInteger ,
					BitHelpers.InfoForType ( typOfInteger ) );
			}	// foreach ( Type typOfInteger in s_atypIntegerTypes )

			Console.WriteLine (
				"{0}End of enumeration of C# integer types{0}" ,
				Environment.NewLine );
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC

			Console.WriteLine (
				"{0}Enumerating character values of hexadecimal digits:{0}" ,
				Environment.NewLine );

			intOrdinal = Util.ARRAY_FIRST_ELEMENT;

			foreach ( char chrHexDigit in s_achrHexDigitChars )
			{
				Console.WriteLine (
					"    Decimal value {0,2} = {1} (numeric value = {2,3}, hex = 0x{2:x2})" ,
					++intOrdinal ,
					chrHexDigit ,
					( int ) chrHexDigit );

				if ( intOrdinal == 10 )
				{	// Skip a line after the ninth digit.
					Console.WriteLine ( );
				}	// if ( intOrdinal == 9 )
			}	// foreach ( char chrHexDigit in s_achrHexDigitChars )

			Console.WriteLine (
				"{0}That's it for the hexadecimal digits.{0}" ,
				Environment.NewLine );
		}	// private static void EnumerateIntegerTypes


		private static void ListGUIDsInOrder (
			string pstrSortState ,
			List<string> plstCompactGUID )
		{
			Console.WriteLine (
				"{0} GUIDs:{1}" ,
				pstrSortState ,
				Environment.NewLine );

			for ( int intOrdinal = Util.ARRAY_FIRST_ELEMENT ; intOrdinal < plstCompactGUID.Count ; intOrdinal++ )
			{
				Console.WriteLine (
					"    {0} = {1}" ,
					intOrdinal + Util.ARRAY_ORDINAL_FROM_SUBSCRIPT ,
					plstCompactGUID [ intOrdinal ] );
			}	// for ( int intOrdinal = Util.ARRAY_FIRST_ELEMENT ; intOrdinal < plstCompactGUID.Count ; intOrdinal++ )

			Console.WriteLine (
				"{1}End of {0} GUIDs{1}" ,
				pstrSortState ,
				Environment.NewLine );
		}	// private static void ListGUIDsInOrder


        private static void TestSpecificBits ( )
        {
            const int STACK = 4;                    // Bit 3
            const int STDERR = 32;                  // Bit 6
            const int METHOD = 1;                   // Bit 1
            const int SOURCE = 2;                   // Bit 2
            const int EVENTLOG = 8;                 // Bit 4
            const int STDOUT = 16;                  // Bit 5

            const int BIT_6 = 6;

            const string MSG_BEGIN = @"Begin TestSpecificBits{0}";
            const string MSG_DONE = @"{0}TestSpecificBits Done{0}";
            const string MSG_TPL_ANTE = @"{3}    Test {0,2} Ante = {1,2} - b32 = {2}";
            const string MSG_TPL_POST = @"            Post = {0,2} - b32 = {1}";
            const string MSG_TPL_BIT_NUMBER = @"    Test {0,2}: Bit Value = {1,3} ({1:x8}) - Bit Number = {2,2}";

            int rintTestNbr = 1;

            Console.WriteLine (
                MSG_BEGIN ,
                Environment.NewLine );

            UInt32 uintBitNumber = BitArray32.BitNumber ( STACK );
            Console.WriteLine (
                MSG_TPL_BIT_NUMBER ,
                rintTestNbr++ ,
                STACK ,
                uintBitNumber );

            uintBitNumber = BitArray32.BitNumber ( STDERR );
            Console.WriteLine (
                MSG_TPL_BIT_NUMBER ,
                rintTestNbr++ ,
                STDERR ,
                uintBitNumber );

            uintBitNumber = BitArray32.BitNumber ( EVENTLOG );
            Console.WriteLine (
                MSG_TPL_BIT_NUMBER ,
                rintTestNbr++ ,
                EVENTLOG ,
                uintBitNumber );

            uintBitNumber = BitArray32.BitNumber ( STDOUT );
            Console.WriteLine (
                MSG_TPL_BIT_NUMBER ,
                rintTestNbr++ ,
                STDOUT ,
                uintBitNumber );

            uintBitNumber = BitArray32.BitNumber ( METHOD );
            Console.WriteLine (
                MSG_TPL_BIT_NUMBER ,
                rintTestNbr++ ,
                METHOD ,
                uintBitNumber );

            byte byt8Bits = STACK | SOURCE;
            WizardWrx.BitMask32 b32 = new WizardWrx.BitMask32 ( byt8Bits );

            Console.WriteLine (
                MSG_TPL_ANTE ,
                new object [ ]
                {
                    rintTestNbr++ ,
                    byt8Bits ,
                    b32 ,
                    Environment.NewLine
                } );
            b32.BitOn ( BIT_6 );
            byt8Bits = ( byte ) b32;
            Console.WriteLine (
                MSG_TPL_POST ,
                byt8Bits ,
                b32 ,
                Environment.NewLine );

            Console.WriteLine (
                MSG_TPL_ANTE ,
                new object [ ]
                {
                    rintTestNbr++ ,
                    byt8Bits ,
                    b32 ,
                    Environment.NewLine
                } );
            b32.BitOff ( BIT_6 );
            byt8Bits = ( byte ) b32;
            Console.WriteLine (
                MSG_TPL_POST ,
                byt8Bits ,
                b32 ,
                Environment.NewLine );

            Console.WriteLine (
                MSG_DONE ,
                Environment.NewLine );
        }   // private static int TestSpecificBits
    }   // class Program
}   // partial namespace TestStand