delete from [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] where FoodListId > 0;

DBCC CHECKIDENT ('[SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe]', RESEED, 0);
 
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(1, 1, N'Cắt xúc xích thành lát mỏng.', '/foodRecipes/food1_1.png'),
(1, 2, N'Đánh tan trứng với chút muối và tiêu để trứng có vị đậm đà.', '/foodRecipes/food1_2.png'),
(1, 3, N'Làm nóng chảo với chút dầu ăn, đổ trứng vào chảo.', '/foodRecipes/food1_3.png'),
(1, 4, N'Khi trứng vừa chín một phần, rải đều xúc xích lên trên, tiếp tục chiên đến khi mặt dưới vàng rồi lật chiên thêm vài giây là xong.', '/foodRecipes/food1_4.png');
 
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(2, 1, N'Ướp thịt bò với muối, tiêu và dầu hào trong 10 phút.', '/foodRecipes/food2_1.png'),
(2, 2, N'Chiên trứng đến độ chín mong muốn.', '/foodRecipes/food2_2.png'),
(2, 3, N'Xào thịt bò trên chảo với chút dầu ăn cho đến khi vừa chín tới.', '/foodRecipes/food2_3.png'),
(2, 4, N'Ăn kèm bánh mì với trứng và thịt bò nóng hổi.', '/foodRecipes/food2_4.png');
 
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(3, 1, N'Luộc gà chín tới, xé thành sợi.', '/foodRecipes/food3_1.png'),
(3, 2, N'Cắt cần tây thành khúc ngắn.', '/foodRecipes/food3_2.png'),
(3, 3, N'Trộn gà, cần tây với ít muối, tiêu và dầu ô liu.', '/foodRecipes/food3_3.png'),
(3, 4, N'Trang trí bằng ít rau mùi hoặc vừng rang.', '/foodRecipes/food3_4.png');
 
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(4, 1, N'Rửa sạch rau muống, để ráo.', '/foodRecipes/food4_1.png'),
(4, 2, N'Cắt đậu phụ thành miếng vuông nhỏ và chiên vàng các mặt.', '/foodRecipes/food4_2.png'),
(4, 3, N'Xào tỏi thơm, thêm rau muống và xào chín tới.', '/foodRecipes/food4_3.png'),
(4, 4, N'Thêm đậu phụ đã chiên, nêm nếm gia vị cho vừa ăn.', '/foodRecipes/food4_4.png');
 
-- Thịt bò xào cần tây
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(5, 1, N'Ướp thịt bò với chút muối, tiêu và dầu hào.', '/foodRecipes/food5_1.png'),
(5, 2, N'Xào tỏi thơm, thêm thịt bò vào xào nhanh với lửa lớn.', '/foodRecipes/food5_2.png'),
(5, 3, N'Thêm cần tây vào xào cùng thịt bò đến khi vừa chín tới.', '/foodRecipes/food5_3.png'),
(5, 4, N'Nêm nếm gia vị vừa ăn và tắt bếp.', '/foodRecipes/food5_4.png');
 
-- Đậu phụ luộc
INSERT INTO [SEP490_G87_VitaNutrientSystem].[FoodData].[Recipe] (
		[FoodListId]
      ,[NumericalOrder]
      ,[Describe]
      ,[URLImage]
) VALUES
(6, 1, N'Rửa sạch đậu phụ và cắt miếng vuông vừa ăn.', '/foodRecipes/food6_1.png'),
(6, 2, N'Đun nước sôi, cho đậu phụ vào luộc khoảng 3-5 phút.', '/foodRecipes/food6_2.png'),
(6, 3, N'Dùng kèm với nước mắm pha chanh tỏi ớt.', '/foodRecipes/food6_3.png');