/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         BitArray32

    File Name:          BitArray32.cs

    Synopsis            Provide a simple, robust mechanism for manipulating and
                        querying bit masks.

    Remarks:            1)  This class encapsulates methods that let you store a
                            bit mask, and use simple methods to set, clear, and
                            test bits.

                        2)  This class presently has two constructors, one that
                            creates a mask with all bits off, and another that
                            takes an existing bit mask, which must be cast to an
                            unsigned int (Uint32). To use another object of this
                            class as input to this overloaded constructor, you
                            need only cast it to Uint32, since the class defines
                            an implicit operator Uint32.

    References:         1)  "How to Turn off Some Bits While Ignoring Others Using Only Bitwise Operators"
                            http://stackoverflow.com/questions/8965521/how-to-turn-off-some-bits-while-ignoring-others-using-only-bitwise-operators

                        2)  "XOR -- from Wolfram MathWorld"
                            http://mathworld.wolfram.com/XOR.html

    Author:             David A. Gray.

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

    Date Written:       Sunday, 30 November 2009
                        Thursday, 22 May 2014 - Finally got TurnBitOff right!

    ---------------------------------------------------------------------------
    Revision History
    ---------------------------------------------------------------------------

    Date       Version  By  Synopsis
    ---------- -------- --- ----------------------------------------------------
    2009/11/30 1.0.     DAG Initial version.

    2009/12/15 2.4.14   DAG Merge into library WizardWrx.SharedUtl2.

    2010/04/04 2.51     DAG Add missing bits of standard documentation to the
                            heading.

    2014/05/22 3.0      DAG Relocated to BitMath namespace and library, and
                            renamed BitArray32, so that the old and new class
                            won't collide if both namespaces happen to get
                            imported into the same module.

    2016/04/29 4.0      DAG Define a custom overload of ToString(string) to show
                            individual bits and a static method to encapsulate
							the somewhat roundabout process required to format
							any integral type as a bit mask.

    2016/04/30 4.1      DAG TrimRuler: Gracefully handle the input string being
                            short, which is always true of the tens string.

    2016/05/02 4.2      DAG TrimRuler: Implement bidirectional rulers.
    ============================================================================
*/

using System;


namespace WizardWrx
{
    /// <summary>
    /// This object exposes properties and methods to simplify using masks of up
    /// to 32 bits in day to day programming.
    /// </summary>
	public class BitArray32
	{
		#region Public Enumerations for use with GetRuler Methods, Among Others
		/// <summary>
		/// Use this enumeration to tell the GetRulerTens and GetRulerUnits
		/// how long you want the ruler to be. The name of the enumeration,
		/// BitCount, should help you to select the correct length, as is its
		/// numeric value.
		/// </summary>
		/// <see cref="GetRulerUnits"/>
		/// <see cref="GetRulerTens"/>
		/// <seealso cref="BitDisplayOrder"/>
		public enum BitCount
		{
			/// <summary>
			/// The default (uninitialized) value of Unspecified is invalid, and
			/// elicits an InvalidEnumArgumentException exception.
			/// </summary>
			Unspecified = 0 ,

			/// <summary>
			/// Create a ruler for masks of 8 bits (1 byte).
			/// </summary>
			Count08 = 8 ,

			/// <summary>
			/// Create a ruler for masks of 16 bits (2 bytes).
			/// </summary>
			Count16 = 16 ,

			/// <summary>
			/// Create a ruler for masks of 32 bits (4 bytes).
			/// </summary>
			Count32 = 32 ,

			/// <summary>
			/// Create a ruler for masks of 64 bits (8 bytes).
			/// </summary>
			Count64 = 64
		}	// BitCount


		/// <summary>
		/// Use this enumeration to tell the GetRulerTens and GetRulerUnits
		/// in which direction you want the ruler to appear.
		/// </summary>
		/// <see cref="GetRulerUnits"/>
		/// <see cref="GetRulerTens"/>
		/// <seealso cref="BitCount"/>
		public enum BitDisplayOrder
		{
			/// <summary>
			/// The default (uninitialized) value of Unspecified is invalid, and
			/// elicits an InvalidEnumArgumentException exception.
			/// </summary>
			Unspecified ,

			/// <summary>
			/// HighBitToLowBit elicits a ruler with its markings numbered from
			/// right to left (High bit to Low), the order in which bits appear
			/// in core dumps and in most of the graphical displays that appear
			/// in technical documentation.
			/// 
			/// Since this is the conventional display order, it is the default
			/// when 
			/// </summary>
			HighBitToLowBit ,

			/// <summary>
			/// LowBitToHighBit elicits a ruler with its markings numbered from
			/// left to right (Low bit to High), which may be more useful for a
			/// carbon unit, especially one who is unaccustomed to reading bit
			/// masks from core dumps.
			/// </summary>
			LowBitToHighBit
		}	// BitDisplayOrder
		#endregion	// Public Enumerations for use with GetRuler Methods, Among Others


		#region Bit Constants
		//  --------------------------------------------------------------------
        //  Define constants for bits 1 through 32. Comments show their binary
        //  representation.
        //  --------------------------------------------------------------------

        /// <summary>
        /// Bit 1
        /// </summary>
        public const UInt32 BIT_01 = 1;             // 00000000000000000000000000000001

        /// <summary>
        /// Bit 2
        /// </summary>
        public const UInt32 BIT_02 = 2;             // 00000000000000000000000000000010

        /// <summary>
        /// Bit 3
        /// </summary>
        public const UInt32 BIT_03 = 4;             // 00000000000000000000000000000100

        /// <summary>
        /// Bit 4
        /// </summary>
        public const UInt32 BIT_04 = 8;             // 00000000000000000000000000001000

        /// <summary>
        /// Bit 5
        /// </summary>
        public const UInt32 BIT_05 = 16;                // 00000000000000000000000000010000

        /// <summary>
        /// Bit 6
        /// </summary>
        public const UInt32 BIT_06 = 32;                // 00000000000000000000000000100000

        /// <summary>
        /// Bit 7
        /// </summary>
        public const UInt32 BIT_07 = 64;                // 00000000000000000000000001000000

        /// <summary>
        /// Bit 8
        /// </summary>
        public const UInt32 BIT_08 = 128;           // 00000000000000000000000010000000

