USE [SAF]
GO
/****** Object:  User [GES]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE USER [GES] FOR LOGIN [GES] WITH DEFAULT_SCHEMA=[GES]
GO
/****** Object:  User [SAF]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE USER [SAF] FOR LOGIN [SAF] WITH DEFAULT_SCHEMA=[SAF]
GO
/****** Object:  User [SEG]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE USER [SEG] FOR LOGIN [SEG] WITH DEFAULT_SCHEMA=[SEG]
GO
/****** Object:  Schema [GES]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE SCHEMA [GES]
GO
/****** Object:  Schema [SAF]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE SCHEMA [SAF]
GO
/****** Object:  Schema [SEG]    Script Date: 02/02/2025 6:43:46 PM ******/
CREATE SCHEMA [SEG]
GO
/****** Object:  UserDefinedFunction [SAF].[fObtenerBodega]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [SAF].[fObtenerBodega](  @Accion VARCHAR(10) = 'CODIGO', @CODIGO VARCHAR(50))

RETURNS INT
AS
BEGIN
	DECLARE @ID INT

	IF (@Accion = 'CODIGO')	
		SELECT @ID = B.ID FROM SAF.bdBodega B WHERE B.CODIGO = @CODIGO

	IF (@Accion = 'PROYECTO')
		SELECT @ID = B.ID FROM SAF.bdBodega B WHERE B.idProyecto = CONVERT(INT, @CODIGO)	

	RETURN @ID
END

GO
/****** Object:  UserDefinedFunction [SEG].[fJsonBIT]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [SEG].[fJsonBIT] (@NombreIn VARCHAR(100), @EstadoIn BIT)
RETURNS VARCHAR(50)
AS BEGIN
    DECLARE @estado VARCHAR(50)	
	SET @estado = '"' + @NombreIn + '":"'

	IF (@EstadoIn IS NOT NULL)
		BEGIN
			SET @estado = @estado + CONVERT(VARCHAR, @EstadoIn)
		END
    RETURN @estado + '"'
END

GO
/****** Object:  UserDefinedFunction [SEG].[fJsonDATE]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [SEG].[fJsonDATE] (@NombreIn VARCHAR(100), @FechaIn DATETIME)
RETURNS VARCHAR(50)
AS BEGIN
    DECLARE @fecha VARCHAR(50)	
	SET @fecha ='"' + @NombreIn + '":"'

	IF (@FechaIn IS NOT NULL)
		BEGIN
			SET @fecha = @fecha + CONVERT(VARCHAR, @FechaIn, 9)
		END
    RETURN @fecha + '"'
END

GO
/****** Object:  UserDefinedFunction [SEG].[fJsonINT]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [SEG].[fJsonINT] (@NombreIn VARCHAR(100), @NumeroIn INT)
RETURNS VARCHAR(50)
AS BEGIN
    DECLARE @numero VARCHAR(50)	
	SET @numero = '"' + @NombreIn + '":"'

	IF (@NumeroIn IS NOT NULL)
		BEGIN
			SET @numero = @numero + CONVERT(VARCHAR, @NumeroIn)
		END
    RETURN @numero + '"'
END

GO
/****** Object:  UserDefinedFunction [SEG].[fJsonTINY]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [SEG].[fJsonTINY] (@NombreIn VARCHAR(100), @NumeroIn TINYINT)
RETURNS VARCHAR(50)
AS BEGIN
    DECLARE @numero VARCHAR(50)	
	SET @numero = '"' + @NombreIn + '":"'

	IF (@NumeroIn IS NOT NULL)
		BEGIN
			SET @numero = @numero + CONVERT(VARCHAR, @NumeroIn)
		END
    RETURN @numero + '"'
END


GO
/****** Object:  UserDefinedFunction [SEG].[fJsonVAR]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE FUNCTION [SEG].[fJsonVAR] (@NombreIn VARCHAR(100), @CampoIn VARCHAR(MAX))
RETURNS VARCHAR(50)
AS BEGIN
    DECLARE @campo VARCHAR(50)	
	SET @campo = '"' + @NombreIn + '":"'

	IF (@CampoIn IS NOT NULL)
		BEGIN
			SET @campo = @campo + CONVERT(VARCHAR, @CampoIn)
		END
    RETURN @campo + '"'
END


GO
/****** Object:  UserDefinedFunction [SEG].[fParseJSON]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE FUNCTION [SEG].[fParseJSON]( @JSON NVARCHAR(MAX))

RETURNS @hierarchy TABLE
  (
   element_id INT IDENTITY(1, 1) NOT NULL, /* internal surrogate primary key gives the order of parsing and the list order */
   sequenceNo [int] NULL, /* the place in the sequence for the element */
   parent_ID INT,/* if the element has a parent then it is in this column. The document is the ultimate parent, so you can get the structure from recursing from the document */
   Object_ID INT,/* each list or object has an object id. This ties all elements to a parent. Lists are treated as objects here */
   NAME NVARCHAR(2000),/* the name of the object */
   StringValue NVARCHAR(MAX) NOT NULL,/*the string representation of the value of the element. */
   ValueType VARCHAR(10) NOT null /* the declared type of the value represented as a string in StringValue*/
  )
AS
BEGIN
  DECLARE
    @FirstObject INT, --the index of the first open bracket found in the JSON string
    @OpenDelimiter INT,--the index of the next open bracket found in the JSON string
    @NextOpenDelimiter INT,--the index of subsequent open bracket found in the JSON string
    @NextCloseDelimiter INT,--the index of subsequent close bracket found in the JSON string
    @Type NVARCHAR(10),--whether it denotes an object or an array
    @NextCloseDelimiterChar CHAR(1),--either a '}' or a ']'
    @Contents NVARCHAR(MAX), --the unparsed contents of the bracketed expression
    @Start INT, --index of the start of the token that you are parsing
    @end INT,--index of the end of the token that you are parsing
    @param INT,--the parameter at the end of the next Object/Array token
    @EndOfName INT,--the index of the start of the parameter at end of Object/Array token
    @token NVARCHAR(200),--either a string or object
    @value NVARCHAR(MAX), -- the value as a string
    @SequenceNo int, -- the sequence number within a list
    @name NVARCHAR(200), --the name as a string
    @parent_ID INT,--the next parent ID to allocate
    @lenJSON INT,--the current length of the JSON String
    @characters NCHAR(36),--used to convert hex to decimal
    @result BIGINT,--the value of the hex symbol being parsed
    @index SMALLINT,--used for parsing the hex value
    @Escape INT --the index of the next escape character
   
 
  DECLARE @Strings TABLE /* in this temporary table we keep all strings, even the names of the elements, since they are 'escaped' in a different way, and may contain, unescaped, brackets denoting objects or lists. These are replaced in the JSON string by tokens representing the string */
    (
     String_ID INT IDENTITY(1, 1),
     StringValue NVARCHAR(MAX)
    )
  SELECT--initialise the characters to convert hex to ascii
    @characters='0123456789abcdefghijklmnopqrstuvwxyz',
    @SequenceNo=0, --set the sequence no. to something sensible.
  /* firstly we process all strings. This is done because [{} and ] aren't escaped in strings, which complicates an iterative parse. */
    @parent_ID=0;
  WHILE 1=1 --forever until there is nothing more to do
    BEGIN
      SELECT
        @start=PATINDEX('%[^a-zA-Z]["]%', @json collate SQL_Latin1_General_CP850_Bin);--next delimited string
      IF @start=0 BREAK --no more so drop through the WHILE loop
      IF SUBSTRING(@json, @start+1, 1)='"'
        BEGIN --Delimited Name
          SET @start=@Start+1;
          SET @end=PATINDEX('%[^\]["]%', RIGHT(@json, LEN(@json+'|')-@start) collate SQL_Latin1_General_CP850_Bin);
        END
      IF @end=0 --no end delimiter to last string
        BREAK --no more
      SELECT @token=SUBSTRING(@json, @start+1, @end-1)
      --now put in the escaped control characters
      SELECT @token=REPLACE(@token, FROMString, TOString)
      FROM
        (SELECT
          '\"' AS FromString, '"' AS ToString
         UNION ALL SELECT '\\', '\'
         UNION ALL SELECT '\/', '/'
         UNION ALL SELECT '\b', CHAR(08)
         UNION ALL SELECT '\f', CHAR(12)
         UNION ALL SELECT '\n', CHAR(10)
         UNION ALL SELECT '\r', CHAR(13)
         UNION ALL SELECT '\t', CHAR(09)
        ) substitutions
      SELECT @result=0, @escape=1
  --Begin to take out any hex escape codes
      WHILE @escape>0
        BEGIN
          SELECT @index=0,
          --find the next hex escape sequence
          @escape=PATINDEX('%\x[0-9a-f][0-9a-f][0-9a-f][0-9a-f]%', @token collate SQL_Latin1_General_CP850_Bin)
          IF @escape>0 --if there is one
            BEGIN
              WHILE @index<4 --there are always four digits to a \x sequence  
                BEGIN
                  SELECT --determine its value
                    @result=@result+POWER(16, @index)
                    *(CHARINDEX(SUBSTRING(@token, @escape+2+3-@index, 1),
                                @characters)-1), @index=@index+1 ;
        
                END
                -- and replace the hex sequence by its unicode value
              SELECT @token=STUFF(@token, @escape, 6, NCHAR(@result))
            END
        END
      --now store the string away
      INSERT INTO @Strings (StringValue) SELECT @token
      -- and replace the string with a token
      SELECT @JSON=STUFF(@json, @start, @end+1,
                    '@string'+CONVERT(NVARCHAR(5), @@identity))
    END
  -- all strings are now removed. Now we find the first leaf. 
  WHILE 1=1  --forever until there is nothing more to do
  BEGIN
 
  SELECT @parent_ID=@parent_ID+1
  --find the first object or list by looking for the open bracket
  SELECT @FirstObject=PATINDEX('%[{[[]%', @json collate SQL_Latin1_General_CP850_Bin)--object or array
  IF @FirstObject = 0 BREAK
  IF (SUBSTRING(@json, @FirstObject, 1)='{')
    SELECT @NextCloseDelimiterChar='}', @type='object'
  ELSE
    SELECT @NextCloseDelimiterChar=']', @type='array'
  SELECT @OpenDelimiter=@firstObject
 
  WHILE 1=1 --find the innermost object or list...
    BEGIN
      SELECT
        @lenJSON=LEN(@JSON+'|')-1
  --find the matching close-delimiter proceeding after the open-delimiter
      SELECT
        @NextCloseDelimiter=CHARINDEX(@NextCloseDelimiterChar, @json,
                                      @OpenDelimiter+1)
  --is there an intervening open-delimiter of either type
      SELECT @NextOpenDelimiter=PATINDEX('%[{[[]%',
             RIGHT(@json, @lenJSON-@OpenDelimiter)collate SQL_Latin1_General_CP850_Bin)--object
      IF @NextOpenDelimiter=0
        BREAK
      SELECT @NextOpenDelimiter=@NextOpenDelimiter+@OpenDelimiter
      IF @NextCloseDelimiter<@NextOpenDelimiter
        BREAK
      IF SUBSTRING(@json, @NextOpenDelimiter, 1)='{'
        SELECT @NextCloseDelimiterChar='}', @type='object'
      ELSE
        SELECT @NextCloseDelimiterChar=']', @type='array'
      SELECT @OpenDelimiter=@NextOpenDelimiter
    END
  ---and parse out the list or name/value pairs
  SELECT
    @contents=SUBSTRING(@json, @OpenDelimiter+1,
                        @NextCloseDelimiter-@OpenDelimiter-1)
  SELECT
    @JSON=STUFF(@json, @OpenDelimiter,
                @NextCloseDelimiter-@OpenDelimiter+1,
                '@'+@type+CONVERT(NVARCHAR(5), @parent_ID))
  WHILE (PATINDEX('%[A-Za-z0-9@+.e]%', @contents collate SQL_Latin1_General_CP850_Bin))<>0
    BEGIN
      IF @Type='Object' --it will be a 0-n list containing a string followed by a string, number,boolean, or null
        BEGIN
          SELECT
            @SequenceNo=0,@end=CHARINDEX(':', ' '+@contents)--if there is anything, it will be a string-based name.
          SELECT  @start=PATINDEX('%[^A-Za-z@][@]%', ' '+@contents collate SQL_Latin1_General_CP850_Bin)--AAAAAAAA
          SELECT @token=SUBSTRING(' '+@contents, @start+1, @End-@Start-1),
            @endofname=PATINDEX('%[0-9]%', @token collate SQL_Latin1_General_CP850_Bin),
            @param=RIGHT(@token, LEN(@token)-@endofname+1)
          SELECT
            @token=LEFT(@token, @endofname-1),
            @Contents=RIGHT(' '+@contents, LEN(' '+@contents+'|')-@end-1)
          SELECT  @name=stringvalue FROM @strings
            WHERE string_id=@param --fetch the name
        END
      ELSE
        SELECT @Name=null,@SequenceNo=@SequenceNo+1
      SELECT
        @end=CHARINDEX(',', @contents)-- a string-token, object-token, list-token, number,boolean, or null
      IF @end=0
        SELECT  @end=PATINDEX('%[A-Za-z0-9@+.e][^A-Za-z0-9@+.e]%', @Contents+' ' collate SQL_Latin1_General_CP850_Bin)
          +1
       SELECT
         @start=PATINDEX('%[^A-Za-z0-9@+.e][A-Za-z0-9@+.e][\-]%', ' '+@contents collate SQL_Latin1_General_CP850_Bin)
		-- Edited: add more condition [\-] in order to detect negative number 08-20-2014
      --select @start,@end, LEN(@contents+'|'), @contents 
      SELECT
        @Value=RTRIM(SUBSTRING(@contents, @start, @End-@Start)),
        @Contents=RIGHT(@contents+' ', LEN(@contents+'|')-@end)

      IF SUBSTRING(@value, 1, 7)='@object'
        INSERT INTO @hierarchy
          (NAME, SequenceNo, parent_ID, StringValue, Object_ID, ValueType)
          SELECT @name, @SequenceNo, @parent_ID, SUBSTRING(@value, 8, 5),
            SUBSTRING(@value, 8, 5), 'object'
      ELSE
        IF SUBSTRING(@value, 1, 6)='@array'
          INSERT INTO @hierarchy
            (NAME, SequenceNo, parent_ID, StringValue, Object_ID, ValueType)
            SELECT @name, @SequenceNo, @parent_ID, SUBSTRING(@value, 7, 5),
              SUBSTRING(@value, 7, 5), 'array'
        ELSE
          IF SUBSTRING(@value, 1, 7)='@string'
            INSERT INTO @hierarchy
              (NAME, SequenceNo, parent_ID, StringValue, ValueType)
              SELECT @name, @SequenceNo, @parent_ID, stringvalue, 'string'
              FROM @strings
              WHERE string_id=SUBSTRING(@value, 8, 5)
          ELSE
            IF @value IN ('true', 'false')
              INSERT INTO @hierarchy
                (NAME, SequenceNo, parent_ID, StringValue, ValueType)
                SELECT @name, @SequenceNo, @parent_ID, @value, 'boolean'
            ELSE
              IF @value='null'
                INSERT INTO @hierarchy
                  (NAME, SequenceNo, parent_ID, StringValue, ValueType)
                  SELECT @name, @SequenceNo, @parent_ID, @value, ''
              ELSE
                IF PATINDEX('%[^0-9]%', @value collate SQL_Latin1_General_CP850_Bin)>0
                  INSERT INTO @hierarchy
                    (NAME, SequenceNo, parent_ID, StringValue, ValueType)
                    SELECT @name, @SequenceNo, @parent_ID, @value, 'real'
                ELSE
                  INSERT INTO @hierarchy
                    (NAME, SequenceNo, parent_ID, StringValue, ValueType)
                    SELECT @name, @SequenceNo, @parent_ID, @value, 'int'
      if @Contents=' ' Select @SequenceNo=0
    END
  END
INSERT INTO @hierarchy (NAME, SequenceNo, parent_ID, StringValue, Object_ID, ValueType)
  SELECT '-',1, NULL, '', @parent_id-1, @type
--
   RETURN
END





