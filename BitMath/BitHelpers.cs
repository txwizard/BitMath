/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         BitHelpers

    File Name:          BitHelpers.cs

    Synopsis            This static class supplies implementations of methods
						that behave essentially the same way, regardless of the
						number of bits being processed.

    Remarks:            This class arose to consolidate the implementations of a
						binary (bit for bit) formatter for logical bit masks of
						8, 16, 32, and 64 bits. In principle, they can support a
						bit mask of any length that is evenly divisible by 8.

    Author:             David A. Gray

	License:            Copyright (C) 2016, David A. Gray.
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

    Date Written:       Monday, 25 April 2016 - Thursday, 28 April 2016

    ---------------------------------------------------------------------------
    Revision History
    ---------------------------------------------------------------------------

    Date       Version  By  Synopsis
    ---------- -------- --- ----------------------------------------------------
	2016/04/29 4.0      DAG This class makes its debut.

	2016/05/02 4.2      DAG Implement low bit to high as an alternative display
                            order.

	2016/05/04 4.3      DAG Promote static method InfoForType to public, so that
						    user code can use it to identify integral types, and
                            give it a descriptive name, InfoForIntegralType. The
                            class must also be promoted, InfoForIntegralType is
							its only accessible member.
    ============================================================================
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace WizardWrx
{
	/// <summary>
	/// This static class defines utility methods to support formatting of
	/// integral data types as arrays of individual bits, along with embedded
	/// read-only data structures to support their work.
	/// </summary>
	public static class BitHelpers
	{
		#region Public Constants
		/// <summary>
		/// The MaximumDecimalDigits property of a BCLIntegerTypeInfo instance
		/// specifies a maximum decimal digits capacity of 3.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_MAX_DEC_DIGITS_03 = 3;
#else
		internal const int BCLI_MAX_DEC_DIGITS_03 = 3;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The MaximumDecimalDigits property of a BCLIntegerTypeInfo instance
		/// specifies a maximum decimal digits capacity of 5.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_MAX_DEC_DIGITS_05 = 5;
#else
		internal const int BCLI_MAX_DEC_DIGITS_05 = 5;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The MaximumDecimalDigits property of a BCLIntegerTypeInfo instance
		/// specifies a maximum decimal digits capacity of 10.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_MAX_DEC_DIGITS_10 = 10;
#else
		internal const int BCLI_MAX_DEC_DIGITS_10 = 10;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC

		/// <summary>
		/// The MaximumDecimalDigits property of a BCLIntegerTypeInfo instance
		/// specifies a maximum decimal digits capacity of 19.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_MAX_DEC_DIGITS_19 = 19;
#else
		internal const int BCLI_MAX_DEC_DIGITS_19 = 19;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The MaximumDecimalDigits property of a BCLIntegerTypeInfo instance
		/// specifies a maximum decimal digits capacity of 20.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_MAX_DEC_DIGITS_20 = 20;
#else
		internal const int BCLI_MAX_DEC_DIGITS_20 = 20;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The RequiredStorageBytes property of a BCLIntegerTypeInfo instance
		/// specifies a storage requirement of 2 bytes.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_BYTES_TO_STORE_1 = 1;
#else
		internal const int BCLI_BYTES_TO_STORE_1 = 1;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The RequiredStorageBytes property of a BCLIntegerTypeInfo instance
		/// specifies a storage requirement of 2 bytes.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_BYTES_TO_STORE_2 = 2;
#else
		internal const int BCLI_BYTES_TO_STORE_2 = 2;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The RequiredStorageBytes property of a BCLIntegerTypeInfo instance
		/// specifies a storage requirement of 4 bytes.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_BYTES_TO_STORE_4 = 4;
#else
		internal const int BCLI_BYTES_TO_STORE_4 = 4;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The RequiredStorageBytes property of a BCLIntegerTypeInfo instance
		/// specifies a storage requirement of 8 bytes.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_BYTES_TO_STORE_8 = 8;
#else
		internal const int BCLI_BYTES_TO_STORE_8 = 8;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The CapacityInBits property of a BCLIntegerTypeInfo instance
		/// specifies its capacity to hold a mask of up to 8 bits.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_CAPACITY_BITS_08 = 8;
#else
		internal const int BCLI_CAPACITY_BITS_08 = 8;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The CapacityInBits property of a BCLIntegerTypeInfo instance
		/// specifies its capacity to hold a mask of up to 16 bits.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_CAPACITY_BITS_16 = 16;
#else
		internal const int BCLI_CAPACITY_BITS_16 = 16;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The CapacityInBits property of a BCLIntegerTypeInfo instance
		/// specifies its capacity to hold a mask of up to 32 bits.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_CAPACITY_BITS_32 = 32;
#else
		internal const int BCLI_CAPACITY_BITS_32 = 32;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The CapacityInBits property of a BCLIntegerTypeInfo instance
		/// specifies its capacity to hold a mask of up to 64 bits.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCLI_CAPACITY_BITS_64 = 64;
#else
		internal const int BCLI_CAPACITY_BITS_64 = 64;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The IsSigned property of a BCLIntegerTypeInfo instance specifies
		/// that it is treated as signed (TRUE) when it is being treated as an
		/// integer.
		/// 
		/// When treated as a bit mask, the sign is ignored, and treated as just
		/// another bit, which happens to occupy its Most Significant Bit
		/// position.
		/// </summary>
		/// <remarks>
		/// Reserving one bit for the sign affects the upper and lower limits of
		/// numbers that can be stored in an integer, decreasing the absolute
		/// values of both by a factor of two. This is evident in the reported
		/// MaxValue and MinValue of an integer of a given size, which is the
		/// absolute value of one less than plus or minus two to the power of B,
		/// where B is the number of bits available to store its absolute value.
		/// </remarks>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const bool BCLI_IS_SIGNED = true;
#else
		internal const bool BCLI_IS_SIGNED = true;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// The IsSigned property of a BCLIntegerTypeInfo instance specifies
		/// that it is treated as unsigned (FALSE) when it is being treated as 
		/// an integer.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const bool BCLI_IS_UNSIGNED = false;
#else
		internal const bool BCLI_IS_UNSIGNED = false;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.Byte
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_BYTE = 2;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_BYTE = 2;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.Int16
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_INT16 = 0;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_INT16 = 0;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.Int32
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_INT32 = 5;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_INT32 = 5;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.Int64
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_INT64 = 6;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_INT64 = 6;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.UInt32
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_UINT32 = 1;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_UINT32 = 1;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC

		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.UInt64
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_UINT64 = 3;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_UINT64 = 3;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use this subscript to address the BCLIntegerTypeInfo array
		/// s_abclintegertypeinfo that stores information about the System.UInt16
		/// integral value type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BCL_INTEGRAL_TYPEINFO_UINT16 = 4;
#else
		internal const int BCL_INTEGRAL_TYPEINFO_UINT16 = 4;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// This static read-only array of BCLIntegerTypeInfo objects is used
		/// internally by the static methods on the BitHelpers class, of which
		/// it is a static property, drives the custom ToString method that
		/// renders integers as arrays of bits.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public static readonly BCLIntegerTypeInfo [ ] s_abclintegertypeinfo =
#else
		internal static readonly BCLIntegerTypeInfo [ ] s_abclintegertypeinfo =
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{	//                       BCLType                    MaximumDecimalDigits     RequiredStorageBytes    CapacityInBits          IsSigned
			new BCLIntegerTypeInfo ( typeof ( System.Int16  ) , BCLI_MAX_DEC_DIGITS_05 , BCLI_BYTES_TO_STORE_2 , BCLI_CAPACITY_BITS_16 , BCLI_IS_SIGNED   ) ,
			new BCLIntegerTypeInfo ( typeof ( System.UInt32 ) , BCLI_MAX_DEC_DIGITS_10 , BCLI_BYTES_TO_STORE_4 , BCLI_CAPACITY_BITS_32 , BCLI_IS_UNSIGNED ) ,
			new BCLIntegerTypeInfo ( typeof ( System.Byte   ) , BCLI_MAX_DEC_DIGITS_03 , BCLI_BYTES_TO_STORE_1 , BCLI_CAPACITY_BITS_08 , BCLI_IS_UNSIGNED ) ,
			new BCLIntegerTypeInfo ( typeof ( System.UInt64 ) , BCLI_MAX_DEC_DIGITS_20 , BCLI_BYTES_TO_STORE_8 , BCLI_CAPACITY_BITS_64 , BCLI_IS_UNSIGNED ) ,
			new BCLIntegerTypeInfo ( typeof ( System.UInt16 ) , BCLI_MAX_DEC_DIGITS_05 , BCLI_BYTES_TO_STORE_2 , BCLI_CAPACITY_BITS_16 , BCLI_IS_UNSIGNED ) ,
			new BCLIntegerTypeInfo ( typeof ( System.Int32  ) , BCLI_MAX_DEC_DIGITS_10 , BCLI_BYTES_TO_STORE_4 , BCLI_CAPACITY_BITS_32 , BCLI_IS_SIGNED   ) ,
			new BCLIntegerTypeInfo ( typeof ( System.Int64  ) , BCLI_MAX_DEC_DIGITS_19 , BCLI_BYTES_TO_STORE_8 , BCLI_CAPACITY_BITS_64 , BCLI_IS_SIGNED   )
		}; // s_abclintegertypeinfo


		/// <summary>
		/// A binary search of this array yields the required subscript into the
		/// s_abytAsBits array.
		/// </summary>
		internal static readonly char [ ] s_achrHexDigit =
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
		};	// s_achrHexDigit


		/// <summary>
		/// This table translates hexadecimal "digits" into their corresponding
		/// bits.
		/// </summary>
		internal static readonly string [ ] s_abytAsBits =
		{
			"0000" ,				// Decimal  0 = hex 0x0
			"0001" ,				// Decimal  1 = hex 0x1
			"0010" ,				// Decimal  2 = hex 0x2
			"0011" ,				// Decimal  3 = hex 0x3
			"0100" ,				// Decimal  4 = hex 0x4
			"0101" ,				// Decimal  5 = hex 0x5
			"0110" ,				// Decimal  6 = hex 0x6
			"0111" ,				// Decimal  7 = hex 0x7
			"1000" ,				// Decimal  8 = hex 0x8
			"1001" ,				// Decimal  9 = hex 0x9
			"1010" ,				// Decimal 10 = hex 0xa
			"1011" ,				// Decimal 11 = hex 0xb
			"1100" ,				// Decimal 12 = hex 0xc
			"1101" ,				// Decimal 13 = hex 0xd
			"1110" ,				// Decimal 14 = hex 0xe
			"1111"					// Decimal 15 = hex 0xf
		};	// s_abytAsBits


		/// <summary>
		/// This constant defines the number of bits per byte. So far as I know,
		/// this applies to computers of any type; a byte is a byte, period.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const int BITS_PER_BYTE = 8;
#else
		internal const int BITS_PER_BYTE = 8;
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// In keeping with standards implemented elsewhere in the Land of .NET,
		/// the default format string is upper case G.
		/// </summary>
		/// <remarks>
		/// None of the supported custom format specifiers overlaps any of the 
		/// standard format specifiers that apply to numeric, dates, times, or
		/// time spans.
		/// </remarks>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const char FORMAT_GENERAL = 'G';
#else
		internal const char FORMAT_GENERAL = 'G';
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC


		/// <summary>
		/// Use upper case H, optionally followed by a numeric substring to 
		/// specify the number of bits to separate into groups by a space, to
		/// have the bits displayed high bit to low. This is the conventional
		/// display order.
		/// </summary>
		/// <remarks>
		/// None of the supported custom format specifiers overlaps any of the 
		/// standard format specifiers that apply to numeric, dates, times, or
		/// time spans.
		/// </remarks>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const char FORMAT_BITS = 'H';
#else
		internal const char FORMAT_BITS_HIGH_TO_LOW = 'H';
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
	
		/// <summary>
		/// Use upper case L, optionally followed by a numeric substring to 
		/// specify the number of bits to separate into groups by a space, to
		/// have the bits displayed low bit to high. This format caters for
		/// readers who prefer their bit positions to count from left to right,
		/// or are unfamiliar with the conventional method of visualizing a bit
		/// mask.
		/// </summary>
		/// <remarks>
		/// None of the supported custom format specifiers overlaps any of the 
		/// standard format specifiers that apply to numeric, dates, times, or
		/// time spans.
		/// </remarks>
#if BCLINTEGERTYPEINFO_PUBLIC
		public const char FORMAT_BITS = 'H';
#else
		internal const char FORMAT_BITS_LOW_TO_HIGH = 'L';
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC

		/// <summary>
		/// This symbolic constant defines the number of hexadecimal digits
		/// (characters) required to store the hexadecimal (base 16) value of
		/// one byte.
		/// </summary>
		internal const int HEX_DIGITS_PER_BYTE = 2;
		#endregion	// Public Constants


		#region Private Constants
		private const string ARGNAME_FORMAT_STRING = @"pstrFormatString";
		/// <summary>
		/// In keeping with my coding standard of using symbolic constants to
		/// specify the meaning of any magic number, zero is mapped to one such
		/// constant, to be interpreted as the array base (the subscript of its
		/// first element).
		/// </summary>
		private const int ARRAY_FIRST_ELEMENT = 0;

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as a string of hexadecimal digits.
		/// </summary>
		/// <remarks>
		/// Since the array uses lower case characters, so must the intermediate
		/// format.
		/// </remarks>
		private const string HEXADECIMAL = @"x";

		/// <summary>
		/// This symbolic constant specifies the minimum length of a format
		/// string.
		/// </summary>
		private const int LEN_MINIMUM = 1;

		/// <summary>
		/// The position of the prefix is specified as its array subscript,
		/// for use in separating the numeric suffix.
		/// </summary>
		private const int POS_PREFIX = ARRAY_FIRST_ELEMENT;

		/// <summary>
		/// In like manner, the position of the numeric modifier is specified in
		/// terms of its array subscript.
		/// </summary>
		private const int POS_MODIFIER = 1;

		/// <summary>
		/// Use this to simplify and clarify when you pass a double quote mark
		/// as an argument.
		/// </summary>
		private const char QUOTE_CHAR = '"';

		/// <summary>
		/// Since there is no practical way to distinguish a space character
		/// from a number of other characters, and strings thereof, that may
		/// appear to be blank, the space character is a stalwart symbolic
		/// constant.
		/// </summary>
		private const char SPACE_CHAR = ' ';
		#endregion	// Private Constants


		#region Internal, Semi-public Methods, ALL Static
		/// <summary>
		/// Format a bit mask; this method is intended to implement the ToString
		/// overload on the BitArray classes.
		/// </summary>
		/// <typeparam name="T">
		/// Though not enforced by the compiler, the input must be an integral
		/// type, though it is left to this method to enforce it at run time.
		/// </typeparam>
		/// <param name="pgenBitMask">
		/// Specify the bit mask to format, which must be an integral type.
		/// System.Byte is considered an integral type that stores 8 bits.
		/// </param>
		/// <param name="pstrFormatString">
		/// Specify the format string, which must be either "G" by itself, "H",
		/// or "L" followed by a positive integer less than the number of bits
		/// represented by pgenBitMask.
		/// </param>
		/// <returns>
		/// if the function succeeds, the return value is a string containing a
		/// 1 or 0 for each bit in pgenBitMask.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// An ArgumentOutOfRangeException exception is thrown when the bits per
		/// group suffix is less than zero.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// An ArgumentException exception is thrown when the bits per group
		/// suffix is non-numeric.
		/// </exception>
		internal static string FormatBitMask<T> (
			T pgenBitMask ,
			string pstrFormatString )
		{
			const string FORMAT_ITEM_PREFIX = "{0:";
			const char FORMAT_ITEM_SUFFIX = '}';

			FormattingParameters fpFormattingParams = ParseFormatString (
				pstrFormatString );
			BCLIntegerTypeInfo bclTypeInfo = InfoForIntegralType (
				pgenBitMask.GetType ( ) );
			string strGeneralFormatString = string.Concat (
				HEXADECIMAL ,
				( bclTypeInfo.RequiredStorageBytes * HEX_DIGITS_PER_BYTE ) );
			string strWorkFormat = string.Format (
				string.Concat (
					FORMAT_ITEM_PREFIX ,
					strGeneralFormatString ,
					FORMAT_ITEM_SUFFIX ) ,
				pgenBitMask );

			//	----------------------------------------------------------------
			//	For the General case, formatting is done, but for the Bits case,
			//	we're just getting underway.
			//	----------------------------------------------------------------

			switch ( fpFormattingParams.FormatPrefix )
			{
				case FormattingParameters.FormatCode.General:
					return strWorkFormat;
				case FormattingParameters.FormatCode.BitsHighToLow:
				case FormattingParameters.FormatCode.BitsLowToHigh:
					StringBuilder rsbOneCharPerBit = new StringBuilder ( bclTypeInfo.CapacityInBits );

					foreach ( char chr4Bits in strWorkFormat )
					{	// Since it happens after the fact, the decision to append a space must precede appending more bits.
						fpFormattingParams.AddPaddingAsSpecified (
							rsbOneCharPerBit );
						rsbOneCharPerBit.Append (
							s_abytAsBits [ Array.BinarySearch<char> (
								s_achrHexDigit ,
								chr4Bits ) ] );
					}	// foreach ( char chr4Bits in strWorkFormat )

					//	--------------------------------------------------------
					//	Since there are only two possible values at this point,
					//	the simplest implementation of the next decision is an
					//	old fashioned IF statement.
					//	--------------------------------------------------------

					if ( fpFormattingParams.FormatPrefix == FormattingParameters.FormatCode.BitsHighToLow )
					{
						return rsbOneCharPerBit.ToString ( );
					}	// TRUE (Use the conventional visualization.) block, if ( fpFormattingParams.FormatPrefix == FormattingParameters.FormatCode.BitsHighToLow )
					else
					{
						return StrReverse ( rsbOneCharPerBit.ToString ( ) );
					}	// FALSE (Use the alternate visualization.) block, if ( fpFormattingParams.FormatPrefix == FormattingParameters.FormatCode.BitsHighToLow )
				default:
					string strMsg = string.Format (
						Properties.Resources.ERRMSG_INVALID_FORMAT_PREFIX_ENUM ,
						( int ) fpFormattingParams.FormatPrefix );
					System.Diagnostics.Debug.Fail (
						strMsg );
					return strMsg;
			}	// switch ( fpFormattingParams.FormatPrefix )
		}	// FormatBitMask


		/// <summary>
		/// Parse the format string, returning the outcome through a new
		/// FormattingParameters instance.
		/// </summary>
		/// <param name="pstrFormatString">
		/// Specify the formatting string to parse.
		/// </param>
		/// <returns>
		/// If the function succeeds, it returns a fully populated
		/// FormattingParameters object.
		/// </returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// An ArgumentOutOfRangeException exception is thrown when the bits per
		/// group suffix is less than zero.
		/// </exception>
		/// <exception cref="ArgumentException">
		/// An ArgumentException exception is thrown when the bits per group
		/// suffix is non-numeric.
		/// </exception>
		internal static FormattingParameters ParseFormatString ( string pstrFormatString )
		{
			FormattingParameters.FormatCode enmDesiredFormat = FormattingParameters.FormatCode.General;
			int intBitsPerGroup = FormattingParameters.SUPPRESS_GROUPING;

			if ( !string.IsNullOrEmpty ( pstrFormatString ) )
			{
				switch ( pstrFormatString [ POS_PREFIX ] )
				{
					case FORMAT_GENERAL:
						enmDesiredFormat = FormattingParameters.FormatCode.General;
						break;
					case FORMAT_BITS_HIGH_TO_LOW:
						enmDesiredFormat = FormattingParameters.FormatCode.BitsHighToLow;
						break;
					case FORMAT_BITS_LOW_TO_HIGH:
						enmDesiredFormat = FormattingParameters.FormatCode.BitsLowToHigh;
						break;
				}	// switch ( pstrFormatString [ POS_PREFIX ] )

				if ( pstrFormatString.Length > LEN_MINIMUM )
				{
					string strBitsPerGroup = pstrFormatString.Substring ( POS_MODIFIER );

					if ( int.TryParse ( strBitsPerGroup , out intBitsPerGroup ) )
					{
						if ( intBitsPerGroup < FormattingParameters.SUPPRESS_GROUPING )
						{
							throw new ArgumentOutOfRangeException (
								ARGNAME_FORMAT_STRING ,
								pstrFormatString ,
								Properties.Resources.ERRMSG_BIT_GROUPSIZE_OUT_OF_RANGE );
						}	// if ( intBitsPerGroup < FormattingParameters.SUPPRESS_GROUPING )
					}	// TRUE (anticipated outcome) block, if ( int.TryParse ( strBitsPerGroup , out intBitsPerGroup ) )
					else
					{
						throw new ArgumentException (
							string.Format (
								Properties.Resources.ERRMSG_BIT_GROUPSIZE_MUST_BE_NUMERIC ,
								pstrFormatString ,
								QUOTE_CHAR ) ,
							ARGNAME_FORMAT_STRING );
					}	// FALSE (error case) block, if ( int.TryParse ( strBitsPerGroup , out intBitsPerGroup ) )
				}	// if ( pstrFormatString.Length > LEN_MINIMUM )
			}	// f ( !string.IsNullOrEmpty ( pstrFormatString ) )

			return new FormattingParameters (
				enmDesiredFormat ,
				intBitsPerGroup );
		}	// ParseFormatString


		/// <summary>
		/// Get the BCLIntegerTypeInfo for a specified type.
		/// </summary>
		/// <param name="ptypForThisType">
		/// Specify the Type for which BCLIntegerTypeInfo is required. The
		/// TypeHandle is extracted and used as the index.
		/// </param>
		/// <returns>
		/// If the function succeeds, the return value is the desired
		/// BCLIntegerTypeInfo object. Otherwise, the return value is null.
		/// </returns>
		public static BCLIntegerTypeInfo InfoForIntegralType ( Type ptypForThisType )
		{
			Int64 intHandle = ptypForThisType.TypeHandle.Value.ToInt64 ( );
			int intNTypes = s_abclintegertypeinfo.Length;

			for ( int intCurrItem = ARRAY_FIRST_ELEMENT ; intCurrItem < intNTypes ; intCurrItem++ )
				if ( s_abclintegertypeinfo [ intCurrItem ].BCLType.TypeHandle.Value.ToInt64 ( ) == intHandle )
					return s_abclintegertypeinfo [ intCurrItem ];

			return null;
		}	// InfoForIntegralType


		/// <summary>
		/// Return a new string with the characters reversed.
		/// </summary>
		/// <param name="pstrInput">
		/// Specify the string to be reversed.
		/// </param>
		/// <returns>
		/// The return value is a copy of the string with its characters
		/// reversed.
		/// </returns>
		/// <example>
		/// C#:
		/// 
		///		string strNormal = "ABC" ;
		///		string strReversed = StrReverse(strNormal);
		///		Console.WriteLine("    Input string  = {0}",strNormal);
		///		Console.WriteLine("    Output string = {0}",strReversed);
		///		
		/// VB:
		/// 
		///		Dim strNormal As String = "ABC"
		///		Dim strReversed As String = StrReverse(strNormal)
		///		Console.WriteLine("    Input string  = {0}",strNormal)
		///		Console.WriteLine("    Output string = {0}",strReversed)
		///		
		/// Output:
		/// 
		///	    Input string  = ABC
		///	    Output string = CBA
		/// </example>
		private static string StrReverse ( string pstrInput )
		{
			StringBuilder rsbReversed = new StringBuilder ( pstrInput.Length );

			for ( int intInputPosition = pstrInput.Length - 1 ; intInputPosition > -1 ; intInputPosition-- )
				rsbReversed.Append ( pstrInput [ intInputPosition ] );

			return rsbReversed.ToString ( );
		}	// StrReverse
		#endregion	// Internal, Semi-public Methods, ALL Static
	}	// static class BitHelpers
}	// partial namespace WizardWrx