        /// <summary>
        /// Bit 9
        /// </summary>
        public const UInt32 BIT_09 = 256;           // 00000000000000000000000100000000

        /// <summary>
        /// Bit 10
        /// </summary>
        public const UInt32 BIT_10 = 512;           // 00000000000000000000001000000000

        /// <summary>
        /// Bit 11
        /// </summary>
        public const UInt32 BIT_11 = 1024;          // 00000000000000000000010000000000

        /// <summary>
        /// Bit 12
        /// </summary>
        public const UInt32 BIT_12 = 2048;          // 00000000000000000000100000000000

        /// <summary>
        /// Bit 13
        /// </summary>
        public const UInt32 BIT_13 = 4096;          // 00000000000000000001000000000000

        /// <summary>
        /// Bit 14
        /// </summary>
        public const UInt32 BIT_14 = 8192;          // 00000000000000000010000000000000

        /// <summary>
        /// Bit 15
        /// </summary>
        public const UInt32 BIT_15 = 16384;         // 00000000000000000100000000000000

        /// <summary>
        /// Bit 16
        /// </summary>
        public const UInt32 BIT_16 = 32768;         // 00000000000000001000000000000000

        /// <summary>
        /// Bit 17
        /// </summary>
        public const UInt32 BIT_17 = 65536;         // 00000000000000010000000000000000

        /// <summary>
        /// Bit 18
        /// </summary>
        public const UInt32 BIT_18 = 131072;        // 00000000000000100000000000000000

        /// <summary>
        /// Bit 19
        /// </summary>
        public const UInt32 BIT_19 = 262144;        // 00000000000001000000000000000000

        /// <summary>
        /// Bit 20
        /// </summary>
        public const UInt32 BIT_20 = 524288;        // 00000000000010000000000000000000

        /// <summary>
        /// Bit 21
        /// </summary>
        public const UInt32 BIT_21 = 1048576;       // 00000000000100000000000000000000

        /// <summary>
        /// Bit 22
        /// </summary>
        public const UInt32 BIT_22 = 2097152;       // 00000000001000000000000000000000

        /// <summary>
        /// Bit 23
        /// </summary>
        public const UInt32 BIT_23 = 4194304;       // 00000000010000000000000000000000

        /// <summary>
        /// Bit 24
        /// </summary>
        public const UInt32 BIT_24 = 8388608;       // 00000000100000000000000000000000

        /// <summary>
        /// Bit 25
        /// </summary>
        public const UInt32 BIT_25 = 16777216;      // 00000001000000000000000000000000

        /// <summary>
        /// Bit 26
        /// </summary>
        public const UInt32 BIT_26 = 33554432;      // 00000010000000000000000000000000

        /// <summary>
        /// Bit 27
        /// </summary>
        public const UInt32 BIT_27 = 67108864;      // 00000100000000000000000000000000

        /// <summary>
        /// Bit 28
        /// </summary>
        public const UInt32 BIT_28 = 134217728;     // 00001000000000000000000000000000

        /// <summary>
        /// Bit 29
        /// </summary>
        public const UInt32 BIT_29 = 268435456;     // 00010000000000000000000000000000

        /// <summary>
        /// Bit 30
        /// </summary>
        public const UInt32 BIT_30 = 536870912;     // 00100000000000000000000000000000

        /// <summary>
        /// Bit 31
        /// </summary>
        public const UInt32 BIT_31 = 1073741824;    // 01000000000000000000000000000000

        /// <summary>
        /// Bit 32
        /// </summary>
        public const UInt32 BIT_32 = 2147483648;    // 10000000000000000000000000000000
        #endregion


        #region Bit Ordinal Constants
        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_01 = 1;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_02 = 2;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_03 = 3;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_04 = 4;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_05 = 5;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_06 = 6;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_07 = 7;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_08 = 8;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_09 = 9;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_10 = 10;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_11 = 11;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_12 = 12;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_13 = 13;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_14 = 14;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_15 = 15;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_16 = 16;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_17 = 17;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_18 = 18;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_19 = 19;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_20 = 20;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_21 = 21;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_22 = 22;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_23 = 23;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_24 = 24;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_25 = 25;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_26 = 26;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_27 = 27;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_28 = 28;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_29 = 29;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_30 = 30;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_31 = 31;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_NUMBER_32 = 32;
        #endregion  // Bit Ordinal Constants


        #region Bit Position Constants
        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_00 = 0;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_01 = 1;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_02 = 2;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_03 = 3;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_04 = 4;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_05 = 5;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_06 = 6;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_07 = 7;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_08 = 8;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_09 = 9;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_10 = 10;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_11 = 11;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_12 = 12;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_13 = 13;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_14 = 14;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_15 = 15;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_16 = 16;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_17 = 17;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_18 = 18;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_19 = 19;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_20 = 20;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_21 = 21;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_22 = 22;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_23 = 23;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_24 = 24;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_25 = 25;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_26 = 26;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_27 = 27;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_28 = 28;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_29 = 29;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_30 = 30;

        /// <summary>
        /// Bits are numbered from lowest (least significant) to highest (most significant).
        /// </summary>
        public const UInt32 BIT_OFFSET_31 = 31;
        #endregion  // Bit Position Constants


		#region Bit Mask Formatting Constants
		/// <summary>
		/// The FormatIntegerAsBitArray overload that takes a Bits Per Group
		/// argument (pintBitsPerGroup) requires its value to either be this
		/// value or an even multiple of it, or SUPPRESS_BIT_GROUPING (zero).
		/// </summary>
		public const int MINIMUM_BIT_GROUP_IN_FORMAT = FormattingParameters.BITS_PER_NIBBLE;

		/// <summary>
		/// Argument pintBitsPerGroup of static method FormatIntegerAsBitArray
		/// must be either zero (this constant value) or an even multiple of
		/// MINIMUM_BIT_GROUP_IN_FORMAT (4), which also operates as its minimum
		/// permitted positive value.
		/// </summary>
		public const int SUPPRESS_BIT_GROUPING = FormattingParameters.SUPPRESS_GROUPING;
		#endregion	// Bit Mask Formatting Constants