GO
/****** Object:  Table [SAF].[bdBodega]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdBodega](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idProyecto] [int] NULL,
	[idProveedor] [smallint] NULL,
	[Codigo] [varchar](50) NOT NULL,
	[Nombre] [varchar](150) NULL,
	[EsSistema] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdBodegas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdProveedor]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdProveedor](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Identificacion] [varchar](20) NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Iniciales] [varchar](10) NOT NULL,
	[Telefono] [varchar](100) NULL,
	[Direccion] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdProveedores] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdProyecto]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdProyecto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCliente] [int] NOT NULL,
	[idCiudad] [varchar](20) NOT NULL,
	[Nombre] [varchar](200) NOT NULL,
	[Tipo] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NULL,
	[Telefono] [varchar](50) NULL,
	[Observacion] [varchar](500) NULL,
	[Fecha] [date] NOT NULL,
	[FormaContacto] [varchar](50) NULL,
	[SistemaMedida] [varchar](50) NULL,
	[IdentificacionResponsable] [varchar](15) NULL,
	[NombreResponsable] [varchar](200) NULL,
	[TelResponsable] [varchar](50) NULL,
	[Activo] [bit] NOT NULL,
	[Estado] [tinyint] NOT NULL,
 CONSTRAINT [PK_bdProyectos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vBodega]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [SAF].[vBodega]
AS
SELECT     B.Id, B.idProyecto, B.idProveedor, B.Nombre, B.Activo, B.EsSistema, P.Nombre AS ProveedorNombre, SAF.bdProyecto.Nombre AS ProyectoNombre, B.Codigo
FROM         SAF.bdBodega AS B LEFT OUTER JOIN
                      SAF.bdProyecto ON B.idProyecto = SAF.bdProyecto.Id LEFT OUTER JOIN
                      SAF.bdProveedor AS P ON B.idProveedor = P.Id

GO
/****** Object:  Table [SAF].[bdDocumento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDocumento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Descripcion] [varchar](1000) NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdMovimientos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdDocumentoTipo]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDocumentoTipo](
	[Id] [varchar](10) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Consecutivo] [bigint] NOT NULL,
	[Operacion] [varchar](1) NOT NULL,
	[CantidadFilas] [smallint] NOT NULL,
	[EsSistema] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdTiposDocumentos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vDocumento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [SAF].[vDocumento]
AS
SELECT     D.Id, D.idDocumentoTipo, D.idBodegaOrigen, D.idBodegaDestino, D.Numero, D.Fecha, D.Descripcion, D.Estado, TD.Nombre AS DocumentoTipoNombre, 
                      BO.Nombre AS BodegaOrigenNombre, BD.Nombre AS BodegaDestinoNombre
FROM         SAF.bdDocumento AS D INNER JOIN
                      SAF.bdDocumentoTipo AS TD ON D.idDocumentoTipo = TD.Id INNER JOIN
                      SAF.bdBodega AS BO ON D.idBodegaOrigen = BO.Id INNER JOIN
                      SAF.bdBodega AS BD ON D.idBodegaDestino = BD.Id

GO
/****** Object:  Table [SAF].[bdDocumentoDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDocumentoDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[idDocumento] [int] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_bd] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdElemento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdElemento](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[idGrupoElemento] [tinyint] NOT NULL,
	[idUnidadMedida] [tinyint] NOT NULL,
	[Referencia] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Mt2] [float] NOT NULL,
	[Peso] [float] NOT NULL,
	[Rotacion] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdElementos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vDocumentoDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [SAF].[vDocumentoDetalle]
AS
SELECT     DD.Id, DD.idElemento, DD.idDocumento, D.Descripcion, D.idBodegaDestino, D.idBodegaOrigen, DD.Cantidad, E.Nombre AS ElementoNombre, 
                      BO.Nombre AS BodegaOrigenNombre, BD.Nombre AS BodegaDestinoNombre
FROM         SAF.bdDocumentoDetalle AS DD INNER JOIN
                      SAF.bdDocumento AS D ON DD.idDocumento = D.Id INNER JOIN
                      SAF.bdElemento AS E ON DD.idElemento = E.Id INNER JOIN
                      SAF.bdBodega AS BO ON D.idBodegaOrigen = BO.Id INNER JOIN
                      SAF.bdBodega AS BD ON D.idBodegaDestino = BD.Id

GO
/****** Object:  Table [SAF].[bdGrupoElemento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdGrupoElemento](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdGruposElementos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdUnidadMedida]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdUnidadMedida](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdUnidadesMedidas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vElemento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [SAF].[vElemento]
AS
SELECT  E.Id ,E.idGrupoElemento, E.idUnidadMedida, E.Referencia, E.Nombre, E.Mt2, E.Peso, E.Rotacion, E.Activo, GE.Nombre GrupoElementoNombre, U.Nombre UnidadMedidaNombre
FROM bdElemento E
INNER JOIN bdGrupoElemento GE ON E.idGrupoElemento = GE.Id
INNER JOIN bdUnidadMedida U ON E.idUnidadMedida = U.Id

GO
/****** Object:  Table [SAF].[bdListaPrecio]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdListaPrecio](
	[Id] [tinyint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdListasPrecios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdListaPrecioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdListaPrecioDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idListaPrecio] [tinyint] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[PrecioAlquiler] [int] NOT NULL,
	[PrecioVenta] [int] NOT NULL,
	[PrecioPerdida] [int] NOT NULL,
 CONSTRAINT [PK_bdDetallesListasPrecios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vListaPrecioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [SAF].[vListaPrecioDetalle]
AS
SELECT  LPD.Id, LPD.idListaPrecio, LPD.idElemento, LPD.PrecioAlquiler, LPD.PrecioVenta, LPD.PrecioPerdida, LP.Nombre ListaPrecioNombre, E.Nombre ElementoNombre
FROM bdListaPrecioDetalle LPD
INNER JOIN bdListaPrecio LP ON LPD.idListaPrecio = LP.Id
INNER JOIN bdElemento E ON LPD.idElemento = E.Id

GO
/****** Object:  Table [SAF].[bdCorte]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdCorte](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idDocumentoTipo] [tinyint] NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdCorte] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdCorteDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdCorteDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCorte] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdDevolucion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDevolucion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[idConductor] [varchar](10) NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[ValorTransporte] [numeric](8, 0) NULL,
	[PesoEquipo] [numeric](6, 2) NULL,
	[ValorEquipo] [numeric](10, 0) NULL,
	[EntregaCliente] [bit] NULL,
	[EntregaParcial] [bit] NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK_bdDevolucion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdDevolucionDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDevolucionDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idDevolucion] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_bdDevolucionDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdDevolucionServicio]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDevolucionServicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idDevolucion] [int] NOT NULL,
	[idProveedor] [smallint] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdDevolucionServicio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdDevolucionServicioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdDevolucionServicioDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idDevolucionServicio] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdMantenimiento]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdMantenimiento](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idDevolucion] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdMantenimiento] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdMantenimientoDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdMantenimientoDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idMantenimiento] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[TipoMantenimiento] [varchar](5) NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_bdMantenimientoDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdOrdenServicio]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdOrdenServicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idRemision] [int] NOT NULL,
	[idProveedor] [smallint] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdOrdenServicio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdOrdenServicioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdOrdenServicioDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idOrdenServicio] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdRemision]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdRemision](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[idConductor] [varchar](10) NULL,
	[Numero] [int] NOT NULL,
	[FechaEntrega] [datetime] NOT NULL,
	[FechaPedido] [datetime] NOT NULL,
	[Transporte] [bit] NULL,
	[ValorTransporte] [numeric](8, 0) NULL,
	[Despachado] [bit] NULL,
	[EquipoAdecuado] [bit] NULL,
	[PesoEquipo] [numeric](6, 2) NULL,
	[ValorEquipo] [numeric](10, 0) NULL,
	[Estado] [varchar](10) NULL,
 CONSTRAINT [PK_bdRemision] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdRemisionDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdRemisionDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idRemision] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_bdRemisionDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdReposicion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdReposicion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idDevolucion] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_bdReposicion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdReposicionDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdReposicionDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idReposicion] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdVenta]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdVenta](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idRemision] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](5) NOT NULL,
 CONSTRAINT [PK_bdVenta] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdVentaDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdVentaDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idVenta] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL,
 CONSTRAINT [PK_bdVentaDetalle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [SAF].[vProyectoPlantilla]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [SAF].[vProyectoPlantilla]
AS
SELECT     R.Id, R.FechaPedido as Fecha, 'R' Tipo,  R.Numero, CAST(DR.Cantidad AS VARCHAR) Cantidad, E.Id idElemento, E.Nombre Elemento, 
		   R.idProyecto, '/Remision/RemisionDetalleEditar/' + CAST(R.Id AS VARCHAR)  Documento
FROM       [saf].[bdRemision] R LEFT JOIN
           [saf].[bdRemisionDetalle] DR ON R.Id = DR.idRemision LEFT JOIN
           [saf].[bdElemento] E ON DR.idElemento = E.Id
UNION
SELECT     R.Id,R.FechaPedido, 'V' Tipo, V.Numero, '(' + CAST(DV.Cantidad AS VARCHAR) + ')' Cantidad, E.Id idElemento, E.Nombre Elemento, 
           R.idProyecto, '/Venta/VentaDetalleConsultar/' + CAST(V.Id AS VARCHAR)  Documento
FROM       [saf].[bdVenta] V INNER JOIN
           [saf].[bdVentaDetalle] DV ON V.Id = DV.idVenta INNER JOIN
           [saf].[bdElemento] E ON DV.idElemento = E.Id INNER JOIN
           [saf].[bdRemision] R ON V.idRemision = R.Id
UNION
SELECT     R.Id, R.FechaPedido, 'OS' Tipo, O.Numero, CAST(DO.Cantidad AS VARCHAR) Cantidad, E.Id idElemento, E.Nombre + ' GV' Elemento, 
           R.idProyecto, '/OrderServicio/OrderServicioDetalleConsultar/' + CAST(O.Id AS VARCHAR)  Documento
FROM       [saf].[bdOrdenServicio] O INNER JOIN
           [saf].[bdOrdenServicioDetalle] DO ON O.Id = DO.idOrdenServicio INNER JOIN
           [saf].[bdElemento] E ON DO.idElemento = E.Id INNER JOIN
           [saf].[bdRemision] R ON O.idRemision = R.Id
UNION
SELECT     C.Id, C.Fecha, + 'L' Tipo, C.Numero, '(' + CAST(DC.Cantidad AS VARCHAR) + ')' Cantidad, E.Id idElemento, E.Nombre Elemento, 
           C.idProyecto, '/Corte/CorteDetalleConsulta/' + CAST(C.Id AS VARCHAR)  Documento
FROM       [saf].[bdCorte] C INNER JOIN
           [saf].[bdCorteDetalle] DC ON C.Id = DC.idCorte INNER JOIN
           [saf].[bdElemento] E ON DC.idElemento = E.Id

UNION
SELECT     D.Id, D.Fecha, 'E' Tipo, D.Numero, '-' + CAST(DC.Cantidad AS VARCHAR) Cantidad, E.Id idElemento, E.Nombre Elemento, 
           D.idProyecto, '/Devolucion/DevolucionDetalleEditar/' + CAST(D.Id AS VARCHAR)  Documento
FROM       [saf].[bdDevolucion] D LEFT JOIN
           [saf].[bdDevolucionDetalle] DC ON D.Id = DC.idDevolucion LEFT JOIN
           [saf].[bdElemento] E ON DC.idElemento = E.Id
           
UNION
SELECT     D.Id, D.Fecha, 'P', R.Numero, '-' + CAST(DR.Cantidad AS VARCHAR) Cantidad, E.Id idElemento, E.Nombre Elemento, 
           D.idProyecto, '/Reposicion/ReposicionDetalleConsulta/' + CAST(R.Id AS VARCHAR)  Documento
FROM       [saf].[bdReposicion] R INNER JOIN
           [saf].[bdReposicionDetalle] DR ON R.Id = DR.idReposicion INNER JOIN
           [saf].[bdElemento] E ON DR.idElemento = E.Id INNER JOIN
           [saf].[bdDevolucion] D ON R.idDevolucion = D.Id

UNION
SELECT     D.Id, D.Fecha, 'M' Tipo, M.Numero, '(' + CAST(DM.Cantidad AS VARCHAR) + ')' Cantidad, E.Id idElemento, E.Nombre Elemento, 
            D.idProyecto, '/Mantenimiento/MantenimientoDetalleConsultar/' + CAST(M.Id AS VARCHAR)  Documento
FROM       [saf].[bdMantenimiento] M INNER JOIN
           [saf].[bdMantenimientoDetalle] DM ON M.Id = DM.idMantenimiento INNER JOIN
           [saf].[bdElemento] E ON DM.idElemento = E.Id INNER JOIN
           [saf].[bdDevolucion] D ON M.idDevolucion = D.Id

UNION
SELECT     D.Id, D.Fecha, 'DS' Tipo, DS.Numero, CAST(DD.Cantidad AS VARCHAR) Cantidad, E.Id idElemento, E.Nombre + ' GV' Elemento, 
           D.idProyecto, '/DevolucionServicio/DevolucionServicioDetalleConsultar/' + CAST(DS.Id AS VARCHAR)  Documento
FROM       [saf].[bdDevolucionServicio] DS INNER JOIN
           [saf].[bdDevolucionServicioDetalle] DD ON DS.Id = DD.idDevolucionServicio INNER JOIN
           [saf].[bdElemento] E ON DD.idElemento = E.Id INNER JOIN
           [saf].[bdDevolucion] D ON DS.idDevolucion = D.Id






GO
/****** Object:  View [SAF].[vRemision]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [SAF].[vRemision]
AS
SELECT     R.Id, R.idDocumentoTipo, R.idBodegaOrigen, R.idBodegaDestino, R.Numero, R.FechaEntrega, R.idProyecto, R.idConductor, R.FechaPedido, R.Transporte, 
                      R.ValorTransporte, R.Despachado, R.EquipoAdecuado, R.PesoEquipo, R.ValorEquipo, R.Estado, TD.Nombre AS DocumentoTipoNombre, 
                      BO.Nombre AS BodegaOrigenNombre, BD.Nombre AS BodegaDestinoNombre
FROM         SAF.bdRemision AS R INNER JOIN
                      SAF.bdDocumentoTipo AS TD ON R.idDocumentoTipo = TD.Id INNER JOIN
                      SAF.bdBodega AS BO ON R.idBodegaOrigen = BO.Id INNER JOIN
                      SAF.bdBodega AS BD ON R.idBodegaDestino = BD.Id

GO
/****** Object:  View [SAF].[vRemisionDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [SAF].[vRemisionDetalle]
AS
SELECT     RD.Id, RD.idElemento, RD.idRemision, RD.Cantidad, E.Nombre AS ElementoNombre
                      
FROM         SAF.bdRemisionDetalle AS RD INNER JOIN
                      SAF.bdRemision AS R ON RD.idRemision = R.Id INNER JOIN
                      SAF.bdElemento AS E ON RD.idElemento = E.Id 



GO
/****** Object:  Table [GES].[Artefacto]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[Artefacto](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idSistema] [tinyint] NOT NULL,
	[Tipo] [varchar](10) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Estado] [varchar](10) NOT NULL,
 CONSTRAINT [PK_Artefacto] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [GES].[ArtefactoHistorial]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[ArtefactoHistorial](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[idArtefacto] [int] NOT NULL,
	[idRequerimiento] [int] NOT NULL,
	[Objetivo] [varchar](max) NOT NULL,
	[Version] [varchar](10) NULL,
 CONSTRAINT [PK_ArtefactoHistorial] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [GES].[Catalogo]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[Catalogo](
	[Id] [varchar](20) NOT NULL,
	[idSistema] [varchar](10) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Catalogo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [GES].[CatalogoDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[CatalogoDetalle](
	[Id] [varchar](20) NOT NULL,
	[idCatalogo] [varchar](20) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[ValorCadena] [varchar](10) NULL,
	[ValorNumero] [int] NULL,
	[ValorDecimal] [decimal](18, 5) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_CatalogoDetalle] PRIMARY KEY CLUSTERED 
(
	[idCatalogo] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [GES].[Parametro]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[Parametro](
	[Codigo] [varchar](20) NOT NULL,
	[idSistema] [varchar](10) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Descripcion] [varchar](500) NULL,
	[Valor] [varchar](200) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_ParametroSistema] PRIMARY KEY CLUSTERED 
(
	[Codigo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [GES].[Sistema]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[Sistema](
	[Id] [varchar](10) NOT NULL,
	[Version] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Sistema] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [GES].[Ticket]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [GES].[Ticket](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [varchar](50) NOT NULL,
	[Descripcion] [varchar](max) NOT NULL,
	[Tipo] [varchar](5) NOT NULL,
	[Prioridad] [varchar](5) NOT NULL,
	[FechaCreacion] [datetime] NOT NULL,
	[Estado] [varchar](5) NOT NULL,
 CONSTRAINT [PK_Ticket] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [SAF].[Agente]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[Agente](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdAgentes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdCliente]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdCliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCiudad] [varchar](20) NOT NULL,
	[Identificacion] [varchar](20) NOT NULL,
	[Nombre1] [varchar](25) NOT NULL,
	[Nombre2] [varchar](25) NULL,
	[Apellido1] [varchar](25) NOT NULL,
	[Apellido2] [varchar](25) NULL,
	[Nombre]  AS (((((([Nombre1]+' ')+[Nombre2])+' ')+[Apellido1])+' ')+[Apellido2]),
	[Direccion] [varchar](200) NOT NULL,
	[Telefono] [varchar](50) NOT NULL,
	[Celular] [varchar](50) NULL,
	[Correo] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_bdClientes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdContrato]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdContrato](
	[Id] [smallint] IDENTITY(1,1) NOT NULL,
	[idProyecto] [int] NOT NULL,
	[idListaPrecio] [tinyint] NOT NULL,
	[idAgente] [smallint] NULL,
	[InformacionBD] [bit] NOT NULL,
	[ContratoAlquiler] [bit] NOT NULL,
	[CartaPagare] [bit] NOT NULL,
	[Pagare] [bit] NOT NULL,
	[LetraCambio] [bit] NOT NULL,
	[GarantiasCondiciones] [bit] NOT NULL,
	[Deposito] [bit] NOT NULL,
	[Anticipo] [bit] NOT NULL,
	[PersonaJuridica] [bit] NOT NULL,
	[PersonaNatural] [bit] NOT NULL,
	[FotoCopiaCedula] [bit] NOT NULL,
	[FotoCopiaNit] [bit] NOT NULL,
	[CamaraComercio] [bit] NOT NULL,
	[DescuentoAlquiler] [tinyint] NOT NULL,
	[DescuentoVenta] [tinyint] NOT NULL,
	[DescuentoReposicion] [tinyint] NOT NULL,
	[DescuentoMantenimiento] [tinyint] NOT NULL,
	[DescuentoTransporte] [tinyint] NOT NULL,
	[PorcentajeAgente] [tinyint] NULL,
 CONSTRAINT [PK_bdContratos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdCorteOrdenServicio]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdCorteOrdenServicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[bdCorte] [int] NOT NULL,
	[idDocumentoTipo] [tinyint] NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](10) NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdCorteOrdenServicioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdCorteOrdenServicioDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idCorteOrdenServicio] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdReposicionServicio]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdReposicionServicio](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idBodegaOrigen] [int] NOT NULL,
	[idBodegaDestino] [int] NOT NULL,
	[idDevolucionServicio] [int] NOT NULL,
	[idDocumentoTipo] [varchar](10) NOT NULL,
	[Numero] [int] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[FechaSistema] [datetime] NOT NULL,
	[Estado] [varchar](5) NOT NULL,
 CONSTRAINT [PK_bdReposicionServicio] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SAF].[bdReposicionServicioDetalle]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SAF].[bdReposicionServicioDetalle](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idReposicion] [int] NOT NULL,
	[idElemento] [smallint] NOT NULL,
	[Cantidad] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Auditoria]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Auditoria](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idSesion] [bigint] NOT NULL,
	[Tabla] [varchar](50) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Operacion] [varchar](50) NOT NULL,
	[Observacion] [varchar](500) NULL,
	[Detalle] [varchar](max) NOT NULL,
 CONSTRAINT [PK_bdAuditoria] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Autenticacion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Autenticacion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [varchar](30) NOT NULL,
	[Token] [varchar](1000) NOT NULL,
	[Terminal] [varchar](50) NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NULL,
 CONSTRAINT [PK_Autenticacion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Grupo]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Grupo](
	[Id] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_Grupo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[GrupoRol]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[GrupoRol](
	[idGrupo] [varchar](50) NOT NULL,
	[idRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GrupoRol] PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC,
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[GrupoUsuario]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[GrupoUsuario](
	[idGrupo] [varchar](50) NOT NULL,
	[idUsuario] [varchar](50) NOT NULL,
 CONSTRAINT [PK_GrupoUsuario] PRIMARY KEY CLUSTERED 
(
	[idGrupo] ASC,
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Menu]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Menu](
	[Id] [varchar](50) NOT NULL,
	[idMenu] [varchar](50) NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Vista] [varchar](100) NULL,
	[Orden] [smallint] NOT NULL,
	[Imagen] [varchar](100) NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Opcion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Opcion](
	[Id] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Consultar] [bit] NOT NULL,
	[Crear] [bit] NOT NULL,
	[Editar] [bit] NOT NULL,
	[Eliminar] [bit] NOT NULL,
	[Anular] [bit] NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_Opcion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Permiso]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Permiso](
	[Id] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Permiso] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[PermisoMenu]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[PermisoMenu](
	[idPermiso] [varchar](50) NOT NULL,
	[idMenu] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PermisoMenu] PRIMARY KEY CLUSTERED 
(
	[idPermiso] ASC,
	[idMenu] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[PermisoOpcion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[PermisoOpcion](
	[idPermiso] [varchar](50) NOT NULL,
	[idOpcion] [varchar](50) NOT NULL,
	[Consultar] [bit] NOT NULL,
	[Crear] [bit] NOT NULL,
	[Editar] [bit] NOT NULL,
	[Eliminar] [bit] NOT NULL,
	[Anular] [bit] NOT NULL,
	[Activar] [bit] NOT NULL,
 CONSTRAINT [PK_PermisoOpcion] PRIMARY KEY CLUSTERED 
(
	[idPermiso] ASC,
	[idOpcion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Rol]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Rol](
	[Id] [varchar](50) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](500) NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Rol] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[RolPermiso]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[RolPermiso](
	[idRol] [varchar](50) NOT NULL,
	[idPermiso] [varchar](50) NOT NULL,
 CONSTRAINT [PK_RolPermiso] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC,
	[idPermiso] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Sesion]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Sesion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[idUsuario] [varchar](30) NOT NULL,
	[Token] [varchar](50) NOT NULL,
	[Terminal] [varchar](50) NULL,
	[FechaInicio] [datetime] NOT NULL,
	[FechaFin] [datetime] NULL,
	[Tiempo] [int] NULL,
	[IdSesionBD] [int] NULL,
 CONSTRAINT [PK_Sesion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[Usuario]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[Usuario](
	[Id] [varchar](50) NOT NULL,
	[Identificacion] [varchar](50) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apellido] [varchar](100) NOT NULL,
	[Usuario] [varchar](15) NOT NULL,
	[Clave] [varchar](50) NOT NULL,
	[Correo] [varchar](100) NOT NULL,
	[Admin] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [SEG].[UsuarioRol]    Script Date: 02/02/2025 6:43:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [SEG].[UsuarioRol](
	[idUsuario] [varchar](50) NOT NULL,
	[idRol] [varchar](50) NOT NULL,
 CONSTRAINT [PK_UsuarioRol] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC,
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [GES].[Catalogo] ([Id], [idSistema], [Descripcion]) VALUES (N'ESTADOS_TICKETS', N'GES', N'Estados Tickets')
GO
INSERT [GES].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'F', N'ESTADOS_TICKETS', N'Finalizado', N'TICKET', NULL, NULL, 0)
GO
INSERT [GES].[CatalogoDetalle] ([Id], [idCatalogo], [Nombre], [ValorCadena], [ValorNumero], [ValorDecimal], [Activo]) VALUES (N'T', N'ESTADOS_TICKETS', N'Tramite', N'TICKET', NULL, NULL, 0)
GO
INSERT [GES].[Sistema] ([Id], [Version]) VALUES (N'GES', N'22.0')
GO
INSERT [GES].[Sistema] ([Id], [Version]) VALUES (N'SAF', N'22.0')
GO
SET IDENTITY_INSERT [GES].[Ticket] ON 
GO
INSERT [GES].[Ticket] ([Id], [Titulo], [Descripcion], [Tipo], [Prioridad], [FechaCreacion], [Estado]) VALUES (1, N'Creación de Estructura Blazor', N'Creación de Estructura Blazor', N'', N'', CAST(N'2022-07-26T17:17:14.853' AS DateTime), N'F')
GO
INSERT [GES].[Ticket] ([Id], [Titulo], [Descripcion], [Tipo], [Prioridad], [FechaCreacion], [Estado]) VALUES (2, N'Crear Modelos EF', N'Crear Modelos EF', N'', N'', CAST(N'2022-07-26T17:17:14.853' AS DateTime), N'F')
GO
INSERT [GES].[Ticket] ([Id], [Titulo], [Descripcion], [Tipo], [Prioridad], [FechaCreacion], [Estado]) VALUES (3, N'GES.Api Construir Recursos', N'Construir recursos para el manejo del sistema, ajustar capa del negocio, Catalogo, Ticket, Parametro y Sistema', N'', N'', CAST(N'2022-07-26T17:23:45.527' AS DateTime), N'P')
GO
SET IDENTITY_INSERT [GES].[Ticket] OFF
GO
INSERT [SEG].[Grupo] ([Id], [Descripcion], [Activar]) VALUES (N'G_ADMIN', N'Grupo de administradores', 1)
GO
INSERT [SEG].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'G_ADMIN', N'R_AGENTE_ADMIN')
GO
INSERT [SEG].[GrupoRol] ([idGrupo], [idRol]) VALUES (N'G_ADMIN', N'R_USUARIO_ADMIN')
GO
INSERT [SEG].[GrupoUsuario] ([idGrupo], [idUsuario]) VALUES (N'G_ADMIN', N'U_GALVAREZ')
GO
INSERT [SEG].[Menu] ([Id], [idMenu], [Nombre], [Vista], [Orden], [Imagen], [Activo]) VALUES (N'M_AGENTE', N'M_TABLAS', N'Agentes', N'/Agente/Listar', 0, NULL, 1)
GO
INSERT [SEG].[Menu] ([Id], [idMenu], [Nombre], [Vista], [Orden], [Imagen], [Activo]) VALUES (N'M_ROL', N'M_SISTEMA', N'Roles', N'/Rol/Listar', 1, NULL, 1)
GO
INSERT [SEG].[Menu] ([Id], [idMenu], [Nombre], [Vista], [Orden], [Imagen], [Activo]) VALUES (N'M_SISTEMA', NULL, N'Sistema', NULL, 0, NULL, 1)
GO
INSERT [SEG].[Menu] ([Id], [idMenu], [Nombre], [Vista], [Orden], [Imagen], [Activo]) VALUES (N'M_TABLAS', NULL, N'Tablas Básicas', NULL, 1, NULL, 1)
GO
INSERT [SEG].[Menu] ([Id], [idMenu], [Nombre], [Vista], [Orden], [Imagen], [Activo]) VALUES (N'M_USUARIO', N'M_SISTEMA', N'Usuarios', N'/Usuario/Listar', 0, NULL, 1)
GO
INSERT [SEG].[Opcion] ([Id], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'O_AGENTE', N'Opción de Agentes', 1, 1, 1, 1, 0, 1)
GO
INSERT [SEG].[Opcion] ([Id], [Descripcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'O_USUARIO', N'Usuarios', 1, 1, 1, 1, 0, 1)
GO
INSERT [SEG].[Permiso] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'P_AGENTE_MENU', N'Menu Agente', N'Ver menu agentes', 1)
GO
INSERT [SEG].[Permiso] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'P_AGENTE_OPCION_ADMIN', N'Opciones Agente Plus', N'Permite crear, editar, eliminar o desactivar un agente', 1)
GO
INSERT [SEG].[Permiso] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'P_USUARIO_MENU', N'Menu Usuario', N'Ver menu usuarios', 1)
GO
INSERT [SEG].[Permiso] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'P_USUARIO_OPCION_ADMIN', N'Opciones Usuario Plus', N'Permitir crear, editar, eliminar o desactivar un usuario', 1)
GO
INSERT [SEG].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'P_AGENTE_MENU', N'M_AGENTE')
GO
INSERT [SEG].[PermisoMenu] ([idPermiso], [idMenu]) VALUES (N'P_USUARIO_MENU', N'M_USUARIO')
GO
INSERT [SEG].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'P_AGENTE_OPCION_ADMIN', N'O_AGENTE', 1, 1, 1, 1, 1, 1)
GO
INSERT [SEG].[PermisoOpcion] ([idPermiso], [idOpcion], [Consultar], [Crear], [Editar], [Eliminar], [Anular], [Activar]) VALUES (N'P_USUARIO_OPCION_ADMIN', N'O_USUARIO', 1, 1, 1, 1, 0, 1)
GO
INSERT [SEG].[Rol] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'R_AGENTE_ADMIN', N'Agente Plus', N'Rol para gestionar como admin del modulo agentes', 1)
GO
INSERT [SEG].[Rol] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'R_USUARIO_ADMIN', N'Usuario Administrador', N'Rol para gestionar como admin del modulo usuario', 1)
GO
INSERT [SEG].[Rol] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'R_USUARIO_CONSULTA', N'Usuario Consulta', N'Rol para consutlar usuario', 1)
GO
INSERT [SEG].[Rol] ([Id], [Nombre], [Descripcion], [Activo]) VALUES (N'R_USUARIO_PLUS', N'Usuario Plus', N'Rol para gestionar usuario', 1)
GO
INSERT [SEG].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'R_AGENTE_ADMIN', N'P_AGENTE_MENU')
GO
INSERT [SEG].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'R_AGENTE_ADMIN', N'P_AGENTE_OPCION_ADMIN')
GO
INSERT [SEG].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'R_USUARIO_ADMIN', N'P_USUARIO_MENU')
GO
INSERT [SEG].[RolPermiso] ([idRol], [idPermiso]) VALUES (N'R_USUARIO_ADMIN', N'P_USUARIO_OPCION_ADMIN')
GO
INSERT [SEG].[Usuario] ([Id], [Identificacion], [Nombre], [Apellido], [Usuario], [Clave], [Correo], [Admin], [Activo]) VALUES (N'U_ADMIN', N'0', N'Administrador', N'Administrador', N'admin', N'1', N'admin@saf.com', 1, 1)
GO
INSERT [SEG].[Usuario] ([Id], [Identificacion], [Nombre], [Apellido], [Usuario], [Clave], [Correo], [Admin], [Activo]) VALUES (N'U_GALVAREZ', N'91519368', N'German', N'Alvarez', N'galvarez', N'1', N'galvarez@saf.com', 1, 1)
GO
ALTER TABLE [GES].[CatalogoDetalle] ADD  CONSTRAINT [DF_CatalogoDetalle_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [GES].[Ticket] ADD  CONSTRAINT [DF_Ticket_Estado]  DEFAULT ('P') FOR [Estado]
GO
ALTER TABLE [SAF].[bdBodega] ADD  CONSTRAINT [DF_bdBodega_EsSistema]  DEFAULT ((0)) FOR [EsSistema]
GO
ALTER TABLE [SAF].[bdDocumentoTipo] ADD  CONSTRAINT [DF_bdDocumentoTipo_CantidadFilas]  DEFAULT ((15)) FOR [CantidadFilas]
GO
ALTER TABLE [SAF].[bdDocumentoTipo] ADD  CONSTRAINT [DF_bdTipoDocumento_EsSistema]  DEFAULT ((0)) FOR [EsSistema]
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle] ADD  CONSTRAINT [DF_bdListaPrecioDetalle_PrecioAlquiler]  DEFAULT ((0)) FOR [PrecioAlquiler]
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle] ADD  CONSTRAINT [DF_bdListaPrecioDetalle_PrecioVenta]  DEFAULT ((0)) FOR [PrecioVenta]
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle] ADD  CONSTRAINT [DF_bdListaPrecioDetalle_PrecioPerdida]  DEFAULT ((0)) FOR [PrecioPerdida]
GO
ALTER TABLE [SAF].[bdProyecto] ADD  CONSTRAINT [DF_bdProyecto_Activo]  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [SAF].[bdProyecto] ADD  CONSTRAINT [DF_bdProyecto_Estado]  DEFAULT ((1)) FOR [Estado]
GO
ALTER TABLE [SEG].[Menu] ADD  CONSTRAINT [DF_bdMenu_Activo]  DEFAULT ((0)) FOR [Activo]
GO
ALTER TABLE [SEG].[Sesion] ADD  CONSTRAINT [DF_Sesion_IdSesionBD]  DEFAULT ((0)) FOR [IdSesionBD]
GO
ALTER TABLE [GES].[ArtefactoHistorial]  WITH CHECK ADD  CONSTRAINT [FK_ArtefactoHistorial_Artefacto] FOREIGN KEY([idArtefacto])
REFERENCES [GES].[Artefacto] ([Id])
GO
ALTER TABLE [GES].[ArtefactoHistorial] CHECK CONSTRAINT [FK_ArtefactoHistorial_Artefacto]
GO
ALTER TABLE [GES].[Catalogo]  WITH CHECK ADD  CONSTRAINT [FK_Catalogo_Sistema] FOREIGN KEY([idSistema])
REFERENCES [GES].[Sistema] ([Id])
GO
ALTER TABLE [GES].[Catalogo] CHECK CONSTRAINT [FK_Catalogo_Sistema]
GO
ALTER TABLE [GES].[CatalogoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_CatalogoDetalle_Catalogo] FOREIGN KEY([idCatalogo])
REFERENCES [GES].[Catalogo] ([Id])
GO
ALTER TABLE [GES].[CatalogoDetalle] CHECK CONSTRAINT [FK_CatalogoDetalle_Catalogo]
GO
ALTER TABLE [GES].[Parametro]  WITH CHECK ADD  CONSTRAINT [FK_Parametro_Sistema] FOREIGN KEY([idSistema])
REFERENCES [GES].[Sistema] ([Id])
GO
ALTER TABLE [GES].[Parametro] CHECK CONSTRAINT [FK_Parametro_Sistema]
GO
ALTER TABLE [SAF].[bdBodega]  WITH NOCHECK ADD  CONSTRAINT [FK_bdBodegaProveedor] FOREIGN KEY([idProveedor])
REFERENCES [SAF].[bdProveedor] ([Id])
GO
ALTER TABLE [SAF].[bdBodega] NOCHECK CONSTRAINT [FK_bdBodegaProveedor]
GO
ALTER TABLE [SAF].[bdContrato]  WITH CHECK ADD  CONSTRAINT [FK_bdContratoAgente] FOREIGN KEY([idAgente])
REFERENCES [SAF].[Agente] ([Id])
GO
ALTER TABLE [SAF].[bdContrato] CHECK CONSTRAINT [FK_bdContratoAgente]
GO
ALTER TABLE [SAF].[bdContrato]  WITH CHECK ADD  CONSTRAINT [FK_bdContratoListaPrecio] FOREIGN KEY([idListaPrecio])
REFERENCES [SAF].[bdListaPrecio] ([Id])
GO
ALTER TABLE [SAF].[bdContrato] CHECK CONSTRAINT [FK_bdContratoListaPrecio]
GO
ALTER TABLE [SAF].[bdContrato]  WITH CHECK ADD  CONSTRAINT [FK_bdContratoProyecto] FOREIGN KEY([idProyecto])
REFERENCES [SAF].[bdProyecto] ([Id])
GO
ALTER TABLE [SAF].[bdContrato] CHECK CONSTRAINT [FK_bdContratoProyecto]
GO
ALTER TABLE [SAF].[bdCorte]  WITH CHECK ADD  CONSTRAINT [FK_bdCorteBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdCorte] CHECK CONSTRAINT [FK_bdCorteBodegaDestino]
GO
ALTER TABLE [SAF].[bdCorte]  WITH CHECK ADD  CONSTRAINT [FK_bdCorteBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdCorte] CHECK CONSTRAINT [FK_bdCorteBodegaOrigen]
GO
ALTER TABLE [SAF].[bdCorte]  WITH CHECK ADD  CONSTRAINT [FK_bdCorteProyecto] FOREIGN KEY([idProyecto])
REFERENCES [SAF].[bdProyecto] ([Id])
GO
ALTER TABLE [SAF].[bdCorte] CHECK CONSTRAINT [FK_bdCorteProyecto]
GO
ALTER TABLE [SAF].[bdDevolucion]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucion] CHECK CONSTRAINT [FK_bdDevolucionBodegaDestino]
GO
ALTER TABLE [SAF].[bdDevolucion]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucion] CHECK CONSTRAINT [FK_bdDevolucionBodegaOrigen]
GO
ALTER TABLE [SAF].[bdDevolucion]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionProyecto] FOREIGN KEY([idProyecto])
REFERENCES [SAF].[bdProyecto] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucion] CHECK CONSTRAINT [FK_bdDevolucionProyecto]
GO
ALTER TABLE [SAF].[bdDevolucionDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionDetalleDevolucion] FOREIGN KEY([idDevolucion])
REFERENCES [SAF].[bdDevolucion] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionDetalle] CHECK CONSTRAINT [FK_bdDevolucionDetalleDevolucion]
GO
ALTER TABLE [SAF].[bdDevolucionDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionDetalleElemento] FOREIGN KEY([idElemento])
REFERENCES [SAF].[bdElemento] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionDetalle] CHECK CONSTRAINT [FK_bdDevolucionDetalleElemento]
GO
ALTER TABLE [SAF].[bdDevolucionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionServicioBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionServicio] CHECK CONSTRAINT [FK_bdDevolucionServicioBodegaDestino]
GO
ALTER TABLE [SAF].[bdDevolucionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionServicioBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionServicio] CHECK CONSTRAINT [FK_bdDevolucionServicioBodegaOrigen]
GO
ALTER TABLE [SAF].[bdDevolucionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionServicioDevolucion] FOREIGN KEY([idDevolucion])
REFERENCES [SAF].[bdDevolucion] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionServicio] CHECK CONSTRAINT [FK_bdDevolucionServicioDevolucion]
GO
ALTER TABLE [SAF].[bdDevolucionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionServicioDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionServicio] CHECK CONSTRAINT [FK_bdDevolucionServicioDocumentoTipo]
GO
ALTER TABLE [SAF].[bdDevolucionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdDevolucionServicioProveedor] FOREIGN KEY([idProveedor])
REFERENCES [SAF].[bdProveedor] ([Id])
GO
ALTER TABLE [SAF].[bdDevolucionServicio] CHECK CONSTRAINT [FK_bdDevolucionServicioProveedor]
GO
ALTER TABLE [SAF].[bdDocumento]  WITH CHECK ADD  CONSTRAINT [FK_bdDocumento_bdDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdDocumento] CHECK CONSTRAINT [FK_bdDocumento_bdDocumentoTipo]
GO
ALTER TABLE [SAF].[bdDocumento]  WITH CHECK ADD  CONSTRAINT [FK_bdDocumentoBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDocumento] CHECK CONSTRAINT [FK_bdDocumentoBodegaDestino]
GO
ALTER TABLE [SAF].[bdDocumento]  WITH CHECK ADD  CONSTRAINT [FK_bdDocumentoBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdDocumento] CHECK CONSTRAINT [FK_bdDocumentoBodegaOrigen]
GO
ALTER TABLE [SAF].[bdDocumentoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdDocumento] FOREIGN KEY([idDocumento])
REFERENCES [SAF].[bdDocumento] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [SAF].[bdDocumentoDetalle] CHECK CONSTRAINT [FK_bdDocumento]
GO
ALTER TABLE [SAF].[bdDocumentoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdDocumentoDetalleElemento] FOREIGN KEY([idElemento])
REFERENCES [SAF].[bdElemento] ([Id])
GO
ALTER TABLE [SAF].[bdDocumentoDetalle] CHECK CONSTRAINT [FK_bdDocumentoDetalleElemento]
GO
ALTER TABLE [SAF].[bdElemento]  WITH CHECK ADD  CONSTRAINT [FK_bdElementoGrupoElemento] FOREIGN KEY([idGrupoElemento])
REFERENCES [SAF].[bdGrupoElemento] ([Id])
GO
ALTER TABLE [SAF].[bdElemento] CHECK CONSTRAINT [FK_bdElementoGrupoElemento]
GO
ALTER TABLE [SAF].[bdElemento]  WITH CHECK ADD  CONSTRAINT [FK_bdElementoUnidadMedida] FOREIGN KEY([idUnidadMedida])
REFERENCES [SAF].[bdUnidadMedida] ([Id])
GO
ALTER TABLE [SAF].[bdElemento] CHECK CONSTRAINT [FK_bdElementoUnidadMedida]
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdListaPrecioDetalleElemento] FOREIGN KEY([idElemento])
REFERENCES [SAF].[bdElemento] ([Id])
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle] CHECK CONSTRAINT [FK_bdListaPrecioDetalleElemento]
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdListaPrecioDetalleListaPrecio] FOREIGN KEY([idListaPrecio])
REFERENCES [SAF].[bdListaPrecio] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [SAF].[bdListaPrecioDetalle] CHECK CONSTRAINT [FK_bdListaPrecioDetalleListaPrecio]
GO
ALTER TABLE [SAF].[bdMantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_bdMantenimientoBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdMantenimiento] CHECK CONSTRAINT [FK_bdMantenimientoBodegaDestino]
GO
ALTER TABLE [SAF].[bdMantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_bdMantenimientoBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdMantenimiento] CHECK CONSTRAINT [FK_bdMantenimientoBodegaOrigen]
GO
ALTER TABLE [SAF].[bdMantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_bdMantenimientoDevolucion] FOREIGN KEY([idDevolucion])
REFERENCES [SAF].[bdDevolucion] ([Id])
GO
ALTER TABLE [SAF].[bdMantenimiento] CHECK CONSTRAINT [FK_bdMantenimientoDevolucion]
GO
ALTER TABLE [SAF].[bdMantenimiento]  WITH CHECK ADD  CONSTRAINT [FK_bdMantenimientoDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdMantenimiento] CHECK CONSTRAINT [FK_bdMantenimientoDocumentoTipo]
GO
ALTER TABLE [SAF].[bdMantenimientoDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdMantenimientoDetalleMantenimiento] FOREIGN KEY([idMantenimiento])
REFERENCES [SAF].[bdMantenimiento] ([Id])
GO
ALTER TABLE [SAF].[bdMantenimientoDetalle] CHECK CONSTRAINT [FK_bdMantenimientoDetalleMantenimiento]
GO
ALTER TABLE [SAF].[bdOrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdOrdenServicioBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdOrdenServicio] CHECK CONSTRAINT [FK_bdOrdenServicioBodegaDestino]
GO
ALTER TABLE [SAF].[bdOrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdOrdenServicioBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdOrdenServicio] CHECK CONSTRAINT [FK_bdOrdenServicioBodegaOrigen]
GO
ALTER TABLE [SAF].[bdOrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdOrdenServicioDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdOrdenServicio] CHECK CONSTRAINT [FK_bdOrdenServicioDocumentoTipo]
GO
ALTER TABLE [SAF].[bdOrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdOrdenServicioProveedor] FOREIGN KEY([idProveedor])
REFERENCES [SAF].[bdProveedor] ([Id])
GO
ALTER TABLE [SAF].[bdOrdenServicio] CHECK CONSTRAINT [FK_bdOrdenServicioProveedor]
GO
ALTER TABLE [SAF].[bdOrdenServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdOrdenServicioRemision] FOREIGN KEY([idRemision])
REFERENCES [SAF].[bdRemision] ([Id])
GO
ALTER TABLE [SAF].[bdOrdenServicio] CHECK CONSTRAINT [FK_bdOrdenServicioRemision]
GO
ALTER TABLE [SAF].[bdProyecto]  WITH CHECK ADD  CONSTRAINT [FK_bdProyecto_bdProyecto] FOREIGN KEY([Id])
REFERENCES [SAF].[bdProyecto] ([Id])
GO
ALTER TABLE [SAF].[bdProyecto] CHECK CONSTRAINT [FK_bdProyecto_bdProyecto]
GO
ALTER TABLE [SAF].[bdProyecto]  WITH CHECK ADD  CONSTRAINT [FK_bdProyectoCliente] FOREIGN KEY([idCliente])
REFERENCES [SAF].[bdCliente] ([Id])
GO
ALTER TABLE [SAF].[bdProyecto] CHECK CONSTRAINT [FK_bdProyectoCliente]
GO
ALTER TABLE [SAF].[bdRemision]  WITH CHECK ADD  CONSTRAINT [FK_bdRemision_bdDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdRemision] CHECK CONSTRAINT [FK_bdRemision_bdDocumentoTipo]
GO
ALTER TABLE [SAF].[bdRemision]  WITH CHECK ADD  CONSTRAINT [FK_bdRemisionBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdRemision] CHECK CONSTRAINT [FK_bdRemisionBodegaDestino]
GO
ALTER TABLE [SAF].[bdRemision]  WITH CHECK ADD  CONSTRAINT [FK_bdRemisionBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdRemision] CHECK CONSTRAINT [FK_bdRemisionBodegaOrigen]
GO
ALTER TABLE [SAF].[bdRemision]  WITH CHECK ADD  CONSTRAINT [FK_bdRemisionProyecto] FOREIGN KEY([idProyecto])
REFERENCES [SAF].[bdProyecto] ([Id])
GO
ALTER TABLE [SAF].[bdRemision] CHECK CONSTRAINT [FK_bdRemisionProyecto]
GO
ALTER TABLE [SAF].[bdRemisionDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdRemisionDetalleElemento] FOREIGN KEY([idElemento])
REFERENCES [SAF].[bdElemento] ([Id])
GO
ALTER TABLE [SAF].[bdRemisionDetalle] CHECK CONSTRAINT [FK_bdRemisionDetalleElemento]
GO
ALTER TABLE [SAF].[bdRemisionDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdRemisionDetalleRemision] FOREIGN KEY([idRemision])
REFERENCES [SAF].[bdRemision] ([Id])
GO
ALTER TABLE [SAF].[bdRemisionDetalle] CHECK CONSTRAINT [FK_bdRemisionDetalleRemision]
GO
ALTER TABLE [SAF].[bdReposicion]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdReposicion] CHECK CONSTRAINT [FK_bdReposicionBodegaDestino]
GO
ALTER TABLE [SAF].[bdReposicion]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdReposicion] CHECK CONSTRAINT [FK_bdReposicionBodegaOrigen]
GO
ALTER TABLE [SAF].[bdReposicion]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionDevolucion] FOREIGN KEY([idDevolucion])
REFERENCES [SAF].[bdDevolucion] ([Id])
GO
ALTER TABLE [SAF].[bdReposicion] CHECK CONSTRAINT [FK_bdReposicionDevolucion]
GO
ALTER TABLE [SAF].[bdReposicion]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdReposicion] CHECK CONSTRAINT [FK_bdReposicionDocumentoTipo]
GO
ALTER TABLE [SAF].[bdReposicionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionServicioBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdReposicionServicio] CHECK CONSTRAINT [FK_bdReposicionServicioBodegaDestino]
GO
ALTER TABLE [SAF].[bdReposicionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionServicioBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdReposicionServicio] CHECK CONSTRAINT [FK_bdReposicionServicioBodegaOrigen]
GO
ALTER TABLE [SAF].[bdReposicionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionServicioDevolucionServicio] FOREIGN KEY([idDevolucionServicio])
REFERENCES [SAF].[bdDevolucionServicio] ([Id])
GO
ALTER TABLE [SAF].[bdReposicionServicio] CHECK CONSTRAINT [FK_bdReposicionServicioDevolucionServicio]
GO
ALTER TABLE [SAF].[bdReposicionServicio]  WITH CHECK ADD  CONSTRAINT [FK_bdReposicionServicioDocumentoTipo] FOREIGN KEY([idDocumentoTipo])
REFERENCES [SAF].[bdDocumentoTipo] ([Id])
GO
ALTER TABLE [SAF].[bdReposicionServicio] CHECK CONSTRAINT [FK_bdReposicionServicioDocumentoTipo]
GO
ALTER TABLE [SAF].[bdVenta]  WITH CHECK ADD  CONSTRAINT [FK_bdVentaBodegaDestino] FOREIGN KEY([idBodegaDestino])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdVenta] CHECK CONSTRAINT [FK_bdVentaBodegaDestino]
GO
ALTER TABLE [SAF].[bdVenta]  WITH CHECK ADD  CONSTRAINT [FK_bdVentaBodegaOrigen] FOREIGN KEY([idBodegaOrigen])
REFERENCES [SAF].[bdBodega] ([Id])
GO
ALTER TABLE [SAF].[bdVenta] CHECK CONSTRAINT [FK_bdVentaBodegaOrigen]
GO
ALTER TABLE [SAF].[bdVenta]  WITH CHECK ADD  CONSTRAINT [FK_bdVentaRemision] FOREIGN KEY([idRemision])
REFERENCES [SAF].[bdRemision] ([Id])
GO
ALTER TABLE [SAF].[bdVenta] CHECK CONSTRAINT [FK_bdVentaRemision]
GO
ALTER TABLE [SAF].[bdVentaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdVentaDetalleElemento] FOREIGN KEY([idElemento])
REFERENCES [SAF].[bdElemento] ([Id])
GO
ALTER TABLE [SAF].[bdVentaDetalle] CHECK CONSTRAINT [FK_bdVentaDetalleElemento]
GO
ALTER TABLE [SAF].[bdVentaDetalle]  WITH CHECK ADD  CONSTRAINT [FK_bdVentaDetalleVenta] FOREIGN KEY([idVenta])
REFERENCES [SAF].[bdVenta] ([Id])
GO
ALTER TABLE [SAF].[bdVentaDetalle] CHECK CONSTRAINT [FK_bdVentaDetalleVenta]
GO
ALTER TABLE [SEG].[GrupoRol]  WITH CHECK ADD  CONSTRAINT [FK_GrupoRol_Grupo] FOREIGN KEY([idGrupo])
REFERENCES [SEG].[Grupo] ([Id])
GO
ALTER TABLE [SEG].[GrupoRol] CHECK CONSTRAINT [FK_GrupoRol_Grupo]
GO
ALTER TABLE [SEG].[GrupoRol]  WITH CHECK ADD  CONSTRAINT [FK_GrupoRol_Rol] FOREIGN KEY([idRol])
REFERENCES [SEG].[Rol] ([Id])
GO
ALTER TABLE [SEG].[GrupoRol] CHECK CONSTRAINT [FK_GrupoRol_Rol]
GO
ALTER TABLE [SEG].[GrupoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_GrupoUsuario_Grupo] FOREIGN KEY([idGrupo])
REFERENCES [SEG].[Grupo] ([Id])
GO
ALTER TABLE [SEG].[GrupoUsuario] CHECK CONSTRAINT [FK_GrupoUsuario_Grupo]
GO
ALTER TABLE [SEG].[GrupoUsuario]  WITH CHECK ADD  CONSTRAINT [FK_GrupoUsuario_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [SEG].[Usuario] ([Id])
GO
ALTER TABLE [SEG].[GrupoUsuario] CHECK CONSTRAINT [FK_GrupoUsuario_Usuario]
GO
ALTER TABLE [SEG].[Menu]  WITH CHECK ADD  CONSTRAINT [FK_MenuPadre] FOREIGN KEY([idMenu])
REFERENCES [SEG].[Menu] ([Id])
GO
ALTER TABLE [SEG].[Menu] CHECK CONSTRAINT [FK_MenuPadre]
GO
ALTER TABLE [SEG].[PermisoMenu]  WITH CHECK ADD  CONSTRAINT [FK_PermisoMenu_Menu] FOREIGN KEY([idMenu])
REFERENCES [SEG].[Menu] ([Id])
GO
ALTER TABLE [SEG].[PermisoMenu] CHECK CONSTRAINT [FK_PermisoMenu_Menu]
GO
ALTER TABLE [SEG].[PermisoMenu]  WITH CHECK ADD  CONSTRAINT [FK_PermisoMenu_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [SEG].[Permiso] ([Id])
GO
ALTER TABLE [SEG].[PermisoMenu] CHECK CONSTRAINT [FK_PermisoMenu_Permiso]
GO
ALTER TABLE [SEG].[PermisoOpcion]  WITH CHECK ADD  CONSTRAINT [FK_PermisoOpcion_Opcion] FOREIGN KEY([idOpcion])
REFERENCES [SEG].[Opcion] ([Id])
GO
ALTER TABLE [SEG].[PermisoOpcion] CHECK CONSTRAINT [FK_PermisoOpcion_Opcion]
GO
ALTER TABLE [SEG].[PermisoOpcion]  WITH CHECK ADD  CONSTRAINT [FK_PermisoOpcion_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [SEG].[Permiso] ([Id])
GO
ALTER TABLE [SEG].[PermisoOpcion] CHECK CONSTRAINT [FK_PermisoOpcion_Permiso]
GO
ALTER TABLE [SEG].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Permiso] FOREIGN KEY([idPermiso])
REFERENCES [SEG].[Permiso] ([Id])
GO
ALTER TABLE [SEG].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Permiso]
GO
ALTER TABLE [SEG].[RolPermiso]  WITH CHECK ADD  CONSTRAINT [FK_RolPermiso_Rol] FOREIGN KEY([idRol])
REFERENCES [SEG].[Rol] ([Id])
GO
ALTER TABLE [SEG].[RolPermiso] CHECK CONSTRAINT [FK_RolPermiso_Rol]
GO
ALTER TABLE [SEG].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Rol] FOREIGN KEY([idRol])
REFERENCES [SEG].[Rol] ([Id])
GO
ALTER TABLE [SEG].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Rol]
GO
ALTER TABLE [SEG].[UsuarioRol]  WITH CHECK ADD  CONSTRAINT [FK_UsuarioRol_Usuario] FOREIGN KEY([idUsuario])
REFERENCES [SEG].[Usuario] ([Id])
GO
ALTER TABLE [SEG].[UsuarioRol] CHECK CONSTRAINT [FK_UsuarioRol_Usuario]
GO
/****** Object:  StoredProcedure [GES].[pCatalogo]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [GES].[pCatalogo]  
(
	@Accion VARCHAR(50) = 'ListarTodos',-- 10: Editar CatlogoDetalle, 11: Eliminar CatlogoDetalle
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, idSistema, Descripcion FROM Catalogo

	IF (@Accion = 'ListarActivo')
		SELECT Id, idSistema, Descripcion FROM Catalogo 
		
	IF(@Accion = 'Insertar')
		BEGIN
			INSERT INTO Catalogo
			SELECT * FROM (SELECT
					max(CASE WHEN name='Id' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Id],
					max(CASE WHEN name='idSistema' THEN convert(INT,StringValue) ELSE 0 END) AS [idSistema],
					max(CASE WHEN name='Descripcion' THEN convert(BIT,StringValue) ELSE 0 END) AS [Descripcion]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Catalago
		END

	IF(@Accion = 'Actualzar')
		BEGIN

			UPDATE A
			SET Descripcion = Catalago.Descripcion
			FROM Catalogo AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Descripcion],
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Catalago ON A.Id = Catalago.Id

		END

	IF(@Accion = 'Borrar')
		BEGIN
			DELETE A
			FROM Catalogo AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Catalago ON A.Id = Catalago.Id
		END

	IF (@Accion = 'ListarDetalleTodos')
		BEGIN
			SELECT S.Id, S.IdCatalogo, S.Nombre, S.ValorCadena, S.ValorNumero, S.ValorDecimal, S.Activo 
			FROM CatalogoDetalle S
			INNER JOIN (SELECT      
							max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo]
						FROM SEG.fParseJSON ( @json) 
					   )  AS CatalogoDetalle
			ON S.IdCatalogo = CatalogoDetalle.IdCatalogo
		END	

	IF (@Accion = 'ListarDetalleActivos')
		BEGIN
			SELECT S.Id, S.IdCatalogo, S.Nombre, S.ValorCadena, S.ValorNumero, S.ValorDecimal, S.Activo 
			FROM CatalogoDetalle S
			INNER JOIN (SELECT max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo],
						max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [Id]
						FROM SEG.fParseJSON (@json) ) AS CatalogoDetalle
			ON S.IdCatalogo = CatalogoDetalle.IdCatalogo 
			WHERE S.Activo = 1
		END	

	IF (@Accion = 'ConsultarDetalle')
		BEGIN
			SELECT S.Id, S.IdCatalogo, S.Nombre, S.ValorCadena, S.ValorNumero, S.ValorDecimal, S.Activo 
			FROM CatalogoDetalle S
			INNER JOIN (SELECT max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo],
						max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [Id]
			FROM SEG.fParseJSON (@json) ) CatalogoDetalle
			ON S.IdCatalogo = CatalogoDetalle.IdCatalogo AND S.Id = CatalogoDetalle.Id
		END	

	IF(@Accion = 'InsetarDetalle')
		BEGIN
			INSERT INTO CatalogoDetalle 
			SELECT IdCatalogo, Id, Nombre, ValorCadena, ValorNumero, ValorDecimal, Activo FROM (SELECT
					max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [Id],
					max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='ValorCadena' THEN convert(VARCHAR(10),StringValue) ELSE (NULL) END) AS [ValorCadena],
					max(CASE WHEN name='ValorNumero' THEN convert(INT, StringValue) ELSE (NULL) END) AS [ValorNumero],
					max(CASE WHEN name='ValorDecimal' THEN convert(REAL, StringValue)  ELSE (NULL) END) AS [ValorDecimal],
					max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo]				
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int' OR ValueType = 'real'
			GROUP BY parent_ID) CatalogoDetalle
		END

	IF(@Accion = 'EditarDetalle')
		BEGIN

			UPDATE A
			SET Nombre = CatalogoDetalle.Nombre,
			ValorCadena = CatalogoDetalle.ValorCadena,			
			ValorNumero = CatalogoDetalle.ValorNumero,
			ValorDecimal = CatalogoDetalle.ValorDecimal
			FROM CatalogoDetalle AS A
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [Id],
					max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='ValorCadena' THEN convert(VARCHAR(10),StringValue) ELSE (NULL) END) AS [ValorCadena],
					max(CASE WHEN name='ValorNumero' THEN convert(INT, StringValue) ELSE (NULL) END) AS [ValorNumero],
					max(CASE WHEN name='ValorDecimal' THEN convert(REAL, StringValue)  ELSE (NULL) END) AS [ValorDecimal],
					max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo]				
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int' OR ValueType = 'real'
			GROUP BY parent_ID) AS CatalogoDetalle ON A.Id = CatalogoDetalle.Id AND A.IdCatalogo = CatalogoDetalle.IdCatalogo

		END

	IF(@Accion = 'BorrarDetalle')
		BEGIN
			DELETE E
			FROM CatalogoDetalle AS E
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [Id],
				   max(CASE WHEN name='IdCatalogo' THEN convert(VARCHAR(20),StringValue) ELSE '0' END) AS [IdCatalogo]
			 FROM  SEG.fParseJSON (	@json )
			 ) AS CatalogoDetalle 
		    ON E.Id = CatalogoDetalle.Id AND E.IdCatalogo = CatalogoDetalle.IdCatalogo
		END
END



GO
/****** Object:  StoredProcedure [GES].[pParametro]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [GES].[pParametro]  
(
	@Accion VARCHAR(15) = 'ListarTodos',
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Codigo, Nombre, Descripcion, Valor, Activo FROM Parametro

	IF (@Accion = 'ListarActivos')
		SELECT Codigo, Nombre, Descripcion, Valor, Activo FROM Parametro WHERE Activo = 1

	IF(@Accion = 'Consultar')
		BEGIN
			SELECT Codigo, Nombre, Descripcion, Valor, Activo FROM Parametro
			WHERE Codigo = 
			(SELECT      
				   max(CASE WHEN name='Codigo' THEN convert(VARCHAR(10),StringValue) ELSE '0' END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 'Insertar')
		BEGIN
			INSERT INTO Parametro 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Codigo' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Codigo,		
					max(CASE WHEN name='idSistema' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS idSistema,		
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS Descripcion,
					max(CASE WHEN name='Valor' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS Valor,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Parametro
		END

	IF(@Accion = 'Editar')
		BEGIN

			UPDATE A
			SET Nombre = Parametro.Nombre,
				Descripcion = Parametro.Descripcion,
				Valor = Parametro.Valor,
				Activo = Parametro.Activo
			FROM Parametro AS A
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Codigo' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Codigo,					
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS Descripcion,
					max(CASE WHEN name='Valor' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS Valor,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Parametro ON A.Codigo = Parametro.Codigo

		END

	IF(@Accion = 'Borrar')
		BEGIN
			DELETE A
			FROM Parametro AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Codigo' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS [Codigo]
			From SEG.fParseJSON
			(
				  @json
			)) AS ListaPrecio ON A.Codigo = ListaPrecio.Codigo
		END
END





GO
/****** Object:  StoredProcedure [GES].[pSistema]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [GES].[pSistema]  
(
	@Accion VARCHAR(15),
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, Version FROM Sistema

	IF (@Accion = 'ListarActivos')
		SELECT Id, Version FROM Sistema

	IF(@Accion = 'Consultar')
		BEGIN
			SELECT Id, Version FROM Sistema
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(varchar(10),StringValue) ELSE '0' END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string'
			GROUP BY parent_ID) 
		END

	IF(@Accion = 'Insertar')
		BEGIN
			INSERT INTO Sistema 
			SELECT * FROM (SELECT	
					max(CASE WHEN name='Id' THEN convert(VARCHAR(10),StringValue) ELSE '-' END) AS [Id],
					max(CASE WHEN name='Version' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Version]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string'
			GROUP BY parent_ID) Sistema
		END

	IF(@Accion = 'Editar')
		BEGIN
			UPDATE U
			SET Version = Sistema.Version
			FROM Sistema AS U
			INNER JOIN 
			(SELECT
				  max(CASE WHEN name='Version' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Version],
				  max(CASE WHEN name='Id' THEN convert(varchar(10),StringValue) ELSE '0' END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string'
			GROUP BY parent_ID) AS Sistema ON U.Id = Sistema.Id

		END

	IF(@Accion = 'Borrar')
		BEGIN
			DELETE U
			FROM Sistema AS U
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(varchar(10),StringValue) ELSE '0' END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string'
			GROUP BY parent_ID) AS Sistema ON U.Id = Sistema.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pAgente]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pAgente]  
(
	@Accion VARCHAR(30) = 'ListarTodos', --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, Nombre, Activo FROM Agente

	IF (@Accion = 'ListarActivos')
		SELECT Id, Nombre, Activo FROM Agente WHERE Activo = 1

	IF(@Accion = 'Consultar')
		BEGIN
			SELECT Id, Nombre, Activo FROM Agente
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 'Insertar')
		BEGIN
			INSERT INTO Agente 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Agente
		END

	IF(@Accion = 'Editar')
		BEGIN

			UPDATE A
			SET Nombre = Agente.Nombre,
				Activo = Agente.Activo
			FROM Agente AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Agente ON A.Id = Agente.Id

		END

	IF(@Accion = 'Borrar')
		BEGIN
			DELETE A
			FROM Agente AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Agente ON A.Id = Agente.Id
		END
END


--GO

--EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'18.0.1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'PROCEDURE',@level1name=N'pAgente'
--GO
GO
/****** Object:  StoredProcedure [SAF].[pBodega]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pBodega]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, idProyecto, idProveedor, Codigo, Nombre, Activo, EsSistema, ProyectoNombre, ProveedorNombre FROM vBodega

	IF (@Accion = 1)
		SELECT Id, idProyecto, idProveedor, Codigo, Nombre, Activo, EsSistema, ProyectoNombre, ProveedorNombre FROM vBodega WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT  Id, idProyecto, idProveedor, Codigo, Nombre, Activo, EsSistema, ProyectoNombre, ProveedorNombre FROM vBodega
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdBodega 
			SELECT * FROM (SELECT
					max(CASE WHEN name='idProyecto' THEN convert(int,StringValue) ELSE 0 END) AS [idProyecto],
					max(CASE WHEN name='idProveedor' THEN convert(int,StringValue) ELSE 0 END) AS [idProveedor],
					max(CASE WHEN name='Codigo' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [Codigo],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Nombre],					
					max(CASE WHEN name='EsSistema' THEN convert(BIT,StringValue) ELSE 0 END) AS [EsSistema],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
			GROUP BY parent_ID) Bodega
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Nombre = Bodega.Nombre,
				Activo = Bodega.Activo
			FROM bdBodega AS A
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo],
					max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
			GROUP BY parent_ID) AS Bodega ON A.Id = Bodega.Id AND A.EsSistema = 0

		END

	IF(@Accion = 5)
		BEGIN
			UPDATE A
			SET Activo = 0
			FROM bdBodega AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'int'
			GROUP BY parent_ID) AS Bodega ON A.Id = Bodega.Id AND A.EsSistema = 0
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pCliente]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [SAF].[pCliente]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, idCiudad, Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Nombre, Direccion, Telefono, Celular, Correo, Activo, CiudadNombre FROM VCliente

	IF (@Accion = 1)
		SELECT Id, idCiudad, Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Nombre, Direccion, Telefono, Celular, Correo, Activo, CiudadNombre FROM VCliente WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, idCiudad, Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Nombre, Direccion, Telefono, Celular, Correo, Activo, CiudadNombre FROM VCliente
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdCliente (idCiudad, Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Direccion, Telefono, Celular, Correo, Activo)
			SELECT idCiudad, Identificacion, Nombre1, Nombre2, Apellido1, Apellido2, Direccion, Telefono, Celular, Correo, Activo FROM (SELECT
					max(CASE WHEN name='idCiudad' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idCiudad],
					max(CASE WHEN name='Identificacion' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [Identificacion],
					max(CASE WHEN name='Nombre1' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Nombre1],
					max(CASE WHEN name='Nombre2' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Nombre2],
					max(CASE WHEN name='Apellido1' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Apellido1],
					max(CASE WHEN name='Apellido2' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Apellido2],
					max(CASE WHEN name='Direccion' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Direccion],
					max(CASE WHEN name='Telefono' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Telefono],
					max(CASE WHEN name='Celular' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Celular],
					max(CASE WHEN name='Correo' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Correo],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Cliente
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE C
			SET idCiudad = Cliente.idCiudad,
				Identificacion = Cliente.Identificacion,
				Nombre1 = Cliente.Nombre1,
				Nombre2 = Cliente.Nombre2,
				Apellido1 = Cliente.Apellido1,
				Apellido2 = Cliente.Apellido2,
				Direccion = Cliente.Direccion,
				Telefono = Cliente.Telefono,
				Celular = Cliente.Celular,
				Correo = Cliente.Correo,
				Activo = Cliente.Activo
			FROM bdCliente AS C
			INNER JOIN 
			(SELECT
				    max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
					max(CASE WHEN name='idCiudad' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idCiudad],
					max(CASE WHEN name='Identificacion' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [Identificacion],
					max(CASE WHEN name='Nombre1' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Nombre1],
					max(CASE WHEN name='Nombre2' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Nombre2],
					max(CASE WHEN name='Apellido1' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Apellido1],
					max(CASE WHEN name='Apellido2' THEN convert(VARCHAR(25),StringValue) ELSE '' END) AS [Apellido2],
					max(CASE WHEN name='Direccion' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Direccion],
					max(CASE WHEN name='Telefono' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Telefono],
					max(CASE WHEN name='Celular' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Celular],
					max(CASE WHEN name='Correo' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Correo],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) AS Cliente ON C.Id = Cliente.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE C
			FROM bdCliente AS C
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Cliente ON C.Id = Cliente.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pConductor]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pConductor]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Activo, Placa FROM bdConductor

	IF (@Accion = 1)
		SELECT Id, Nombre, Activo, Placa FROM bdConductor WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT  Id, Nombre, Activo, Placa FROM bdConductor
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdConductor 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Placa' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Placa],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Conductor
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Nombre = Conductor.Nombre,
				Placa = Conductor.Placa,
				Activo = Conductor.Activo
			FROM bdConductor AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Placa' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Placa],
				   max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Conductor ON A.Id = Conductor.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdConductor AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Conductor ON A.Id = Conductor.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pContrato]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [SAF].[pContrato]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF(@Accion = 0)
		SELECT Id, idProyecto, idListaPrecio, idAgente, InformacionBD, ContratoAlquiler, CartaPagare, Pagare, LetraCambio, GarantiasCondiciones 
				 , Deposito, Anticipo, PersonaJuridica, PersonaNatural, FotoCopiaCedula, FotoCopiaNit, CamaraComercio, DescuentoAlquiler, DescuentoVenta
				 , DescuentoReposicion, DescuentoMantenimiento, DescuentoTransporte, PorcentajeAgente, AgenteNombre, ListaPrecioNombre
		FROM vContrato

	IF(@Accion = 1)
		SELECT Id, idProyecto, idListaPrecio, idAgente, InformacionBD, ContratoAlquiler, CartaPagare, Pagare, LetraCambio, GarantiasCondiciones 
				 , Deposito, Anticipo, PersonaJuridica, PersonaNatural, FotoCopiaCedula, FotoCopiaNit, CamaraComercio, DescuentoAlquiler, DescuentoVenta
				 , DescuentoReposicion, DescuentoMantenimiento, DescuentoTransporte, PorcentajeAgente, AgenteNombre, ListaPrecioNombre
		FROM vContrato

	IF(@Accion = 2)
		BEGIN
			SELECT Id, idProyecto, idListaPrecio, idAgente, InformacionBD, ContratoAlquiler, CartaPagare, Pagare, LetraCambio, GarantiasCondiciones 
				 , Deposito, Anticipo, PersonaJuridica, PersonaNatural, FotoCopiaCedula, FotoCopiaNit, CamaraComercio, DescuentoAlquiler, DescuentoVenta
				 , DescuentoReposicion, DescuentoMantenimiento, DescuentoTransporte, PorcentajeAgente, AgenteNombre, ListaPrecioNombre
		    FROM vContrato 
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			PRINT 'Se insertar en el momento de insertar el proyecto'
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE C
			SET idListaPrecio = Contrato.idListaPrecio,
				idAgente = CASE WHEN Contrato.idAgente <= 0 THEN null ELSE Contrato.idAgente END, 
				InformacionBD = Contrato.InformacionBD,
				ContratoAlquiler = Contrato.ContratoAlquiler, 
				CartaPagare = Contrato.CartaPagare, 
				Pagare = Contrato.Pagare, 
				LetraCambio = Contrato.LetraCambio, 
				GarantiasCondiciones = Contrato.GarantiasCondiciones, 
				Deposito = Contrato.Deposito, 
				Anticipo = Contrato.Anticipo, 
				PersonaJuridica = Contrato.PersonaJuridica, 
				PersonaNatural = Contrato.PersonaNatural, 
				FotoCopiaCedula = Contrato.FotoCopiaCedula, 
				FotoCopiaNit = Contrato.FotoCopiaNit, 
				CamaraComercio = Contrato.CamaraComercio, 
				DescuentoAlquiler = Contrato.DescuentoAlquiler, 
				DescuentoVenta = Contrato.DescuentoVenta, 
				DescuentoReposicion = Contrato.DescuentoReposicion, 
				DescuentoMantenimiento = Contrato.DescuentoMantenimiento, 
				DescuentoTransporte = Contrato.DescuentoTransporte, 
				PorcentajeAgente = Contrato.PorcentajeAgente				
			FROM bdContrato AS C
			INNER JOIN 
			(SELECT
				    max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
					max(CASE WHEN name='idListaPrecio' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idListaPrecio],
					max(CASE WHEN name='idAgente' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idAgente],					
					max(CASE WHEN name='InformacionBD' THEN convert(BIT,StringValue) ELSE 0 END) AS [InformacionBD],
					max(CASE WHEN name='ContratoAlquiler' THEN convert(BIT,StringValue) ELSE 0 END) AS [ContratoAlquiler],
					max(CASE WHEN name='CartaPagare' THEN convert(BIT,StringValue) ELSE 0 END) AS [CartaPagare],
					max(CASE WHEN name='Pagare' THEN convert(BIT,StringValue) ELSE 0 END) AS [Pagare],
					max(CASE WHEN name='LetraCambio' THEN convert(BIT,StringValue) ELSE 0 END) AS [LetraCambio],
					max(CASE WHEN name='GarantiasCondiciones' THEN convert(BIT,StringValue) ELSE 0 END) AS [GarantiasCondiciones],
					max(CASE WHEN name='Deposito' THEN convert(BIT,StringValue) ELSE 0 END) AS [Deposito],
					max(CASE WHEN name='Anticipo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Anticipo],
					max(CASE WHEN name='PersonaJuridica' THEN convert(BIT,StringValue) ELSE 0 END) AS [PersonaJuridica],
					max(CASE WHEN name='PersonaNatural' THEN convert(BIT,StringValue) ELSE 0 END) AS [PersonaNatural],
					max(CASE WHEN name='FotoCopiaCedula' THEN convert(BIT,StringValue) ELSE 0 END) AS [FotoCopiaCedula],
					max(CASE WHEN name='FotoCopiaNit' THEN convert(BIT,StringValue) ELSE 0 END) AS [FotoCopiaNit],
					max(CASE WHEN name='CamaraComercio' THEN convert(BIT,StringValue) ELSE 0 END) AS [CamaraComercio],
					max(CASE WHEN name='DescuentoAlquiler' THEN convert(INT,StringValue) ELSE 0 END) AS [DescuentoAlquiler],
					max(CASE WHEN name='DescuentoVenta' THEN convert(INT,StringValue) ELSE 0 END) AS [DescuentoVenta],
					max(CASE WHEN name='DescuentoReposicion' THEN convert(INT,StringValue) ELSE 0 END) AS [DescuentoReposicion],
					max(CASE WHEN name='DescuentoMantenimiento' THEN convert(INT,StringValue) ELSE 0 END) AS [DescuentoMantenimiento],
					max(CASE WHEN name='DescuentoTransporte' THEN convert(INT,StringValue) ELSE 0 END) AS [DescuentoTransporte],
					max(CASE WHEN name='PorcentajeAgente' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [PorcentajeAgente]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Contrato ON C.Id = Contrato.Id

		END

	IF(@Accion = 5)
		BEGIN
			PRINT 'No se permite eliminar'
		END
END




GO
/****** Object:  StoredProcedure [SAF].[pDocumento]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [SAF].[pDocumento]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar, 6: Insertar Lista y Detalle, 7: Editar Lista y Detalle
	@Json NVARCHAR(max)	,
	@IdDocumento VARCHAR(10) OUTPUT
)
AS 
BEGIN
	SET @IdDocumento = ''

	IF (@Accion = 0)
		SELECT Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Fecha, Descripcion, Estado, DocumentoTipoNombre, BodegaOrigenNombre, BodegaDestinoNombre FROM vDocumento

	IF (@Accion = 1)
		SELECT Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Fecha, Descripcion, Estado, DocumentoTipoNombre, BodegaOrigenNombre, BodegaDestinoNombre FROM vDocumento WHERE Estado = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Fecha, Descripcion, Estado, DocumentoTipoNombre, BodegaOrigenNombre, BodegaDestinoNombre FROM vDocumento
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdDocumento (idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, Fecha, Descripcion, Estado)
			SELECT idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, Fecha, Descripcion, Estado 
					FROM (SELECT
					max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idDocumentoTipo],
					max(CASE WHEN name='idBodegaOrigen' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaOrigen],
					max(CASE WHEN name='idBodegaDestino' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaDestino],
					max(CASE WHEN name='Numero' THEN convert(INT,StringValue) ELSE 0 END) AS [Numero],
					max(CASE WHEN name='Fecha' THEN convert(DATETIME,StringValue) ELSE '' END) AS [Fecha],
					max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS [Descripcion],
					max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Documento
		END

	IF(@Accion = 4)
		BEGIN
			UPDATE D
			SET idDocumentoTipo = Documento.idDocumentoTipo,
				idBodegaOrigen = Documento.idBodegaOrigen,
				idBodegaDestino = Documento.idBodegaDestino,
				Fecha = Documento.Fecha,
				Descripcion = Documento.Descripcion,
				Estado = Documento.Estado
			FROM bdDocumento AS D
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
				    max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20), StringValue) ELSE '' END) AS [idDocumentoTipo],
					max(CASE WHEN name='idUnidadMedida' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idBodegaOrigen],
					max(CASE WHEN name='idBodegaDestino' THEN convert(VARCHAR(50),StringValue) ELSE 0 END) AS [idBodegaDestino],
					max(CASE WHEN name='Fecha' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Fecha],
					max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Descripcion],
					max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Documento ON D.Id = Documento.Id
		END

	IF(@Accion = 5)
		BEGIN
			DELETE D
			FROM bdDocumento AS D
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Documento ON D.Id = Documento.Id
		END

	IF(@Accion = 6)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY 

				--Insertar Documento				
				INSERT INTO bdDocumento (idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, Fecha, Descripcion, Estado)
						SELECT idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, Fecha, Descripcion, Estado 
						FROM (SELECT
						max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idDocumentoTipo],
						max(CASE WHEN name='idBodegaOrigen' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaOrigen],
						max(CASE WHEN name='idBodegaDestino' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaDestino],
						max(CASE WHEN name='Numero' THEN convert(INT,StringValue) ELSE 0 END) AS [Numero],
						max(CASE WHEN name='Fecha' THEN convert(DATETIME,StringValue) ELSE '' END) AS [Fecha],
						max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS [Descripcion],
						max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado]
							FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
						GROUP BY parent_ID) Documento
				WHERE Documento.Fecha <> ''

				SELECT @IdDocumento = SCOPE_IDENTITY()

				--Insertar Detalle
				INSERT INTO bdDocumentoDetalle (idDocumento, idElemento, Cantidad)
				SELECT @IdDocumento, idElemento, Cantidad
				FROM (SELECT
							max(CASE WHEN name='Fecha' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Fecha],
							max(CASE WHEN name='idElemento' THEN convert(INT,StringValue) ELSE 0 END) AS [idElemento],
							max(CASE WHEN name='Cantidad' THEN convert(INT,StringValue) ELSE 0 END) AS [Cantidad]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) DocumentoDetalle
				WHERE DocumentoDetalle.Fecha = '' AND DocumentoDetalle.Cantidad > 0 AND DocumentoDetalle.idElemento > 0
				
			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
	
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END

	IF(@Accion = 7)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY
				--Editar Documento
				UPDATE A
				SET idDocumentoTipo = Documento.idDocumentoTipo,
					idBodegaOrigen = Documento.idBodegaOrigen,
					idBodegaDestino = Documento.idBodegaDestino,
					Fecha = Documento.Fecha,
					Descripcion = Documento.Descripcion,
					Estado =  Documento.Estado
				FROM bdDocumento AS A
				INNER JOIN 
					(SELECT
							max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idDocumentoTipo],
							max(CASE WHEN name='idBodegaOrigen' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaOrigen],
							max(CASE WHEN name='idBodegaDestino' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaDestino],
							max(CASE WHEN name='Fecha' THEN convert(DATETIME,StringValue) ELSE '' END) AS [Fecha],
							max(CASE WHEN name='Descripcion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS [Descripcion],
							max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado],
							max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
					FROM SEG.fParseJSON
						(@json)
					WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
					GROUP BY parent_ID) AS Documento ON A.Id = Documento.Id
				WHERE Documento.Fecha <> ''
					
				--Borrar Detalle
				DELETE DD
				FROM bdDocumentoDetalle DD
				INNER JOIN 
				(SELECT max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id],
						max(CASE WHEN name='Fecha' THEN convert(DATETIME,StringValue) ELSE '' END) AS [Fecha]
				FROM SEG.fParseJSON
					(@json)
				WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
				GROUP BY parent_ID) AS Documento ON DD.idDocumento = Documento.Id 
				WHERE Documento.Fecha <> ''
				
				--Insertar Detalle
				SELECT @IdDocumento  = Documento.Id 
				FROM
				(SELECT max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id],
						max(CASE WHEN name='Fecha' THEN convert(DATETIME,StringValue) ELSE '' END) AS [Fecha]
				FROM SEG.fParseJSON
					(@json)
				WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
				GROUP BY parent_ID) AS Documento 
				WHERE Documento.Fecha <> ''

				INSERT INTO bdDocumentoDetalle (idDocumento, idElemento, Cantidad)
				SELECT @IdDocumento, idElemento, Cantidad
				FROM (SELECT
							max(CASE WHEN name='Fecha' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Fecha],
							max(CASE WHEN name='idElemento' THEN convert(INT,StringValue) ELSE 0 END) AS [idElemento],
							max(CASE WHEN name='Cantidad' THEN convert(INT,StringValue) ELSE 0 END) AS [Cantidad]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) DocumentoDetalle
				WHERE DocumentoDetalle.Fecha = '' AND DocumentoDetalle.Cantidad > 0 AND DocumentoDetalle.idElemento > 0

			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
  
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END
END





GO
/****** Object:  StoredProcedure [SAF].[pDocumentoDetalle]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [SAF].[pDocumentoDetalle]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, idDocumento, idElemento, idBodegaDestino, idBodegaOrigen, Cantidad, ElementoNombre, BodegaDestinoNombre, BodegaOrigenNombre, Descripcion  FROM vDocumentoDetalle

	IF (@Accion = 1)
		SELECT Id, idDocumento, idElemento, idBodegaDestino, idBodegaOrigen, Cantidad, ElementoNombre, BodegaDestinoNombre, BodegaOrigenNombre, Descripcion FROM vDocumentoDetalle WHERE Cantidad > 0 

	IF(@Accion = 2)
		BEGIN
			SELECT Id, idDocumento, idElemento, idBodegaDestino, idBodegaOrigen, Cantidad, ElementoNombre, BodegaDestinoNombre, BodegaOrigenNombre, Descripcion FROM vDocumentoDetalle
			WHERE idDocumento = 
			(SELECT      
				   max(CASE WHEN name='idDocumento' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

END






GO
/****** Object:  StoredProcedure [SAF].[pDocumentoTipo]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pDocumentoTipo]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Consecutivo,  Operacion, CantidadFilas, EsSistema, Activo FROM bdDocumentoTipo

	IF (@Accion = 1)
		SELECT Id, Nombre, Consecutivo,  Operacion, CantidadFilas, EsSistema, Activo FROM bdDocumentoTipo WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Nombre, Consecutivo,  Operacion, CantidadFilas, EsSistema, Activo FROM bdDocumentoTipo
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(100),StringValue) ELSE '0' END) AS Id
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdDocumentoTipo 
			SELECT * FROM (SELECT		
					max(CASE WHEN name='Id' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Id,			
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Nombre,
					max(CASE WHEN name='Consecutivo' THEN convert(BIGINT,StringValue) ELSE 0 END) AS Consecutivo,
					max(CASE WHEN name='Operacion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Operacion,
					max(CASE WHEN name='CantidadFilas' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS CantidadFilas,
					max(CASE WHEN name='EsSistema' THEN convert(BIT,StringValue) ELSE 0 END) AS EsSistema,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS Activo
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
			GROUP BY parent_ID) DocumentoTipo
		END
	
	IF(@Accion = 4)
		BEGIN
			UPDATE A
			SET Nombre = DocumentoTipo.Nombre,
				Consecutivo = DocumentoTipo.Consecutivo,
				Operacion = DocumentoTipo.Operacion,
				CantidadFilas = DocumentoTipo.CantidadFilas,
				EsSistema = DocumentoTipo.EsSistema,
				Activo = DocumentoTipo.Activo
			FROM bdDocumentoTipo AS A
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Id,					
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Nombre,
					max(CASE WHEN name='Consecutivo' THEN convert(BIGINT,StringValue) ELSE 0 END) AS Consecutivo,
					max(CASE WHEN name='Operacion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Operacion,
					max(CASE WHEN name='CantidadFilas' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS CantidadFilas,
					max(CASE WHEN name='EsSistema' THEN convert(BIT,StringValue) ELSE 0 END) AS EsSistema,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS Activo
			FROM SEG.fParseJSON
			(@json)
			) AS DocumentoTipo ON A.Id = DocumentoTipo.Id AND A.EsSistema = 0
		END
	
	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdDocumentoTipo AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(10),StringValue) ELSE '0' END) AS Id
			FROM SEG.fParseJSON
			(@json)
			) AS DocumentoTipo ON A.Id = DocumentoTipo.Id
		END
END


GO
/****** Object:  StoredProcedure [SAF].[pElemento]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [SAF].[pElemento]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id ,idGrupoElemento, idUnidadMedida, Referencia, Nombre, Mt2, Peso, Rotacion, Activo, GrupoElementoNombre, UnidadMedidaNombre FROM vElemento

	IF (@Accion = 1)
		SELECT Id ,idGrupoElemento, idUnidadMedida, Referencia, Nombre, Mt2, Peso, Rotacion, Activo, GrupoElementoNombre, UnidadMedidaNombre FROM vElemento WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id ,idGrupoElemento, idUnidadMedida, Referencia, Nombre, Mt2, Peso, Rotacion, Activo, GrupoElementoNombre, UnidadMedidaNombre FROM vElemento
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdElemento (idGrupoElemento, idUnidadMedida, Referencia, Nombre, Mt2, Peso, Rotacion, Activo)
			SELECT idGrupoElemento, idUnidadMedida, Referencia, Nombre, Mt2, Peso, Rotacion, Activo FROM (SELECT
					max(CASE WHEN name='idGrupoElemento' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idGrupoElemento],
					max(CASE WHEN name='idUnidadMedida' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idUnidadMedida],
					max(CASE WHEN name='Referencia' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Referencia],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Mt2' THEN convert(FLOAT,StringValue) ELSE '' END) AS [Mt2],
					max(CASE WHEN name='Peso' THEN convert(FLOAT,StringValue) ELSE '' END) AS [Peso],
					max(CASE WHEN name='Rotacion' THEN convert(BIT,StringValue) ELSE 0 END) AS [Rotacion],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Elemento
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE E
			SET idGrupoElemento = Elemento.idGrupoElemento,
				idUnidadMedida = Elemento.idUnidadMedida,
				Referencia = Elemento.Referencia,
				Nombre =Elemento.Nombre,
				Mt2 = Elemento.Mt2,
				Peso = Elemento.Peso,
				Rotacion = Elemento.Rotacion,
				Activo = Elemento.Activo
			FROM bdElemento AS E
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
				    max(CASE WHEN name='idGrupoElemento' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idGrupoElemento],
					max(CASE WHEN name='idUnidadMedida' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idUnidadMedida],
					max(CASE WHEN name='Referencia' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Referencia],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Mt2' THEN convert(FLOAT,StringValue) ELSE '' END) AS [Mt2],
					max(CASE WHEN name='Peso' THEN convert(FLOAT,StringValue) ELSE '' END) AS [Peso],
					max(CASE WHEN name='Rotacion' THEN convert(BIT,StringValue) ELSE 0 END) AS [Rotacion],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Elemento ON E.Id = Elemento.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE E
			FROM bdElemento AS E
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Elemento ON E.Id = Elemento.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pGrupoElemento]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pGrupoElemento]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Activo FROM bdGrupoElemento

	IF (@Accion = 1)
		SELECT Id, Nombre, Activo FROM bdGrupoElemento WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Nombre, Activo FROM bdGrupoElemento
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdGrupoElemento 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) GrupoElemento
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Nombre = GrupoElemento.Nombre,
				Activo = GrupoElemento.Activo
			FROM bdGrupoElemento AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS GrupoElemento ON A.Id = GrupoElemento.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdGrupoElemento AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS GrupoElemento ON A.Id = GrupoElemento.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pListaPrecio]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pListaPrecio]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar, 6: Insertar Lista y Detalle, 7: Editar Lista y Detalle
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Activo FROM bdListaPrecio

	IF (@Accion = 1)
		SELECT Id, Nombre, Activo FROM bdListaPrecio WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Nombre, Activo FROM bdListaPrecio
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdListaPrecio 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) ListaPrecio
		END

	IF(@Accion = 4)
		BEGIN
			UPDATE A
			SET Nombre = ListaPrecio.Nombre,
				Activo = ListaPrecio.Activo
			FROM bdListaPrecio AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS ListaPrecio ON A.Id = ListaPrecio.Id
		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdListaPrecio AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS ListaPrecio ON A.Id = ListaPrecio.Id
		END

	IF(@Accion = 6)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY 
				DECLARE @ID_LISTA AS INT

				--Insertar Lista
				INSERT INTO bdListaPrecio
				SELECT * 
				FROM (SELECT
							max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
							max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) ListaPrecio
				WHERE ListaPrecio.Nombre <> ''

				SELECT @ID_LISTA = SCOPE_IDENTITY()

				--Insertar Detalle
				INSERT INTO bdListaPrecioDetalle
				SELECT @ID_LISTA, idElemento, PrecioAlquiler, PrecioVenta, PrecioPerdida
				FROM (SELECT
							max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
							max(CASE WHEN name='idElemento' THEN convert(INT,StringValue) ELSE 0 END) AS [idElemento],
							max(CASE WHEN name='PrecioAlquiler' THEN convert(INT,StringValue) ELSE 0 END) AS [PrecioAlquiler],
							max(CASE WHEN name='PrecioVenta' THEN convert(INT,StringValue) ELSE 0 END) AS [PrecioVenta],
							max(CASE WHEN name='PrecioPerdida' THEN convert(INT,StringValue) ELSE 0 END) AS [PrecioPerdida]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) ListaPrecio
				WHERE ListaPrecio.Nombre = ''
			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
  
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END

	IF(@Accion = 7)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY
				--Editar Lista
				UPDATE A
				SET Nombre = ListaPrecio.Nombre,
					Activo = ListaPrecio.Activo
				FROM bdListaPrecio AS A
				INNER JOIN 
					(SELECT
							max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
							max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo],
							max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
					FROM SEG.fParseJSON
						(@json)
					WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
					GROUP BY parent_ID) AS ListaPrecio ON A.Id = ListaPrecio.Id
				WHERE ListaPrecio.Nombre <> ''
					
				--Editar Detalle
				UPDATE A
				SET PrecioAlquiler = ListaPrecioDetalle.PrecioAlquiler,
					PrecioVenta = ListaPrecioDetalle.PrecioVenta,
					PrecioPerdida = ListaPrecioDetalle.PrecioPerdida
				FROM bdListaPrecioDetalle AS A
				INNER JOIN 
				(SELECT
						max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id],
						max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
						max(CASE WHEN name='PrecioAlquiler' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioAlquiler],
						max(CASE WHEN name='PrecioVenta' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioVenta],
						max(CASE WHEN name='PrecioPerdida' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioPerdida]
				FROM SEG.fParseJSON
					(@json)
				WHERE ValueType = 'string' OR ValueType = 'int'
				GROUP BY parent_ID) AS ListaPrecioDetalle ON A.Id = ListaPrecioDetalle.Id
				WHERE ListaPrecioDetalle.Nombre = ''
			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
  
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END
END




GO
/****** Object:  StoredProcedure [SAF].[pListaPrecioDetalle]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pListaPrecioDetalle]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0 or @Accion = 1)
		SELECT Id, idListaPrecio, idElemento, ListaPrecioNombre, ElementoNombre, PrecioAlquiler, PrecioVenta, PrecioPerdida FROM vListaPrecioDetalle

	IF(@Accion = 2)
		BEGIN
			SELECT Id, idListaPrecio, idElemento, ListaPrecioNombre, ElementoNombre, PrecioAlquiler, PrecioVenta, PrecioPerdida FROM vListaPrecioDetalle
			WHERE idListaPrecio = 
				(SELECT      
					   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
				FROM SEG.fParseJSON
				( @json )
			) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdListaPrecioDetalle 
			SELECT * FROM (SELECT
					max(CASE WHEN name='idListaPrecio' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [idListaPrecio],
					max(CASE WHEN name='idElemento' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [idElemento],
					max(CASE WHEN name='PrecioAlquiler' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [PrecioAlquiler],
					max(CASE WHEN name='PrecioVenta' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [PrecioVenta],
					max(CASE WHEN name='PrecioPerdida' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [PrecioPerdida]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) ListaPrecioDetalle
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET PrecioAlquiler = ListaPrecioDetalle.PrecioAlquiler,
				PrecioVenta = ListaPrecioDetalle.PrecioVenta,
				PrecioPerdida = ListaPrecioDetalle.PrecioPerdida
			FROM bdListaPrecioDetalle AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id],
				   max(CASE WHEN name='PrecioAlquiler' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioAlquiler],
				   max(CASE WHEN name='PrecioVenta' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioVenta],
				   max(CASE WHEN name='PrecioPerdida' THEN convert(INT,StringValue) ELSE '' END) AS [PrecioPerdida]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS ListaPrecioDetalle ON A.Id = ListaPrecioDetalle.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdListaPrecioDetalle AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS ListaPrecioDetalle ON A.Id = ListaPrecioDetalle.Id
		END
END




GO
/****** Object:  StoredProcedure [SAF].[pProveedor]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pProveedor]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Identificacion, Nombre, Iniciales, Telefono, Direccion, Activo FROM bdProveedor

	IF (@Accion = 1)
		SELECT Id, Identificacion, Nombre, Iniciales, Telefono, Direccion, Activo FROM bdProveedor WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Identificacion, Nombre, Iniciales, Telefono, Direccion, Activo FROM bdProveedor
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(10),StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdProveedor 
			SELECT * FROM (SELECT									
					max(CASE WHEN name='Identificacion' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS Identificacion,
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Nombre,
					max(CASE WHEN name='Iniciales' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Iniciales,
					max(CASE WHEN name='Telefono' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Descripcion,
					max(CASE WHEN name='Direccion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Valor,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS Activo
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Proveedor
		END
	
	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Identificacion = Proveedor.Identificacion,
				Nombre = Proveedor.Nombre,
				Iniciales = Proveedor.Iniciales,
				Telefono = Proveedor.Telefono,
				Direccion = Proveedor.Direccion,
				Activo = Proveedor.Activo
			FROM bdProveedor AS A
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(smallint,StringValue) ELSE 0 END) AS Id,					
					max(CASE WHEN name='Identificacion' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS Identificacion,
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS Nombre,
					max(CASE WHEN name='Iniciales' THEN convert(VARCHAR(10),StringValue) ELSE '' END) AS Iniciales,
					max(CASE WHEN name='Telefono' THEN (CASE WHEN StringValue = 'null' THEN NULL ELSE convert(VARCHAR(10),StringValue) END) ELSE '' END) AS Telefono,
					max(CASE WHEN name='Direccion' THEN (CASE WHEN StringValue = 'null' THEN NULL ELSE convert(VARCHAR(10),StringValue) END) ELSE '' END) AS Direccion,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS Activo
			FROM SEG.fParseJSON
			(@json)
			) AS Proveedor ON A.Id = Proveedor.Id
		END
	
	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdProveedor AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(smallint,StringValue) ELSE 0 END) AS Id
			FROM SEG.fParseJSON
			(@json)
			) AS ListaPrecio ON A.Id = ListaPrecio.Id
		END
END





GO
/****** Object:  StoredProcedure [SAF].[pProyecto]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pProyecto]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar, 6:Generar Plantilla
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, idCliente, idCiudad, Nombre, Tipo, Direccion, Telefono, Observacion, Fecha, FormaContacto, SistemaMedida, IdentificacionResponsable, NombreResponsable, TelResponsable, Activo, Estado
	           ,CiudadNombre, ClienteNombre, idContrato FROM vProyecto

	IF (@Accion = 1)
		SELECT Id, idCliente, idCiudad, Nombre, Tipo, Direccion, Telefono, Observacion, Fecha, FormaContacto, SistemaMedida, IdentificacionResponsable, NombreResponsable, TelResponsable, Activo, Estado
	           ,CiudadNombre, ClienteNombre, idContrato FROM vProyecto WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, idCliente, idCiudad, Nombre, Tipo, Direccion, Telefono, Observacion, Fecha, FormaContacto, SistemaMedida, IdentificacionResponsable, NombreResponsable, TelResponsable, Activo, Estado
	           ,CiudadNombre, ClienteNombre, idContrato FROM vProyecto 
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdProyecto (idCliente, idCiudad, Nombre, Tipo, Direccion, Telefono, Observacion, Fecha, FormaContacto, SistemaMedida, IdentificacionResponsable, NombreResponsable, TelResponsable, Activo, Estado)
			SELECT idCliente, idCiudad, Nombre, Tipo, Direccion, Telefono, Observacion, GETDATE(), FormaContacto, SistemaMedida, IdentificacionResponsable, NombreResponsable, TelResponsable, 1, 1
			       FROM (SELECT
				    max(CASE WHEN name='idCliente' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idCliente],
					max(CASE WHEN name='idCiudad' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idCiudad],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Tipo' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Tipo],					
					max(CASE WHEN name='Direccion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Direccion],
					max(CASE WHEN name='Telefono' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Telefono],
					max(CASE WHEN name='Observacion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS [Observacion],
					max(CASE WHEN name='FormaContacto' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [FormaContacto],
					max(CASE WHEN name='SistemaMedida' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [SistemaMedida],
					max(CASE WHEN name='IdentificacionResponsable' THEN convert(VARCHAR(15),StringValue) ELSE '' END) AS [IdentificacionResponsable],
					max(CASE WHEN name='NombreResponsable' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [NombreResponsable],
					max(CASE WHEN name='TelResponsable' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [TelResponsable]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Proyecto
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE P
			SET idCliente = Proyecto.idCliente,
				idCiudad = Proyecto.idCiudad,
				Nombre = Proyecto.Nombre,
				Tipo = Proyecto.Tipo, 
				Direccion = Proyecto.Direccion,
				Telefono = Proyecto.Telefono,
				Observacion = Proyecto.Observacion,				
				FormaContacto = Proyecto.FormaContacto,
				SistemaMedida = Proyecto.SistemaMedida, 
				IdentificacionResponsable = Proyecto.IdentificacionResponsable,  
				NombreResponsable = Proyecto.NombreResponsable, 
				TelResponsable = Proyecto.TelResponsable
			FROM bdProyecto AS P
			INNER JOIN 
			(SELECT
				    max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
					max(CASE WHEN name='idCliente' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idCliente],
					max(CASE WHEN name='idCiudad' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idCiudad],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Tipo' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Tipo],					
					max(CASE WHEN name='Direccion' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Direccion],
					max(CASE WHEN name='Telefono' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Telefono],
					max(CASE WHEN name='Observacion' THEN convert(VARCHAR(500),StringValue) ELSE '' END) AS [Observacion],					
					max(CASE WHEN name='FormaContacto' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [FormaContacto],
					max(CASE WHEN name='SistemaMedida' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [SistemaMedida],
					max(CASE WHEN name='IdentificacionResponsable' THEN convert(VARCHAR(15),StringValue) ELSE '' END) AS [IdentificacionResponsable],
					max(CASE WHEN name='NombreResponsable' THEN convert(VARCHAR(200),StringValue) ELSE '' END) AS [NombreResponsable],
					max(CASE WHEN name='TelResponsable' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [TelResponsable]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) AS Proyecto ON P.Id = Proyecto.Id

		END

	IF(@Accion = 5)
		BEGIN
			UPDATE P
			SET Activo = 0
			FROM bdProyecto AS P
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Proyecto ON P.Id = Proyecto.Id
		END

	IF(@Accion = 6)
		BEGIN
			
			SET LANGUAGE Spanish;
			DECLARE @idProyecto AS INT
			DECLARE @NoDocumento AS VARCHAR(400)
			DECLARE @Columnas AS VARCHAR(400)

			SET @idProyecto = (SELECT max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id]
							   FROM SEG.fParseJSON ( @json ))

			DECLARE Plantilla CURSOR FOR 
				SELECT Tipo + '_' + CAST(Id AS VARCHAR) 
				FROM SAF.vProyectoPlantilla
				WHERE idProyecto = @idProyecto
				GROUP BY Tipo + '_' + CAST(Id AS VARCHAR) , Fecha
				ORDER BY Fecha

			SET @Columnas = N'idElemento SMALLINT NULL, Elemento VARCHAR(100) NULL, '
			OPEN Plantilla
			FETCH NEXT FROM Plantilla INTO @NoDocumento
			WHILE @@fetch_status = 0
			BEGIN
				SET @Columnas = @Columnas + @NoDocumento + N' VARCHAR(100) NULL, '
				FETCH NEXT FROM Plantilla INTO @NoDocumento 
			END
			CLOSE Plantilla
			DEALLOCATE Plantilla

			SET @Columnas = @Columnas + N'Total VARCHAR(6) NULL'

			DECLARE @sql AS nvarchar(MAX)
			SET @sql = 'CREATE TABLE ##PlantillaTemp( ' + @Columnas + ')'
			EXEC (@sql)

			INSERT INTO ##PlantillaTemp (idElemento, Elemento, Total) VALUES (0, 'Columna', 0)
			INSERT INTO ##PlantillaTemp (idElemento, Elemento, Total) VALUES (0, 'Fecha', 0)
			INSERT INTO ##PlantillaTemp (idElemento, Elemento, Total) VALUES (0, 'Vista', 0)
			INSERT INTO ##PlantillaTemp (idElemento, Elemento, Total)
			SELECT P.idElemento, P.Elemento, 
					(SELECT SUM(CAST(Cantidad AS INT)) FROM SAF.vProyectoPlantilla WHERE idProyecto = @idProyecto AND Tipo IN ('E','R') AND idElemento = P.idElemento AND Elemento = P.Elemento)
			FROM SAF.vProyectoPlantilla P
			WHERE P.idProyecto = @idProyecto
			GROUP BY P.idElemento, P.Elemento
			ORDER BY P.Elemento 

			DECLARE @Tipo AS VARCHAR(2)
			DECLARE @Id AS INT
			DECLARE @IdElemento AS SMALLINT
			DECLARE @Cantidad AS VARCHAR(50)

			DECLARE Plantilla CURSOR FOR 
				SELECT Tipo, Id, idElemento, Cantidad
				FROM SAF.vProyectoPlantilla
				WHERE idProyecto = @idProyecto

			OPEN Plantilla
			FETCH NEXT FROM Plantilla INTO @Tipo, @Id, @IdElemento, @Cantidad
			WHILE @@fetch_status = 0
			BEGIN
				SET @sql = 'UPDATE ##PlantillaTemp SET ' + @Tipo + '_' + CAST(@Id AS VARCHAR) + ' = ''' + @Cantidad + ''' WHERE idElemento = ' + CAST(@IdElemento AS VARCHAR)
				EXEC (@sql)	
				FETCH NEXT FROM Plantilla INTO @Tipo, @Id, @IdElemento, @Cantidad 
			END
			CLOSE Plantilla
			DEALLOCATE Plantilla

			DECLARE @Documento AS VARCHAR(10)
			DECLARE @Fecha AS VARCHAR(10)
			DECLARE @Vista AS VARCHAR(100)

			DECLARE Plantilla CURSOR FOR 
				SELECT Tipo, Id, Tipo + '_' + CAST(Id AS VARCHAR) Documento , CONVERT(VARCHAR, MIN(Fecha), 7) Fecha, MIN(Documento) Vista
				FROM SAF.vProyectoPlantilla
				WHERE idProyecto = @idProyecto
				GROUP BY Tipo, Id, Tipo + '_' + CAST(Id AS VARCHAR)

			OPEN Plantilla
			FETCH NEXT FROM Plantilla INTO @Tipo, @Id, @Documento, @Fecha, @Vista
			WHILE @@fetch_status = 0
			BEGIN	
				SET @sql = 'UPDATE ##PlantillaTemp SET ' + @Documento + ' = ''' + @Tipo + '. ' + CAST(@Id AS VARCHAR) + ''' WHERE Elemento = ''Columna'''		
				EXEC (@sql)
				SET @sql = 'UPDATE ##PlantillaTemp SET ' + @Documento + ' = ''' + @Fecha + ''' WHERE Elemento = ''Fecha'''		
				EXEC (@sql)
				SET @sql = 'UPDATE ##PlantillaTemp SET ' + @Documento + ' = ''' + @Vista + ''' WHERE Elemento = ''Vista'''	
				EXEC (@sql)
				FETCH NEXT FROM Plantilla INTO @Tipo, @Id, @Documento, @Fecha, @Vista 
			END
			CLOSE Plantilla
			DEALLOCATE Plantilla

			SELECT * FROM ##PlantillaTemp
			DROP TABLE ##PlantillaTemp

		END
END



GO
/****** Object:  StoredProcedure [SAF].[pRemision]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pRemision]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar, 6: Insertar Lista y Detalle, 7: Editar Lista y Detalle
	@Json NVARCHAR(max)	,
	@IdRemision INT OUTPUT
)
AS 
BEGIN
	SET @IdRemision = 0

	IF (@Accion = 0)
		SELECT vRemision.Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, idConductor, idProyecto, FechaEntrega, FechaPedido, vRemision.Estado, DocumentoTipoNombre, BodegaOrigenNombre, 
		Transporte, ValorTransporte, Despachado, EquipoAdecuado, PesoEquipo, ValorEquipo, BodegaDestinoNombre, C.Nombre ClienteNombre
		FROM vRemision
		INNER JOIN SAF.bdProyecto AS P ON vRemision.idProyecto = P.Id 
		INNER JOIN SAF.bdCliente AS C ON P.idCliente = C.Id

	IF (@Accion = 1)
		SELECT vRemision.Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, idConductor, idProyecto, FechaEntrega, FechaPedido, vRemision.Estado, DocumentoTipoNombre, BodegaOrigenNombre, 
		Transporte, ValorTransporte, Despachado, EquipoAdecuado, PesoEquipo, ValorEquipo, BodegaDestinoNombre, C.Nombre ClienteNombre 
		FROM vRemision
		INNER JOIN SAF.bdProyecto AS P ON vRemision.idProyecto = P.Id 
		INNER JOIN SAF.bdCliente AS C ON P.idCliente = C.Id
		WHERE vRemision.Estado = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Numero, idDocumentoTipo, idBodegaOrigen, idBodegaDestino, idConductor, idProyecto, FechaEntrega, FechaPedido, Estado, DocumentoTipoNombre, BodegaOrigenNombre, 
				   Transporte, ValorTransporte, Despachado, EquipoAdecuado, PesoEquipo, ValorEquipo, BodegaDestinoNombre FROM vRemision
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdRemision (idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, FechaEntrega, Estado)
			SELECT idDocumentoTipo, idBodegaOrigen, idBodegaDestino, Numero, FechaEntrega, Estado 
					FROM (SELECT
					max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20),StringValue) ELSE '' END) AS [idDocumentoTipo],
					max(CASE WHEN name='idBodegaOrigen' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaOrigen],
					max(CASE WHEN name='idBodegaDestino' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idBodegaDestino],
					max(CASE WHEN name='Numero' THEN convert(INT,StringValue) ELSE 0 END) AS [Numero],
					max(CASE WHEN name='FechaEntrega' THEN convert(DATETIME,StringValue) ELSE '' END) AS [FechaEntrega],
					max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Remision
		END

	IF(@Accion = 4)
		BEGIN
			UPDATE R
			SET idDocumentoTipo = Remision.idDocumentoTipo,
				idBodegaOrigen = Remision.idBodegaOrigen,
				idBodegaDestino = Remision.idBodegaDestino,
				FechaEntrega = Remision.FechaEntrega,
				Estado = Remision.Estado
			FROM bdRemision AS R
			INNER JOIN 
			(SELECT
					max(CASE WHEN name='Id' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [Id],
				    max(CASE WHEN name='idDocumentoTipo' THEN convert(VARCHAR(20), StringValue) ELSE '' END) AS [idDocumentoTipo],
					max(CASE WHEN name='idUnidadMedida' THEN convert(SMALLINT, StringValue) ELSE 0 END) AS [idBodegaOrigen],
					max(CASE WHEN name='idBodegaDestino' THEN convert(VARCHAR(50),StringValue) ELSE 0 END) AS [idBodegaDestino],
					max(CASE WHEN name='FechaEntrega' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaEntrega],
					max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS Remision ON R.Id = Remision.Id
		END

	IF(@Accion = 5)
		BEGIN
			DELETE R
			FROM bdRemision AS R
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS Remision ON R.Id = Remision.Id
		END

	IF(@Accion = 6)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY 

				--Insertar Remision				
				INSERT INTO bdRemision (idDocumentoTipo, idBodegaOrigen, idBodegaDestino, idProyecto, Numero, FechaEntrega, FechaPedido, Estado)
				SELECT 'REM', SAF.fObtenerBodega('CODIGO', 'PRINCIPAL'), SAF.fObtenerBodega('PROYECTO', idProyecto), idProyecto, 0, FechaEntrega, GETDATE(), 'A' 
				FROM (SELECT				
				max(CASE WHEN name='idProyecto' THEN convert(INT,StringValue) ELSE 0 END) AS [idProyecto],
				max(CASE WHEN name='FechaEntrega' THEN convert(DATETIME,StringValue) ELSE '' END) AS [FechaEntrega]
					FROM SEG.fParseJSON
					( @Json )
				WHERE ValueType = 'int' OR ValueType = 'string' OR ValueType = 'boolean'
				GROUP BY parent_ID) Remision
				WHERE Remision.FechaEntrega <> ''

				SELECT @IdRemision = SCOPE_IDENTITY()

				--Insertar Detalle
				INSERT INTO bdRemisionDetalle (idRemision, idElemento, Cantidad)
				SELECT @IdRemision, idElemento, Cantidad
				FROM (SELECT
							max(CASE WHEN name='FechaEntrega' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaEntrega],
							max(CASE WHEN name='idElemento' THEN convert(INT,StringValue) ELSE 0 END) AS [idElemento],
							max(CASE WHEN name='Cantidad' THEN convert(INT,StringValue) ELSE 0 END) AS [Cantidad]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) RemisionDetalle
				WHERE RemisionDetalle.FechaEntrega = '' AND RemisionDetalle.Cantidad > 0 AND RemisionDetalle.idElemento > 0
				
			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
	
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END

	IF(@Accion = 7)
		BEGIN
			BEGIN TRANSACTION;  
			BEGIN TRY
				--Editar Documento
				UPDATE R
				SET FechaEntrega = Remision.FechaEntrega,
					Estado =  Remision.Estado
				FROM bdRemision AS R
				INNER JOIN 
					(SELECT							
							max(CASE WHEN name='FechaEntrega' THEN convert(DATETIME,StringValue) ELSE '' END) AS [FechaEntrega],
							max(CASE WHEN name='Estado' THEN convert(BIT,StringValue) ELSE 0 END) AS [Estado],
							max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
					FROM SEG.fParseJSON
						(@json)
					WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
					GROUP BY parent_ID) AS Remision ON R.Id = Remision.Id
				WHERE Remision.FechaEntrega <> ''
					
				--Borrar Detalle
				DELETE RD
				FROM bdRemisionDetalle RD
				INNER JOIN 
				(SELECT max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id],
						max(CASE WHEN name='FechaEntrega' THEN convert(DATETIME,StringValue) ELSE '' END) AS [FechaEntrega]
				FROM SEG.fParseJSON
					(@json)
				WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
				GROUP BY parent_ID) AS Remision ON RD.idRemision = Remision.Id 
				WHERE Remision.FechaEntrega <> ''
				
				--Insertar Detalle
				SELECT @IdRemision  = Remision.Id 
				FROM
				(SELECT max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id],
						max(CASE WHEN name='FechaEntrega' THEN convert(DATETIME,StringValue) ELSE '' END) AS [FechaEntrega]
				FROM SEG.fParseJSON
					(@json)
				WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
				GROUP BY parent_ID) AS Remision 
				WHERE Remision.FechaEntrega <> ''

				INSERT INTO bdRemisionDetalle (idRemision, idElemento, Cantidad)
				SELECT @IdRemision, idElemento, Cantidad
				FROM (SELECT
							max(CASE WHEN name='FechaEntrega' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaEntrega],
							max(CASE WHEN name='idElemento' THEN convert(INT,StringValue) ELSE 0 END) AS [idElemento],
							max(CASE WHEN name='Cantidad' THEN convert(INT,StringValue) ELSE 0 END) AS [Cantidad]
						FROM SEG.fParseJSON
							( @Json )
						WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'int'
						GROUP BY parent_ID) RemisionDetalle
				WHERE RemisionDetalle.FechaEntrega = '' AND RemisionDetalle.Cantidad > 0 AND RemisionDetalle.idElemento > 0

			END TRY  
			BEGIN CATCH  
				SELECT   
					ERROR_NUMBER() AS ErrorNumber  
					,ERROR_SEVERITY() AS ErrorSeverity  
					,ERROR_STATE() AS ErrorState  
					,ERROR_PROCEDURE() AS ErrorProcedure  
					,ERROR_LINE() AS ErrorLine  
					,ERROR_MESSAGE() AS ErrorMessage;  
  
				IF @@TRANCOUNT > 0  
					ROLLBACK TRANSACTION;  
			END CATCH;  
  
			IF @@TRANCOUNT > 0  
				COMMIT TRANSACTION;  
		END

	IF(@Accion = 8)
		BEGIN
			SELECT Id, idElemento, idRemision, Cantidad, ElementoNombre FROM SAF.vRemisionDetalle
			WHERE idRemision = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END
END






GO
/****** Object:  StoredProcedure [SAF].[pTipoMantenimiento]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SAF].[pTipoMantenimiento]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Valor, Activo FROM bdTipoMantenimiento

	IF (@Accion = 1)
		SELECT Id, Nombre, Valor, Activo FROM bdTipoMantenimiento WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Nombre, Valor, Activo FROM bdTipoMantenimiento
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SAFseg.dbo.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdTipoMantenimiento 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Valor' THEN convert(numeric(18,0),StringValue) ELSE 0 END) AS Valor,
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SAFseg.dbo.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean' OR ValueType = 'real'
			GROUP BY parent_ID) TipoMantenimiento
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Nombre = TipoMantenimiento.Nombre,
				Valor = TipoMantenimiento.Valor,
				Activo = TipoMantenimiento.Activo
			FROM bdTipoMantenimiento AS A
			INNER JOIN 
			(SELECT				   
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id],
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Valor' THEN convert(numeric(18,0),StringValue) ELSE 0 END) AS Valor,
				   max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SAFseg.dbo.fParseJSON
			(
				  @json
			)) AS TipoMantenimiento ON A.Id = TipoMantenimiento.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdTipoMantenimiento AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SAFseg.dbo.fParseJSON
			(
				  @json
			)) AS TipoMantenimiento ON A.Id = TipoMantenimiento.Id
		END
END



GO
/****** Object:  StoredProcedure [SAF].[pUnidadMedida]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SAF].[pUnidadMedida]  
(
	@Accion INT = 0, --0:Listar Todos, 1: Listar Activos, 2: Consultar, 3: Insertar, 4: Editar, 5: Borrar
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 0)
		SELECT Id, Nombre, Activo FROM bdUnidadMedida

	IF (@Accion = 1)
		SELECT Id, Nombre, Activo FROM bdUnidadMedida WHERE Activo = 1

	IF(@Accion = 2)
		BEGIN
			SELECT Id, Nombre, Activo FROM bdUnidadMedida
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) 
		END

	IF(@Accion = 3)
		BEGIN
			INSERT INTO bdUnidadMedida 
			SELECT * FROM (SELECT
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo]
			FROM SEG.fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) UnidadMedida
		END

	IF(@Accion = 4)
		BEGIN

			UPDATE A
			SET Nombre = UnidadMedida.Nombre,
				Activo = UnidadMedida.Activo
			FROM bdUnidadMedida AS A
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Activo' THEN convert(bit,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM SEG.fParseJSON
			(
				  @json
			)) AS UnidadMedida ON A.Id = UnidadMedida.Id

		END

	IF(@Accion = 5)
		BEGIN
			DELETE A
			FROM bdUnidadMedida AS A
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE 0 END) AS [Id]
			From SEG.fParseJSON
			(
				  @json
			)) AS UnidadMedida ON A.Id = UnidadMedida.Id
		END
END




GO
/****** Object:  StoredProcedure [SEG].[pAutenticacion]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [SEG].[pAutenticacion]  
(
	@Accion VARCHAR(20) = 'ListarTodos',
	@Json NVARCHAR(max)
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin FROM Autenticacion

	IF (@Accion = 'ListarActivos')
		SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin FROM Autenticacion WHERE FechaFin IS NULL

	IF (@Accion = 'Verificar')
		BEGIN
			SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin
			FROM Autenticacion
			WHERE FechaFin IS NULL AND idUsuario = 
			(
					SELECT idUsuario
					FROM (SELECT
								max(CASE WHEN name='Usuario' THEN convert(VARCHAR(30),StringValue) ELSE '' END) AS [idUsuario]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string') Autenticacion 
			) AND
			Token = 
			(
					SELECT Token
					FROM (SELECT
								max(CASE WHEN name='Token' THEN convert(VARCHAR(1000),StringValue) ELSE '' END) AS [Token]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string') Autenticacion 
			) 
		END

	IF (@Accion = 'Insertar')
		BEGIN

			DECLARE @Id AS int

			SELECT @Id = Id
			FROM Autenticacion
			WHERE FechaFin IS NULL AND idUsuario = 
			(
					SELECT idUsuario
					FROM (SELECT
								max(CASE WHEN name='Usuario' THEN convert(VARCHAR(30),StringValue) ELSE '' END) AS [idUsuario]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string') Autenticacion 
			) 

			IF (@Id > 0 )
				BEGIN
					UPDATE A
					SET FechaFin = GETDATE()
					FROM Autenticacion A
					WHERE FechaFin IS NULL AND idUsuario = 
					(
							SELECT idUsuario
							FROM (SELECT
										max(CASE WHEN name='Usuario' THEN convert(VARCHAR(30),StringValue) ELSE '' END) AS [idUsuario]
								FROM fParseJSON
								( @Json )
							WHERE ValueType = 'string') Autenticacion 
					)
				END	
			
				INSERT INTO Autenticacion 
				SELECT idUsuario, Token, null, CONVERT( VARCHAR(22), FechaInicio ,108), null FROM (SELECT
						max(CASE WHEN name='Usuario' THEN convert(VARCHAR(30),StringValue) ELSE '' END) AS [idUsuario],
						max(CASE WHEN name='Token' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Token],
						max(CASE WHEN name='FechaInicio' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaInicio]
				FROM fParseJSON
				( @Json )
				WHERE ValueType = 'string' OR ValueType = 'boolean'
				GROUP BY parent_ID) Autenticacion

		END
END


GO
/****** Object:  StoredProcedure [SEG].[pSesion]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [SEG].[pSesion]  
(
	@Accion VARCHAR(20) = 'ListarTodos', --0:Listar Todos, 1: Listar Sesion Activas, 2: Consultar, 3: Insertar, 4: Abrir Conexion, 5: Cerrar Conexion
	@Json NVARCHAR(max),
	@Id_Sesion INT = 0 OUTPUT
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin, Tiempo FROM Sesion

	IF (@Accion = 'ListarActivos')
		SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin, Tiempo FROM Sesion WHERE FechaFin IS NULL

	IF (@Accion = 'Consultar')
		BEGIN
			SELECT Id, idUsuario, Token, Terminal, FechaInicio, FechaFin, Tiempo FROM Sesion 
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(int,StringValue) ELSE 0 END) AS [Id]
			FROM fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'int'
			GROUP BY parent_ID) 
		END

	IF (@Accion = 'Insertar')
		BEGIN
			SELECT @Id_Sesion = Id 
			FROM Sesion
			WHERE FechaFin IS NULL AND idUsuario = 
			(
					SELECT idUsuario
					FROM (SELECT
								max(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [idUsuario]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string') Sesion 
			) AND
			Token = 
			(
					SELECT Token
					FROM (SELECT
								max(CASE WHEN name='Token' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Token]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string') Sesion 
			)

			IF (@Id_Sesion IS NULL OR @Id_Sesion <= 0 )
				BEGIN
					UPDATE S
					SET FechaFin = GETDATE()
					FROM Sesion S
					WHERE FechaFin IS NULL AND idUsuario = 
					(
							SELECT idUsuario
							FROM (SELECT
										max(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [idUsuario]
								FROM fParseJSON
								( @Json )
							WHERE ValueType = 'string') Sesion 
					)					

					INSERT INTO Sesion 
					SELECT idUsuario, Token, 'SERVER', CONVERT( VARCHAR(22), FechaInicio ,108), null, null, 0
					FROM (SELECT
								max(CASE WHEN name='Id' THEN convert(VARCHAR(30),StringValue) ELSE '' END) AS [idUsuario],
								max(CASE WHEN name='Token' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Token],
								max(CASE WHEN name='Terminal' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Terminal],
								max(CASE WHEN name='FechaInicio' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaInicio],
								max(CASE WHEN name='FechaFin' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [FechaFin],
								max(CASE WHEN name='Tiempo' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Tiempo]					
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'string' OR ValueType = 'int') Sesion

					SELECT @Id_Sesion = SCOPE_IDENTITY()
				END
		END

	IF (@Accion = 'AbrirSesion')
		BEGIN
			UPDATE S
			SET IdSesionBd =  @@SPID 
			FROM Sesion AS S
			WHERE Id = 
			(
					SELECT IdSession
					FROM (SELECT
								max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE '' END) AS [IdSession]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'int') Sesion 
			)		
		END
	IF (@Accion = 'CerrarSesion')
		BEGIN
			UPDATE S
			SET IdSesionBd =  0 
			FROM Sesion AS S
			WHERE Id = 
			(
					SELECT IdSession
					FROM (SELECT
								max(CASE WHEN name='Id' THEN convert(INT,StringValue) ELSE '' END) AS [IdSession]
						FROM fParseJSON
						( @Json )
					WHERE ValueType = 'int') Sesion 
			)		
		END
END


GO
/****** Object:  StoredProcedure [SEG].[pUsuario]    Script Date: 02/02/2025 6:43:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [SEG].[pUsuario]  
(
	@Accion VARCHAR(20) = 'ListarTodos', 
	@Json NVARCHAR(max)	
)
AS 
BEGIN
	IF (@Accion = 'ListarTodos')
		SELECT Id, Identificacion, Nombre, Apellido, Usuario, Clave, Correo, Activo, Admin FROM Usuario

	IF (@Accion = 'ListarActivos')
		SELECT Id, Identificacion, Nombre, Apellido, Usuario, Clave, Correo, Activo, Admin FROM Usuario WHERE Activo = 1

	IF(@Accion = 'Consultar')
		BEGIN
			SELECT Id, Identificacion, Nombre, Apellido, Usuario, Clave, Correo, Activo, Admin FROM Usuario 
			WHERE Id = 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Id]
			FROM fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) 
		END

	IF(@Accion = 'Insertar')
		BEGIN
			INSERT INTO Usuario 
			SELECT * FROM (SELECT
					max(CASE WHEN name='idRol' THEN convert(SMALLINT,StringValue) ELSE 0 END) AS [idRol],
					max(CASE WHEN name='Identificacion' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Identificacion],
					max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
					max(CASE WHEN name='Apellido' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Apellido],
					max(CASE WHEN name='Usuario' THEN convert(VARCHAR(15),StringValue) ELSE '' END) AS [Usuario],
					max(CASE WHEN name='Clave' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Clave],
					max(CASE WHEN name='Correo' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Correo],					
					max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo],
					max(CASE WHEN name='Admin' THEN convert(BIT,StringValue) ELSE 0 END) AS [Admin]
			FROM fParseJSON
			( @Json )
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) Usuario
		END

	IF(@Accion = 'Editar')
		BEGIN
			UPDATE U
			SET Nombre = Usuario.Nombre,
				Activo = Usuario.Activo
			FROM Usuario AS U
			INNER JOIN 
			(SELECT
				   max(CASE WHEN name='Nombre' THEN convert(VARCHAR(100),StringValue) ELSE '' END) AS [Nombre],
				   max(CASE WHEN name='Activo' THEN convert(BIT,StringValue) ELSE 0 END) AS [Activo],
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Id]
			FROM fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) AS Usuario ON U.Id = Usuario.Id

		END

	IF(@Accion = 'Borrar')
		BEGIN
			DELETE U
			FROM Usuario AS U
			INNER JOIN 
			(SELECT      
				   max(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Id]
			From fParseJSON
			(
				  @json
			)
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) AS Usuario ON U.Id = Usuario.Id
		END

	IF(@Accion = 'Autenticar')
		BEGIN
			SELECT Id, Identificacion, Nombre, Apellido, U.Usuario, U.Clave, Correo, Activo, Admin 
			FROM Usuario AS U
			INNER JOIN 
			(SELECT 
				max(CASE WHEN name='Usuario' THEN convert(VARCHAR(15),StringValue) ELSE '' END) AS [Usuario],
				max(CASE WHEN name='Clave' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Clave]
			FROM fParseJSON
			(
				@json
			)
			WHERE ValueType = 'string' OR ValueType = 'boolean'
			GROUP BY parent_ID) AS Usuario ON U.Usuario = Usuario.Usuario AND U.Clave = Usuario.Clave
		END
	IF (@Accion = 'ListarMenu')
		BEGIN

			DECLARE @Usuario AS VARCHAR(50) 
			SET @Usuario =  (	SELECT      
								MAX(CASE WHEN name='Id' THEN convert(VARCHAR(50),StringValue) ELSE '' END) AS [Id]
								FROM fParseJSON ( @json )
								WHERE ValueType = 'string' OR ValueType = 'boolean'
								GROUP BY parent_ID)

			CREATE TABLE ##TablaTemporal (Id varchar(50), Nombre varchar(50), Vista varchar(50), Orden int, SubOrden int, Imagen Varchar(100) )

			DECLARE @idMenu AS Varchar(50) 
			DECLARE @idMenuPadre AS Varchar(50) 
			DECLARE @Nombre AS Varchar(50)
			DECLARE @Vista AS Varchar(100)
			DECLARE @Orden AS smallint
			DECLARE @Image AS Varchar(100)
			DECLARE @exite AS smallint 
			DECLARE @OrdenPadre AS smallint 

			--Construir Usuario - Menu
			DECLARE UsuarioMenu 
			CURSOR FOR 
				SELECT M.id, M.idMenu, M.Nombre, M.Vista, M.Orden, M.Imagen
				FROM SEG.Menu M
				INNER JOIN SEG.PermisoMenu PM ON M.Id = PM.idMenu
				INNER JOIN SEG.Permiso P ON PM.idPermiso = P.Id
				INNER JOIN SEG.RolPermiso RP ON P.Id = RP.idPermiso
				INNER JOIN SEG.Rol R ON RP.idRol = R.Id
				INNER JOIN SEG.GrupoRol GR ON R.Id = GR.idRol
				INNER JOIN SEG.Grupo G ON GR.idGrupo = G.Id
				INNER JOIN SEG.GrupoUsuario GS ON G.Id = GS.idGrupo
				INNER JOIN SEG.Usuario U ON GS.idUsuario = U.Id and U.Id = @Usuario

			OPEN UsuarioMenu
			FETCH NEXT FROM UsuarioMenu INTO @idMenu, @idMenuPadre, @Nombre, @Vista, @Orden, @Image
			WHILE @@fetch_status = 0
			BEGIN
				IF ( @idMenuPadre is null)
				BEGIN			
					SELECT @exite = Id from ##TablaTemporal where Id = @idMenu
					IF (@exite IS NULL)
					BEGIN
						INSERT INTO ##TablaTemporal VALUES (@idMenu, @Nombre, @Vista, @Orden, null, @Image)			
						SELECT @OrdenPadre = Orden FROM SEG.Menu WHERE Id = @idMenu
					END
					INSERT INTO ##TablaTemporal
					SELECT M.id, M.Nombre, M.Vista, @OrdenPadre, M.Orden, M.Imagen FROM SEG.Menu M WHERE M.idMenu = @idMenu
				END
				ELSE
				BEGIN
					SELECT @exite = Id from ##TablaTemporal where Id = @idMenuPadre
					IF (@exite IS NULL)
					BEGIN	
						INSERT INTO ##TablaTemporal
						SELECT Id, Nombre, Vista, Orden, null, Imagen FROM SEG.Menu WHERE Id = @idMenuPadre

						SELECT @OrdenPadre = Orden FROM SEG.Menu WHERE Id = @idMenuPadre
					END

					SET @exite = NULL
					SELECT @exite = Id from ##TablaTemporal where Id = @idMenu
					IF (@exite IS NULL)
						INSERT INTO ##TablaTemporal VALUES (@idMenu, @Nombre, @Vista, @OrdenPadre, @Orden, @Image)
				END	
				FETCH NEXT FROM UsuarioMenu INTO  @idMenu, @idMenuPadre, @Nombre, @Vista, @Orden, @Image
			END

			CLOSE UsuarioMenu
			DEALLOCATE UsuarioMenu

			SELECT * FROM ##TablaTemporal ORDER BY Orden, SubOrden
			DROP TABLE ##TablaTemporal
		END
END


GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'18.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pAgente'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pBodega'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pCliente'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'18.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pConductor'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pContrato'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pDocumento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pDocumentoTipo'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pGrupoElemento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pListaPrecio'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pListaPrecioDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pProveedor'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pProyecto'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pTipoMantenimiento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'PROCEDURE',@level1name=N'pUnidadMedida'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'18.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'Agente'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdBodega'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdCliente'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdContrato'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdDocumento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdDocumentoDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdDocumentoTipo'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdElemento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdGrupoElemento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdListaPrecio'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdListaPrecioDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdProveedor'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Catalogo [CIUDADES]' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdProyecto', @level2type=N'COLUMN',@level2name=N'idCiudad'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdProyecto'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'CATALOGO [CONDUCTORES]' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdRemision', @level2type=N'COLUMN',@level2name=N'idConductor'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'TABLE',@level1name=N'bdUnidadMedida'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[41] 4[20] 2[17] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "B"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 115
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 3
         End
         Begin Table = "bdProyecto (SAF)"
            Begin Extent = 
               Top = 0
               Left = 444
               Bottom = 109
               Right = 649
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "P"
            Begin Extent = 
               Top = 135
               Left = 451
               Bottom = 244
               Right = 602
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vBodega'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vBodega'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.2' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vBodega'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[46] 4[15] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "D"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 115
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TD"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 115
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BO"
            Begin Extent = 
               Top = 6
               Left = 427
               Bottom = 115
               Right = 578
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BD"
            Begin Extent = 
               Top = 6
               Left = 616
               Bottom = 115
               Right = 767
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumento'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "DD"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 115
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "D"
            Begin Extent = 
               Top = 6
               Left = 227
               Bottom = 115
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "E"
            Begin Extent = 
               Top = 6
               Left = 427
               Bottom = 115
               Right = 588
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BO"
            Begin Extent = 
               Top = 6
               Left = 626
               Bottom = 115
               Right = 777
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BD"
            Begin Extent = 
               Top = 6
               Left = 815
               Bottom = 115
               Right = 966
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumentoDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumentoDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vDocumentoDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vElemento'
GO
EXEC sys.sp_addextendedproperty @name=N'Version', @value=N'19.0.1' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vListaPrecioDetalle'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[23] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "R"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 115
               Right = 200
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "TD"
            Begin Extent = 
               Top = 6
               Left = 238
               Bottom = 115
               Right = 389
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BO"
            Begin Extent = 
               Top = 120
               Left = 38
               Bottom = 229
               Right = 189
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "BD"
            Begin Extent = 
               Top = 120
               Left = 227
               Bottom = 229
               Right = 378
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vRemision'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'SAF', @level1type=N'VIEW',@level1name=N'vRemision'
GO
