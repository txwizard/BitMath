/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         BitMaskAndRuler

    File Name:          BitMaskAndRuler.cs

    Synopsis            Instances of this class organize complete sets of bit
						masks and the appropriate rulers.

    Remarks:            This class simplifies creation of matched sets of bit
						masks and guide rulers for display on reports.

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
	2016/05/03 4.2      DAG This class makes its debut.
    ============================================================================
*/


using System;
using System.Collections.Generic;
using System.Text;

namespace WizardWrx
{
	/// <summary>
	/// Instances of this class keep a bit mask and a correctly oriented ruler
	/// together.
	/// </summary>
	public class BitMaskAndRuler
	{
		#region Public Symbolic Constants
		/// <summary>
		/// This constant represents the element in the string array returned by
		/// the DisplayBitMask method that points to the tens row of the ruler.
		/// </summary>
		public const int DISPLAY_TENS_ROW = 0;


		/// <summary>
		/// This constant represents the element in the string array returned by
		/// the DisplayBitMask method that points to the units row of the ruler,
		/// which is shown below the tens row of the ruler.
		/// </summary>
		public const int DISPLAY_UNITS_ROW = 1;


		/// <summary>
		/// This constant represents the element in the string array returned by
		/// the DisplayBitMask method that points to the bit array row, which is
		/// shown below the units row of the ruler.
		/// </summary>
		public const int DISPLAY_BITS_ROW = 2;


		/// <summary>
		/// This constant represents the number of elements in the string array
		/// returned by the 
		/// </summary>
		public const int DISPLAY_ROW_COUNT = 3;
		#endregion	// Public Symbolic Constants


		#region Constructors
		/// <summary>
		/// To enforce creation of fully initialized types, the default
		/// constructor is hidden.
		/// </summary>
		private BitMaskAndRuler ( )
		{
		}	// BitMaskAndRuler constructor (1 of 8)

		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="pbytBitMask">
		/// Specify the Byte (Unsigned 8 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			System.Byte pbytBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance ( 
				BitHelpers.BCL_INTEGRAL_TYPEINFO_BYTE ,
				pbytBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (2 of 8)


		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="puintBitMask">
		/// Specify the UInt16 (Unsigned 16 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			UInt16 puintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT16 ,
				puintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (3 of 8)


		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="puintBitMask">
		/// Specify the UInt32 (Unsigned 32 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			UInt32 puintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT32 ,
				puintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (4 of 8)


		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="puintBitMask">
		/// Specify the UInt64 (Unsigned 64 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			UInt64 puintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_UINT64 ,
				puintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (5 of 8)

		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="pintBitMask">
		/// Specify the Int16 (Signed 16 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			Int16 pintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_INT16 ,
				pintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (3 of 8)


		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="pintBitMask">
		/// Specify the Int32 (Signed 32 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			Int32 pintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_INT32 ,
				pintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (4 of 8)


		/// <summary>
		/// Construct a bit mask from any integral type. In this context, a Byte
		/// is treated as an unsigned integer of 8 bits.
		/// </summary>
		/// <param name="pintBitMask">
		/// Specify the Int64 (Signed 64 bit integer) to treat as a bit mask.
		/// </param>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitArray32.BitDisplayOrder"/>
		public BitMaskAndRuler (
			Int64 pintBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			InitializeInstance (
				BitHelpers.BCL_INTEGRAL_TYPEINFO_INT64 ,
				pintBitMask ,
				penmBitDisplayOrder );
		}	// BitMaskAndRuler constructor (5 of 8)
		#endregion	// Constructors


		#region Properties, All Read Only
		/// <summary>
		/// This property gets the string that contains the array of bits.
		/// </summary>
		public string ArrayOfBits
		{
			get
			{
				return _strArrayOfBits;
			}	// public string ArrayOfBits property getter
		}	// public string ArrayOfBits property


		/// <summary>
		/// This property gets the decimal representation of the integral bit
		/// mask.
		/// </summary>
		public string DecimalRepresentation
		{
			get
			{
				return _strDecimalRepresentation;
			}	// public string DecimalReDisplayBitMask method getter
		}	// public string DecimalReDisplayBitMask method


		/// <summary>
		/// This property gets the hexadecimal representation of the integral
		/// bit mask.
		/// </summary>
		public string HexadecimalRepresentation
		{
			get
			{
				return _strHexadecimalRepresentation;
			}	// public string HexadecimalReDisplayBitMask method getter
		}	// public string HexadecimalReDisplayBitMask method


		/// <summary>
		/// This property gets the Tens row of the ruler.
		/// </summary>
		public string Tens
		{
			get
			{
				return _strTens;
			}	// public string Tens property getter
		}	// public string Tens property


		/// <summary>
		/// This property gets the Units row of the ruler.
		/// </summary>
		public string Units
		{
			get
			{
				return _strUnits;
			}	// public string Units property getter
		}	// public string Units property
		#endregion	// Properties, All Read Only