		#region Other Related Constants
		/// <summary>
        /// This symbolic constant for identifying a bit as ON maps to bool true.
        /// </summary>
        public const bool ON = true;

        /// <summary>
        /// This symbolic constant for identifying a bit as OFF maps to bool false.
        /// </summary>
        public const bool OFF = false;


        /// <summary>
        /// Since this class numbers bits from 1. the highest supported bit number is 32.
        /// </summary>
        public const int BIT_NBR_MAX = 32;

        /// <summary>
        /// Since this class numbers bits from 1. the first bit number is 1.
        /// </summary>
        public const int BIT_NBR_MIN = 1;
        #endregion  // Other Related Constants


        #region Lookup Table
        //  --------------------------------------------------------------------
        //  The subscript of this array corresponds to the position occupied by
        //  the bit whose value corresponds to the bit being ON. Coincidentally,
        //  the positions of all but the first bit correspond to the integral
        //  power of two that it represents.
        //  --------------------------------------------------------------------

        static UInt32 [ ] s_auintPowersOf2 =
        {
            BIT_01 ,
            BIT_02 ,
            BIT_03 ,
            BIT_04 ,
            BIT_05 ,
            BIT_06 ,
            BIT_07 ,
            BIT_08 ,
            BIT_09 ,
            BIT_10 ,
            BIT_11 ,
            BIT_12 ,
            BIT_13 ,
            BIT_14 ,
            BIT_15 ,
            BIT_16 ,
            BIT_17 ,
            BIT_18 ,
            BIT_19 ,
            BIT_20 ,
            BIT_21 ,
            BIT_22 ,
            BIT_23 ,
            BIT_24 ,
            BIT_25 ,
            BIT_26 ,
            BIT_27 ,
            BIT_28 ,
            BIT_29 ,
            BIT_30 ,
            BIT_31 ,
            BIT_32
        };  // static UInt32 [ ] s_auintPowersOf2
        #endregion  // Lookup Table


        #region Private Constants and Storage for Instance
		const int ARRAY_SUBSCRIPT_FROM_ORDINAL = 1;

        const UInt32 BIT_IS_OFF = 0;
        const UInt32 BIT_IS_ON = 1;
        const UInt32 BITS_TOO_MANY = BIT_NBR_MAX + 1;
        const UInt32 BITS_TOO_FEW = BIT_NBR_MIN - 1;

        const UInt32 ALL_OFF = 0;
        const UInt32 FIRST_BIT_OFF = 0;
        const UInt32 FIRST_BIT_ON = 1;

		const string FORMAT_FIRST_ITEM_AS_ANY_L = @"{0:";
		const string FORMAT_FIRST_ITEM_AS_BIT_ARRAY = @"{0:B}";
		const string FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY_L = @"{0:B";
		const char FORMAT_FIRST_ITEM_AS_ANY_R = '}';

		const string HEXADECIMAL_ANY = @"X";
        const string HEXADECIMAL_8 = @"X8";

		const string ARG_PINTBITNBR = @"pintBitNumber";
        const string ARG_RANGE_MSG = @"Value is invalid for a bit on a mask of 32 bits.";

        const string ARG_DESCRIPTION = @"puintBitValue (Power of Two)";
        const string ARG_REQUIREMENT = @"Argument must be evenly divisible by two.";

        const int MATCH_FOUND = 0;
        const int ORDINAL_FROM_POSITION = 1;
		const int REQUIRED_RULER_STRING_LENGTH = 64;

        UInt32 _uintBitMask = ALL_OFF;
        #endregion  // Private Constants and Storage for Instance


        #region Constructors, All Public
        /// <summary>
        /// This default constructor for a BitArray32 object creates an
        /// object with all bits OFF.
        /// </summary>
        /// <remarks>
        /// Internally, the object is implemented as an unsigned 32 bit integer,
        /// a native value type.
        /// </remarks>
        public BitArray32 ( ) { }

        /// <summary>
        /// This overloaded constructor for a BitArray32 object creates an
        /// object with a specified initial value.
        /// </summary>
        /// <param name="puintInitialValue">
        /// Pass the initial value into the constructor as an unsigned 32 bit
        /// integer, a native value type.
        /// </param>
        public BitArray32 ( UInt32 puintInitialValue ) { _uintBitMask = puintInitialValue; }
        #endregion  // Constructors, All Public


        #region Instance Methods
        /// <summary>
        /// Turn a specified bit in the _uintBitMask instance variable OFF.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Ordinal number of bit to turn OFF. Bits are numbered from 1 to 32,
        /// with 1 being the least significant bit.
        /// </param>
        /// <remarks>
        /// This code uses a temporary instance of the BitArray32 class, which is
        /// discarded as the method returns.
        ///
        /// Since neither operand of the exclusive-OR assignment operator can be
        /// a BitArray32 object _ubmTheBit must be cast to UInt32, using the
        /// implicit operator UInt32, defined elsewhere in this class. Private
        /// variable _uintBitMask is already a UInt32.
        /// </remarks>
        public void BitOff ( int pintBitNumber )
        {
            BitArray32 _ubmTheBit = TurnBitOn ( pintBitNumber );
            _uintBitMask = _uintBitMask & ( ~( UInt32 ) _ubmTheBit );
        }   // BitOff


        /// <summary>
        /// Turn a specified bit in the _uintBitMask instance variable ON.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Ordinal number of bit to turn ON. Bits are numbered from 1 to 32,
        /// with 1 being the least significant bit.
        /// </param>
        /// <remarks>
        /// This code uses a temporary instance of the BitArray32 class, which is
        /// discarded as the method returns.
        ///
        /// Since neither operand of the logical OR assignment operator can be
        /// a BitArray32 object _ubmTheBit must be cast to UInt32, using the
        /// implicit operator UInt32, defined elsewhere in this class. Private
        /// variable _uintBitMask is already a UInt32.
        /// </remarks>
        public void BitOn ( int pintBitNumber )
        {
            BitArray32 _ubmTheBit = TurnBitOn ( pintBitNumber );
            _uintBitMask |= ( UInt32 ) _ubmTheBit;
        }   // BitOn


