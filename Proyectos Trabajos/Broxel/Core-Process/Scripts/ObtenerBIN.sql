select    o.[EXTERNAL_ID] as IssuerCode, pm.[ID_BIN] as Bin from [dbo].[IBC_ISSUER_BIN] as pm
inner join [dbo].[IBC_ISSUER] as iss on pm.[ID_ISSUER] = iss.[ID_ISSUER]
inner join C_organization  as o on o.[ID_ORGANIZATION] = iss.[ID_ORGANIZATION]
where o.[ORGANIZATION_TYPE] = 2  and  pm.[ID_BIN] like'506382%'