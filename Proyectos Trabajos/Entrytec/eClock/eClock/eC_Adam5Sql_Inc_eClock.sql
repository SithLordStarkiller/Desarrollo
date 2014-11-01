﻿SELECT     EC_PERSONAS_DATOS.PERSONA_LINK_ID, EC_PERSONAS_DATOS.COMPANIA, EC_PERSONAS_DIARIO.PERSONA_DIARIO_FECHA, 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_TT, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TC, 
                      EC_PERSONAS_DIARIO.PERSONA_DIARIO_TDE, EC_PERSONAS_DIARIO.PERSONA_DIARIO_TES, 
                      EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_TXT, 
                      EC_V_PERSONAS_DIARIO_EX.TIPO_FALTA_EX_ID, EC_V_PERSONAS_DIARIO_EX.TIPO_INCIDENCIAS_EX_PARAM, 
                      EC_V_PERSONAS_DIARIO_EX.INCIDENCIA_COMENTARIO
FROM         EC_PERSONAS_DATOS INNER JOIN
                      EC_PERSONAS ON EC_PERSONAS_DATOS.PERSONA_ID = EC_PERSONAS.PERSONA_ID INNER JOIN
                      EC_PERSONAS_DIARIO ON EC_PERSONAS.PERSONA_ID = EC_PERSONAS_DIARIO.PERSONA_ID INNER JOIN
                      EC_V_PERSONAS_DIARIO_EX ON EC_PERSONAS_DIARIO.PERSONA_DIARIO_ID = EC_V_PERSONAS_DIARIO_EX.PERSONA_DIARIO_ID