        /// <summary>
        /// Test the state of a specified bit, returning TRUE if it is OFF.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Ordinal number of bit to evaluate. Bits are numbered from 1 to 32,
        /// with 1 being the least significant bit.
        /// </param>
        /// <returns>
        /// TRUE if the specified bit is ON, else FALSE.
        /// </returns>
        /// <remarks>
        /// This code uses a temporary instance of the BitArray32 class, which is
        /// discarded as the method returns.
        ///
        /// Unlike the BitOff and BitOn operators, everything is already cast to
        /// Uint32. Therefore, this method works just fine without any casts.
        ///
        /// Since this code is unlikely to change, it uses the most concise form
        /// of the IF statement, which supports a single executable statement in
        /// each branch.
        /// </remarks>
        public bool IsBitOff ( int pintBitNumber )
        {
            BitArray32 _ubmTheBit = TurnBitOn ( pintBitNumber );

            if ( ( _uintBitMask & _ubmTheBit ) == _ubmTheBit )
                return false;
            else
                return true;
        }   // IsBitOff


        /// <summary>
        /// Test the state of a specified bit, returning TRUE if it is ON.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Ordinal number of bit to evaluate. Bits are numbered from 1 to 32,
        /// with 1 being the least significant bit.
        /// </param>
        /// <returns>
        /// TRUE if the specified bit is ON, else FALSE.
        /// </returns>
        /// <remarks>
        /// This code uses a temporary instance of the BitArray32 class, which is
        /// discarded as the method returns.
        ///
        /// Unlike the BitOff and BitOn operators, everything is already cast to
        /// Uint32. Therefore, this method works just fine without any casts.
        ///
        /// Since this code is unlikely to change, it uses the most concise form
        /// of the IF statement, which supports a single executable statement in
        /// each branch.
        /// </remarks>
        public bool IsBitOn ( int pintBitNumber )
        {
            BitArray32 _ubmTheBit = TurnBitOn ( pintBitNumber );

            if ( ( _uintBitMask & _ubmTheBit ) == _ubmTheBit )
                return true;
            else
                return false;
        }   // IsBitOn
        #endregion  // Instance Methods


        #region Public Static Methods
        /// <summary>
        /// Return the bit number that corresponds to the given value. Bits are
        /// numbered from 1, starting with the least significant bit.
        /// </summary>
        /// <param name="puintBitValue">
        /// Unsigned integer puintBitValue must be a integral power of two.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the bit number that
        /// corresponds to the specified integral power of two.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// An ArgumentOutOfRangeException exception is thrown if the input
        /// value is not an integral power of two.
        /// </exception>
        public static UInt32 BitNumber ( UInt32 puintBitValue )
        {
            return BitPosition ( puintBitValue ) + ORDINAL_FROM_POSITION;
        }   // public static UInt32 BitNumber (1 of 2)


        /// <summary>
        /// Return the bit number that corresponds to the given value. Bits are
        /// numbered from 1, starting with the least significant bit.
        /// </summary>
        /// <param name="pintBitValue">
        /// Signed integer puintBitValue must be a positive integral power of
        /// two.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the bit number that
        /// corresponds to the specified integral power of two.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// An ArgumentOutOfRangeException exception is thrown if the input
        /// value is not an integral power of two.
        /// </exception>
        public static UInt32 BitNumber ( Int32 pintBitValue )
        {
            if ( pintBitValue > 0 )
                return BitPosition ( pintBitValue ) + ORDINAL_FROM_POSITION;
            else
                throw new ArgumentOutOfRangeException (
                    ARG_DESCRIPTION ,
                    pintBitValue ,
                    ARG_REQUIREMENT );
        }   // public static UInt32 BitNumber (2 of 2)


        /// <summary>
        /// Return the bit position that corresponds to the given value. Bit
        /// positions are numbered from 0, starting with the least significant
        /// bit.
        /// </summary>
        /// <param name="puintBitValue">
        /// Unsigned integer puintBitValue must be a integral power of two.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the bit position that
        /// corresponds to the specified integral power of two.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// An ArgumentOutOfRangeException exception is thrown if the input
        /// value is not an integral power of two.
        /// </exception>
        public static UInt32 BitPosition ( UInt32 puintBitValue )
        {
            int rintIndex = Array.BinarySearch (
                s_auintPowersOf2 ,
                puintBitValue );

            if ( rintIndex >= MATCH_FOUND )
                return ( UInt32 ) rintIndex;
            else
                throw new ArgumentOutOfRangeException (
                    ARG_DESCRIPTION ,
                    puintBitValue ,
                    ARG_REQUIREMENT );
        }   // public static UInt32 BitPosition (1 of 2)


        /// <summary>
        /// Return the bit position that corresponds to the given value. Bit
        /// positions are numbered from 0, starting with the least significant
        /// bit.
        /// </summary>
        /// <param name="pintBitValue">
        /// Signed integer pintBitValue must be a positive integral power of
        /// two.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the bit position that
        /// corresponds to the specified integral power of two.
        /// </returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// An ArgumentOutOfRangeException exception is thrown if the input
        /// value is not an integral power of two.
        /// </exception>
        public static UInt32 BitPosition ( Int32 pintBitValue )
        {
            if ( pintBitValue > 0 )
            {   // Positive integers only, please.
                int rintIndex = Array.BinarySearch (
                    s_auintPowersOf2 ,
                    ( UInt32 ) pintBitValue );

                if ( rintIndex >= MATCH_FOUND )
                {
                    return ( UInt32 ) rintIndex;
                }
                else
                {
                    throw new ArgumentOutOfRangeException (
                        ARG_DESCRIPTION ,
                        pintBitValue ,
                        ARG_REQUIREMENT );
                }   // if ( rintIndex >= MATCH_FOUND )
            }
            else
            {   // Negative integers are unsupported.
                throw new ArgumentOutOfRangeException (
                    ARG_DESCRIPTION ,
                    pintBitValue ,
                    ARG_REQUIREMENT );
            }   // if ( pintBitValue > 0 )
        }   // public static UInt32 BitPosition (2 of 2)


