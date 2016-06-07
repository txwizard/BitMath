/*
    ============================================================================

    Namespace:          TestStand

    Class Name:         Util

    File Name:          Util.cs

    Synopsis:           This static class is a container for utility routines
                        intended for general consumption.

    Remarks:            Since TimeZoneInfo was added to Microsoft .NET Framework
                        version 3.5, any assembly that incorporates this class
                        must be built against that framework or higher.

                        This class was created around GetRegMultiStringValue, a
                        DllUtlity routine for gracefully handling REG_MULTI_SZ
                        values stored in Windows Registry keys. Putting them in
                        a separate class leaves me with the option of moving the
                        code into a class library.

	Reference:			"Json.net fails when trying to Deserialize a Class that Inherits from Exception,"
						http://stackoverflow.com/questions/14186000/json-net-fails-when-trying-to-deserialize-a-class-that-inherits-from-exception#new-answer

    Author:             David A. Gray

    License:            Copyright (C) 2014-2016, David A. Gray. 
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

    Created:            Friday, 29 August 2014 - Wednesday, 03 September 2014

    ----------------------------------------------------------------------------
    Revision History
    ----------------------------------------------------------------------------

    Date       Version Author Description
    ---------- ------- ------ --------------------------------------------------
	2016/05/01 4.0     DAG	  Move a cut down version of this class into this
                              assembly.

	2016/05/03 4.2     DAG	  Define a constant for the second element in an
                              array or other zero based index, and eliminate
                              unused assembly references.

	2016/05/04 4.3     DAG    Incorporate ShowKeyAssemblyProperties, copied from
							  WizardWrx.DllServices2.Util, as a public method,
							  adding GetAssemblyGuidStrin as a private method to
                              support it, and remove an unreferenced method,
							  GetInternalResourceName, that came along for the
							  ride when I copied several routines from the same
							  module for version 4.0.
    ============================================================================
*/


using System;
using System.Reflection;
using System.Text;


using WizardWrx;


namespace TestStand
{
    /// <summary>
    /// This static class exposes DllUtlity constants and methods that run the
    /// gamut from syntactic sugar to functions that hide useful, but somewhat
    /// obscure capabilities of the Microsoft Base Class Library.
	/// 
	/// Since static classes are implicitly sealed, this class cannot be inherited.
	/// </summary>
    public static class Util
	{
		#region Public Constants
		/// <summary>
		/// Designate the first element of an array.
		/// </summary>
		public const int ARRAY_FIRST_ELEMENT = 0;


		/// <summary>
		/// Add to a subscript to derive the ordinal.
		/// </summary>
		public const int ARRAY_ORDINAL_FROM_SUBSCRIPT = 1;


		/// <summary>
		/// Designate the second element of an array.
		/// </summary>
		public const int ARRAY_SECOND_ELEMENT = 1;


		/// <summary>
		/// Subtract from an ordinal to derive the subscript.
		/// </summary>
		public const int ARRAY_SUBSCRIPT_FROM_ORDINAL = 1;


		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// insert a space between every 4th byte.
		/// </summary>
		public const uint BYTES_TO_STRING_BLOCK_OF_4 = 4;


		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// insert a space between every 8th byte.
		/// </summary>
		public const uint BYTES_TO_STRING_BLOCK_OF_8 = 8;


		/// <summary>
		/// Use this to set ByteArrayToHexDigitString argument puintGroupSize to
		/// format the string without any spaces.
		/// </summary>
		/// <remarks>
		/// This constant is intended primarily for internal use by the first
		/// overload, which omits the second argument, to call the second
		/// overload, which does the work.
		/// </remarks>
		public const uint BYTES_TO_STRING_NO_SPACING = 0;

		public const int EMPTY_STRING_LENGTH = 0;

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as an arbitrary string of hexadecimal digits.
		/// </summary>
		public const string HEXADECIMAL = @"X";

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as a string of 2 hexadecimal digits.
		/// </summary>
		public const string HEXADECIMAL_2 = @"X2";

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as a string of 4 hexadecimal digits.
		/// </summary>
		public const string HEXADECIMAL_4 = @"X4";

		/// <summary>
		/// Pass this constant to the ToString method on any integral type to
		/// format it as a string of 8 hexadecimal digits.
		/// </summary>
		public const string HEXADECIMAL_8 = @"X8";

		/// <summary>
		/// Substitute this into a format string as a prefix to a hexadecimal
		/// number display. This string renders exactly as shown, 0h.
		/// </summary>
		public const string HEXADECIMAL_PREFIX_0H_LC = @"0h";

		/// <summary>
		/// Substitute this into a format string as a prefix to a hexadecimal
		/// number display. This string renders exactly as shown, 0H.
		/// </summary>
		public const string HEXADECIMAL_PREFIX_0H_UC = @"0H";

		/// <summary>
		/// Substitute this into a format string as a prefix to a hexadecimal
		/// number display. This string renders exactly as shown, 0x.
		/// </summary>
		public const string HEXADECIMAL_PREFIX_0X_LC = @"0x";

		/// <summary>
		/// Substitute this into a format string as a prefix to a hexadecimal
		/// number display. This string renders exactly as shown, 0X.
		/// </summary>
		public const string HEXADECIMAL_PREFIX_0X_UC = @"0X";

		/// <summary>
		/// The ubiquitous, ambiguous space character always gets a constant.
		/// </summary>
		public const char SPACE_CHAR = ' ';
		#endregion	// Public Constants


		#region Public Methods
		/// <summary>
		/// Convert a byte array into a printable hexadecimal representation.
		/// </summary>
		/// <param name="pbytInputData">
		/// Specify the byte array to be formatted. Any byte array will do.
		/// </param>
		/// <returns>
		/// The return value is a string that contains two characters for each
		/// byte in the array.
		/// </returns>
		/// <summary>
		/// Return a stack trace with its subsequent lines lengthened by
		/// appending a specified number of spaces so that they align vertically
		/// with the first line in the printed output.
		/// </summary>
		/// <param name="pstrRawStacktrace">
		/// Specify the string that contains the raw stack trace, as it appears
		/// on the exception being reported.
		/// </param>
		/// <param name="pintAdditionalWhiteSpaceChars">
		/// Specify the number of characters required to align the word "at" in
		/// each subsequent line with the first one, taking into account the
		/// number of characters occupied by its label.
		/// </param>
		/// <returns>
		/// The return value is a new string, ready to drop into the format.
		/// </returns>
		/// <remarks>
		/// The design of this method assumes that the stack trace is displayed
		/// on a new line, and that argument pstrRawStacktrace is a reference to
		/// the StackTrace property on an Exception object or a derivative
		/// thereof.
		/// </remarks>
		internal static string BeautifyStackTrace (
			string pstrRawStacktrace ,
			int pintAdditionalWhiteSpaceChars )
		{
			const string ITEM_LEADER = @"   at ";

			return pstrRawStacktrace.Replace (
				string.Concat (
					Environment.NewLine ,
					ITEM_LEADER ) ,
				string.Concat (
					Environment.NewLine ,
					new string ( SPACE_CHAR , pintAdditionalWhiteSpaceChars ) ,
					ITEM_LEADER ) );
		}	// internal static string BeautifyStackTrace


		/// <summary>
		/// This method prepends a specified number of spaces to each of the
		/// subsequent lines of its input string.
		/// </summary>
		/// <param name="pstrExceptionMessage">
		/// Though intended for use on the Message property of an Exception, it
		/// can be applied to any string.
		/// </param>
		/// <param name="pintAdditionalWhiteSpaceChars">
		/// Specify the number of leading spaces to insert after each newline.
		/// </param>
		/// <returns>
		/// The returned string has each of its subsequent lines lengthened by a
		/// specified number of characters, which are inserted at the beginning
		/// of each line. The first line is left as is.
		/// </returns>
		internal static string BeautifyExceptionMessage (
			string pstrExceptionMessage ,
			int pintAdditionalWhiteSpaceChars )
		{
			return pstrExceptionMessage.Replace (
				Environment.NewLine ,
				string.Concat (
					Environment.NewLine ,
					new string (
						SPACE_CHAR ,
						pintAdditionalWhiteSpaceChars ) ) );
		}	// internal static string BeautifyExceptionMessage


		public static string ByteArrayToHexDigitString ( byte [ ] pbytInputData )
		{
			return ByteArrayToHexDigitString (
				pbytInputData ,
				BYTES_TO_STRING_NO_SPACING );
		}   // ByteArrayToHexDigitString (1 of 2)


