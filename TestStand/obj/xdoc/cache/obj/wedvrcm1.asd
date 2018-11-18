id: BithMathTestStand
language: CSharp
name:
  Default: BithMathTestStand
qualifiedName:
  Default: BithMathTestStand
type: Assembly
modifiers: {}
items:
- id: TestStand
  commentId: N:TestStand
  language: CSharp
  name:
    CSharp: TestStand
    VB: TestStand
  nameWithType:
    CSharp: TestStand
    VB: TestStand
  qualifiedName:
    CSharp: TestStand
    VB: TestStand
  type: Namespace
  assemblies:
  - BithMathTestStand
  modifiers: {}
  items:
  - id: TestStand.Util
    commentId: T:TestStand.Util
    language: CSharp
    name:
      CSharp: Util
      VB: Util
    nameWithType:
      CSharp: Util
      VB: Util
    qualifiedName:
      CSharp: TestStand.Util
      VB: TestStand.Util
    type: Class
    assemblies:
    - BithMathTestStand
    namespace: TestStand
    source:
      remote:
        path: TestStand/Util.cs
        branch: master
        repo: https://github.com/txwizard/BitMath.git
      id: Util
      path: ../TestStand/Util.cs
      startLine: 106
    summary: "\nThis static class exposes DllUtlity constants and methods that run the\ngamut from syntactic sugar to functions that hide useful, but somewhat\nobscure capabilities of the Microsoft Base Class Library.\n\nSince static classes are implicitly sealed, this class cannot be inherited.\n"
    example: []
    syntax:
      content:
        CSharp: public static class Util
        VB: Public Module Util
    inheritance:
    - System.Object
    inheritedMembers:
    - System.Object.ToString
    - System.Object.Equals(System.Object)
    - System.Object.Equals(System.Object,System.Object)
    - System.Object.ReferenceEquals(System.Object,System.Object)
    - System.Object.GetHashCode
    - System.Object.GetType
    - System.Object.MemberwiseClone
    modifiers:
      CSharp:
      - public
      - static
      - class
      VB:
      - Public
      - Module
    items:
    - id: TestStand.Util.ARRAY_FIRST_ELEMENT
      commentId: F:TestStand.Util.ARRAY_FIRST_ELEMENT
      language: CSharp
      name:
        CSharp: ARRAY_FIRST_ELEMENT
        VB: ARRAY_FIRST_ELEMENT
      nameWithType:
        CSharp: Util.ARRAY_FIRST_ELEMENT
        VB: Util.ARRAY_FIRST_ELEMENT
      qualifiedName:
        CSharp: TestStand.Util.ARRAY_FIRST_ELEMENT
        VB: TestStand.Util.ARRAY_FIRST_ELEMENT
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ARRAY_FIRST_ELEMENT
        path: ../TestStand/Util.cs
        startLine: 112
      summary: "\nDesignate the first element of an array.\n"
      example: []
      syntax:
        content:
          CSharp: public const int ARRAY_FIRST_ELEMENT = 0
          VB: Public Const ARRAY_FIRST_ELEMENT As Integer = 0
        return:
          type: System.Int32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
      commentId: F:TestStand.Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
      language: CSharp
      name:
        CSharp: ARRAY_ORDINAL_FROM_SUBSCRIPT
        VB: ARRAY_ORDINAL_FROM_SUBSCRIPT
      nameWithType:
        CSharp: Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
        VB: Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
      qualifiedName:
        CSharp: TestStand.Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
        VB: TestStand.Util.ARRAY_ORDINAL_FROM_SUBSCRIPT
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ARRAY_ORDINAL_FROM_SUBSCRIPT
        path: ../TestStand/Util.cs
        startLine: 118
      summary: "\nAdd to a subscript to derive the ordinal.\n"
      example: []
      syntax:
        content:
          CSharp: public const int ARRAY_ORDINAL_FROM_SUBSCRIPT = 1
          VB: Public Const ARRAY_ORDINAL_FROM_SUBSCRIPT As Integer = 1
        return:
          type: System.Int32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.ARRAY_SECOND_ELEMENT
      commentId: F:TestStand.Util.ARRAY_SECOND_ELEMENT
      language: CSharp
      name:
        CSharp: ARRAY_SECOND_ELEMENT
        VB: ARRAY_SECOND_ELEMENT
      nameWithType:
        CSharp: Util.ARRAY_SECOND_ELEMENT
        VB: Util.ARRAY_SECOND_ELEMENT
      qualifiedName:
        CSharp: TestStand.Util.ARRAY_SECOND_ELEMENT
        VB: TestStand.Util.ARRAY_SECOND_ELEMENT
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ARRAY_SECOND_ELEMENT
        path: ../TestStand/Util.cs
        startLine: 124
      summary: "\nDesignate the second element of an array.\n"
      example: []
      syntax:
        content:
          CSharp: public const int ARRAY_SECOND_ELEMENT = 1
          VB: Public Const ARRAY_SECOND_ELEMENT As Integer = 1
        return:
          type: System.Int32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
      commentId: F:TestStand.Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
      language: CSharp
      name:
        CSharp: ARRAY_SUBSCRIPT_FROM_ORDINAL
        VB: ARRAY_SUBSCRIPT_FROM_ORDINAL
      nameWithType:
        CSharp: Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
        VB: Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
      qualifiedName:
        CSharp: TestStand.Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
        VB: TestStand.Util.ARRAY_SUBSCRIPT_FROM_ORDINAL
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ARRAY_SUBSCRIPT_FROM_ORDINAL
        path: ../TestStand/Util.cs
        startLine: 130
      summary: "\nSubtract from an ordinal to derive the subscript.\n"
      example: []
      syntax:
        content:
          CSharp: public const int ARRAY_SUBSCRIPT_FROM_ORDINAL = 1
          VB: Public Const ARRAY_SUBSCRIPT_FROM_ORDINAL As Integer = 1
        return:
          type: System.Int32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_4
      commentId: F:TestStand.Util.BYTES_TO_STRING_BLOCK_OF_4
      language: CSharp
      name:
        CSharp: BYTES_TO_STRING_BLOCK_OF_4
        VB: BYTES_TO_STRING_BLOCK_OF_4
      nameWithType:
        CSharp: Util.BYTES_TO_STRING_BLOCK_OF_4
        VB: Util.BYTES_TO_STRING_BLOCK_OF_4
      qualifiedName:
        CSharp: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_4
        VB: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_4
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: BYTES_TO_STRING_BLOCK_OF_4
        path: ../TestStand/Util.cs
        startLine: 137
      summary: "\nUse this to set ByteArrayToHexDigitString argument puintGroupSize to\ninsert a space between every 4th byte.\n"
      example: []
      syntax:
        content:
          CSharp: public const uint BYTES_TO_STRING_BLOCK_OF_4 = 4U
          VB: Public Const BYTES_TO_STRING_BLOCK_OF_4 As UInteger = 4UI
        return:
          type: System.UInt32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_8
      commentId: F:TestStand.Util.BYTES_TO_STRING_BLOCK_OF_8
      language: CSharp
      name:
        CSharp: BYTES_TO_STRING_BLOCK_OF_8
        VB: BYTES_TO_STRING_BLOCK_OF_8
      nameWithType:
        CSharp: Util.BYTES_TO_STRING_BLOCK_OF_8
        VB: Util.BYTES_TO_STRING_BLOCK_OF_8
      qualifiedName:
        CSharp: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_8
        VB: TestStand.Util.BYTES_TO_STRING_BLOCK_OF_8
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: BYTES_TO_STRING_BLOCK_OF_8
        path: ../TestStand/Util.cs
        startLine: 144
      summary: "\nUse this to set ByteArrayToHexDigitString argument puintGroupSize to\ninsert a space between every 8th byte.\n"
      example: []
      syntax:
        content:
          CSharp: public const uint BYTES_TO_STRING_BLOCK_OF_8 = 8U
          VB: Public Const BYTES_TO_STRING_BLOCK_OF_8 As UInteger = 8UI
        return:
          type: System.UInt32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.BYTES_TO_STRING_NO_SPACING
      commentId: F:TestStand.Util.BYTES_TO_STRING_NO_SPACING
      language: CSharp
      name:
        CSharp: BYTES_TO_STRING_NO_SPACING
        VB: BYTES_TO_STRING_NO_SPACING
      nameWithType:
        CSharp: Util.BYTES_TO_STRING_NO_SPACING
        VB: Util.BYTES_TO_STRING_NO_SPACING
      qualifiedName:
        CSharp: TestStand.Util.BYTES_TO_STRING_NO_SPACING
        VB: TestStand.Util.BYTES_TO_STRING_NO_SPACING
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: BYTES_TO_STRING_NO_SPACING
        path: ../TestStand/Util.cs
        startLine: 156
      summary: "\nUse this to set ByteArrayToHexDigitString argument puintGroupSize to\nformat the string without any spaces.\n"
      remarks: "\nThis constant is intended primarily for internal use by the first\noverload, which omits the second argument, to call the second\noverload, which does the work.\n"
      example: []
      syntax:
        content:
          CSharp: public const uint BYTES_TO_STRING_NO_SPACING = 0U
          VB: Public Const BYTES_TO_STRING_NO_SPACING As UInteger = 0UI
        return:
          type: System.UInt32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.EMPTY_STRING_LENGTH
      commentId: F:TestStand.Util.EMPTY_STRING_LENGTH
      language: CSharp
      name:
        CSharp: EMPTY_STRING_LENGTH
        VB: EMPTY_STRING_LENGTH
      nameWithType:
        CSharp: Util.EMPTY_STRING_LENGTH
        VB: Util.EMPTY_STRING_LENGTH
      qualifiedName:
        CSharp: TestStand.Util.EMPTY_STRING_LENGTH
        VB: TestStand.Util.EMPTY_STRING_LENGTH
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: EMPTY_STRING_LENGTH
        path: ../TestStand/Util.cs
        startLine: 158
      syntax:
        content:
          CSharp: public const int EMPTY_STRING_LENGTH = 0
          VB: Public Const EMPTY_STRING_LENGTH As Integer = 0
        return:
          type: System.Int32
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL
      commentId: F:TestStand.Util.HEXADECIMAL
      language: CSharp
      name:
        CSharp: HEXADECIMAL
        VB: HEXADECIMAL
      nameWithType:
        CSharp: Util.HEXADECIMAL
        VB: Util.HEXADECIMAL
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL
        VB: TestStand.Util.HEXADECIMAL
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL
        path: ../TestStand/Util.cs
        startLine: 164
      summary: "\nPass this constant to the ToString method on any integral type to\nformat it as an arbitrary string of hexadecimal digits.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL = "X"
          VB: Public Const HEXADECIMAL As String = "X"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_2
      commentId: F:TestStand.Util.HEXADECIMAL_2
      language: CSharp
      name:
        CSharp: HEXADECIMAL_2
        VB: HEXADECIMAL_2
      nameWithType:
        CSharp: Util.HEXADECIMAL_2
        VB: Util.HEXADECIMAL_2
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_2
        VB: TestStand.Util.HEXADECIMAL_2
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_2
        path: ../TestStand/Util.cs
        startLine: 170
      summary: "\nPass this constant to the ToString method on any integral type to\nformat it as a string of 2 hexadecimal digits.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_2 = "X2"
          VB: Public Const HEXADECIMAL_2 As String = "X2"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_4
      commentId: F:TestStand.Util.HEXADECIMAL_4
      language: CSharp
      name:
        CSharp: HEXADECIMAL_4
        VB: HEXADECIMAL_4
      nameWithType:
        CSharp: Util.HEXADECIMAL_4
        VB: Util.HEXADECIMAL_4
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_4
        VB: TestStand.Util.HEXADECIMAL_4
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_4
        path: ../TestStand/Util.cs
        startLine: 176
      summary: "\nPass this constant to the ToString method on any integral type to\nformat it as a string of 4 hexadecimal digits.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_4 = "X4"
          VB: Public Const HEXADECIMAL_4 As String = "X4"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_8
      commentId: F:TestStand.Util.HEXADECIMAL_8
      language: CSharp
      name:
        CSharp: HEXADECIMAL_8
        VB: HEXADECIMAL_8
      nameWithType:
        CSharp: Util.HEXADECIMAL_8
        VB: Util.HEXADECIMAL_8
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_8
        VB: TestStand.Util.HEXADECIMAL_8
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_8
        path: ../TestStand/Util.cs
        startLine: 182
      summary: "\nPass this constant to the ToString method on any integral type to\nformat it as a string of 8 hexadecimal digits.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_8 = "X8"
          VB: Public Const HEXADECIMAL_8 As String = "X8"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_PREFIX_0H_LC
      commentId: F:TestStand.Util.HEXADECIMAL_PREFIX_0H_LC
      language: CSharp
      name:
        CSharp: HEXADECIMAL_PREFIX_0H_LC
        VB: HEXADECIMAL_PREFIX_0H_LC
      nameWithType:
        CSharp: Util.HEXADECIMAL_PREFIX_0H_LC
        VB: Util.HEXADECIMAL_PREFIX_0H_LC
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_PREFIX_0H_LC
        VB: TestStand.Util.HEXADECIMAL_PREFIX_0H_LC
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_PREFIX_0H_LC
        path: ../TestStand/Util.cs
        startLine: 188
      summary: "\nSubstitute this into a format string as a prefix to a hexadecimal\nnumber display. This string renders exactly as shown, 0h.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_PREFIX_0H_LC = "0h"
          VB: Public Const HEXADECIMAL_PREFIX_0H_LC As String = "0h"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_PREFIX_0H_UC
      commentId: F:TestStand.Util.HEXADECIMAL_PREFIX_0H_UC
      language: CSharp
      name:
        CSharp: HEXADECIMAL_PREFIX_0H_UC
        VB: HEXADECIMAL_PREFIX_0H_UC
      nameWithType:
        CSharp: Util.HEXADECIMAL_PREFIX_0H_UC
        VB: Util.HEXADECIMAL_PREFIX_0H_UC
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_PREFIX_0H_UC
        VB: TestStand.Util.HEXADECIMAL_PREFIX_0H_UC
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_PREFIX_0H_UC
        path: ../TestStand/Util.cs
        startLine: 194
      summary: "\nSubstitute this into a format string as a prefix to a hexadecimal\nnumber display. This string renders exactly as shown, 0H.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_PREFIX_0H_UC = "0H"
          VB: Public Const HEXADECIMAL_PREFIX_0H_UC As String = "0H"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_PREFIX_0X_LC
      commentId: F:TestStand.Util.HEXADECIMAL_PREFIX_0X_LC
      language: CSharp
      name:
        CSharp: HEXADECIMAL_PREFIX_0X_LC
        VB: HEXADECIMAL_PREFIX_0X_LC
      nameWithType:
        CSharp: Util.HEXADECIMAL_PREFIX_0X_LC
        VB: Util.HEXADECIMAL_PREFIX_0X_LC
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_PREFIX_0X_LC
        VB: TestStand.Util.HEXADECIMAL_PREFIX_0X_LC
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_PREFIX_0X_LC
        path: ../TestStand/Util.cs
        startLine: 200
      summary: "\nSubstitute this into a format string as a prefix to a hexadecimal\nnumber display. This string renders exactly as shown, 0x.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_PREFIX_0X_LC = "0x"
          VB: Public Const HEXADECIMAL_PREFIX_0X_LC As String = "0x"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.HEXADECIMAL_PREFIX_0X_UC
      commentId: F:TestStand.Util.HEXADECIMAL_PREFIX_0X_UC
      language: CSharp
      name:
        CSharp: HEXADECIMAL_PREFIX_0X_UC
        VB: HEXADECIMAL_PREFIX_0X_UC
      nameWithType:
        CSharp: Util.HEXADECIMAL_PREFIX_0X_UC
        VB: Util.HEXADECIMAL_PREFIX_0X_UC
      qualifiedName:
        CSharp: TestStand.Util.HEXADECIMAL_PREFIX_0X_UC
        VB: TestStand.Util.HEXADECIMAL_PREFIX_0X_UC
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: HEXADECIMAL_PREFIX_0X_UC
        path: ../TestStand/Util.cs
        startLine: 206
      summary: "\nSubstitute this into a format string as a prefix to a hexadecimal\nnumber display. This string renders exactly as shown, 0X.\n"
      example: []
      syntax:
        content:
          CSharp: public const string HEXADECIMAL_PREFIX_0X_UC = "0X"
          VB: Public Const HEXADECIMAL_PREFIX_0X_UC As String = "0X"
        return:
          type: System.String
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.SPACE_CHAR
      commentId: F:TestStand.Util.SPACE_CHAR
      language: CSharp
      name:
        CSharp: SPACE_CHAR
        VB: SPACE_CHAR
      nameWithType:
        CSharp: Util.SPACE_CHAR
        VB: Util.SPACE_CHAR
      qualifiedName:
        CSharp: TestStand.Util.SPACE_CHAR
        VB: TestStand.Util.SPACE_CHAR
      type: Field
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: SPACE_CHAR
        path: ../TestStand/Util.cs
        startLine: 211
      summary: "\nThe ubiquitous, ambiguous space character always gets a constant.\n"
      example: []
      syntax:
        content:
          CSharp: public const char SPACE_CHAR = ' '
          VB: Public Const SPACE_CHAR As Char = " "c
        return:
          type: System.Char
      modifiers:
        CSharp:
        - public
        - const
        VB:
        - Public
        - Const
    - id: TestStand.Util.ByteArrayToHexDigitString(System.Byte[])
      commentId: M:TestStand.Util.ByteArrayToHexDigitString(System.Byte[])
      language: CSharp
      name:
        CSharp: ByteArrayToHexDigitString(Byte[])
        VB: ByteArrayToHexDigitString(Byte())
      nameWithType:
        CSharp: Util.ByteArrayToHexDigitString(Byte[])
        VB: Util.ByteArrayToHexDigitString(Byte())
      qualifiedName:
        CSharp: TestStand.Util.ByteArrayToHexDigitString(System.Byte[])
        VB: TestStand.Util.ByteArrayToHexDigitString(System.Byte())
      type: Method
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ByteArrayToHexDigitString
        path: ../TestStand/Util.cs
        startLine: 296
      syntax:
        content:
          CSharp: public static string ByteArrayToHexDigitString(byte[] pbytInputData)
          VB: Public Shared Function ByteArrayToHexDigitString(pbytInputData As Byte()) As String
        parameters:
        - id: pbytInputData
          type: System.Byte[]
        return:
          type: System.String
      overload: TestStand.Util.ByteArrayToHexDigitString*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: TestStand.Util.ByteArrayToHexDigitString(System.Byte[],System.UInt32)
      commentId: M:TestStand.Util.ByteArrayToHexDigitString(System.Byte[],System.UInt32)
      language: CSharp
      name:
        CSharp: ByteArrayToHexDigitString(Byte[], UInt32)
        VB: ByteArrayToHexDigitString(Byte(), UInt32)
      nameWithType:
        CSharp: Util.ByteArrayToHexDigitString(Byte[], UInt32)
        VB: Util.ByteArrayToHexDigitString(Byte(), UInt32)
      qualifiedName:
        CSharp: TestStand.Util.ByteArrayToHexDigitString(System.Byte[], System.UInt32)
        VB: TestStand.Util.ByteArrayToHexDigitString(System.Byte(), System.UInt32)
      type: Method
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ByteArrayToHexDigitString
        path: ../TestStand/Util.cs
        startLine: 318
      summary: "\nConvert a byte array into a printable hexadecimal representation.\n"
      example: []
      syntax:
        content:
          CSharp: public static string ByteArrayToHexDigitString(byte[] pbytInputData, uint puintGroupSize)
          VB: Public Shared Function ByteArrayToHexDigitString(pbytInputData As Byte(), puintGroupSize As UInteger) As String
        parameters:
        - id: pbytInputData
          type: System.Byte[]
          description: "\nSpecify the byte array to be formatted. Any byte array will do.\n"
        - id: puintGroupSize
          type: System.UInt32
          description: "\nSpecify the number of bytes to display as a group.\n"
        return:
          type: System.String
          description: "\nThe return value is a string that contains two characters for each\nbyte in the array, plus one space between every puintGroupSizeth\nbyte.\n"
      overload: TestStand.Util.ByteArrayToHexDigitString*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
    - id: TestStand.Util.ShowKeyAssemblyProperties(System.Reflection.Assembly)
      commentId: M:TestStand.Util.ShowKeyAssemblyProperties(System.Reflection.Assembly)
      language: CSharp
      name:
        CSharp: ShowKeyAssemblyProperties(Assembly)
        VB: ShowKeyAssemblyProperties(Assembly)
      nameWithType:
        CSharp: Util.ShowKeyAssemblyProperties(Assembly)
        VB: Util.ShowKeyAssemblyProperties(Assembly)
      qualifiedName:
        CSharp: TestStand.Util.ShowKeyAssemblyProperties(System.Reflection.Assembly)
        VB: TestStand.Util.ShowKeyAssemblyProperties(System.Reflection.Assembly)
      type: Method
      assemblies:
      - BithMathTestStand
      namespace: TestStand
      source:
        remote:
          path: TestStand/Util.cs
          branch: master
          repo: https://github.com/txwizard/BitMath.git
        id: ShowKeyAssemblyProperties
        path: ../TestStand/Util.cs
        startLine: 353
      summary: "\nList selected properties of any assembly on a console.\n"
      example: []
      syntax:
        content:
          CSharp: public static void ShowKeyAssemblyProperties(Assembly pmyLib)
          VB: Public Shared Sub ShowKeyAssemblyProperties(pmyLib As Assembly)
        parameters:
        - id: pmyLib
          type: System.Reflection.Assembly
          description: "\nPass in a reference to the desired assembly, which may be the\nassembly that exports a specified type, the executing assembly, the\ncalling assembly, the entry assembly, or any other assembly for\nwhich you can obtain a reference.\n"
      overload: TestStand.Util.ShowKeyAssemblyProperties*
      modifiers:
        CSharp:
        - public
        - static
        VB:
        - Public
        - Shared