		/// <summary>
		/// Format any integral value type as an array of bits, listed from most
		/// significant to least.
		/// </summary>
		/// <typeparam name="T">
		/// This method accepts all integral value types except IntPtr. However,
		/// since there is no documented provision in the .NET Framework to
		/// enforce such a restriction on a generic type at compile time, the
		/// task is left to this method, which enforces it at run time. However,
		/// when fed an incompatible type, this method degrades gracefully by
		/// applying its default string format.
		/// </typeparam>
		/// <param name="pintIntegralValueType">
		/// This argument is expected to be an integral type, but a non-integral
		/// type is processed by degrading to its default ToString method.
		/// </param>
		/// <returns>
		/// When argument pintIntegralValueType is an integral value type, the
		/// returned string is composed of zeros and ones that represent the
		/// bits that store its value, listing them from most significant bit to
		/// least.
		/// 
		/// If the integer has a sign, the most significant bit is the bit that
		/// is reserved to hold it. Likewise, the most significant bit of an
		/// unsigned integer is the bit that stores the highest power of two
		/// that it can hold.
		/// </returns>
		/// <remarks>
		/// This method could easily be implemented as a call to the second
		/// overload, but I chose to keep their implementations separate for two
		/// reasons.
		/// 
		/// 1) The second overload concatenates its second argument to the same
		/// string constant used by this method, passing the resulting string as
		/// the second argument to the string.Format overload that takes a
		/// format provider as its first argument. Since string concatenation is
		/// expensive, why use it when the simpler method call achieves the goal
		/// so much more cheaply.
		/// 
		/// 2) Since string formatters can see high usage, and the only difference
		/// between the two overloads is the string concatenation, calling the
		/// second overload not only incurs a wasteful string concatenation, but it
		/// must waste a stack frame for this dubious objective.
		/// </remarks>
		public static string FormatIntegerAsBitArray<T> ( T pintIntegralValueType )
		{
			return string.Format (
				new WizardWrx.BitMaskFormat ( ) ,
				FORMAT_FIRST_ITEM_AS_BIT_ARRAY ,
				pintIntegralValueType );
		}	// public static string FormatIntegerAsBitArray (1 of 2)


		/// <summary>
		/// Format any integral value type as an array of bits, listed from most
		/// significant to least, with optional grouping.
		/// </summary>
		/// <typeparam name="T">
		/// This method accepts all integral value types except IntPtr. However,
		/// since there is no documented provision in the .NET Framework to
		/// enforce such a restriction on a generic type at compile time, the
		/// task is left to this method to do so at run time. However, when fed
		/// an incompatible type, this method degrades gracefully by applying
		/// its default string format.
		/// </typeparam>
		/// <param name="pintIntegralValueType">
		/// This argument is expected to be an integral type, but a non-integral
		/// type is processed by degrading to its default ToString method.
		/// </param>
		/// <param name="pintBitsPerGroup">
		/// Specify the number of bits to include in each group, or specify zero
		/// to suppress formatting, equivalent to calling the first overload.
		/// 
		/// IMPORTANT: The mechanism by which the spaces are inserted imposes a
		/// restriction that this value must be evenly divisible by 4, such that
		/// the four bits of each byte are kept together.
		/// </param>
		/// <returns>
		/// When argument pintIntegralValueType is an integral value type, the
		/// returned string is composed of zeros and ones that represent the
		/// bits that store its value, listing them from most significant bit to
		/// least.
		/// 
		/// If the integer has a sign, the most significant bit is the bit that
		/// is reserved to hold it. Likewise, the most significant bit of an
		/// unsigned integer is the bit that stores the highest power of two
		/// that it can hold.
		/// </returns>
		/// <exception cref="FormatException">
		/// Invalid formatting instructions raise a FormatException exception,
		/// which typically wraps an ArgumentException, and supplies additional
		/// detail not usually available from a typical FormatException.
		/// </exception>
		public static string FormatIntegerAsBitArray<T> (
			T pintIntegralValueType ,
			int pintBitsPerGroup )
		{	// Rather than have it done twice, all argument parsing is left to the format provider.
			return string.Format (
				new WizardWrx.BitMaskFormat ( ) ,
				string.Concat (
					FORMAT_FIRST_ITEM_AS_FORMATTED_BIT_ARRAY_L ,
					pintBitsPerGroup ,
					FORMAT_FIRST_ITEM_AS_ANY_R ) ,
				pintIntegralValueType );
		}	// public static string FormatIntegerAsBitArray (2 of 2)


		/// <summary>
		/// Format any integral value type as an array of hexadecimal "digits", 
		/// listed from most significant to least, specifying a standard numeric
		/// format string and a minimum length appropriate to its maximum value.
		/// 
		/// The advantage of this method over the standard ToString overloads is
		/// that you don't have to figure out how many hexadecimal digits you
		/// must allow.
		/// </summary>
		/// <typeparam name="T">
		/// This method accepts all integral value types except IntPtr. However,
		/// since there is no documented provision in the .NET Framework to
		/// enforce such a restriction on a generic type at compile time, the
		/// task is left to this method to do so at run time. However, when fed
		/// an incompatible type, this method degrades gracefully by applying
		/// its default string format.
		/// </typeparam>
		/// <param name="pintIntegralValueType">
		/// This argument is expected to be an integral type, but a non-integral
		/// type is processed by degrading to its default ToString method.
		/// </param>
		/// <returns>
		/// When argument pintIntegralValueType is an integral value type, the
		/// returned string is composed of hexadecimal "digits" that represent
		/// its value in base 16. The length of the string is a function of the
		/// maximum value that can be stored in an integer of the input type.
		/// Hence, a Byte, being an 8 bit integer, returns a two-character hex
		/// string, a UInt16 returns four, UInt32 eight, and UInt64 sixteen.
		/// 
		/// Otherwise, the default ToString method on its type is called, and
		/// the returned string is whatever that method provides.
		/// </returns>
		public static string FormatIntegerAsHex<T> ( T pintIntegralValueType )
		{
			//	----------------------------------------------------------------
			//	Since C# prohibits declarations in IF statements, this is the
			//	best we can do. Oh, the joys of writing in several programming
			//	languages that differ in such subtle ways! Thankfully, the scope
			//	of this variable ensures that it has a very short life anyway.
			//	----------------------------------------------------------------

			BCLIntegerTypeInfo bclTypeInfo = BitHelpers.InfoForIntegralType ( pintIntegralValueType.GetType ( ) );

			if ( bclTypeInfo != null )
			{
				int intMaxHexDigits = bclTypeInfo.RequiredStorageBytes * BitHelpers.HEX_DIGITS_PER_BYTE;
				string strFormatSpecification = string.Concat (
					new object [ ]
					{
						FORMAT_FIRST_ITEM_AS_ANY_L ,
						HEXADECIMAL_ANY ,
						intMaxHexDigits ,
						FORMAT_FIRST_ITEM_AS_ANY_R
					} );

				return string.Format (
					strFormatSpecification ,
					pintIntegralValueType );
			}	// TRUE (anticipated outcome) block, if ( bclTypeInfo != null )
			else
			{	// Degrade to the default parameterless ToString method on the object.
				return pintIntegralValueType.ToString ( );
			}	// FALSE (degraded outcome) block, if ( bclTypeInfo != null )
		}	// public static string FormatIntegerAsHex


