/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         FormattingParameters

    File Name:          FormattingParameters.cs

    Synopsis            Internal static method ParseFormatString returns an
						instance of this class, which informs its internal
						caller through a set of read only properties of the
						external caller's formatting instructions.

    Remarks:            While this could be implemented as a structure, I prefer
						the tighter control that I get from implementing it as a
						class.

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
    ============================================================================
*/

using System;
using System.Text;


namespace WizardWrx
{
	/// <summary>
	/// Static method ParseFormatString on class BitHelpers reports its findings
	/// by constructing and returning an instance of this class.
	/// </summary>
	internal class FormattingParameters
	{
		internal const int BITS_PER_NIBBLE = 4;
		const int EVENLY_DIVISIBLE = 0;

		private FormatCode _enmFormatCode = FormatCode.General;
		private int _intBitsPerGroup = SUPPRESS_GROUPING;
		private int _intBitsDisplayed = SUPPRESS_GROUPING;


		internal const string ARGNAME_BITS_PER_GROUP = "pintBitsPerGroup";
		/// <summary>
		/// Setting the BitsPerGroup property to this value suppresses grouping.
		/// </summary>
		internal const int SUPPRESS_GROUPING = 0;


		/// <summary>
		/// A read only property maps the format code character onto this
		/// enumeration.
		/// </summary>
		internal enum FormatCode
		{
			/// <summary>
			/// The format code prefix is G, General, which permits no modifiers.
			/// </summary>
			General ,

			/// <summary>
			/// The format code prefix is H, Bits, which permits an integral
			/// modifier, causing bits to be displayed from highest to lowest.
			///
			/// This is the conventional display order for visualization of bit
			/// masks.
			/// </summary>
			BitsHighToLow ,

			/// <summary>
			/// The format code prefix is L, Bits, which permits an integral
			/// modifier, causing bits to be displayed from lowest to highest.
			/// 
			/// This is an alternative format for use by people who are either
			/// uncomfortable with the conventional visualization of bit masks
			/// or are unfamiliar with it.
			/// </summary>
			BitsLowToHigh
		}	// FormatCode


		/// <summary>
		/// The default constructor is marked private, to ensure that only fully
		/// initialized instances can be constructed.
		/// </summary>
		private FormattingParameters ( )
		{
		}	// FormattingParameters constructor 1 of 2 is marked private.


		/// <summary>
		/// ParseFormatString renders its report by calling this constructor.
		/// </summary>
		/// <param name="penmFormatCode">
		/// A member of the FormatCode enumeration specifies the format prefix
		/// code.
		/// </param>
		/// <param name="pintBitsPerGroup">
		/// An optional positive integer specifies how many bits to include in
		/// each group. If this value is zero, there is no grouping.
		/// </param>
		internal FormattingParameters (
			FormatCode penmFormatCode ,
			int pintBitsPerGroup )
		{
			_enmFormatCode = penmFormatCode;

			if ( pintBitsPerGroup >= SUPPRESS_GROUPING )
				if ( pintBitsPerGroup % BITS_PER_NIBBLE == EVENLY_DIVISIBLE )
					_intBitsPerGroup = pintBitsPerGroup;
				else
					throw new ArgumentException ( 
						string.Format (
							Properties.Resources.ERRMSG_INVALID_BIT_GROUPSIZE ,
							BITS_PER_NIBBLE ,
							pintBitsPerGroup ) );
			else
				throw new ArgumentOutOfRangeException (
					ARGNAME_BITS_PER_GROUP ,
					pintBitsPerGroup ,
					Properties.Resources.ERRMSG_BIT_GROUPS_LESS_THAN_ZERO );
		}	// FormattingParameters constructor 2 of 2 creates the report in one call.


		/// <summary>
		/// This property reports the selected FormatCode enumeration member
		/// that maps to the specified format prefix character.
		/// </summary>
		internal FormatCode FormatPrefix
		{
			get
			{
				return _enmFormatCode;
			}	// FormatPrefix property getter
		}	// internal read-only FormatPrefix property


		/// <summary>
		/// This property reports the bits per group integer, which may be zero.
		/// </summary>
		internal int BitsPerGroup
		{
			get
			{
				return _intBitsPerGroup;
			}	// BitsPerGroup property getter
		}	// internal read-only BitsPerGroup property


		/// <summary>
		/// Append padding if the specification calls for it and there are
		/// enough characters in the buffer.
		/// </summary>
		/// <param name="psbWork">
		/// Padding, if any, is appended to the StringBuilder to which the
		/// calling static FormatBitMask method on the BitHelpers class is
		/// building the bit array.
		/// </param>
		internal void AddPaddingAsSpecified ( StringBuilder psbWork )
		{
			const char SPACE = ' ';

			//	----------------------------------------------------------------
			//	This method does nothing unless the BitsPerGroup property is
			//	greater than zero. If so, internal counter _intBitsDisplayed is
			//	incremented, and, if its value is greater than zero and its 
			//	value modulus BitsPerGroup is equal to zero, a space is appended
			//	to the supplied StringBuilder.
			//	----------------------------------------------------------------

			if ( _intBitsPerGroup > SUPPRESS_GROUPING )
			{	// Caller wants it formatted.
				if ( _intBitsDisplayed > EVENLY_DIVISIBLE && _intBitsDisplayed % _intBitsPerGroup == EVENLY_DIVISIBLE )
				{	// Append a space.
					psbWork.Append ( SPACE );
				}	// if ( _intBitsDisplayed > EVENLY_DIVISIBLE && _intBitsDisplayed % _intBitsPerGroup == EVENLY_DIVISIBLE )

				_intBitsDisplayed += BITS_PER_NIBBLE;
			}	// if ( _intBitsPerGroup > SUPPRESS_GROUPING )
		}	// AddPaddingAsSpecified
	}	// internal class FormattingParameters
}	// partial namespace WizardWrx