references:
  System:
    name:
      CSharp:
      - name: System
        nameWithType: System
        qualifiedName: System
        isExternal: true
      VB:
      - name: System
        nameWithType: System
        qualifiedName: System
    isDefinition: true
    commentId: N:System
  System.Object:
    name:
      CSharp:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      VB:
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Object
  System.Object.ToString:
    name:
      CSharp:
      - id: System.Object.ToString
        name: ToString
        nameWithType: Object.ToString
        qualifiedName: System.Object.ToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.ToString
        name: ToString
        nameWithType: Object.ToString
        qualifiedName: System.Object.ToString
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.ToString
  System.Object.Equals(System.Object):
    name:
      CSharp:
      - id: System.Object.Equals(System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.Equals(System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.Equals(System.Object)
  System.Object.Equals(System.Object,System.Object):
    name:
      CSharp:
      - id: System.Object.Equals(System.Object,System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.Equals(System.Object,System.Object)
        name: Equals
        nameWithType: Object.Equals
        qualifiedName: System.Object.Equals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.Equals(System.Object,System.Object)
  System.Object.ReferenceEquals(System.Object,System.Object):
    name:
      CSharp:
      - id: System.Object.ReferenceEquals(System.Object,System.Object)
        name: ReferenceEquals
        nameWithType: Object.ReferenceEquals
        qualifiedName: System.Object.ReferenceEquals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.ReferenceEquals(System.Object,System.Object)
        name: ReferenceEquals
        nameWithType: Object.ReferenceEquals
        qualifiedName: System.Object.ReferenceEquals
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: ', '
        nameWithType: ', '
        qualifiedName: ', '
      - id: System.Object
        name: Object
        nameWithType: Object
        qualifiedName: System.Object
        isExternal: true
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.ReferenceEquals(System.Object,System.Object)
  System.Object.GetHashCode:
    name:
      CSharp:
      - id: System.Object.GetHashCode
        name: GetHashCode
        nameWithType: Object.GetHashCode
        qualifiedName: System.Object.GetHashCode
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.GetHashCode
        name: GetHashCode
        nameWithType: Object.GetHashCode
        qualifiedName: System.Object.GetHashCode
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.GetHashCode
  System.Object.GetType:
    name:
      CSharp:
      - id: System.Object.GetType
        name: GetType
        nameWithType: Object.GetType
        qualifiedName: System.Object.GetType
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.GetType
        name: GetType
        nameWithType: Object.GetType
        qualifiedName: System.Object.GetType
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.GetType
  System.Object.MemberwiseClone:
    name:
      CSharp:
      - id: System.Object.MemberwiseClone
        name: MemberwiseClone
        nameWithType: Object.MemberwiseClone
        qualifiedName: System.Object.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
      VB:
      - id: System.Object.MemberwiseClone
        name: MemberwiseClone
        nameWithType: Object.MemberwiseClone
        qualifiedName: System.Object.MemberwiseClone
        isExternal: true
      - name: (
        nameWithType: (
        qualifiedName: (
      - name: )
        nameWithType: )
        qualifiedName: )
    isDefinition: true
    parent: System.Object
    commentId: M:System.Object.MemberwiseClone
  System.Int32:
    name:
      CSharp:
      - id: System.Int32
        name: Int32
        nameWithType: Int32
        qualifiedName: System.Int32
        isExternal: true
      VB:
      - id: System.Int32
        name: Int32
        nameWithType: Int32
        qualifiedName: System.Int32
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Int32
  System.UInt32:
    name:
      CSharp:
      - id: System.UInt32
        name: UInt32
        nameWithType: UInt32
        qualifiedName: System.UInt32
        isExternal: true
      VB:
      - id: System.UInt32
        name: UInt32
        nameWithType: UInt32
        qualifiedName: System.UInt32
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.UInt32
  System.String:
    name:
      CSharp:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
      VB:
      - id: System.String
        name: String
        nameWithType: String
        qualifiedName: System.String
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.String
  System.Char:
    name:
      CSharp:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
      VB:
      - id: System.Char
        name: Char
        nameWithType: Char
        qualifiedName: System.Char
        isExternal: true
    isDefinition: true
    parent: System
    commentId: T:System.Char
  System.Byte[]:
    name:
      CSharp:
      - id: System.Byte
        name: Byte
        nameWithType: Byte
        qualifiedName: System.Byte
        isExternal: true
      - name: '[]'
        nameWithType: '[]'
        qualifiedName: '[]'
      VB:
      - id: System.Byte
        name: Byte
        nameWithType: Byte
        qualifiedName: System.Byte
        isExternal: true
      - name: ()
        nameWithType: ()
        qualifiedName: ()
    isDefinition: false
  TestStand.Util.ByteArrayToHexDigitString*:
    name:
      CSharp:
      - id: TestStand.Util.ByteArrayToHexDigitString*
        name: ByteArrayToHexDigitString
        nameWithType: Util.ByteArrayToHexDigitString
        qualifiedName: TestStand.Util.ByteArrayToHexDigitString
      VB:
      - id: TestStand.Util.ByteArrayToHexDigitString*
        name: ByteArrayToHexDigitString
        nameWithType: Util.ByteArrayToHexDigitString
        qualifiedName: TestStand.Util.ByteArrayToHexDigitString
    isDefinition: true
    commentId: Overload:TestStand.Util.ByteArrayToHexDigitString
  System.Reflection:
    name:
      CSharp:
      - name: System.Reflection
        nameWithType: System.Reflection
        qualifiedName: System.Reflection
        isExternal: true
      VB:
      - name: System.Reflection
        nameWithType: System.Reflection
        qualifiedName: System.Reflection
    isDefinition: true
    commentId: N:System.Reflection
  System.Reflection.Assembly:
    name:
      CSharp:
      - id: System.Reflection.Assembly
        name: Assembly
        nameWithType: Assembly
        qualifiedName: System.Reflection.Assembly
        isExternal: true
      VB:
      - id: System.Reflection.Assembly
        name: Assembly
        nameWithType: Assembly
        qualifiedName: System.Reflection.Assembly
        isExternal: true
    isDefinition: true
    parent: System.Reflection
    commentId: T:System.Reflection.Assembly
  TestStand.Util.ShowKeyAssemblyProperties*:
    name:
      CSharp:
      - id: TestStand.Util.ShowKeyAssemblyProperties*
        name: ShowKeyAssemblyProperties
        nameWithType: Util.ShowKeyAssemblyProperties
        qualifiedName: TestStand.Util.ShowKeyAssemblyProperties
      VB:
      - id: TestStand.Util.ShowKeyAssemblyProperties*
        name: ShowKeyAssemblyProperties
        nameWithType: Util.ShowKeyAssemblyProperties
        qualifiedName: TestStand.Util.ShowKeyAssemblyProperties
    isDefinition: true
    commentId: Overload:TestStand.Util.ShowKeyAssemblyProperties
  TestStand.Util:
    name:
      CSharp:
      - id: TestStand.Util
        name: Util
        nameWithType: Util
        qualifiedName: TestStand.Util
      VB:
      - id: TestStand.Util
        name: Util
        nameWithType: Util
        qualifiedName: TestStand.Util
    isDefinition: true
    commentId: T:TestStand.Util
  TestStand:
    name:
      CSharp:
      - name: TestStand
        nameWithType: TestStand
        qualifiedName: TestStand
      VB:
      - name: TestStand
        nameWithType: TestStand
        qualifiedName: TestStand
    isDefinition: true
    commentId: N:TestStand