		/// <summary>
		/// Get the tens marks of the bit mask ruler from the string resources.
		/// </summary>
		/// <param name="penmBitCount">
		/// Indicate the length of ruler to generate by way of a member of the
		/// BitCount enumeration. See the BitCount enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitDisplayOrder"/>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitCount"/>
		/// <returns>
		/// The return value is a string, exactly 60 characters long, containing
		/// nine spaces, each followed by the next sequential integer, counting
		/// from 1.
		/// </returns>
		/// <seealso cref="GetRulerUnits"/>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		/// This method throws an ComponentModel.InvalidEnumArgumentException
		/// exception when the penmBitCount argument is not a valid member of
		/// the BitCount enumeration and when the penmBitDisplayOrder argument
		/// is not a valid member of the BitDisplayOrder enumeration. In both
		/// cases, a value of Unspecified is treated as invalid.
		/// 
		/// If the BitCount value that is invalid, the exception is actually
		/// thrown by a private method, TrimRuler, which this method, by virtue
		/// of being unguarded, allows to bubble up the stack, as would be the
		/// case if it had thrown the exception.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This method allows the InvalidOperationException exception thrown
		/// when it calls private method TrimRuler, sending a pstrWholeRuler
		/// string that has a string length that differs from the length implied
		/// by the numeric value of the penmBitCount enumeration argument to
		/// bubble up the call stack.
		/// </exception>
		/// <remarks>
		/// This is a method call, rather than a property, because the string is
		/// read from a resource. 
		/// 
		/// The InvalidOperationException and InvalidEnumArgumentException
		/// exceptions bubble up, rather than being caught in this routine to
		/// avoid duplicating the task in both of its internal callers. 
		/// </remarks>
		public static string GetRulerTens (
			BitCount penmBitCount ,
			BitDisplayOrder penmBitDisplayOrder )
		{
			switch ( penmBitDisplayOrder )
			{	// Since value Unspecified is considered invalid, it causes execution to reach the default block and throw an InvalidEnumArgumentException exception.
				case BitDisplayOrder.LowBitToHighBit:
					return TrimRuler (
						Properties.Resources.BIT_RULER_TENS_LOWTOHIGH ,
						penmBitCount ,
						penmBitDisplayOrder );
				case BitDisplayOrder.HighBitToLowBit:
					return TrimRuler (
						Properties.Resources.BIT_RULER_TENS_HIGHTOLOW ,
						penmBitCount ,
						penmBitDisplayOrder );
				default:
					throw new System.ComponentModel.InvalidEnumArgumentException (
						"penmBitDisplayOrder" ,
						( int ) penmBitDisplayOrder ,
						typeof ( BitDisplayOrder ) );
			}	// switch ( penmBitDisplayOrder )
		}	// public static string GetRulerTens


		/// <summary>
		/// Get the units marks of the bit mask ruler from the string resources.
		/// </summary>
		/// <param name="penmBitCount">
		/// Indicate the length of ruler to generate by way of a member of the
		/// BitCount enumeration. See the BitCount enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitCount"/>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitDisplayOrder"/>
		/// <returns>
		/// The return value is a string, exactly 64 characters long, containing
		/// numbers from 1 to 10, repeated six times, followed by numbers
		/// 1 through 4, enough to cover the bits in mask of 64 bits.
		/// </returns>
		/// <seealso cref="GetRulerTens"/>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		/// This method throws an ComponentModel.InvalidEnumArgumentException
		/// exception when the penmBitCount argument is not a valid member of
		/// the BitCount enumeration and when the penmBitDisplayOrder argument
		/// is not a valid member of the BitDisplayOrder enumeration. In both 
		/// cases, a value of Unspecified is treated as invalid.
		/// 
		/// If the BitCount value that is invalid, the exception is actually
		/// thrown by a private method, TrimRuler, which this method, by virtue
		/// of being unguarded, allows to bubble up the stack, as would be the
		/// case if it had thrown the exception.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This method allows the InvalidOperationException exception thrown
		/// when it calls private method TrimRuler, sending a pstrWholeRuler
		/// string that has a string length that differs from the length implied
		/// by the numeric value of the penmBitCount enumeration argument to
		/// bubble up the call stack.
		/// </exception>
		/// <remarks>
		/// This is a method call, rather than a property, because the string is
		/// read from a resource. 
		/// 
		/// The InvalidOperationException and InvalidEnumArgumentException
		/// exceptions bubble up, rather than being caught in this routine to
		/// avoid duplicating the task in both of its internal callers. 
		/// </remarks>
		public static string GetRulerUnits (
			BitCount penmBitCount ,
			BitDisplayOrder penmBitDisplayOrder )
		{
			switch ( penmBitDisplayOrder )
			{	// Since value Unspecified is considered invalid, it causes execution to reach the default block and throw an InvalidEnumArgumentException exception.
				case BitDisplayOrder.LowBitToHighBit:
					return TrimRuler (
						Properties.Resources.BIT_RULER_UNITS_LOW2HIGH ,
						penmBitCount ,
						penmBitDisplayOrder );
				case BitDisplayOrder.HighBitToLowBit:
					return TrimRuler (
						Properties.Resources.BIT_RULER_UNITS_HIGHTOLOW ,
						penmBitCount ,
						penmBitDisplayOrder );
				default:
					throw new System.ComponentModel.InvalidEnumArgumentException (
						"penmBitDisplayOrder" ,
						( int ) penmBitDisplayOrder ,
						typeof ( BitDisplayOrder ) );
			}	// switch ( penmBitDisplayOrder )
		}	// public static string GetRulerUnits


