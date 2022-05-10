USE [lcapasdb_prod]
GO

SELECT S.[SettingsId]
       ,S.[Name]
	   ,SSV.[Value] "ShortStringValue"
	   ,BV.[Value] BooleanValue
	   ,DTV.[Value] "DateTimeValue"
	   ,IV.[Value] "IntegerValue"
       ,DV.[Value] "DoubleValue"
       ,S.[SettingCategory_CategoryId] "CategoryId"
	   ,SC.[CategoryName] "CategoryName"
  FROM [dbo].[Settings] S
  LEFT JOIN [dbo].SettingCategories SC on SC.CategoryId = S.SettingCategory_CategoryId
  LEFT JOIN [dbo].ShortStringValues SSV on SSV.Setting_SettingsId = S.SettingsId
  LEFT JOIN [dbo].BooleanValues BV on BV.Setting_SettingsId = S.SettingsId
  LEFT JOIN [dbo].DateTimeValues DTV on DTV.Setting_SettingsId = S.SettingsId
  LEFT JOIN [dbo].IntegerValues IV on IV.Setting_SettingsId = S.SettingsId
  LEFT JOIN [dbo].DoubleValues DV on DV.Setting_SettingsId = S.SettingsId
GO
