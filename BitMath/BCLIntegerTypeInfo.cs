/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         BCLIntegerTypeInfo

    File Name:          BCLIntegerTypeInfo.cs

    Synopsis            Instances of this class store properties of the BCK 
						(Base Class Library) integer data types that are related
						to their capacity in bits.

    Remarks:            1)	System.Byte is classified as an integral type that
							stores an 8 bit unsigned integer.

						2)	Although the class is publicly accessible, none of
							its constructors is, because the reason for making
							it public is to permit the static array of instances
							in the BitHelpers class to be made publicly
							accessible through its static InfoForType method.

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

    Date Written:       Wednesday, 27 April 2016

    ---------------------------------------------------------------------------
    Revision History
    ---------------------------------------------------------------------------

    Date       Version  By  Synopsis
    ---------- -------- --- ----------------------------------------------------
	2016/04/29 4.0      DAG This class makes its debut.

	2016/05/04 4.3      DAG Promote this class to Public accessibility, and fix
                            an error in the format control string of its
                            ToString override, which escaped testing until I did
                            the promotion, and used it indirectly in the test
                            report.
    ============================================================================
*/

using System;


namespace WizardWrx
{
	/// <summary>
	/// Instances of this type are built into the program as a static read-only
	/// array, which is incorporated into the static BitHelpers class for use by
	/// its static methods, all of which are internal to this assembly.
	/// </summary>
	public class BCLIntegerTypeInfo
	{
		#region Private and Internal Members
		private Type _typBCLType;
		private int _intMaximumDecimalDigits;
		private int _intBytesOccupied;
		private int _intBitsCapacity;
		private bool _fIsSigned;

		/// <summary>
		/// The default constructor is defined and marked private, to enforce
		/// creation of populated instances. Since instances are essentially
		/// baked into the program, this is pretty much a moot point.
		/// </summary>
		/// <remarks>
		/// Though all but one will almost certainly go unused, this class 
		/// overrides every method on its implicit base class, System.Object. In
		/// particular, the two comparison methods, CompareTo and Equals, are
		/// unlikely to see action because the assembly exposes a complete array
		/// of these objects that is presorted and accessible through public
		/// subscript constants and a public access method that returns the
		/// object that corresponds to a specified type.
		/// </remarks>
		private BCLIntegerTypeInfo ( )
		{
		}	// BCLIntegerTypeInfo constructor (1 of 2)


		/// <summary>
		/// The alternate constructor creates a populated instance.
		/// </summary>
		/// <param name="ptypBCLTypeInfo">
		/// Specify the System.Type that corresponds to the specified GUID.
		/// </param>
		/// <param name="pintMaxDigits">
		/// Specify the maximum number of decimal digits that the type can
		/// express.
		/// </param>
		/// <param name="pintBytesOccupied">
		/// Specify the number of bytes of machine memory occupied by instances.
		/// </param>
		/// <param name="pintBitsCapacity">
		/// Specify the number of bits that can be stored in instances. For the
		/// purpose of this exercise, the sign is treated as just another bit.
		/// </param>
		/// <param name="pfIsSigned">
		/// Specify TRUE to indicate that the type has a sign, otherwise FALSE.
		/// </param>
		internal BCLIntegerTypeInfo (
			Type ptypBCLTypeInfo ,
			int pintMaxDigits ,
			int pintBytesOccupied ,
			int pintBitsCapacity ,
			bool pfIsSigned )
		{
			_typBCLType = ptypBCLTypeInfo;
			_intMaximumDecimalDigits = pintMaxDigits;
			_intBytesOccupied = pintBytesOccupied;
			_intBitsCapacity = pintBitsCapacity;
			_fIsSigned = pfIsSigned;
		}	// BCLIntegerTypeInfo constructor (2 of 2)
		#endregion	// Private and Internal Members


		#region Public Properties
		/// <summary>
		/// This property gets the actual System.Type of the integral type
		/// represented by this instance.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public Type BCLType
#else
		internal Type BCLType
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _typBCLType;
			}	// BCLType property getter
		}	// read-only Type BCLType property


		/// <summary>
		/// This property gets the maximum number of decimal digits that can be
		/// represented by this type.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public int MaximumDecimalDigits
#else
		internal int MaximumDecimalDigits
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _intMaximumDecimalDigits;
			}	// MaximumDecimalDigits property getter
		}	// read-only int MaximumDecimalDigits property


		/// <summary>
		/// This property gets the number of bytes occupied by an instance of
		/// this type in machine memory or on disk.
		/// </summary>
#if BCLINTEGERTYPEINFO_PUBLIC
		public int RequiredStorageBytes