        /// <summary>
        /// Return a new BitArray32 object in which a specified bit is turned ON
        /// and all others are OFF.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Ordinal number of bit to turn OFF. Bits are numbered from 1 to 32,
        /// with 1 being the least significant bit.
        /// </param>
        /// <returns>
        /// A new BitArray32 object with a single bit turned OFF, and all others
        /// already turned OFF.
        /// </returns>
        /// <remarks>
        /// In essence, this method returns an instance of the BitArray32 created
        /// by calling its default constructor.
        ///
        /// This method and its companion, TurnBitOn, bounds checks its input,
        /// notwithstanding the fact that the bitwise operators discard the high
        /// bits in the bit count operand of the left logical shift operator.
        ///
        /// Since all instance methods call these static methods, these bounds
        /// checks suffice for the entire class.
        /// </remarks>
        public static BitArray32 TurnBitOff ( int pintBitNumber )
        {
            if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
            {
                UInt32 _uintBitsToShift = ( UInt32 ) pintBitNumber - 1; // Shift one fewer bits than the requested bit number.
                BitArray32 _uintMask = FIRST_BIT_OFF;

                return _uintMask;
            }   // TRUE (expected outcome) block, if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
            else
            {
                throw new ArgumentOutOfRangeException ( ARG_PINTBITNBR ,
                                                        pintBitNumber ,
                                                        ARG_RANGE_MSG );
            }   // FALSE (unexpected outcome) block, if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
        }   // static TurnBitOff method


        /// <summary>
        /// Return a new BitArray32 with bit number (ordinal) pintBitNumber
        /// turned ON.
        /// </summary>
        /// <param name="pintBitNumber">
        /// Number of bit to turn on with bits numbered from 1 to 32, starting
        /// with Bit 1 as the least significant bit.
        /// </param>
        /// <returns>
        /// A new BitArray32 object, with the specified bit ON and all others 
        /// OFF.
        /// </returns>
        /// <remarks>
        /// This method and its companion, TurnBitOff, bounds checks its input,
        /// notwithstanding the fact that the bitwise operators discard the high
        /// bits in the bit count operand of the left logical shift operator.
        ///
        /// Since all instance methods call these static methods, these bounds
        /// checks suffice for the entire class.
        /// </remarks>
        public static BitArray32 TurnBitOn ( int pintBitNumber )
        {
            if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
            {
                Int32 _intBitsToShift = pintBitNumber - 1;  // Shift one fewer bits than the requested bit number.
                uint _uintMask = FIRST_BIT_ON;

                _uintMask = _uintMask << _intBitsToShift;

                return _uintMask;
            }   // TRUE (expected outcome) block, if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
            else
            {
                throw new ArgumentOutOfRangeException ( ARG_PINTBITNBR ,
                                                        pintBitNumber ,
                                                        ARG_RANGE_MSG );
            }   // FALSE (unexpected outcome) block, if ( pintBitNumber < BITS_TOO_MANY && pintBitNumber > BITS_TOO_FEW )
        }   // static TurnBitOn method
        #endregion  // Public Static Methods


		#region Private Static Methods
		/// <summary>
		/// Trim the ruler to fit.
		/// </summary>
		/// <param name="pstrWholeRuler">
		/// Specify a reference to the ruler string to trim.
		/// </param>
		/// <param name="penmBitCount">
		/// Indicate the length of ruler to generate by way of a member of the
		/// BitCount enumeration. See the BitCount enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitDisplayOrder"/>
		/// <param name="penmBitDisplayOrder">
		/// Indicate the direction in which the ruler should be displayed. See
		/// the XML documentation for the BitDisplayOrder enumeration for a full
		/// explanation of each value.
		/// </param>
		/// <see cref="BitCount"/>
		/// <returns>
		/// The ruler is trimmed to fit a bit mask of the specified width, and
		/// returned as a new string.
		/// </returns>
		/// <exception cref="System.ComponentModel.InvalidEnumArgumentException">
		/// This method throws an ComponentModel.InvalidEnumArgumentException
		/// exception when the penmBitCount argument is not a valid member of
		/// the BitCount enumeration. A value of Unspecified is treated as an
		/// invalid value.
		/// </exception>
		/// <exception cref="InvalidOperationException">
		/// This method throws an InvalidOperationException exception when the
		/// pstrWholeRuler string length differs from the length implied by the
		/// numeric value of the penmBitCount enumeration argument.
		/// </exception>
		private static string TrimRuler (
			string pstrWholeRuler ,
			BitCount penmBitCount ,
			BitDisplayOrder penmBitDisplayOrder )
		{
			const int BEGINNING = 0;

			if ( pstrWholeRuler.Length == REQUIRED_RULER_STRING_LENGTH )
			{
				switch ( penmBitCount )
				{
					case BitCount.Count08:
					case BitCount.Count16:
					case BitCount.Count32:
					case BitCount.Count64:
						switch ( penmBitDisplayOrder )
						{	// Since the calling routine should catch invalid penmBitDisplayOrder, the default block should never execute. 
							case BitDisplayOrder.LowBitToHighBit:
								return pstrWholeRuler.Substring (
									BEGINNING ,
									( int ) penmBitCount );
							case BitDisplayOrder.HighBitToLowBit:
								return pstrWholeRuler.Substring (
									pstrWholeRuler.Length
									- ( int ) penmBitCount );
							default:
								throw new System.ComponentModel.InvalidEnumArgumentException (
									"penmBitDisplayOrder" ,
									( int ) penmBitDisplayOrder ,
									typeof ( BitDisplayOrder ) );
						}	// switch ( penmBitDisplayOrder )
					default:
						throw new System.ComponentModel.InvalidEnumArgumentException (
							"penmBitCount" ,
							( int ) penmBitCount ,
							typeof ( BitCount ) );
				}	// switch ( penmBitCount )
			}	// TRUE (anticipated outcome) block, if ( pstrWholeRuler.Length == REQUIRED_RULER_STRING_LENGTH )
			else
			{
				throw new InvalidOperationException (
					string.Format (
						Properties.Resources.ERRMSG_INVALID_RULER_STRING ,		// Format control string
						REQUIRED_RULER_STRING_LENGTH ,							// Format Item 0 = Expected length
						pstrWholeRuler.Length ,									// Format Item 1 = Actual length
						Environment.NewLine ) );								// Format Item 2 = Embedded Newline
			}	// FALSE (error outcome) block, if ( pstrWholeRuler.Length == REQUIRED_RULER_STRING_LENGTH )
		}	// private static string TrimRuler
		#endregion	// Private Static Methods


