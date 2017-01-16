-- Descripcion: Actualiza e inserta CatEtiqueta
-- Autor: Maximiliano Silva
-- Fecha: 2016-06-01
UPDATE CatEtiqueta SET TX_DESCRIPCION_ETIQUETA =  'Vig Rea Sc Ofrecer Solucion' WHERE CV_ETIQUETA = 'C07'
UPDATE CatEtiqueta SET TX_DESCRIPCION_ETIQUETA =  'Ven Rea Cc (Ir por mto_recup)' WHERE CV_ETIQUETA = 'S04'
INSERT INTO CatEtiqueta VALUES ('C14','Vig Rea Sc Ofrecer FPP')
INSERT INTO CatEtiqueta VALUES ('E02','Solicitud STM')