		/// <summary>
		/// Convert a byte array into a printable hexadecimal representation.
		/// </summary>
		/// <param name="pbytInputData">
		/// Specify the byte array to be formatted. Any byte array will do.
		/// </param>
		/// <param name="puintGroupSize">
		/// Specify the number of bytes to display as a group.
		/// </param>
		/// <returns>
		/// The return value is a string that contains two characters for each
		/// byte in the array, plus one space between every puintGroupSizeth
		/// byte.
		/// </returns>
		public static string ByteArrayToHexDigitString (
			byte [ ] pbytInputData ,
			uint puintGroupSize )
		{
			StringBuilder sbOutput = new StringBuilder ( pbytInputData.Length );

			//  ----------------------------------------------------------------
			//	Loop through each byte of the hashed data, and format each one
			//	as a hexadecimal string. Although this For loop will never
			//	contain more than one statement, I left the braces to separate
			//	that statement from the third line of the For statement, which I
			//	spread across three lines because of its length.
			//  ----------------------------------------------------------------

			for ( int intOffset = ARRAY_FIRST_ELEMENT ;
				  intOffset < pbytInputData.Length ;
				  intOffset++ )
			{
				if ( puintGroupSize > 0 && intOffset > 0 && intOffset % puintGroupSize == 0 )
					sbOutput.Append ( SPACE_CHAR );

				sbOutput.Append ( pbytInputData [ intOffset ].ToString ( HEXADECIMAL_2 ).ToLowerInvariant ( ) );
			}	//	for ( int intOffset = ARRAY_FIRST_ELEMENT ; ...

			return sbOutput.ToString ( );		//	Return the hexadecimal string.
		}   // ByteArrayToHexDigitString (2 of 2)
		/// <summary>
		/// List selected properties of any assembly on a console.
		/// </summary>
		/// <param name="pmyLib">
		/// Pass in a reference to the desired assembly, which may be the
		/// assembly that exports a specified type, the executing assembly, the
		/// calling assembly, the entry assembly, or any other assembly for
		/// which you can obtain a reference.
		/// </param>
		public static void ShowKeyAssemblyProperties ( System.Reflection.Assembly pmyLib )
		{
			System.Reflection.AssemblyName MyNameIs = System.Reflection.AssemblyName.GetAssemblyName ( pmyLib.Location );
			System.Diagnostics.FileVersionInfo myVersionInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo ( pmyLib.Location );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_SELECTED_DLL_PROPS_BEGIN , Environment.NewLine );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYFILEBASENAME , System.IO.Path.GetFileNameWithoutExtension ( pmyLib.Location ) );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_VERSIONSTRING , myVersionInfo.FileVersion );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_CULTURE , MyNameIs.CultureInfo.DisplayName );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_PUBLICKEYTOKEN , Util.ByteArrayToHexDigitString ( MyNameIs.GetPublicKeyToken ( ) ) );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_RUNTIME_VERSION , pmyLib.ImageRuntimeVersion );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYGUIDSTRING , GetAssemblyGuidString ( pmyLib ) );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_PRODUCTNAME , myVersionInfo.ProductName );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_LEGALCOPYRIGHT , myVersionInfo.LegalCopyright );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_LEGALTRADEMARKS , myVersionInfo.LegalTrademarks );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_COMPANYNAME , myVersionInfo.CompanyName );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_DESCRIPTION , myVersionInfo.FileDescription );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_COMMENTS , myVersionInfo.Comments , Environment.NewLine );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBYDIRNAME , System.IO.Path.GetDirectoryName ( pmyLib.Location ) );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_ASSEMBLYFILENAME , System.IO.Path.GetFileName ( pmyLib.Location ) , Environment.NewLine );

			string strAssemblyFileFQFN = pmyLib.Location;
			System.IO.FileInfo fiLibraryFile = new System.IO.FileInfo ( strAssemblyFileFQFN );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_FILE_CREATION_DATE , fiLibraryFile.CreationTime , fiLibraryFile.CreationTimeUtc );
			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_FILE_MODIFIED_DATE , fiLibraryFile.LastWriteTime , fiLibraryFile.LastWriteTimeUtc );

			Console.WriteLine ( Properties.Resources.MSG_ASM_PROPS_SELECTED_DLL_PROPS_END , Environment.NewLine );
		}   // private static void ShowKeAssemblyProperties method
		#endregion	// Public Static Methods


		#region Private Static Methods
		/// <summary>
        /// Use the list of Manifest Resource Names returned by method
        /// GetManifestResourceNames on a specified assembly. Each of several
        /// methods employs a different mechanism to identify the assembly of
        /// interest.
        /// </summary>
        /// <param name="pstrResourceName">
        /// Specify the name of the file from which the embedded resource was
        /// created. Typically, this will be the local name of the file in the
        /// source code tree.
        /// </param>
        /// <param name="pasmSource">
        /// Pass a reference to the Assembly that is supposed to contain the
        /// desired resource.
        /// </param>
        /// <returns>
        /// If the function succeeds, the return value is the internal name of
        /// the requested resource, which is fed to GetManifestResourceStream on
        /// the same assembly, which returns a read-only Stream backed by the
        /// embedded resource. If the specified resource is not found, it
        /// returns null.
        /// </returns>
        /// <remarks>
        /// Since I cannot imagine any use for this method beyond its
        /// infrastructure role in this class, I marked it private.
        /// </remarks>
		/// <summary>
		/// Get the GUID string (Registry format) attached to an assembly.
		/// </summary>
		/// <param name="pasm">
		/// Assembly from which to return the GUID string.
		/// </param>
		/// <returns>
		/// If the method succeeds, the return value is the GUID attached to it
		/// and intended to be associated with its type library if the assembly
		/// is exposed to COM.
		/// </returns>
		private static string GetAssemblyGuidString ( Assembly pasm )
		{
			object [ ] objAttribs = pasm.GetCustomAttributes (
				typeof ( System.Runtime.InteropServices.GuidAttribute ) ,
				false );

			if ( objAttribs.Length > EMPTY_STRING_LENGTH )
			{
				System.Runtime.InteropServices.GuidAttribute oMyGuid = ( System.Runtime.InteropServices.GuidAttribute ) objAttribs [ ARRAY_FIRST_ELEMENT ];
				return oMyGuid.Value.ToString ( );
			}   // TRUE (anticipated outcome) block, if ( objAttribs.Length > ListInfo.EMPTY_STRING_LENGTH )
			else
			{
				return string.Empty;
			}   // FALSE (UNanticipated outcome) block, if ( objAttribs.Length > ListInfo.EMPTY_STRING_LENGTH )
		}   // GetAssemblyGuidStrin
		#endregion  //  Private Static Methods
	}   // public static class Util
}   // partial namespace TestStand