		#region Overridden Methods of Base Class
		/// <summary>
        /// Override the ToString method to provide a useful output, in the form
        /// of a hexadecimal representation of the value of its bit mask.
        /// </summary>
        /// <returns>
        /// A String representation of the private variable that holds the
        /// current value of the bit mask, formatted as a hexadecimal
        /// representation of its value.
        /// </returns>
        public override string ToString ( )
        {
            return _uintBitMask.ToString ( HEXADECIMAL_8 );
        }   // public override string ToString (1 of 2)


		/// <summary>
		/// Override the ToString method to provide additional useful outputs,
		/// such as a couple of optional binary (bit level) formats.
		/// </summary>
		/// <param name="format">
		/// In keeping with the standard nomenclature, this format string gives
		/// details about the output format desired.
		/// </param>
		/// <returns></returns>
		public string ToString ( string format )
		{
			return BitHelpers.FormatBitMask (
				_uintBitMask ,
				format );
		}   // public override string ToString (1 of 2)


        /// <summary>
        /// Override the default Equals method, so that class instances can
        /// participate in meaningful equality tests.
        /// </summary>
        /// <param name="pComparand">
        /// A reference to a generic object to be compared against the current
        /// instance.
        /// </param>
        /// <returns>
        /// True if the two meet our definition of Equals, which is that the two
        /// bit masks are equal.
        /// </returns>
        public override bool Equals ( object pComparand )
        {
            if ( pComparand == null )
                return false;

            BitArray32 ba32CastComparand = pComparand as BitArray32;

            if ( ( System.Object ) ba32CastComparand == null )
                return false;

            return ( _uintBitMask == ba32CastComparand._uintBitMask );
        }   // public bool Equals (1 of 2)


        /// <summary>
        /// Provide a typecast version of the generic Equals method.
        /// </summary>
        /// <param name="pComparand">
        /// The comparand is the other BitArray32 to be tested for equality. 
        /// </param>
        /// A reference to another instance of the BitArray32 class, to be
        /// compared against the current instance.
        /// <returns>
        /// True if the two meet our definition of Equals, which is that the two
        /// bit masks are equal.
        /// </returns>
        public bool Equals ( BitArray32 pComparand )
        {
			if ( pComparand == null )
				return false;
			else
				return ( pComparand._uintBitMask == this._uintBitMask );
        }   // public bool Equals (2 of 2)


        /// <summary>
        /// This class overrides the GetHashCode method of its base class to
        /// return the hash code for the uint32 primitive that stores its data.
        /// </summary>
        /// <returns>
        /// An Integer which is the value returned by the GetHashCode method of
        /// the uint32 object.
        /// </returns>
        public override int GetHashCode ( )
        {
            return _uintBitMask.GetHashCode ( );
        }   // public override int GetHashCode
        #endregion  // Overridden Methods of Base Class


        #region Overloaded Operators
        /// <summary>
        /// Though not recommended, members of this class override the equality
        /// and inequality operators.
        /// </summary>
        /// <param name="pobjLValue">
        /// Value on left of equal sign, otherwise known as the LValue or left
        /// operand.
        /// </param>
        /// <param name="pobjRValue">
        /// Value on right of equal sign, otherwise known as the RValue or right
        /// operand.
        /// </param>
        /// <returns>
        /// True if both value are equal, or if they refer to the same object.
        /// </returns>
        public static bool operator == ( 
            BitArray32 pobjLValue ,
            BitArray32 pobjRValue )
        {
            if ( System.Object.ReferenceEquals ( pobjLValue , pobjRValue ) )
                return true;

            if ( ( ( object ) pobjLValue == null ) || ( ( object ) pobjRValue == null ) )
                return false;

            return ( pobjLValue._uintBitMask == pobjRValue._uintBitMask );
        }   // overloaded operator ==


        /// <summary>
        /// Though not recommended, members of this class override the equality
        /// and inequality operators.
        /// </summary>
        /// <param name="pobjLValue">
        /// Value on left of equal sign, otherwise known as the LValue or left
        /// operand.
        /// </param>
        /// <param name="pojbRValue">
        /// Value on right of equal sign, otherwise known as the RValue or right
        /// operand.
        /// </param>
        /// <returns>
        /// Inverse of the value returned by the overloaded equals operator.
        /// </returns>
        public static bool operator != (
            BitArray32 pobjLValue ,
            BitArray32 pojbRValue )
        {
            return !( pobjLValue == pojbRValue );
        }   // overloaded operator !=
        #endregion  // Overloaded Operators


        #region Implicit Cast Operators
        /// <summary>
        /// Cast a BitArray32 object to its underlying type, UInt32.
        /// </summary>
        /// <param name="pMask">
        /// The BitArray32 to be cast to a UInt32.
        /// </param>
        /// <returns>
        /// A reference to its input variable, cast to a UInt32.
        /// </returns>
        public static implicit operator UInt32 ( BitArray32 pMask )
        {
            return pMask._uintBitMask;
        }   // public static implicit operator UInt32


        /// <summary>
        /// Cast a Uint32, the underlying type of a BitArray32, to an object of
        /// type BitArray32.
        /// </summary>
        /// <param name="pUInt32">
        /// The Uint32 to be cast to a BitArray32.
        /// </param>
        /// <returns>
        /// A reference to its input variable, cast to a BitArray32.
        /// </returns>
        public static implicit operator BitArray32 ( UInt32 pUInt32 )
        {
            return new BitArray32 ( pUInt32 );
        }   // public static implicit operator BitArray32
        #endregion  // Implicit Conversion Operators
	}   // class BitArray32
}   // partial namespace WizardWrx