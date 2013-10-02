--- This Script must be run against CARA2012 DB ---

DECLARE @OnlineApp int

IF NOT Exists(SELECT 1 FROM NavMenu WHERE Text='Online App' AND Controller IS NULL) 
BEGIN
	INSERT INTO NavMenu([Text], [Controller], [Action], [Script], [IsEnabled], [ParentMenuItemid], [ViewOrder], [UserGroupAllowed1], [UserGroupAllowed2])
	VALUES ('Online App',NULL,NULL,NULL, 1,NULL,45,1,2) 

	SELECT @OnlineApp=@@IDENTITY

	INSERT INTO NavMenu([Text], [Controller], [Action], [Script], [IsEnabled], [ParentMenuItemid], [ViewOrder], [UserGroupAllowed1], [UserGroupAllowed2])
	VALUES ('Pending Users', 'OnlineApp', 'Pending Users', NULL, 1,@OnlineApp, 100, 1, 2) 

END
 