#else
		internal int RequiredStorageBytes
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _intBytesOccupied;
			}	// RequiredStorageBytes property getter
		}	// read-only int RequiredStorageBytes property


		/// <summary>
		/// This property gets the number of bits that can be stored in an
		/// instance of this type. The sign, which occupies the most significant
		/// bit of signed integers, is included in the bit count, because it is
		/// just like any other bit when the integer is treated as a bit mask.
		/// </summary>
		/// <see cref="IsSigned"/>
#if BCLINTEGERTYPEINFO_PUBLIC
		public int CapacityInBits
#else
		internal int CapacityInBits
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _intBitsCapacity;
			}	// CapacityInBits property getter
		}	// read-only int CapacityInBits property


		/// <summary>
		/// This property returns TRUE if the integer represented by the
		/// instance has a sign. Otherwise, this property returns FALSE.
		/// </summary>
		/// <see cref="CapacityInBits"/>
#if BCLINTEGERTYPEINFO_PUBLIC
		public bool IsSigned
#else
		internal bool IsSigned
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _fIsSigned;
			}	// IsSigned property getter
		}	// read-only bool IsSigned property


		/// <summary>
		/// This property gets the raw GUID from the System.Type represented by
		/// the instance.
		/// </summary>
		/// <seealso cref="BCLType"/>
#if BCLINTEGERTYPEINFO_PUBLIC
		public Guid GUIDPerType
#else
		internal Guid GUIDPerType
#endif	// #if BCLINTEGERTYPEINFO_PUBLIC
		{
			get
			{
				return _typBCLType.GUID;
			}	// GUIDPerType property getter
		}	// read-only GUID GUIDPerType property
		#endregion	// Public Properties


		#region Overrides of Other Methods of Object
		/// <summary>
		/// This method overrides the Equal method on System.Object by
		/// evaluating the compact GUID strings if the other comparand is a
		/// BCLIntegerTypeInfo. In all other cases, this method returns FALSE.
		/// </summary>
		/// <param name="obj">
		/// Specify another BCLIntegerTypeInfo instance. A null reference or one
		/// to any other type is meaningless, and is covered by returning FALSE.
		/// </param>
		/// <returns>
		/// This method returns TRUE when both instances have identical BCLType
		/// properties. Otherwise, the return value is FALSE.
		/// </returns>
		public override bool Equals ( object obj )
		{
			if ( obj == null )
			{	// The null reference is never equal to an instance!
				return false;
			}	// TRUE (degenerate case) block, if ( obj == null )
			else
			{	// The next step depends on whether or not the objects are of like kind.
				if ( obj.GetType ( ) == this.GetType ( ) )
				{	// They are. Cast other to one of our kind, then call the Equals method on the compact GUID member.
					BCLIntegerTypeInfo bclOtherIntegerTypeInfo = ( BCLIntegerTypeInfo ) obj;
					return _typBCLType.Equals ( bclOtherIntegerTypeInfo._typBCLType );
				}	// TRUE (anticipated outcome) block, if ( obj.GetType ( ) == this.GetType ( ) )
				else
				{	// Objects of different kinds are implicitly unequal.
					return false;
				}	// FALSE (unanticipated outcome) block, if ( obj.GetType ( ) == this.GetType ( ) )
			}	// FALSE (anticipated case) block, if ( obj == null )				
		}	// public override bool Equals


		/// <summary>
		/// This method overrides the default GetHashCode implementation in the
		/// base class by substituting the hash code of its BCLType property.
		/// </summary>
		/// <returns>
		/// The return value is the hash code of the BCLType property of the
		/// instance.
		/// </returns>
		public override int GetHashCode ( )
		{
			return _typBCLType.GetHashCode ( );
		}	// public override int GetHashCode


		/// <summary>
		/// This method overrides the default ToString method on System.Object
		/// to deliver a compact rendering of the properties of the instance.
		/// </summary>
		/// <returns>
		/// The return value is a compact formatted rendering of every property
		/// of the instance, including the raw GUID, which is extracted from the
		/// System.Type reference stored in the instance.
		/// </returns>
		public override string ToString ( )
		{
			return string.Format (
				Properties.Resources.BCLI_TOSTRING_TEMPLATE ,					// Format Control String
				new object [ ] {												// Array of substitution tokens, cast to their lowest common denominator
					_typBCLType ,												// Format Item 0 = System.Type of the integral type represented by the instance
					_intMaximumDecimalDigits ,									// Format Item 1 = Maximum number of digits representable by instances of this System.Type
					_intBytesOccupied ,											// Format Item 2 = Number of bytes required to store instances of this System.Type
					_intBitsCapacity ,											// Format Item 3 = Number of bits representable by instances of this System.Type
					_fIsSigned ,												// Format Item 4 = TRUE if instances of this System.Type are treated as signed integers
					_typBCLType.GUID } );										// Format Item 5 = The raw GUID extracted from the BCLType property of the instance
		}	// public override string ToString
		#endregion	// Overrides of Other Methods of Object
	}	// internal class BCLIntegerTypeInfo
}	// partial namespace WizardWrx