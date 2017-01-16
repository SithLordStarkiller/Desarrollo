
/****** Object:  Index [IX_AutorizacionSMS_002]    Script Date: 20/06/2016 11:05:13 a.m. ******/
CREATE NONCLUSTERED INDEX [IX_AutorizacionSMS_002] ON [dbo].[AutorizacionSMS]
(
	[idOrden] ASC
)
INCLUDE ( 	[num_Cred]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO


