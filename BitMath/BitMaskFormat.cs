/*
    ============================================================================

    Namespace:          WizardWrx

    Class Name:         BitMaskFormat

    File Name:          BitMaskFormat.cs

    Synopsis            It appears that custom formatting requires a class that
						implements the IFormatProvider, ICustomFormatter
						interfaces.

    Remarks:            I knew there was a good reason to put those new bit mask
						formatting routines into a new static class!

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
using System.Globalization;

namespace WizardWrx
{
	/// <summary>
	/// A default instance of this class is fed into an overload of 
	/// string.Format to render the bit array.
	/// </summary>
	public class BitMaskFormat : IFormatProvider , ICustomFormatter
	{
		#region ICustomFormatter Members
		/// <summary>
		/// Format integral types as arrays of bits. Other types are handed off
		/// to the system formatting engine.
		/// </summary>
		/// <param name="format">
		/// Specify the format string to apply. This method supports a custom
		/// format code, B, and the standard G code.
		/// </param>
		/// <param name="arg">
		/// Specify the integral type to format.
		/// </param>
		/// <param name="formatProvider">
		/// Specify the accompanying format provider.
		/// </param>
		/// <returns>
		/// If the method succeeds and the format code is B, the return value is
		/// a string that represents the integral type as a string of bits, 
		/// listed from most significant to least.
		/// 
		/// If the method succeeds and the format code is G, the return value is
		/// a hexadecimal representation of the integer, sufficiently padded to
		/// hold the maximum value supported by an integer of the specified type.
		/// 
		/// All other cases are handed over to the default formatting engine and
		/// the outcome is undefined, since it depends upon the behavior of a
		/// component that is outside of my control.
		/// </returns>
		string ICustomFormatter.Format (
			string format ,
			object arg ,
			IFormatProvider formatProvider )
		{
			try
			{
				if ( BitHelpers.InfoForIntegralType ( arg.GetType ( ) ) != null )
				{
					return BitHelpers.FormatBitMask (
						arg ,
						format );
				}	// TRUE (anticipated outcome) block, if ( BitHelpers.InfoForType ( arg.GetType ( ) ) != null )
				else
				{
					if ( arg is IFormattable )
					{	// Arg is a non-integral type that implements the IFormattable interface. 
						return ( ( IFormattable ) arg ).ToString (
							format ,
							CultureInfo.CurrentCulture );
					}	// if ( arg is IFormattable )
					else if ( arg != null )
					{	// Arg isn't null, but it doesn't implement IFormattable.
						return arg.ToString ( );
					}	// else if ( arg != null )
					else
					{	// Arg isn't an integer, doesn't implement IFormattable, but is a null reference.
						return string.Empty;
					}	// else neither ( arg is IFormattable ), nor ( arg != null ) is true.
				}	// FALSE (degenerate case) block, if ( BitHelpers.InfoForType ( arg.GetType ( ) ) != null )
			}
			catch ( FormatException exFormat )
			{
				throw new FormatException (
					EnrichedFormatException (
						format ,
						arg ) ,
					exFormat );
			}
			catch ( ArgumentException exBadArg )
			{
				throw new FormatException (
					EnrichedFormatException (
						format ,
						arg ) ,
					exBadArg );
			}
			catch ( Exception exAllOtherKinds )
			{
				throw new FormatException (
					EnrichedFormatException (
						format ,
						arg ) ,
					exAllOtherKinds );
			}
		}	// public string ICustomFormatter.Format


		/// <summary>
		/// Construct my own FormatException exception that includes the format
		/// string and its intended argument.
		/// </summary>
		/// <param name="pstrFormatString">
		/// Specify the format string to apply. This method supports a custom
		/// format code, B, and the standard G code.
		/// </param>
		/// <param name="pobjArgument">
		/// Specify the integral type to format.
		/// </param>
		/// <returns>
		/// The return value becomes the Message of a new FormatException
		/// exception to which the original FormatException exception is
		/// attached, so that nothing is lost, especially the original stack
		/// trace.
		/// </returns>
		private string EnrichedFormatException (
			string pstrFormatString ,
			object pobjArgument )
		{
			return string.Format (
				Properties.Resources.ERRMSG_ENRICED_FORMAT_EXCEPTION ,			// Format control string, read from the DLL resource table
				pstrFormatString ,												// The format string that gave rise to the exception
				pobjArgument ,													// The object that was intended to be formatted, rendered in its default format
				Environment.NewLine );											// Embedded Newline
		}	// private string EnrichedFormatException
		#endregion	// ICustomFormatter Members


		#region IFormatProvider Members
		/// <summary>
		/// This method implements the IFormatProvider provider interface on
		/// behalf of the BitMaskFormat class.
		/// </summary>
		/// <param name="formatType">
		/// Provided that the type of formatType is ICustomFormatter, return the
		/// instance of BitMaskFormat on behalf of which this method was called.
		/// </param>
		/// <returns>
		/// If formatType is valid, the return value is a reference to the
		/// instance of BitMaskFormat on behalf of which this method was called.
		/// Otherwise, a null reference is returned.
		/// </returns>
		object IFormatProvider.GetFormat ( Type formatType )
		{
			if ( formatType == typeof ( ICustomFormatter ) )
				return this;
			else
				return null;
		}	// public object IFormatProvider.GetFormat
		#endregion	// IFormatProvider Members
	}	// public class BitMaskFormat
}	// partial namespace WizardWrx