		#region Public Instance Methods
		/// <summary>
		/// This method returns the three elements that are intended to be
		/// displayed as a unit.
		/// </summary>
		/// <returns>
		/// The return value is an array of strings, which always contains
		/// exactly three elements containing the tens row of the ruler, the
		/// units row of the ruler, and the bit array, in that order.
		/// </returns>
		/// <seealso cref="DisplayBitMaskFromMultilineString()"/>
		/// <seealso cref="DisplayBitMaskFromMultilineString(int)"/>
		public string [ ] DisplayBitMaskFromArray ( )
		{
			string [ ] rastrPresentation = new string [ DISPLAY_ROW_COUNT ];

			rastrPresentation [ DISPLAY_TENS_ROW ] = _strTens;
			rastrPresentation [ DISPLAY_UNITS_ROW ] = _strUnits;
			rastrPresentation [ DISPLAY_BITS_ROW ] = _strArrayOfBits;

			return rastrPresentation;
		}	// public string DisplayBitMaskFromArray


		/// <summary>
		/// This method returns the three elements that are intended to be
		/// displayed as a unit as a multi-line string.
		/// </summary>
		/// <returns>
		/// Unlike companion method DisplayBitMaskFromArray, this method returns
		/// one long string, WITH each line EXCEPT the last one terminated with
		/// a newline.
		/// </returns>
		/// <seealso cref="DisplayBitMaskFromArray"/>
		public string DisplayBitMaskFromMultilineString ( )
		{
			const string MULTILINE_TEMPLATE = @"{0}{3}{1}{3}{2}";

			return string.Format (
				MULTILINE_TEMPLATE , new string [ ]
				{
					_strTens ,
					_strUnits ,
					_strArrayOfBits ,
					Environment.NewLine
				} );
		}	// public string DisplayBitMaskFromMultilineString (1 OF 2)


		/// <summary>
		/// This method returns the three elements that are intended to be
		/// displayed as a unit as a multi-line string.
		/// </summary>
		/// <param name="pintNLeadingSpaces">
		/// Specify the number of leading spaces to insert in front of each of
		/// the three lines.
		/// </param>
		/// <returns>
		/// Unlike companion method DisplayBitMaskFromArray, this method returns
		/// one long string, WITH each line EXCEPT the last one terminated with
		/// a newline.
		/// </returns>
		/// <seealso cref="DisplayBitMaskFromArray"/>
		public string DisplayBitMaskFromMultilineString ( int pintNLeadingSpaces )
		{
			const string MULTILINE_TEMPLATE = @"{3}{0}{4}{3}{1}{4}{3}{2}";
			const char SPACE = ' ';

			return string.Format (
				MULTILINE_TEMPLATE ,
				new string [ ]
				{
					_strTens ,
					_strUnits ,
					_strArrayOfBits ,
					new string (
						SPACE ,
						pintNLeadingSpaces ) ,
					Environment.NewLine
				} );
		}	// public string DisplayBitMaskFromMultilineString (2 OF 2)
		#endregion	// Public Instance Methods


		#region Private Instance Methods
		private void InitializeInstance ( 
			int pintTypeInfoIndex ,
			object pbytBitMask ,
			BitArray32.BitDisplayOrder penmBitDisplayOrder )
		{
			_bclTypeInfo = BitHelpers.s_abclintegertypeinfo [ pintTypeInfoIndex ];

			_objUnderlyingInteger = pbytBitMask;
			_enmBitDisplayOrder = penmBitDisplayOrder;

			_strDecimalRepresentation = string.Format (
				FORMAT_FIRST_ITEM_AS_DECIMAL ,
				_objUnderlyingInteger );
			_strHexadecimalRepresentation = BitArray32.FormatIntegerAsHex ( _objUnderlyingInteger );

			_strTens = BitArray32.GetRulerTens (
				( BitArray32.BitCount ) _bclTypeInfo.CapacityInBits ,
				_enmBitDisplayOrder );
			_strUnits = BitArray32.GetRulerUnits (
				( BitArray32.BitCount ) _bclTypeInfo.CapacityInBits ,
				_enmBitDisplayOrder );
			_strArrayOfBits = string.Format (
				new WizardWrx.BitMaskFormat ( ) ,
				_enmBitDisplayOrder == BitArray32.BitDisplayOrder.LowBitToHighBit
					? FORMAT_FIRST_ITEM_AS_REVERSED_BIT_ARRAY :
					FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
				_objUnderlyingInteger );
		}	// private void InitializeInstance
		#endregion	// Private Instance Methods


		#region Private Instance Storage
		private object _objUnderlyingInteger;
		private BitArray32.BitDisplayOrder _enmBitDisplayOrder;

		private BCLIntegerTypeInfo _bclTypeInfo;

		private string _strArrayOfBits;
		private string _strDecimalRepresentation;
		private string _strHexadecimalRepresentation;

		private string _strTens;
		private string _strUnits;
		#endregion	// Private Instance Storage


		#region Private Symbolic Constants
		const string FORMAT_FIRST_ITEM_AS_DECIMAL = @"{0:N0}";
		const string FORMAT_FIRST_ITEM_AS_BIT_ARRAY = "{0:H}";
		const string FORMAT_FIRST_ITEM_AS_REVERSED_BIT_ARRAY = "{0:L}";
		const string FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY = "{0:H4}";
		#endregion	// Private Symbolic Constants
	}	// public class BitMaskAndRuler
}	// partial namespace WizardWrx