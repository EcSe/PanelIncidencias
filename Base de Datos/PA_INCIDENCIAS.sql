
IF OBJECT_ID('RMC_INCIDENCIA') IS NOT NULL
DROP PROC RMC_INCIDENCIA 
GO
CREATE PROC RMC_INCIDENCIA
@PCH_TIPO_TRANSACCION CHAR(1), --I=INSERT,S=SELECT,U=UPDATE,D=DELETE
@PVC_ID_INCIDENCIA VARCHAR(6) = NULL,
@PVC_TITULO VARCHAR(200)=NULL,
@PVC_ID_EMISOR VARCHAR(25) = NULL,
@PVC_ID_RECEPTOR VARCHAR(25) = NULL,
@PDT_FECHA DATETIME = NULL,
@PVC_DESCRIPCION VARCHAR(200) = NULL,
@PVC_ESTADO CHAR(1) = NULL, --P=PENDIENTE H=HECHO
@PVB_VALOR_BINARIO_1 VARBINARY(MAX) = NULL,
@PVB_VALOR_BINARIO_2 VARBINARY(MAX) = NULL,
@PVB_VALOR_BINARIO_3 VARBINARY(MAX) = NULL,
@PVB_VALOR_BINARIO_4 VARBINARY(MAX) = NULL,
@PVB_VALOR_BINARIO_5 VARBINARY(MAX) = NULL

AS
DECLARE @LVC_SQL NVARCHAR(MAX);
BEGIN
	BEGIN TRY
		IF @PCH_TIPO_TRANSACCION = 'I' --INSERT
		BEGIN 
		INSERT INTO INCIDENCIAS(VC_ID_INCIDENCIA,VC_TITULO,VC_ID_EMISOR,VC_ID_RECEPTOR,
			DT_FECHA,VC_DESCRIPCION,VC_ESTADO,VB_VALOR_BINARIO_1,VB_VALOR_BINARIO_2,VB_VALOR_BINARIO_3,
			VB_VALOR_BINARIO_4,VB_VALOR_BINARIO_5) 
		VALUES(@PVC_ID_INCIDENCIA,@PVC_TITULO,@PVC_ID_EMISOR,@PVC_ID_RECEPTOR,@PDT_FECHA,
			@PVC_DESCRIPCION,@PVC_ESTADO,@PVB_VALOR_BINARIO_1,@PVB_VALOR_BINARIO_2,@PVB_VALOR_BINARIO_3,
			@PVB_VALOR_BINARIO_4,@PVB_VALOR_BINARIO_5);
	END;
	ELSE IF @PCH_TIPO_TRANSACCION = 'U' --UPDATE
	BEGIN	
		UPDATE INCIDENCIAS
				SET VC_TITULO = @PVC_TITULO,
					VC_ID_EMISOR = @PVC_ID_EMISOR,
					VC_ID_RECEPTOR = @PVC_ID_RECEPTOR,
					DT_FECHA = @PDT_FECHA,
					VC_DESCRIPCION = @PVC_DESCRIPCION,
					VC_ESTADO = @PVC_ESTADO,
					VB_VALOR_BINARIO_1 = @PVB_VALOR_BINARIO_1,
					VB_VALOR_BINARIO_2 = @PVB_VALOR_BINARIO_2,
					VB_VALOR_BINARIO_3 = @PVB_VALOR_BINARIO_3,
					VB_VALOR_BINARIO_4 = @PVB_VALOR_BINARIO_4,
					VB_VALOR_BINARIO_5 = @PVB_VALOR_BINARIO_5
		WHERE VC_ID_INCIDENCIA = @PVC_ID_INCIDENCIA
	END;
	ELSE IF @PCH_TIPO_TRANSACCION = 'S' --SELECT
		BEGIN
		SET @LVC_SQL=' SELECT VC_ID_INCIDENCIA,VC_TITULO,VC_ID_EMISOR,VC_ID_EMISOR,VC_ID_RECEPTOR,
				DT_FECHA,VC_DESCRIPCION,VC_ESTADO,VB_VALOR_BINARIO_1,VB_VALOR_BINARIO_2,VB_VALOR_BINARIO_3,
				VB_VALOR_BINARIO_4,VB_VALOR_BINARIO_5 
				FROM INCIDENCIAS		
			WHERE 1=1 ' 
		IF @PVC_ID_INCIDENCIA IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + 'AND VC_ID_INCIDENCIA ='''+ @PVC_ID_INCIDENCIA+''' ';
		IF @PVC_ID_EMISOR IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' AND VC_ID_EMISOR = '''+ @PVC_ID_EMISOR+ ''' ';
		IF @PVC_ID_RECEPTOR IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' AND VC_ID_RECEPTOR = '''+ @PVC_ID_RECEPTOR+''' ';
		IF @PDT_FECHA IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' AND DT_FECHA = '''+ @PDT_FECHA + ''' ';
		IF @PVC_ESTADO IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' AND VC_ESTADO = '''+ @PVC_ESTADO + ''' ';
	EXEC (@LVC_SQL); 
	END;
	ELSE IF @PCH_TIPO_TRANSACCION = 'Z' --SELECT PERSONALIZADO
	BEGIN
	SET @LVC_SQL = 'SELECT I.VC_ID_INCIDENCIA,I.VC_ESTADO,U.VC_NOMBRE+'' ''+VC_APELLIDO_PATERNO+'' ''+VC_APELLIDO_MATERNO NOMBRE ,I.VC_TITULO, I.VC_DESCRIPCION,I.DT_FECHA
					FROM INCIDENCIAS I  '
		IF @PVC_ID_INCIDENCIA IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' INNER JOIN USUARIOS U ON I.VC_ID_EMISOR = U.VC_ID_USUARIO  WHERE I.VC_ID_INCIDENCIA = '''+@PVC_ID_INCIDENCIA+''' ';
		IF @PVC_ID_RECEPTOR IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' INNER JOIN USUARIOS U ON I.VC_ID_EMISOR = U.VC_ID_USUARIO WHERE VC_ID_RECEPTOR= '''+@PVC_ID_RECEPTOR+''' AND I.VC_ESTADO = ''P'' ';
		IF @PVC_ID_EMISOR IS NOT NULL
		SET @LVC_SQL = @LVC_SQL + ' INNER JOIN USUARIOS U ON I.VC_ID_RECEPTOR = U.VC_ID_USUARIO WHERE VC_ID_EMISOR= '''+@PVC_ID_EMISOR+''' ';
		SET @LVC_SQL = @LVC_SQL + ' ORDER BY I.DT_FECHA DESC' ;
		EXEC (@LVC_SQL)
	END
END TRY
BEGIN CATCH
	DECLARE @ErrorMessage NVARCHAR(4000);
		DECLARE @ErrorSeverity INT;
		DECLARE @ErrorState INT; 
		SELECT     @ErrorMessage = ERROR_MESSAGE(),
			@ErrorSeverity = ERROR_SEVERITY(),
			@ErrorState = ERROR_STATE(); 
		RAISERROR (@ErrorMessage, -- Message text.
				   @ErrorSeverity,-- Severity.
				   @ErrorState -- State.
				   ); 
END CATCH;
END;



