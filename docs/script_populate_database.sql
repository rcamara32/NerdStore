USE [NerdStoreDb]
GO

TRUNCATE TABLE Products
DELETE FROM Categories

-- CATEGORIES
INSERT INTO Categories VALUES (NEWID(), 'Games', 100)
INSERT INTO Categories VALUES (NEWID(), 'Toys', 110)
INSERT INTO Categories VALUES (NEWID(), 'T-Shirts', 120)
INSERT INTO Categories VALUES (NEWID(), 'Wallets', 130)
INSERT INTO Categories VALUES (NEWID(), 'Mugs & Glasses', 140)
INSERT INTO Categories VALUES (NEWID(), 'Bags', 150)


-- GAMES
declare @gamesCatId  uniqueidentifier = (select Id from Categories Where Code = 100)

INSERT INTO [dbo].[Products] VALUES
           (NEWID(), @gamesCatId, 'HORIZON CHASE TURBO (PS4)'
		   ,'Relive classic arcade gameplay as you master each racing circuit and blast your way past opponents at the speed of fun! Featuring a fresh, stylized take on old-school visuals, 4-player local multiplayer, over 100 tracks spread across dozens of locations, and a thumping soundtrack from industry legend Barry Leitch, Horizon Chase Turbo is ready to take you for a ride!'
           ,1 ,'34.99' ,GETDATE() ,'products/horizon-chase-turbo-ps4.jpg' ,50 ,1 ,1 ,1)

INSERT INTO [dbo].[Products] VALUES
           (NEWID(), @gamesCatId, 'STREET FIGHTER V (PS4)'
		   ,'The legendary fighting franchise returns with Street Fighter V! Powered by Unreal Engine 4 technology, stunning visuals depict the next generation of World Warriors in unprecedented detail, while exciting and accessible battle mechanics deliver endless fighting fun that both beginners and veterans can enjoy. Challenge friends online, or compete for fame and glory on the Capcom Pro Tour. Street Fighter V will be released exclusively for the PlayStation 4 and PC'
		   ,1 ,'13.99' ,GETDATE() ,'products/sfvhits1_ps4.jpg' ,50 ,1 ,1 ,1)


-- T-Shirts
declare @tShirtsCatId  uniqueidentifier = (select Id from Categories Where Code = 120)

INSERT INTO [dbo].[Products] VALUES
           (NEWID(), @tShirtsCatId, 'ATARI RED LOGO T-SHIRT'
		   ,'Official Atari product. Available in sizes: S - 2XL',
		   1 ,'19.99' ,GETDATE() ,'products/t-shirt-atari-black.jpg' ,50 ,1 ,1 ,1)

INSERT INTO [dbo].[Products] VALUES
	(NEWID(), @tShirtsCatId, 'SUPER MARIO JAPANESE MARIO WOMENS T-SHIRT'
	,'Girl gamers need cool T-shirts too! Thats why here at Geekstore we have gone and hooked you up with this unbelievably rad official Super Mario Japanese Mario Womens T-Shirt.',
	1 ,'14.99' ,GETDATE() ,'products/t-shirt-mario-bros-silver.jpg' ,50 ,1 ,1 ,1)


